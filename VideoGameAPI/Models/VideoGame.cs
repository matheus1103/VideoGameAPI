namespace VideoGameAPI.Models
{
    public class VideoGame
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Genre { get; set; }
        public string? Platform { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string? Publisher { get; set; }

    }
}
