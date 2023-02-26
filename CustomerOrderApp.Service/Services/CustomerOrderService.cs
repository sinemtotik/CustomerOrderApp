using AutoMapper;
using CustomerOrderApp.Core;
using CustomerOrderApp.Core.DTOs;
using CustomerOrderApp.Core.Models;
using CustomerOrderApp.Core.Repositories;
using CustomerOrderApp.Core.ResponseModel;
using CustomerOrderApp.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CustomerOrderApp.Service.Services
{
    public class CustomerOrderService : Service<CustomerOrder>, ICustomerOrderService
    {
        private readonly IMapper _mapper;
        private readonly ICustomerService _customerService;
        private readonly IProductService _productService;

        public CustomerOrderService(IGenericRepository<CustomerOrder> repository, IUnitOfWork unitOfWork,
            IMapper mapper, IProductService productService, ICustomerService customerService)
            : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _productService = productService;
            _customerService = customerService;
        }

        public async Task<ApiResponse> CustomerOrderInsert(List<CustomerOrderInsertDto> orders)
        {
            try
            {
                CustomerOrderInsertDto orderResponse = new CustomerOrderInsertDto();
                CustomerOrder entity = new CustomerOrder();

                foreach (var model in orders)
                {
                    entity.CreatedDate = DateTime.Now;
                    entity.UpdatedDate = DateTime.Now;
                    entity.CustomerId = model.cust_ord_id;
                    entity.cust_ord_address = model.cust_ord_address;
                    entity.cust_ord_barcode = model.cust_ord_barcode;
                    entity.cust_ord_description = model.cust_ord_description;
                    entity.cust_ord_quantity = model.cust_ord_quantity;
                    entity.cust_ord_price = model.cust_ord_price;

                    // Eğer ürün barkodu yoksa burada gelen barkoda göre insert işlemi yapılır
                    if (model.cust_ord_id != 0)
                    {
                        var customer = await _customerService.GetByIdAsync(model.cust_ord_id);

                        if (customer is null)
                        {
                            customer = new Customer();

                            customer.cust_name = model.cust_ord_name;
                            customer.cust_address = model.cust_ord_address;

                            await _customerService.InsertCustomer(customer);
                        }
                    }

                    // Eğer ürün barkodu yoksa burada gelen barkoda göre insert işlemi yapılır
                    if (model.cust_ord_barcode is not null)
                    {
                        var product = await _productService.GetByBarkodAsync(model.cust_ord_barcode);

                        if (product is null)
                        {
                            product = new Product();

                            product.pro_barcode = model.cust_ord_barcode;
                            product.pro_description = model.cust_ord_description;

                            await _productService.InsertProduct(product);
                        }
                    }

                    var resultInsert = await this.AddAsync(entity);
                    orderResponse = _mapper.Map<CustomerOrderInsertDto>(resultInsert);


                    if (orderResponse is null)
                    {
                        return ApiResponse.CreateResponse(HttpStatusCode.NoContent, ApiResponse.ErrorMessage);
                    }
                }

                return ApiResponse.CreateResponse(HttpStatusCode.OK, ApiResponse.SuccessMessage, orderResponse);

            }
            catch (Exception ex)
            {
                return ApiResponse.CreateResponse(HttpStatusCode.InternalServerError, ApiResponse.ErrorMessage);
            }
        }


        //[HttpPut("{customerId}/address")]
        //public async Task<IActionResult> UpdateCustomerOrderAddress(CustomerOrderAddressUpdateDto order)
        //{
        //    try
        //    {
        //        if (order.cust_ord_order_id != 0)
        //        {
        //            var customer_order = await this.GetByIdAsync(order.cust_ord_order_id);

        //            if (customer_order is not null)
        //            {
        //                customer_order = new CustomerOrder();

        //                customer_order.cust_ord_address = order.cust_ord_address;

        //                await this.UpdateAsync(customer_order);
        //            }
        //        }
        //        else
        //        {
        //            // girilen sipariş id si bulunamadı
        //        }
        //        //return ApiResponse.CreateResponse(HttpStatusCode.NoContent, ApiResponse.ErrorMessage);
        //    }
        //    catch (ArgumentException ex)
        //    {
        //        //return ApiResponse.CreateResponse(HttpStatusCode.InternalServerError, ApiResponse.ErrorMessage);
        //    }
        //}
    }
}
