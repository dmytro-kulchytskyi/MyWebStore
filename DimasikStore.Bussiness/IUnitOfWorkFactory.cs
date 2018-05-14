namespace DimasilStore.Business.Managers
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create();
    }
}