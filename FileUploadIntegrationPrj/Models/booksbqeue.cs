using Newtonsoft.Json;

namespace FileUploadIntegrationPrj.Models
{
    public class booksbqeue
    {
        public string BookImageName { get; set; }

        public string BookBlobPath { get; set; }
        public string BookAction { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

    }
}
