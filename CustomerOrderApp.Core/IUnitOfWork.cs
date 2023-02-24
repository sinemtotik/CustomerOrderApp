namespace CustomerOrderApp.Core
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
        void Commit();
    }
}
