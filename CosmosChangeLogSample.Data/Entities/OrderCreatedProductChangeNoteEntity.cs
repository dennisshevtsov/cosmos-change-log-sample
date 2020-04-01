// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace CosmosChangeLogSample.Data.Entities
{
  using System;

  public sealed class OrderCreatedProductChangeNoteEntity : ProductChangeNoteEntityBase
  {
    public Guid OrderId { get; set; }

    public string OrderClientName { get; set; }
  }
}
