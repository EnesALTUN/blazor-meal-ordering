using MealOrdering.Repository.Abstract;
using MealOrdering.Repository.Concrete.EntityFramework;
using MealOrdering.Server.Data.EntityFramework.Context;

namespace MealOrdering.Repository.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MealOrderingDbContext _context;
        private readonly IOrderRepository _orderRepository;
        private readonly ISubOrderRepository _subOrderRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly IUserRepository _userRepository;

        public UnitOfWork(MealOrderingDbContext context)
        {
            _context = context;
        }

        public IOrderRepository Order => _orderRepository ?? new EfOrderRepository(_context);

        public ISubOrderRepository SubOrder => _subOrderRepository ?? new EfSubOrderRepository(_context);

        public ISupplierRepository Supplier => _supplierRepository ?? new EfSupplierRepository(_context);

        public IUserRepository User => _userRepository ?? new EfUserRepository(_context);

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }
    }
}
