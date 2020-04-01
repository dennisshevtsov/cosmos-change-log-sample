// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace CosmosChangeLogSample.Data.Repositories
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Reflection;
  using System.Threading;
  using System.Threading.Tasks;

  using AutoMapper;
  using Microsoft.EntityFrameworkCore;

  using CosmosChangeLogSample.Data.Entities;

  internal sealed class ProductChangeNoteRepository : IProductChangeNoteRepository
  {
    private readonly IMapper _mapper;
    private readonly IDbContextProvider _dbContextProvider;

    public ProductChangeNoteRepository(IMapper mapper, IDbContextProvider dbContextProvider)
    {
      _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
      _dbContextProvider = dbContextProvider ?? throw new ArgumentNullException(nameof(dbContextProvider));
    }

    public async Task<IEnumerable<ProductChangeNoteEntityBase>> GetProductChangeNotesAsync(
      Guid productId, CancellationToken cancellationToken)
    {
      using (var context = _dbContextProvider.GetNewDbContext())
      {
        return await context.Set<ProductChangeNoteEntityBase>()
                            .AsNoTracking()
                            .Where(entity => entity.ProductId == productId)
                            .ToArrayAsync(cancellationToken);
      }
    }

    public async Task CreateProductFieldChangedProductChangeNoteAsync(
      ProductEntity productEntity, CancellationToken cancellationToken)
    {
      using (var context = _dbContextProvider.GetNewDbContext())
      {
        var dbProductEntity = await context.Set<ProductEntity>()
                                           .FirstOrDefaultAsync(entity => entity.Id == productEntity.Id);
        var fieldChangedRecordEntities = new List<FieldChangedRecordEntity>();

        foreach (var property in typeof(ProductEntity).GetProperties(
          BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty))
        {
          if (property.PropertyType.IsValueType ||
              property.PropertyType == typeof(string))
          {
            var previusValue = property.GetValue(dbProductEntity);
            var newValue = property.GetValue(productEntity);

            if (previusValue != null && !previusValue.Equals(newValue))
            {
              fieldChangedRecordEntities.Add(new FieldChangedRecordEntity
              {
                FieldName = property.Name,
                PreviousValue = previusValue.ToString(),
                NewValue = newValue.ToString(),
              });
            }
          }
        }

        if (fieldChangedRecordEntities.Count > 0)
        {
          var productFieldChangedProductChangeNoteEntity = new ProductFieldChangedProductChangeNoteEntity
          {
            CreateOn = DateTime.UtcNow,
            ProductId = productEntity.Id,
            ChangedFields = fieldChangedRecordEntities,
          };

          var entry = context.Add(productFieldChangedProductChangeNoteEntity);

          await context.SaveChangesAsync();
        }
      }
    }

    public async Task CreateOrderCreatedProductChangeNotesAsync(
      OrderEntity orderEntity, CancellationToken cancellationToken)
    {
      using (var context = _dbContextProvider.GetNewDbContext())
      {
        var relationEntities = await context.Set<OrderProductRelationEntity>()
                                            .AsNoTracking()
                                            .Where(entity => entity.OrderId == orderEntity.Id)
                                            .ToArrayAsync();

        context.AddRange(_mapper.Map<IEnumerable<OrderCreatedProductChangeNoteEntity>>(relationEntities));
        await context.SaveChangesAsync(cancellationToken);
      }
    }
  }
}
