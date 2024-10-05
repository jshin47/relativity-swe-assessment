namespace WebApi.Entities;

public class ShowDto
{
    public int Id { get; set; }
    public string ShowId { get; set; }
    public string Type { get; set; }
    public string Title { get; set; }
    public string Director { get; set; }
    public string Country { get; set; }
    public DateOnly DateAdded { get; set; }
    public int ReleaseYear { get; set; }
    public string Rating { get; set; }
    public string Duration { get; set; }
    public List<string> Categories { get; set; }
}
