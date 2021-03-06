﻿using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Grpc.Health.V1;
using MediatR;
using MicroCommerce.Catalog.API.Application.Products.Commands;
using MicroCommerce.Catalog.API.Application.Products.Models;
using MicroCommerce.Catalog.API.Application.Products.Queries;
using MicroCommerce.Catalog.API.Infrastructure;
using MicroCommerce.Catalog.API.Infrastructure.Filters;
using MicroCommerce.Catalog.API.Infrastructure.Paged;
using MicroCommerce.Catalog.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MicroCommerce.Catalog.API.Controllers.Http
{
    [Authorize]
    [Route("api/products")]
    [TranslateResultToActionResult]
    public class ProductController : ApiControllerBase
    {
        public ProductController(ILogger<ProductController> logger, IMediator mediator) : base(logger, mediator)
        {
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<Result<ProductDto>> Get(int id)
        {
            return await Mediator.Send(new FindProductByIdQuery { Id = id });
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<OffsetPaged<ProductDto>> Get([FromQuery] FindProductsQuery request)
        {
            return await Mediator.Send(request);
        }

        [HttpPost]
        public async Task<Result<ProductDto>> Create([FromForm] CreateProductCommand request)
        {
            return await Mediator.Send(request);
        }

        [HttpPut]
        public async Task<Result<ProductDto>> Update([FromForm] UpdateProductCommand request)
        {
            return await Mediator.Send(request);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Result>> Delete(int id)
        {
            return await Mediator.Send(new DeleteProductCommand { Id = id });
        }

        [AllowAnonymous]
        [HttpGet("/health/ordering")]
        public async Task<IActionResult> HealthOrdering([FromServices] IOrderingServiceClient orderingServiceClient)
        {
            await Mediator.Send(new FindProductsQuery());
            var a = await orderingServiceClient.SayHello();

            return Ok(new { a });
        }
    }
}
