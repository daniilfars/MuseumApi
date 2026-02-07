using Microsoft.AspNetCore.Mvc;
using MuseumApi.Controllers;
using MuseumApi.Models;
using MuseumApi.Services;

namespace MuseumApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PaintingController : ControllerBase
{
    public PaintingController() { }

    [HttpGet]
    public ActionResult<List<Painting>> GetAll() => PaintingService.GetAll();

    [HttpGet("{id}")]
    public ActionResult<Painting> Get(int id)
    {
        Painting painting = PaintingService.Get(id);

        if (painting == null)
            return NotFound();

        return painting;
    }

    [HttpPost]
    public IActionResult Create(Painting painting)
    {
        PaintingService.Add(painting);
        return CreatedAtAction(nameof(Get), new { id = painting.Id }, painting);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Painting painting)
    {
        if (id != painting.Id)
            return BadRequest();

        var existingPainting = PaintingService.Get(id);

        if (existingPainting == null)
            return NotFound();

        PaintingService.Update(painting);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var painting = PaintingService.Get(id);

        if (painting == null)
            return NotFound();

        PaintingService.Delete(id);

        return NoContent();
    }
}