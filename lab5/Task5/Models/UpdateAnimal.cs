using System.ComponentModel.DataAnnotations;
namespace Task5.Models
{
    public class UpdateAnimal
    {
        [MinLength(3)]
        [MaxLength(200)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string? Description { get; set; }
        [MaxLength(200)]
        public string? Category { get; set; }
        [MaxLength(200)]
        public string? Area { get; set; }
    }
}
