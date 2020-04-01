// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace CosmosChangeLogSample.Data.Repositories
{
  using System;
  using System.Collections.Generic;
  using System.Threading;
  using System.Threading.Tasks;

  using CosmosChangeLogSample.Data.Defaults;
  using CosmosChangeLogSample.Data.Entities;

  public interface IProductRepository
  {
    public Task<ProductEntity> CreateProductAsync(
      ProductEntity productEntity, CancellationToken cancellationToken);

    public Task<ProductEntity> UpdateProducAsync(
      ProductEntity productEntity, CancellationToken cancellationToken);

    public Task<IEnumerable<ProductEntity>> GetProductsForOrderAsync(
      Guid orderId,
      OrderProductSortProperty sortProperty,
      SortDirection sortDirection,
      CancellationToken cancellationToken);

    public Task<IEnumerable<ProductEntity>> GetProductsAsync(
      IEnumerable<Guid> productIdCollection, CancellationToken cancellationToken);
  }
}
