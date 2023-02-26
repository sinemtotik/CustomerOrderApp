using CustomerOrderApp.Core.DTOs;
using CustomerOrderApp.Core.ResponseModel;
using CustomerOrderApp.Core.Services;
using CustomerOrderApp.Repository.Migrations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerOrderApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerOrderController : ControllerBase
    {
        private readonly ICustomerOrderService _customerOrderService;

        public CustomerOrderController(ICustomerOrderService customerOrderService)
        {
            _customerOrderService = customerOrderService;
        }

        [HttpPost]
        [ActionName("CustomerOrderInsert")]
        [Route("CustomerOrderInsert")]
        public async Task<IActionResult> CustomerOrderInsert(List<CustomerOrderInsertDto> orders)
        {
            ApiResponse result = new ApiResponse();
            try
            {
                foreach (var order in orders)
                {
                    result = await _customerOrderService.CustomerOrderInsert(orders);
                }

                return StatusCode((int)result.StatusCode, result);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "İşlem başarısız.");
            }
        }

        [HttpPut("api/orders/{orderId}")]
        public IActionResult UpdateOrder(CustomerOrderAddressUpdateDto order)
        {
            result = await _customerOrderService.CustomerOrderInsert(order);
            return Ok();
        }
    }
}
