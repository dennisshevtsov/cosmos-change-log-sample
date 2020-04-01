// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace CosmosChangeLogSample.Data.Configurations
{
  using Microsoft.EntityFrameworkCore;
  using Microsoft.EntityFrameworkCore.Metadata.Builders;

  using CosmosChangeLogSample.Data.Entities;

  internal sealed class ProductEntityTypeConfiguration : EntityTypeConfigurationBase<ProductEntity>
  {
    protected override void ConfigureInternal(EntityTypeBuilder<ProductEntity> builder)
    {
      builder.Property(entity => entity.Sku).ToJsonProperty("sku").IsRequired().HasMaxLength(16);
      builder.Property(entity => entity.Name).ToJsonProperty("name").IsRequired().HasMaxLength(256);
      builder.Property(entity => entity.Description).ToJsonProperty("description").IsRequired().HasMaxLength(512);
      builder.Property(entity => entity.Price).ToJsonProperty("price").IsRequired();
      builder.Property(entity => entity.Enabled).ToJsonProperty("enabled").IsRequired();

      builder.HasMany(entity => entity.OrderProductRelations)
             .WithOne(entity => entity.Product)
             .HasForeignKey(entity => entity.ProductId)
             .HasPrincipalKey(entity => entity.Id);

      builder.HasMany(entity => entity.ChangeNotes)
             .WithOne(entity => entity.Product)
             .HasForeignKey(entity => entity.ProductId)
             .HasPrincipalKey(entity => entity.Id);
      //builder.Ignore(entity => entity.ChangeNotes);
    }
  }
}
