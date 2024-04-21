using Base.Domain;

namespace App.BLL.DTO;

public class Exercise : DomainEntityId
{
    public string ExerciseName { get; set; } = default!;
    public string? ExerciseDescription { get; set; }
}