using WildForest.Infrastructure.Persistence.UoW;
namespace WildForest.UnitTests.Common;

public abstract class DbUnitTest : IDisposable
{
    protected readonly UnitOfWork UnitOfWork;
    
    public DbUnitTest()
    {
        UnitOfWork = UnitOfWorkFactoryInMemory.Create();
    }

    public virtual void Dispose()
    {
        UnitOfWorkFactoryInMemory.Destroy(UnitOfWork);
    }
}