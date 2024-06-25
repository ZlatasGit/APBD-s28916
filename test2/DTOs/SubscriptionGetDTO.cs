namespace DTOs;

public class SubscriptionGetDTO
{
    public int IdOne { get; set; }
    public string Name { get; set; }
    public ICollection<ManyGetDTO> Manies { get; set; }
}