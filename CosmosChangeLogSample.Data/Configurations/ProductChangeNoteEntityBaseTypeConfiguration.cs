// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace CosmosChangeLogSample.Data.Configurations
{
  using Microsoft.EntityFrameworkCore;
  using Microsoft.EntityFrameworkCore.Metadata.Builders;

  using CosmosChangeLogSample.Data.Entities;
  using CosmosChangeLogSample.Data.ValueGeneration;

  internal class ProductChangeNoteEntityBaseTypeConfiguration : EntityTypeConfigurationBase<ProductChangeNoteEntityBase>
  {
    protected override void ConfigureInternal(EntityTypeBuilder<ProductChangeNoteEntityBase> builder)
    {
      builder.Property(entity => entity.CreateOn).ToJsonProperty("createdOn").IsRequired().HasValueGenerator<CurrentTimeValueGenerator>();
      builder.Property(entity => entity.ProductId).ToJsonProperty("productId").IsRequired();
    }
  }
}
