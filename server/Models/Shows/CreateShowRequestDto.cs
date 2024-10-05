namespace WebApi.Models.Shows;

using System.ComponentModel.DataAnnotations;

public class CreateShowRequestDto
{
    public string ShowId { get; set; }
    
    [Required]
    public string Type { get; set; }
    
    [Required]
    public string Title { get; set; }
    
    public string Director { get; set; }
    
    public string Country { get; set; }
        
    [Required]
    public DateOnly DateAdded { get; set; }
    
    [Required]
    public int ReleaseYear { get; set; }
    
    [Required]
    public string Rating { get; set; }
    
    [Required]
    public string Duration { get; set; }
    public List<string> Categories { get; set; } = [];
}