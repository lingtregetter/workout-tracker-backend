using Base.Domain;

namespace App.BLL.DTO;

public class Weight : DomainEntityId
{
    public decimal UsedWeight { get; set; }
    
    public Guid WorkoutSetId { get; set; }
    public WorkoutSet? WorkoutSet { get; set; }
}