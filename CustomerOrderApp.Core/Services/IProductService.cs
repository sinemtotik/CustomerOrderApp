using CustomerOrderApp.Core.DTOs;
using CustomerOrderApp.Core.Models;
using CustomerOrderApp.Core.ResponseModel;
using Microsoft.AspNetCore.Mvc;

namespace CustomerOrderApp.Core.Services
{
    public interface IProductService : IService<Product>
    {
        Task<Product> GetByBarkodAsync(string barkod);
        Task<ApiResponse> InsertProduct(Product model);
    }
}
