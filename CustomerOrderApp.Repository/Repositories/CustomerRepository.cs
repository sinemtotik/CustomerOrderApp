using CustomerOrderApp.Core.Models;
using CustomerOrderApp.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CustomerOrderApp.Repository.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(AppDbContext context) : base(context)
        {
        }
    }
}
