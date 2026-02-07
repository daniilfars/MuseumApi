using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace MuseumApi.Models;

public class Painting
{
    public int Id { get; set; }
    [Required]public string Title { get; set; }
    public string Author { get; set; }
    public int Year { get; set; }
}
