using CustomerOrderApp.Core.DTOs;
using CustomerOrderApp.Core.Models;
using CustomerOrderApp.Core.ResponseModel;
using Microsoft.AspNetCore.Mvc;

namespace CustomerOrderApp.Core.Services
{
    public interface ICustomerService : IService<Customer>
    {
        Task<ApiResponse> InsertCustomer(Customer model);
    }
}
