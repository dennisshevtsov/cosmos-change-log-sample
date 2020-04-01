// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace CosmosChangeLogSample.Data.Repositories
{
  using System;
  using System.Collections.Generic;
  using System.Threading;
  using System.Threading.Tasks;

  using CosmosChangeLogSample.Data.Entities;

  public interface IProductChangeNoteRepository
  {
    public Task<IEnumerable<ProductChangeNoteEntityBase>> GetProductChangeNotesAsync(
      Guid productId, CancellationToken cancellationToken);

    public Task CreateProductFieldChangedProductChangeNoteAsync(
      ProductEntity productEntity, CancellationToken cancellationToken);

    public Task CreateOrderCreatedProductChangeNotesAsync(
      OrderEntity orderEntity, CancellationToken cancellationToken);
  }
}
