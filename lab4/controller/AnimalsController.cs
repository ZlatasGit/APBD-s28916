using Microsoft.AspNetCore.Mvc;

namespace lab4
{
    [ApiController]
    [Route("[controller]")]
    class AnimalsController : ControllerBase
    {
        private static List<Animal> animals = [];

        [HttpGet] // retrieve all animals
        public IActionResult Get() => Ok(animals);

        [HttpGet("{id}")] // retrieve animal by id
        public IActionResult Get(Guid id)
        {
            var animal = animals.FirstOrDefault(a => a.Id == id);
            if (animal == null) return NotFound();
            return Ok(animal);
        }

        [HttpPost] // add animal
        public IActionResult Post(Animal animal)
        {
            animals.Add(animal);
            return CreatedAtAction(nameof(Get), new { id = animal.id}, animal);
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