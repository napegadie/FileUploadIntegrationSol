using Azure.Messaging.EventGrid;
using Azure.Messaging.ServiceBus;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using Azure.Storage.Sas;
using FileUploadIntegrationPrj.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;

namespace FileUploadIntegrationPrj.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BlobServiceClient _blobServiceClient;
        private readonly ServiceBusClient _sbclient;
        private readonly BookSearchDbContext _context;
        private readonly EventGridPublisherClient _egclient;
        private static string _imagename;
        private static string _blobPath;
        private List<Book> _bookList;


        public HomeController(ILogger<HomeController> logger, BlobServiceClient blobServiceClient, BookSearchDbContext context, ServiceBusClient sbclient, EventGridPublisherClient egclient)
        {
            _logger = logger;
            _blobServiceClient = blobServiceClient;
            _sbclient = sbclient;
            _context = context;
            _egclient = egclient;
        }
        public async Task<IActionResult> Index()
        {

            if (_bookList != null) _bookList.Clear();
            _bookList = await _context.Books.ToListAsync();

            // Create the container and return a container client object
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient("book");

            foreach (var book in _bookList)
            {
                // Get a reference to a blob
                BlobClient blobClient = containerClient.GetBlobClient(book.BookLocationName);
                book.BookLocationPath = await GetUserDelegationSasBlob(blobClient);

            }

            if (_bookList == null) return NotFound();
            else
                return View(_bookList);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string searchString)
        {
            if (_bookList != null) _bookList.Clear();
            _bookList = await _context.Books.ToListAsync();

            if (!String.IsNullOrEmpty(searchString) && _bookList != null)
            {
                _bookList = _bookList.Where(s => s.BookTitle.Contains(searchString)).ToList();
            }

            // Create the container and return a container client object
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient("book");

            foreach (var book in _bookList)
            {
                // Get a reference to a blob
                BlobClient blobClient = containerClient.GetBlobClient(book.BookLocationName);
                book.BookLocationPath = await GetUserDelegationSasBlob(blobClient);

            }

            return View(_bookList);
        }


        private async Task<string> GetUserDelegationSasBlob(BlobClient blobClient)
        {
            BlobServiceClient blobServiceClient =
                blobClient.GetParentBlobContainerClient().GetParentBlobServiceClient();


            // Create a SAS token that's also valid for 10 min.
            BlobSasBuilder sasBuilder = new BlobSasBuilder()
            {
                BlobContainerName = blobClient.BlobContainerName,
                BlobName = blobClient.Name,
                Resource = "b",
                StartsOn = DateTimeOffset.UtcNow,
                Protocol = SasProtocol.Https,
                ExpiresOn = DateTimeOffset.UtcNow.AddMinutes(10)
            };

            // Specify read and write permissions for the SAS.
            sasBuilder.SetPermissions(BlobSasPermissions.Read |
                                      BlobSasPermissions.Write);


            //return blobUriBuilder.ToString();
            var sas = blobClient.GenerateSasUri(sasBuilder);


            return sas.ToString();
        }


        public IActionResult Add()
        {
            Book book = new Book();
            return View(book);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Book newBook)
        {
            try
            {
                if (newBook == null)
                {
                    throw new ArgumentNullException(nameof(newBook));
                }

                newBook.BookLocationName = _imagename;
                newBook.BookLocationPath = _blobPath;

                _context.Books.Add(newBook);
                bool resp = await _context.SaveChangesAsync() >= 1;

                if (resp)
                {

                    booksbqeue sbq = new booksbqeue();
                    sbq.BookImageName = _imagename;
                    sbq.BookBlobPath = _blobPath;
                    sbq.BookAction = "Add";

                    EventGridEvent egEvent = new EventGridEvent(
                         subject: $"New book cover",
                         eventType: "Books.Registration.New",
                         dataVersion: "1.0",
                         data: sbq
                      );

                    await _egclient.SendEventAsync(egEvent);

                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Error", "There is an saving records");
                    return View(newBook);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Caught exception : " + ex);
                //return "Error：" + e.Message;
                return View(newBook);
            }
        }


        [HttpPost()]
        public async Task<IActionResult> Upload(IFormFile files)
        {
            try
            {

                if (files != null)
                {

                    HttpClient httpClient = new HttpClient();

                    // Create the container and return a container client object
                    BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient("demo");
                    await containerClient.CreateIfNotExistsAsync();

                    // Get a reference to a blob
                    BlobClient blobClient = containerClient.GetBlobClient(files.FileName);
                    BlobHttpHeaders httpHeaders = new BlobHttpHeaders()
                    {
                        ContentType = files.ContentType,
                        CacheControl = "public"
                    };

                    // Upload data from the local file
                    var blobContentInfo = await blobClient.UploadAsync(files.OpenReadStream(), httpHeaders);

                    booksbqeue sbq = new booksbqeue();
                    sbq.BookImageName = files.FileName;
                    _imagename = files.FileName;
                    sbq.BookBlobPath = blobClient.Uri.AbsoluteUri;
                    _blobPath = blobClient.Uri.AbsoluteUri;

                    // create the sender
                    ServiceBusSender sender = _sbclient.CreateSender("csacontextqueue");

                    var objAsText = JsonConvert.SerializeObject(sbq);

                    // create a message that we can send. UTF-8 encoding is used when providing a string.
                    ServiceBusMessage message = new ServiceBusMessage(objAsText);

                    // send the message
                    await sender.SendMessageAsync(message);



                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return NoContent();
        }


        public async Task<IActionResult> Delete(int Id)
        {

            var _book = await _context.Books.FirstOrDefaultAsync(n => n.BookId == Id);

            if (_book == null)
            {
                throw new Exception($"The Book with id: {Id} does not exist");

            }

            booksbqeue sbq = new booksbqeue();
            sbq.BookImageName = _book.BookLocationName;
            sbq.BookBlobPath = _book.BookLocationPath;


            _context.Books.Remove(_book);
            bool resp = await _context.SaveChangesAsync() >= 1;


            if (resp)
            {

                var _bookToDelete = await _context.Books.Where(n => n.BookLocationName == _book.BookLocationName).CountAsync();

                if (_bookToDelete == 0)
                    sbq.BookAction = "Delete";
                else
                    sbq.BookAction = "NoDelete";

                EventGridEvent egEvent = new EventGridEvent(
                     subject: $"Delete book cover",
                     eventType: "Books.Registration.New",
                     dataVersion: "1.0",
                     data: sbq
                  );

                await _egclient.SendEventAsync(egEvent);



                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }


        }
    }
}