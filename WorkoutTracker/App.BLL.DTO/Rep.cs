using Base.Domain;

namespace App.BLL.DTO;

public class Rep : DomainEntityId
{
    public int RepAmount { get; set; }
    
    public Guid WorkoutSetId { get; set; }
    public WorkoutSet? WorkoutSet { get; set; }
}