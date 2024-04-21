namespace App.Public.DTO.v1;

public class PersonalInformation
{
    public Guid Id { get; set; }
    public string Gender { get; set; } = default!;
    public decimal Height { get; set; }
    public decimal Weight { get; set; }
}