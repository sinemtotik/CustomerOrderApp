using CustomerOrderApp.Core.DTOs;
using CustomerOrderApp.Core.Models;
using CustomerOrderApp.Core.ResponseModel;
using Microsoft.AspNetCore.Mvc;

namespace CustomerOrderApp.Core.Services
{
    public interface ICustomerOrderService : IService<CustomerOrder>
    {
        Task<ApiResponse> CustomerOrderInsert(List<CustomerOrderInsertDto> orders);
    }
}
