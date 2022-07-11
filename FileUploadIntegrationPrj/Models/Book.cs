using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace FileUploadIntegrationPrj.Models
{
    public class Book
    {
        [Key]
        [Required]
        public int BookId { get; set; }
        public string BookTitle { get; set; }

        public string BookLocationName { get; set; }
        public string BookLocationPath { get; set; }
        public string BookAuthor { get; set; }
        public string BookGenre { get; set; }
        public DateTime? DatePublished { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

    }
}
