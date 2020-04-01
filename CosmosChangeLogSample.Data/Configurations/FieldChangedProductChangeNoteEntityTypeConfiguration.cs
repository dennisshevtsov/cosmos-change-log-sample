// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace CosmosChangeLogSample.Data.Configurations
{
  using Microsoft.EntityFrameworkCore;
  using Microsoft.EntityFrameworkCore.Metadata.Builders;

  using CosmosChangeLogSample.Data.Entities;

  internal sealed class FieldChangedProductChangeNoteEntityTypeConfiguration :
    IEntityTypeConfiguration<ProductFieldChangedProductChangeNoteEntity>
  {
    public void Configure(EntityTypeBuilder<ProductFieldChangedProductChangeNoteEntity> builder)
    {
      builder.HasPartitionKey("modelName");
      builder.HasBaseType<ProductChangeNoteEntityBase>();

      ConfigureChangeFields(builder.OwnsMany(entity => entity.ChangedFields));
    }

    private static void ConfigureChangeFields(
      OwnedNavigationBuilder<ProductFieldChangedProductChangeNoteEntity, FieldChangedRecordEntity> builder)
    {
      builder.ToJsonProperty("changedFields");

      builder.Property(entity => entity.FieldName).ToJsonProperty("fieldName").IsRequired();
      builder.Property(entity => entity.PreviousValue).ToJsonProperty("previousValue").IsRequired();
      builder.Property(entity => entity.NewValue).ToJsonProperty("newValue").IsRequired();
    }
  }
}
