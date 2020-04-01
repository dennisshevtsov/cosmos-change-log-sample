// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace CosmosChangeLogSample.Data.Entities
{
  using System;

  public class ProductChangeNoteEntityBase : EntityBase
  {
    public DateTime CreateOn { get; set; }

    public Guid ProductId { get; set; }

    public ProductEntity Product { get; set; }
  }
}
