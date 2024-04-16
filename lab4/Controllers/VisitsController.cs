using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;

namespace lab4
{
    [ApiController]
    [Route("api/visits")]
    public class VisitsController : ControllerBase
    {
        // List of visits
        public static List<Visit> visits = 
        [
            new Visit()
            {
                AnimalId = AnimalsController.animals[0].Id,
                Price = "100",
                Date = new DateOnly(2021, 10, 10),
                AnimalDescription = "Flee treatment for Fluffy the cat"
            },
            new Visit()
            {
                AnimalId = AnimalsController.animals[1].Id,
                Price = "200",
                Date = new DateOnly(2021, 10, 11),
                AnimalDescription = "Vaccination for Daisy the dog"
            }
        ];

        [HttpGet("/visits")] //Retrieve all visits
        public IActionResult GetAll() => Ok(visits);
        
        [HttpGet("{id}")] //Retrieve visists with specific animal by the Id
        public IActionResult Get(Guid id)
        {
            return Ok(visits.Find(v => v.AnimalId == id));
        }
        
        [HttpPost("{id}")]//Add a new visit
        public IActionResult Post(Visit visit)
        {
            visits.Add(visit);
            return CreatedAtAction(nameof(Get), new { id = visit.AnimalId }, visit);
        }
    }
}