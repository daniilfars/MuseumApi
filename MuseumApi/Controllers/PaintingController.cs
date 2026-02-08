using Microsoft.AspNetCore.Mvc;
using MuseumApi.Controllers;
using MuseumApi.Models;
using MuseumApi.Services;

namespace MuseumApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PaintingController : ControllerBase
{
    private readonly IPaintingService paintingService;

    public PaintingController(IPaintingService service) 
    {
        paintingService = service;
    }

    [HttpGet]
    public ActionResult<List<Painting>> GetAll() => paintingService.GetAll();

    [HttpGet("{id}")]
    public ActionResult<Painting> Get(int id)
    {
        Painting painting = paintingService.Get(id);

        if (painting == null)
            return NotFound();

        return painting;
    }

    [HttpPost]
    public IActionResult Create(Painting painting)
    {
        paintingService.Add(painting);
        return CreatedAtAction(nameof(Get), new { id = painting.Id }, painting);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Painting painting)
    {
        if (id != painting.Id)
            return BadRequest();

        var existingPainting = paintingService.Get(id);

        if (existingPainting == null)
            return NotFound();

        paintingService.Update(id, painting);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var painting = paintingService.Get(id);

        if (painting == null)
            return NotFound();

        paintingService.Delete(id);

        return NoContent();
    }
}