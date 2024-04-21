namespace App.Public.DTO.v1;

public class TrainingBlockWithProgram
{
    public Guid Id { get; set; }
    public Guid TrainingProgramId { get; set; }
    public List<string> Blocks { get; set; } = default!;
}