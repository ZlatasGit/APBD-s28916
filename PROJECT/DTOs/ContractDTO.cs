namespace PROJECT.DTOs;

public class ContractDTO
{
    public int ClientId { get; set; }
    public string Description { get; set; }
    public double Amount { get; set; }
    public DateTime StartDate { get; set; }
    public int PaymentInterval { get; set; }
    public int SoftwareId { get; set; }
    public int VersionId { get; set; }
    public int UpdatesExtension { get; set; } = 1;
}