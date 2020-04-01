// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace CosmosChangeLogSample.Data.ValueGeneration
{
  using Microsoft.EntityFrameworkCore.ChangeTracking;
  using Microsoft.EntityFrameworkCore.ValueGeneration;

  internal sealed class EntityTypeNameValueGenerator : ValueGenerator<string>
  {
    public override bool GeneratesTemporaryValues => false;

    public override string Next(EntityEntry entry) => entry.Entity.GetType().Name;
  }
}
