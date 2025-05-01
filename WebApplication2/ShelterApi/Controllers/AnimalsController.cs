using Microsoft.AspNetCore.Mvc;
using ShelterApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace ShelterApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnimalsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Animal>> GetAll() =>
            Database.Animals;

        [HttpGet("{id}")]
        public ActionResult<Animal> GetById(int id)
        {
            var a = Database.Animals.FirstOrDefault(x => x.Id == id);
            if (a == null) return NotFound();
            return a;
        }

        [HttpPost]
        public ActionResult<Animal> Create(Animal animal)
        {
            animal.Id = Database.Animals.Any() 
                ? Database.Animals.Max(x => x.Id) + 1 
                : 1;
            Database.Animals.Add(animal);
            return CreatedAtAction(nameof(GetById), new { id = animal.Id }, animal);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Animal updated)
        {
            var a = Database.Animals.FirstOrDefault(x => x.Id == id);
            if (a == null) return NotFound();
            a.Name     = updated.Name;
            a.Category = updated.Category;
            a.Weight   = updated.Weight;
            a.FurColor = updated.FurColor;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var a = Database.Animals.FirstOrDefault(x => x.Id == id);
            if (a == null) return NotFound();
            Database.Animals.Remove(a);
            return NoContent();
        }

        [HttpGet("{id}/visits")]
        public ActionResult<List<Visit>> GetVisits(int id)
        {
            if (!Database.Animals.Any(x => x.Id == id)) return NotFound();
            return Database.Visits.Where(v => v.AnimalId == id).ToList();
        }

        [HttpPost("{id}/visits")]
        public ActionResult<Visit> AddVisit(int id, Visit visit)
        {
            if (!Database.Animals.Any(x => x.Id == id)) return NotFound();
            visit.Id       = Database.Visits.Any() ? Database.Visits.Max(v => v.Id) + 1 : 1;
            visit.AnimalId = id;
            Database.Visits.Add(visit);
            return CreatedAtAction(nameof(GetVisits), new { id }, visit);
        }
        
        [HttpGet("search")]
        public ActionResult<List<Animal>> SearchByName([FromQuery] string name)
        {
            var results = Database.Animals
                .Where(a => a.Name
                    .Contains(name, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (!results.Any())
                return NotFound();

            return results;
        }

    }
}

