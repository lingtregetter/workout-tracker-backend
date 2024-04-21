namespace App.Public.DTO.v1;

public class UserProgram
{
    public Guid Id { get; set; }
    public string? TrainingProgramName { get; set; }

    public string? TrainingProgramDescription { get; set; }
    
    public Guid? TrainingProgramId { get; set; }
}