// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace CosmosChangeLogSample.Data.Entities
{
  using System;
  using System.Collections.Generic;

  public sealed class OrderEntity : EntityBase
  {
    public string ClientName { get; set; }

    public string DeliveryAddress { get; set; }

    public string Description { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? OpenedOn { get; set; }

    public DateTime? ClosedOn { get; set; }

    public DateTime? CompletedOn { get; set; }

    public bool Created => OpenedOn == null && ClosedOn == null && CompletedOn == null;

    public bool Opened => OpenedOn != null && ClosedOn == null && CompletedOn == null;

    public bool Closed => OpenedOn != null && ClosedOn != null && CompletedOn == null;

    public bool Completed => OpenedOn != null && ClosedOn == null && CompletedOn != null;

    public IEnumerable<OrderProductRelationEntity> OrderProductRelations { get; set; }

    public IEnumerable<ProductEntity> Products { get; set; }

    public IEnumerable<Guid> ProductIdCollection { get; set; }
  }
}
