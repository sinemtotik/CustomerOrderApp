using CustomerOrderApp.Core.Models;
using CustomerOrderApp.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CustomerOrderApp.Repository.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Product> GetByBarkodAsync(string barkod)
        {
            return await _context.Products.FirstOrDefaultAsync(x => x.pro_barcode == barkod);
        }
    }
}
