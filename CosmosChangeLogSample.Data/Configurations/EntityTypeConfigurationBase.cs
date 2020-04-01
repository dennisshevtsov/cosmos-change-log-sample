// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace CosmosChangeLogSample.Data.Configurations
{
  using Microsoft.EntityFrameworkCore;
  using Microsoft.EntityFrameworkCore.Metadata.Builders;
  using Microsoft.EntityFrameworkCore.ValueGeneration;

  using CosmosChangeLogSample.Data.Entities;
  using CosmosChangeLogSample.Data.ValueGeneration;

  internal abstract class EntityTypeConfigurationBase<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : EntityBase
  {
    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
      builder.ToContainer("ordering");
      builder.HasDiscriminator("modelName", typeof(string));

      builder.HasKey(entity => entity.Id);
      builder.HasPartitionKey("modelName");

      builder.Property(entity => entity.Id).ToJsonProperty("primaryKey").HasValueGenerator<GuidValueGenerator>();
      builder.Property(typeof(string), "modelName").ToJsonProperty("modelName").HasValueGenerator<EntityTypeNameValueGenerator>();

      ConfigureInternal(builder);
    }

    protected abstract void ConfigureInternal(EntityTypeBuilder<TEntity> builder);
  }
}
