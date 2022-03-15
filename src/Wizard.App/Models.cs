namespace Wizard.App.Models;

public record Province(int Id, string Name);
public record Country(int Id, string Name);
public record EmailInfo(string Email);

public record ProvincesResponse(IEnumerable<Province> Provinces);
public record CountriesResponse(IEnumerable<Country> Countries);
public record AddRegistrationResponse(string? Email, string? Error);
public record AddRegistrationRequest(string Email, string Password, int CountryId, int ProvinceId);
