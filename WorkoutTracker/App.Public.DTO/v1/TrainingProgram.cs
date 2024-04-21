using Swashbuckle.AspNetCore.Annotations;

namespace App.Public.DTO.v1;

public class TrainingProgram
{
    [SwaggerSchema(ReadOnly = true)]
    public Guid Id { get; set; }
    public string ProgramName { get; set; } = default!;
    public string? ProgramDescription { get; set; }
    public List<TrainingProgramBlock>? TrainingBlocks { get; set; } 
}