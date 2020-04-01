// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace CosmosChangeLogSample.Data.ValueGeneration
{
  using System;

  using Microsoft.EntityFrameworkCore.ChangeTracking;
  using Microsoft.EntityFrameworkCore.ValueGeneration;

  internal sealed class ConstantValueGenerator : ValueGenerator<string>
  {
    private readonly string _partitionKey;

    public ConstantValueGenerator(string partitionKey)
    {
      _partitionKey = partitionKey ?? throw new ArgumentNullException(nameof(partitionKey));
    }

    public override bool GeneratesTemporaryValues => false;

    public override string Next(EntityEntry entry) => _partitionKey;
  }
}
