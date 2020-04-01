// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace CosmosChangeLogSample.Data.MappingProfiles
{
  using AutoMapper;

  using CosmosChangeLogSample.Data.Entities;

  internal sealed class ProductChangeNoteProfile : Profile
  {
    public ProductChangeNoteProfile()
    {
      ProductChangeNoteProfile.InitializeOrderCreatedMapping(this);
    }

    private static void InitializeOrderCreatedMapping(IProfileExpression expression)
    {
      expression.CreateMap<OrderProductRelationEntity, OrderCreatedProductChangeNoteEntity>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreateOn, opt => opt.Ignore())
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.Product, opt => opt.Ignore())
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.OrderId))
                //.ForMember(dest => dest.Order, opt => opt.Ignore())
                .ForMember(dest => dest.OrderClientName, opt => opt.MapFrom(src => src.OrderClientName));
    }
  }
}
