namespace Wizard.Domain.Entities;

public class Province
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public Country Country { get; set; }
}