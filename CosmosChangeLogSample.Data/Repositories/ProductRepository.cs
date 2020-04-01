// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace CosmosChangeLogSample.Data.Repositories
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading;
  using System.Threading.Tasks;

  using AutoMapper;
  using Microsoft.EntityFrameworkCore;

  using CosmosChangeLogSample.Data.Defaults;
  using CosmosChangeLogSample.Data.Entities;

  internal sealed class ProductRepository : IProductRepository
  {
    private readonly IMapper _mapper;
    private readonly IDbContextProvider _dbContextProvider;
    private readonly IProductChangeNoteRepository _productChangeNoteRepository;

    public ProductRepository(IMapper mapper,
                             IDbContextProvider dbContextProvider,
                             IProductChangeNoteRepository productChangeNoteRepository)
    {
      _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
      _dbContextProvider = dbContextProvider ?? throw new ArgumentNullException(nameof(dbContextProvider));
      _productChangeNoteRepository = productChangeNoteRepository ?? throw new ArgumentNullException(nameof(productChangeNoteRepository));
    }

    public async Task<ProductEntity> CreateProductAsync(
      ProductEntity productEntity, CancellationToken cancellationToken)
    {
      using (var context = _dbContextProvider.GetNewDbContext())
      {
        var entry = context.Add(productEntity);

        await context.SaveChangesAsync();

        return entry.Entity;
      }
    }

    public async Task<ProductEntity> UpdateProducAsync(
      ProductEntity productEntity, CancellationToken cancellationToken)
    {
      await _productChangeNoteRepository.CreateProductFieldChangedProductChangeNoteAsync(productEntity, cancellationToken);

      using (var context = _dbContextProvider.GetNewDbContext())
      {
        var dbEntity = await context.FindAsync<ProductEntity>(
          new object[] { productEntity.Id, }, cancellationToken);
        var dbEntry = context.Entry(dbEntity);

        dbEntry.CurrentValues.SetValues(productEntity);

        var relations = await context.Set<OrderProductRelationEntity>()
                                     .Where(entity => entity.ProductId == productEntity.Id)
                                     .ToArrayAsync(cancellationToken);

        Parallel.ForEach(relations, relation => _mapper.Map(productEntity, relation));

        await context.SaveChangesAsync();

        dbEntry.State = EntityState.Detached;

        return dbEntity;
      }
    }

    public async Task<IEnumerable<ProductEntity>> GetProductsForOrderAsync(
      Guid orderId,
      OrderProductSortProperty sortProperty,
      SortDirection sortDirection,
      CancellationToken cancellationToken)
    {
      using (var context = _dbContextProvider.GetNewDbContext())
      {
        var query = context.Set<OrderProductRelationEntity>()
                           .AsNoTracking()
                           .Where(entity => entity.OrderId == orderId);

        query = SortProducts(query, sortProperty, sortDirection);

        var relations = await query.ToArrayAsync(cancellationToken);
        var products = _mapper.Map<IEnumerable<ProductEntity>>(relations);

        return products;
      }
    }

    public async Task<IEnumerable<ProductEntity>> GetProductsAsync(
      IEnumerable<Guid> productIdCollection, CancellationToken cancellationToken)
    {
      using (var context = _dbContextProvider.GetNewDbContext())
      {
        var available = productIdCollection.ToArray();

        return await context.Set<ProductEntity>()
                            .AsNoTracking()
                            .Where(entity => available.Contains(entity.Id))
                            .ToArrayAsync();
      }
    }

    private static IQueryable<OrderProductRelationEntity> SortProducts(
      IQueryable<OrderProductRelationEntity> query,
      OrderProductSortProperty sortProperty,
      SortDirection sortDirection)
    {
      if (sortProperty == OrderProductSortProperty.Sku)
      {
        if (sortDirection == SortDirection.Descending)
        {
          return query.OrderByDescending(entity => entity.ProductSku);
        }

        return query.OrderBy(entity => entity.ProductSku);
      }

      if (sortDirection == SortDirection.Descending)
      {
        return query.OrderByDescending(entity => entity.ProductName);
      }

      return query.OrderBy(entity => entity.ProductName);
    }
  }
}
