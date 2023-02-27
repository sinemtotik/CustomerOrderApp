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

        public async Task<ApiResponse> CustomerOrderInsert(CustomerOrderInsertDto orders)
        {
            try
            {
                CustomerOrderInsertDto orderResponse = new CustomerOrderInsertDto();
                CustomerOrder entity = new CustomerOrder();


                // Eğer ürün id yoksa burada gelen idye göre insert işlemi yapılır
                if (orders.cust_ord_cust_id != 0)
                {
                    var customer = await _customerService.GetByIdAsync(orders.cust_ord_cust_id);

                    if (customer is null)
                    {
                        customer = new Customer();

                        customer.cust_name = orders.cust_ord_name;
                        customer.cust_address = orders.cust_ord_address;

                        await _customerService.InsertCustomer(customer);
                    }
                }

                if (orders.CustomProductDto.Count() > 0)
                {
                    foreach (var model in orders.CustomProductDto)
                    {
                        entity = new CustomerOrder();

                        entity.CreatedDate = DateTime.Now;
                        entity.UpdatedDate = DateTime.Now;
                        entity.CustomerId = orders.cust_ord_cust_id;
                        entity.cust_order_no = orders.cust_ord_no;
                        entity.cust_ord_address = orders.cust_ord_address;
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

                        entity.cust_ord_barcode = model.cust_ord_barcode;
                        entity.cust_ord_description = model.cust_ord_description;
                        entity.cust_ord_quantity = model.cust_ord_quantity;
                        entity.cust_ord_price = model.cust_ord_price;

                        var resultInsert = await this.AddAsync(entity);
                        orderResponse = _mapper.Map<CustomerOrderInsertDto>(resultInsert);


                        if (orderResponse is null)
                        {
                            return ApiResponse.CreateResponse(HttpStatusCode.NoContent, ApiResponse.ErrorMessage);
                        }
                    }

                }

                return ApiResponse.CreateResponse(HttpStatusCode.OK, ApiResponse.SuccessMessage, orderResponse);

            }
            catch (Exception ex)
            {
                return ApiResponse.CreateResponse(HttpStatusCode.InternalServerError, ApiResponse.ErrorMessage);
            }
        }

        public async Task<ApiResponse> CustomerOrderProductInsert(CustomerOrderProductInsertDto orders)
        {
            try
            {
                CustomerOrderInsertDto orderResponse = new CustomerOrderInsertDto();
                CustomerOrder entity = new CustomerOrder();


                // Eğer ürün id yoksa burada gelen idye göre insert işlemi yapılır
                if (orders.cust_ord_cust_id != 0)
                {
                    var customer = await _customerService.GetByIdAsync(orders.cust_ord_cust_id);

                    if (customer is null)
                    {
                        return ApiResponse.CreateResponse(HttpStatusCode.BadRequest, "Müşteri Bulunamadı");
                    }
                }

                entity = new CustomerOrder();

                entity.CreatedDate = DateTime.Now;
                entity.UpdatedDate = DateTime.Now;
                entity.CustomerId = orders.cust_ord_cust_id;
                entity.cust_order_no = orders.cust_ord_no;
                entity.cust_ord_address = orders.cust_ord_address;

                // Eğer ürün barkodu yoksa burada gelen barkoda göre insert işlemi yapılır
                if (orders.cust_ord_barcode is not null)
                {
                    var product = await _productService.GetByBarkodAsync(orders.cust_ord_barcode);

                    if (product is null)
                    {
                        product = new Product();

                        product.pro_barcode = orders.cust_ord_barcode;
                        product.pro_description = orders.cust_ord_description;

                        await _productService.InsertProduct(product);
                    }
                }

                entity.cust_ord_barcode = orders.cust_ord_barcode;
                entity.cust_ord_description = orders.cust_ord_description;
                entity.cust_ord_quantity = orders.cust_ord_quantity;
                entity.cust_ord_price = orders.cust_ord_price;

                var resultInsert = await this.AddAsync(entity);
                orderResponse = _mapper.Map<CustomerOrderInsertDto>(resultInsert);


                if (orderResponse is null)
                {
                    return ApiResponse.CreateResponse(HttpStatusCode.NoContent, ApiResponse.ErrorMessage);
                }

                return ApiResponse.CreateResponse(HttpStatusCode.OK, ApiResponse.SuccessMessage, orderResponse);

            }
            catch (Exception ex)
            {
                return ApiResponse.CreateResponse(HttpStatusCode.InternalServerError, ApiResponse.ErrorMessage);
            }
        }

        public async Task<ApiResponse> RemoveCustomerOrder(int id)
        {
            try
            {
                if (id > 0)
                {
                    var result = await this.GetByIdAsync(id);

                    if (result is not null)
                    {
                        var resultRemove = this.RemoveAsync(result);
                    }
                    else
                    {
                        return ApiResponse.CreateResponse(HttpStatusCode.NoContent, "Girilen id'ye ait sipariş bulunamadı");
                    }
                }
                else
                {
                    return ApiResponse.CreateResponse(HttpStatusCode.NoContent, "Lütfen sipariş id giriniz");
                }

                return ApiResponse.CreateResponse(HttpStatusCode.OK, ApiResponse.SuccessMessage);
            }
            catch (ArgumentException ex)
            {
                return ApiResponse.CreateResponse(HttpStatusCode.InternalServerError, ApiResponse.ErrorMessage);
            }
        }

        public async Task<ApiResponse> RemoveCustomerOrderByBarcode(int customer_id, string order_no, string barcode)
        {
            try
            {
                if (customer_id > 0 && !string.IsNullOrWhiteSpace(order_no) && !string.IsNullOrWhiteSpace(barcode))
                {
                    var result = this.GetAllAsync().Result.Where(x => x.CustomerId == customer_id && x.cust_order_no == order_no && x.cust_ord_barcode == barcode).FirstOrDefault();

                    if (result is not null)
                    {
                        await this.RemoveAsync(result);
                    }
                    else
                    {
                        return ApiResponse.CreateResponse(HttpStatusCode.NoContent, "Girilen id'ye ait sipariş bulunamadı");
                    }
                }
                else
                {
                    return ApiResponse.CreateResponse(HttpStatusCode.NoContent, "Lütfen sipariş id giriniz");
                }

                return ApiResponse.CreateResponse(HttpStatusCode.OK, ApiResponse.SuccessMessage);
            }
            catch (ArgumentException ex)
            {
                return ApiResponse.CreateResponse(HttpStatusCode.InternalServerError, ApiResponse.ErrorMessage);
            }
        }

        public async Task<ApiResponse> UpdateCustomerOrderAddress(CustomerOrderAddressUpdateDto order)
        {
            try
            {
                if (order.cust_ord_cust_id > 0 && !string.IsNullOrWhiteSpace(order.cust_ord_order_no))
                {
                    var customer_order = this.GetAllAsync().Result.Where(x => x.CustomerId == order.cust_ord_cust_id &&
                                                             x.cust_order_no == order.cust_ord_order_no).FirstOrDefault();

                    if (customer_order is not null)
                    {
                        customer_order.cust_ord_address = order.cust_ord_address;

                        await this.UpdateAsync(customer_order);
                    }
                    else
                    {
                        return ApiResponse.CreateResponse(HttpStatusCode.NoContent, "Girilen id'ye ait sipariş bulunamadı");
                    }
                }
                else
                {
                    return ApiResponse.CreateResponse(HttpStatusCode.NoContent, "Lütfen sipariş id giriniz");
                }

                return ApiResponse.CreateResponse(HttpStatusCode.OK, ApiResponse.SuccessMessage);
            }
            catch (ArgumentException ex)
            {
                return ApiResponse.CreateResponse(HttpStatusCode.InternalServerError, ApiResponse.ErrorMessage);
            }
        }

        public async Task<ApiResponse> UpdateCustomerOrderQuantity(CustomerOrderQuantityUpdateDto order)
        {
            try
            {
                if (order.cust_ord_cust_id != 0 && !string.IsNullOrWhiteSpace(order.cust_ord_order_no) && !string.IsNullOrWhiteSpace(order.cust_ord_barcode))
                {
                    var customer_order = this.GetAllAsync().Result.Where(x => x.CustomerId == order.cust_ord_cust_id &&
                                                                                 x.cust_order_no == order.cust_ord_order_no &&
                                                                                 x.cust_ord_barcode == order.cust_ord_barcode).FirstOrDefault();

                    if (customer_order is not null)
                    {
                        customer_order.cust_ord_quantity = order.cust_ord_quantity;

                        await this.UpdateAsync(customer_order);
                    }
                    else
                    {
                        return ApiResponse.CreateResponse(HttpStatusCode.NoContent, "Girilen id'ye ait sipariş bulunamadı");
                    }
                }
                else
                {
                    return ApiResponse.CreateResponse(HttpStatusCode.NoContent, "Lütfen sipariş id giriniz");
                }

                return ApiResponse.CreateResponse(HttpStatusCode.OK, ApiResponse.SuccessMessage);
            }
            catch (ArgumentException ex)
            {
                return ApiResponse.CreateResponse(HttpStatusCode.InternalServerError, ApiResponse.ErrorMessage);
            }
        }

    }
}
