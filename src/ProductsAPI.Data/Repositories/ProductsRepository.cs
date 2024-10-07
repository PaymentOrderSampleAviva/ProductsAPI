﻿using Microsoft.EntityFrameworkCore;
using ProductsAPI.Domain.Entities;
using ProductsAPI.Domain.Repositories;

namespace ProductsAPI.Data.Repositories;

public class ProductsRepository(ApplicationDbContext dbContext) : IProductsRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Products.AsNoTracking().ToListAsync(cancellationToken);
    }
}
