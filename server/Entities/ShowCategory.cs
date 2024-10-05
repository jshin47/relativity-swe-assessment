using Microsoft.EntityFrameworkCore;

namespace WebApi.Entities;

[Index(nameof(Name), IsUnique = true)]
public class ShowCategory
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Show> Shows { get; } = [];
}
