namespace Base.DAL.Contracts;

public interface IBaseUnitOfWork
{
    Task<int> SaveChangesAsync();
    // ?? how to contain and create repositories
}