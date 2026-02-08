using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace MuseumApi.Models;

public class Painting
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required(ErrorMessage = "Название обязательно")]
    public string Title { get; set; }
    public string Author { get; set; }
    [Range(1200, 2026, ErrorMessage = "Год должен быть между 1200 и 2026")]
    public int Year { get; set; }
}
