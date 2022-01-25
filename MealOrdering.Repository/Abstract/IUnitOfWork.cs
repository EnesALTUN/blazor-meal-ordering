namespace MealOrdering.Repository.Abstract
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IOrderRepository Order { get; }

        ISubOrderRepository SubOrder { get; }

        ISupplierRepository Supplier { get; }

        IUserRepository User { get; }

        Task<int> SaveAsync();
    }
}
