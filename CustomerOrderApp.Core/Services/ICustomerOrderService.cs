using CustomerOrderApp.Core.DTOs;
using CustomerOrderApp.Core.Models;
using CustomerOrderApp.Core.ResponseModel;
using Microsoft.AspNetCore.Mvc;

namespace CustomerOrderApp.Core.Services
{
    public interface ICustomerOrderService : IService<CustomerOrder>
    {
        Task<ApiResponse> CustomerOrderInsert(CustomerOrderInsertDto orders);
        Task<ApiResponse> CustomerOrderProductInsert(CustomerOrderProductInsertDto orders);
        Task<ApiResponse> RemoveCustomerOrder(int id);
        Task<ApiResponse> RemoveCustomerOrderByBarcode(int customer_id, string order_no, string barcode);
        Task<ApiResponse> UpdateCustomerOrderAddress(CustomerOrderAddressUpdateDto order);
        Task<ApiResponse> UpdateCustomerOrderQuantity(CustomerOrderQuantityUpdateDto order);
    }
}
