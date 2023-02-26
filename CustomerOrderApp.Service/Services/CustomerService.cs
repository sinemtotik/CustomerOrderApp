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
    public class CustomerService : Service<Customer>, ICustomerService
    {
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(IGenericRepository<Customer> repository, IUnitOfWork unitOfWork, 
            IMapper mapper, ICustomerRepository customerRepository)
            : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _customerRepository = customerRepository;
        }

        public async Task<ApiResponse> InsertCustomer(Customer model)
        {
            try
            {
                Customer entity = new Customer();

                entity.cust_name = model.cust_name;
                entity.cust_address = model.cust_address;
                entity.CreatedDate = DateTime.Now;
                entity.UpdatedDate = DateTime.Now;

                var resultInsert = await AddAsync(entity);

                if (resultInsert is null)
                    return ApiResponse.CreateResponse(HttpStatusCode.NoContent, ApiResponse.ErrorMessage);

                return ApiResponse.CreateResponse(HttpStatusCode.OK, ApiResponse.SuccessMessage, resultInsert);

            }
            catch (Exception ex)
            {
                return ApiResponse.CreateResponse(HttpStatusCode.InternalServerError, ApiResponse.ErrorMessage);
            }
        }
    }
}
