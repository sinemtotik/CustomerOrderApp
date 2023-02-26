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
    public class ProductService : Service<Product>, IProductService
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public ProductService(IGenericRepository<Product> repository, IUnitOfWork unitOfWork, IMapper mapper,
            IProductRepository productRepository)
            : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public Task<Product> GetByBarkodAsync(string barkod)
        {
            return _productRepository.GetByBarkodAsync(barkod);
        }

        public async Task<ApiResponse> InsertProduct(Product model)
        {
            try
            {
                Product entity = new Product();

                entity.pro_barcode = model.pro_barcode;
                entity.pro_description = model.pro_description;
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
