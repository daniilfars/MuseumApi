using MuseumApi.Models;

namespace MuseumApi.Services;

public interface IPaintingService
{
    List<Painting> GetAll();
    Painting? Get(int id);
    void Add(Painting painting);
    void Delete(int id);
    void Update(int id, Painting painting);
}
