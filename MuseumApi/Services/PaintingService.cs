using MuseumApi.Models;

namespace MuseumApi.Services;

public static class PaintingService
{
    static List<Painting> Paintings { get; }
    static int nextId = 3;
    static PaintingService()
    {
        Paintings = new List<Painting>
        {
            new Painting {Id = 1, Title = "Mono", Author = "Da Vinchi", Year = 1872},
            new Painting {Id = 2, Title = "Football", Author = "Lionel Messi", Year = 2014},
        }; 
    }
    public static List<Painting> GetAll() => Paintings;
    public static Painting? Get(int id) => Paintings.FirstOrDefault(p => p.Id == id);
    
    public static void Add(Painting painting)
    {
        painting.Id = nextId++;
        Paintings.Add(painting);
    }
    public static void Delete(int id)
    {
        Painting painting = Get(id);

        if(painting != null)
            Paintings.Remove(painting);
    }
    public static void Update(Painting painting)
    {
        int index = Paintings.FindIndex(p => p.Id == painting.Id);

        if (index == -1)
            return;

        Paintings[index] = painting;
    }
}
