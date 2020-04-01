// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace CosmosChangeLogSample.Data.Configurations
{
  using Microsoft.EntityFrameworkCore;
  using Microsoft.EntityFrameworkCore.Metadata.Builders;

  using CosmosChangeLogSample.Data.Entities;

  internal sealed class OrderCreatedProductChangeNoteEntityTypeConfiguration :
    IEntityTypeConfiguration<OrderCreatedProductChangeNoteEntity>
  {
    public void Configure(EntityTypeBuilder<OrderCreatedProductChangeNoteEntity> builder)
    {
      builder.HasPartitionKey("modelName");
      builder.HasBaseType<ProductChangeNoteEntityBase>();

      builder.Property(entity => entity.OrderId).ToJsonProperty("orderId").IsRequired();
      builder.Property(entity => entity.OrderClientName).ToJsonProperty("orderClientName").IsRequired();
    }
  }
}
