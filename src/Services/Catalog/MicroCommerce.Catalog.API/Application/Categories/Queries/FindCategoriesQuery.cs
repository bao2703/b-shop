﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using MicroCommerce.Catalog.API.Application.Categories.Models;
using MicroCommerce.Catalog.API.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MicroCommerce.Catalog.API.Application.Categories.Queries
{
    public class FindCategoriesQuery : IRequest<Result<IEnumerable<CategoryDto>>>
    {
    }

    public class FindCategoriesQueryHandler : IRequestHandler<FindCategoriesQuery, Result<IEnumerable<CategoryDto>>>
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public FindCategoriesQueryHandler(IMapper mapper, ApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public async Task<Result<IEnumerable<CategoryDto>>> Handle(FindCategoriesQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Categories.ToListAsync(cancellationToken);

            return Result.Success(_mapper.Map<IEnumerable<CategoryDto>>(result));
        }
    }
}
