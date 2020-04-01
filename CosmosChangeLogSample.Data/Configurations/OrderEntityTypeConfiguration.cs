// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace CosmosChangeLogSample.Data.Configurations
{
  using Microsoft.EntityFrameworkCore;
  using Microsoft.EntityFrameworkCore.Metadata.Builders;

  using CosmosChangeLogSample.Data.Entities;
  using CosmosChangeLogSample.Data.ValueGeneration;

  internal sealed class OrderEntityTypeConfiguration : EntityTypeConfigurationBase<OrderEntity>
  {
    protected override void ConfigureInternal(EntityTypeBuilder<OrderEntity> builder)
    {
      builder.Property(entity => entity.ClientName).ToJsonProperty("clientName").IsRequired();
      builder.Property(entity => entity.DeliveryAddress).ToJsonProperty("deliveryAddress").IsRequired();
      builder.Property(entity => entity.Description).ToJsonProperty("description");
      builder.Property(entity => entity.CreatedOn).ToJsonProperty("createdOn").IsRequired().HasValueGenerator<CurrentTimeValueGenerator>();
      builder.Property(entity => entity.OpenedOn).ToJsonProperty("openedOn");
      builder.Property(entity => entity.ClosedOn).ToJsonProperty("closedOn");
      builder.Property(entity => entity.CompletedOn).ToJsonProperty("completedOn");

      builder.HasMany(entity => entity.OrderProductRelations)
             .WithOne(entity => entity.Order)
             .HasForeignKey(entity => entity.OrderId)
             .HasPrincipalKey(entity => entity.Id);

      builder.Ignore(entity => entity.Created);
      builder.Ignore(entity => entity.Opened);
      builder.Ignore(entity => entity.Closed);
      builder.Ignore(entity => entity.Completed);
      builder.Ignore(entity => entity.Products);
      builder.Ignore(entity => entity.ProductIdCollection);
    }
  }
}
