using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using RentalPoint.Data.Entities;
using RentalPoint.ViewModels;

namespace RentalPoint.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateOrUpdateCategoryViewModel, Category>();
            CreateMap<CreateOrUpdateInventoryViewModel, Inventory>()
                .AfterMap((src, dest) =>
                {
                    dest.Categories = new List<InventoryCategory>();
                    foreach (var categoryId in src.CategoryIds)
                    {
                        dest.Categories.Add(new InventoryCategory
                        {
                            CategoryId = categoryId
                        });
                    }
                });
            
            CreateMap<CreateOrUpdatePromoViewModel, Promo>();
            CreateMap<CreateOrUpdateFaqViewModel, Faq>();
            CreateMap<CreateOrUpdateNewsViewModel, News>();
            CreateMap<CreateOrUpdateUserViewModel, User>();
            CreateMap<CreateOrderViewModel, CreateOrder>();
            CreateMap<CreateInventoryOrderViewModel, CreateInventoryOrder>();
            CreateMap<UpdateOrderViewModel, Order>();
            CreateMap<UpdatePenaltyViewModel, Penalty>();
            CreateMap<CreatePenaltyViewModel, Penalty>();
            
            CreateMap<Inventory, InventoryViewModel>()
                .ForMember(dest => dest.CategoryIds,
                    e => e.MapFrom(src => src.Categories != null ? src.Categories.Select(x => x.CategoryId) : new List<Guid>()));
            
            CreateMap<News, NewsViewModel>()
                .ForMember(dest => dest.UserName, e => e.MapFrom(src => src.User.Name));
            
            CreateMap<LoginViewModel, User>();
            CreateMap<RegisterViewModel, User>();
        }
    }
}