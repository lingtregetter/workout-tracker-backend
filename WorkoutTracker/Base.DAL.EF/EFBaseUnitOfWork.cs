using Base.DAL.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Base.DAL.EF;

public class EFBaseUnitOfWork<TDbContext> : IBaseUnitOfWork where TDbContext : DbContext
{
    protected readonly TDbContext UnitOfWorkDbContext;
    
    public EFBaseUnitOfWork(TDbContext dataContext)
    {
        UnitOfWorkDbContext = dataContext;
    }
    
    public virtual async Task<int> SaveChangesAsync()
    {
        return await UnitOfWorkDbContext.SaveChangesAsync();
    }
}