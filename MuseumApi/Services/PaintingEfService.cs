using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using MuseumApi.Models;
using MuseumApi.Data;

namespace MuseumApi.Services;

public class PaintingEfService : IPaintingService
{
    readonly AppDbContext db;
    readonly IMemoryCache MemoryCache;
    readonly int CacheDurationMinutes = 5;

    public PaintingEfService(AppDbContext context, IMemoryCache cache)
    {
        db = context;
        MemoryCache = cache;
    }
    public List<Painting> GetAll()
    {
        const string cacheKey = "AllPaintings";

        if (MemoryCache.TryGetValue(cacheKey, out List<Painting> paintings))
        {
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Данные получены из кэша");
            return paintings;
        }

        paintings = db.Paintings.ToList();

        Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Данные получены из базы");
        MemoryCache.Set(cacheKey, paintings, TimeSpan.FromMinutes(CacheDurationMinutes));

        return paintings;
    }
    public Painting? Get(int id)
    {
        string cacheKey = $"Painting_{id}";

        if (MemoryCache.TryGetValue(cacheKey, out Painting painting))
        {
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Данные получены из кэша");
            return painting;
        }

        painting = db.Paintings.Find(id);

        if (painting != null)
        {
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Данные получены из базы");
            MemoryCache.Set(cacheKey, painting, TimeSpan.FromMinutes(CacheDurationMinutes));
        }

        return painting;
    }

    public void Add(Painting painting)
    {
        db.Paintings.Add(painting);
        db.SaveChanges();

        MemoryCache.Remove("AllPaintings");
    }
    public void Delete(int id)
    {
        Painting painting = Get(id);

        if (painting != null)
        {
            db.Paintings.Remove(painting);
            db.SaveChanges();

            MemoryCache.Remove("AllPaintings");
            MemoryCache.Remove($"Painting_{id}");
        }
    }
    public void Update(int id, Painting painting)
    {
        var res = db.Paintings.Find(id);

        if (res == null)
            return;

        res.Title = painting.Title;
        res.Author = painting.Author;
        res.Year = painting.Year;

        db.SaveChanges();

        MemoryCache.Remove("AllPaintings");
        MemoryCache.Remove($"Painting_{painting.Id}");
    }
}