
namespace JokesApp.Models
{
    public class Joke
    {

        public int ID { get; set; }
        public required string Author { get; set; }
        public required string Text { get; set; }

        public DateTime UploadedOn { get; set; } = DateTime.Now;

        public required int UploaderID { get; set; }
    }
}
