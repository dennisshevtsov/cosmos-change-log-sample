﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace CosmosChangeLogSample.Data.Entities
{
  using System.Collections.Generic;

  public sealed class ProductEntity : EntityBase
  {
    public string Sku { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public float Price { get; set; }

    public bool Enabled { get; set; }

    public IEnumerable<OrderProductRelationEntity> OrderProductRelations { get; set; }

    public IEnumerable<ProductChangeNoteEntityBase> ChangeNotes { get; set; }
  }
}
