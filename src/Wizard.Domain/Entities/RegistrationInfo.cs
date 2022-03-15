namespace Wizard.Domain.Entities;

public class RegistrationInfo
{
    public string Email { get; set; }
    public string Password { get; set; }
    public int CountryId { get; set; }
    public Country Country { get; set; }
    public int ProvinceId { get; set; }
    public Province Province { get; set; }
}