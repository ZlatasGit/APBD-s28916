namespace PROJECT.Models;
using System.ComponentModel.DataAnnotations;

public class Discount(string name, double value, DateOnly startDate, DateOnly endDate)
{
    [Key]
    public int IdDiscount { get; set; }
    [Required]
    public string Name { get; set; } = name;
    [Required]
    [Range(1, 100, ErrorMessage = "Discount must be between 1 and 100")]
    public double Value { get; set; } = value;
    [Required]
    public DateOnly StartDate { get; set; } = startDate;
    [Required]
    public DateOnly EndDate { get; set; } = endDate;
}