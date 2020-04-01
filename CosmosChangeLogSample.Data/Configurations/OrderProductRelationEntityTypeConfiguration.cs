// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace CosmosChangeLogSample.Data.Configurations
{
  using Microsoft.EntityFrameworkCore;
  using Microsoft.EntityFrameworkCore.Metadata.Builders;

  using CosmosChangeLogSample.Data.Entities;

  internal sealed class OrderProductRelationEntityTypeConfiguration : EntityTypeConfigurationBase<OrderProductRelationEntity>
  {
    protected override void ConfigureInternal(EntityTypeBuilder<OrderProductRelationEntity> builder)
    {
      builder.Property(entity => entity.OrderId).ToJsonProperty("orderId").IsRequired();
      builder.Property(entity => entity.ProductId).ToJsonProperty("productId").IsRequired();

      builder.Property(entity => entity.ProductSku).ToJsonProperty("productSku").IsRequired().HasMaxLength(16);
      builder.Property(entity => entity.ProductName).ToJsonProperty("productName").IsRequired().HasMaxLength(256);
      builder.Property(entity => entity.ProductPrice).ToJsonProperty("productPrice").IsRequired().HasMaxLength(512);
      builder.Property(entity => entity.OrderClientName).ToJsonProperty("orderClientName").IsRequired().HasMaxLength(512);
      builder.Property(entity => entity.OrderDeliveryAddress).ToJsonProperty("orderDeliveryAddress").IsRequired().HasMaxLength(512);
      
      builder.HasOne(entity => entity.Order)
             .WithMany(entity => entity.OrderProductRelations)
             .HasForeignKey(entity => entity.OrderId)
             .HasPrincipalKey(entity => entity.Id);
      builder.HasOne(entity => entity.Product)
             .WithMany(entity => entity.OrderProductRelations)
             .HasForeignKey(entity => entity.ProductId)
             .HasPrincipalKey(entity => entity.Id);
    }
  }
}
