// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace CosmosChangeLogSample.Data.Entities
{
  using System;

  public sealed class OrderProductRelationEntity : EntityBase
  {
    public Guid OrderId { get; set; }

    public OrderEntity Order { get; set; }

    public Guid ProductId { get; set; }

    public ProductEntity Product { get; set; }

    public string ProductSku { get; set; }

    public string ProductName { get; set; }

    public float ProductPrice { get; set; }

    public string OrderClientName { get; set; }

    public string OrderDeliveryAddress { get; set; }
  }
}
