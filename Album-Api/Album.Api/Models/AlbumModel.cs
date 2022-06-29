namespace AlbumApi.Models
{
    public class AlbumModel
    {
        public AlbumModel() { }

        public AlbumModel(int id, string name, string artist, string imageUrl)
        {
            Id = id;
            Name = name;
            Artist = artist;
            ImageUrl = imageUrl;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Artist { get; set; }
        public string ImageUrl { get; set; }
    }
}