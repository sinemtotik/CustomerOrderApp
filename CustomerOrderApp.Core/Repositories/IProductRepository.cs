using CustomerOrderApp.Core.Models;

namespace CustomerOrderApp.Core.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<Product> GetByBarkodAsync(string barkod);
    }
}
