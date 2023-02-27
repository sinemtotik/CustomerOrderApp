using CustomerOrderApp.Core.DTOs;
using CustomerOrderApp.Core.ResponseModel;
using CustomerOrderApp.Core.Services;
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
        public async Task<IActionResult> CustomerOrderInsert(CustomerOrderInsertDto orders)
        {
            ApiResponse result = new ApiResponse();
            try
            {
                result = await _customerOrderService.CustomerOrderInsert(orders);

                return StatusCode((int)result.StatusCode, result);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "İşlem başarısız.");
            }
        }

        [HttpPost]
        [ActionName("CustomerOrderProductInsert")]
        [Route("CustomerOrderProductInsert")]
        public async Task<IActionResult> CustomerOrderProductInsert(CustomerOrderProductInsertDto orders)
        {
            ApiResponse result = new ApiResponse();
            try
            {
                result = await _customerOrderService.CustomerOrderProductInsert(orders);

                return StatusCode((int)result.StatusCode, result);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "İşlem başarısız.");
            }
        }

        [HttpPut]
        [ActionName("UpdateCustomerOrderAddress")]
        [Route("update-customer-order-address")]
        public async Task<IActionResult> UpdateCustomerOrderAddress(CustomerOrderAddressUpdateDto order)
        {
            ApiResponse result = new ApiResponse();

            try
            {
                result = await _customerOrderService.UpdateCustomerOrderAddress(order);

                return StatusCode((int)result.StatusCode, result);
            }
            catch (Exception)
            {
                return StatusCode(500, "İşlem başarısız.");
            }
        }

        [HttpPut]
        [ActionName("UpdateCustomerOrderQuantity")]
        [Route("update-customer-order-quantity")]
        public async Task<IActionResult> UpdateCustomerOrderQuantity(CustomerOrderQuantityUpdateDto order)
        {
            ApiResponse result = new ApiResponse();

            try
            {
                result = await _customerOrderService.UpdateCustomerOrderQuantity(order);

                return StatusCode((int)result.StatusCode, result);
            }
            catch (Exception)
            {
                return StatusCode(500, "İşlem başarısız.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            try
            {
                var result = await _customerOrderService.RemoveCustomerOrder(id);

                return StatusCode((int)result.StatusCode, result);
            }
            catch (Exception)
            {
                return StatusCode(500, "İşlem başarısız.");
            }
        }

        [HttpDelete("{customer_id}/{order_no}/{barcode}")]
        public async Task<IActionResult> DeleteOrder(int customer_id, string order_no, string barcode)
        {
            try
            {
                var result = await _customerOrderService.RemoveCustomerOrderByBarcode(customer_id, order_no, barcode);

                return StatusCode((int)result.StatusCode, result);
            }
            catch (Exception)
            {
                return StatusCode(500, "İşlem başarısız.");
            }
        }
    }
}
