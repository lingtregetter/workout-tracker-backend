using Base.BLL.Contracts;
using Base.DAL.Contracts;

namespace Base.BLL;

public abstract class BaseBLL<TUnitOfWork> : IBaseBLL where TUnitOfWork : IBaseUnitOfWork
{
    protected readonly TUnitOfWork UnitOfWork;

    protected BaseBLL(TUnitOfWork unitOfWork)
    {
        UnitOfWork = unitOfWork;
    }

    public virtual async Task<int> SaveChangesAsync()
    {
        return await UnitOfWork.SaveChangesAsync();
    }
}