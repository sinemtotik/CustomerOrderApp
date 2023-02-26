using CustomerOrderApp.Core.Models;
using CustomerOrderApp.Core.Repositories;

namespace CustomerOrderApp.Repository.Repositories
{
    public class CustomerOrderRepository : GenericRepository<CustomerOrder>, ICustomerOrderRepository
    {
        public CustomerOrderRepository(AppDbContext context) : base(context)
        {
        }
    }
}
