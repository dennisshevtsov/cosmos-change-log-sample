// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace CosmosChangeLogSample.Data.MappingProfiles
{
  using AutoMapper;

  using CosmosChangeLogSample.Data.Entities;

  internal sealed class OrderProductRelationProfile : Profile
  {
    public OrderProductRelationProfile()
    {
      OrderProductRelationProfile.InitializeProductMappings(this);
      OrderProductRelationProfile.InitializeOrderMappings(this);
    }

    private static void InitializeProductMappings(IProfileExpression expression)
    {
      expression.CreateMap<OrderProductRelationEntity, ProductEntity>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.Sku, opt => opt.MapFrom(src => src.ProductSku))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ProductName))
                .ForMember(dest => dest.Description, opt => opt.Ignore())
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.ProductPrice))
                .ForMember(dest => dest.Enabled, opt => opt.Ignore())
                .ForMember(dest => dest.OrderProductRelations, opt => opt.Ignore())
                .ForMember(dest => dest.ChangeNotes, opt => opt.Ignore());

      expression.CreateMap<ProductEntity, OrderProductRelationEntity>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.OrderId, opt => opt.Ignore())
                .ForMember(dest => dest.Order, opt => opt.Ignore())
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Product, opt => opt.Ignore())
                .ForMember(dest => dest.ProductSku, opt => opt.MapFrom(src => src.Sku))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.OrderClientName, opt => opt.Ignore())
                .ForMember(dest => dest.OrderDeliveryAddress, opt => opt.Ignore());
    }

    private static void InitializeOrderMappings(IProfileExpression expression)
    {
      expression.CreateMap<OrderProductRelationEntity, OrderEntity>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.OrderId))
                .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.OrderClientName))
                .ForMember(dest => dest.DeliveryAddress, opt => opt.MapFrom(src => src.OrderDeliveryAddress))
                .ForMember(dest => dest.Description, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedOn, opt => opt.Ignore())
                .ForMember(dest => dest.OpenedOn, opt => opt.Ignore())
                .ForMember(dest => dest.ClosedOn, opt => opt.Ignore())
                .ForMember(dest => dest.CompletedOn, opt => opt.Ignore())
                .ForMember(dest => dest.Created, opt => opt.Ignore())
                .ForMember(dest => dest.Opened, opt => opt.Ignore())
                .ForMember(dest => dest.Closed, opt => opt.Ignore())
                .ForMember(dest => dest.Completed, opt => opt.Ignore())
                .ForMember(dest => dest.OrderProductRelations, opt => opt.Ignore())
                .ForMember(dest => dest.Products, opt => opt.Ignore())
                .ForMember(dest => dest.ProductIdCollection, opt => opt.Ignore());

      expression.CreateMap<OrderEntity, OrderProductRelationEntity>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Order, opt => opt.Ignore())
                .ForMember(dest => dest.ProductId, opt => opt.Ignore())
                .ForMember(dest => dest.Product, opt => opt.Ignore())
                .ForMember(dest => dest.ProductSku, opt => opt.Ignore())
                .ForMember(dest => dest.ProductName, opt => opt.Ignore())
                .ForMember(dest => dest.ProductPrice, opt => opt.Ignore())
                .ForMember(dest => dest.OrderClientName, opt => opt.MapFrom(src => src.ClientName))
                .ForMember(dest => dest.OrderDeliveryAddress, opt => opt.MapFrom(src => src.DeliveryAddress));
    }
  }
}
