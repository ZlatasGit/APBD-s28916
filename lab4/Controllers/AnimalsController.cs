using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;

namespace lab4
{
    [ApiController]
    [Route("[controller]")]
    class AnimalsController : ControllerBase
    {
        public static List<Animal> animals = 
        [
            new Animal()
            {
                Category = "cat", Name = "fluffy", Weight = 3.2, FurColor = "white"
            },
            new Animal()
            {
                Category = "dog", Name = "daisy", Weight = 7.4, FurColor = "black"
            }
        ];

        [HttpGet("/animals")] // retrieve all animals
        public IActionResult GetAll() => Ok(animals);

        [HttpGet("{id}")] // retrieve animal by id
        public IActionResult Get(Guid id)
        {
            return Ok(animals.Find(a => a.Id == id));
        }

        [HttpPost("{id}")]
        public IActionResult Post(Animal animal)
        {
            animals.Add(animal);
            return CreatedAtAction(nameof(Get), new { id = animal.Id}, animal);
        }

        [HttpPut("{id}")] // update animal by id
        public IActionResult Put(Guid id, Animal updatedAnimal)
        {
            var index = animals.FindIndex(a => a.Id == id);
            if (index == -1) return NotFound();
            animals[index] = updatedAnimal;
            return NoContent();
        }

        [HttpDelete("{id}")] // delete animal
        public IActionResult Delete(Guid id)
        {
            var index = animals.FindIndex(a => a.Id == id);
            if (index == -1) return NotFound();
            animals.RemoveAt(index);
            return NoContent();
        }
    }
}