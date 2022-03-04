using AutoMapper;
using MealOrdering.Core.Entities.Dto;
using MealOrdering.Entities.Concrete;
using MealOrdering.Entities.Dto;

namespace MealOrdering.Business.ModelMapping.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            AllowNullDestinationValues = true;
            AllowNullCollections = true;

            CreateMap<Supplier, SupplierDto>()
                .ReverseMap();

            CreateMap<Order, OrderDto>()
                .ForMember(x => x.CreatedUserFullName, y => y.MapFrom(z => $"{z.CreateUser.FirstName} {z.CreateUser.LastName}"))
                .ForMember(x => x.SupplierName, y => y.MapFrom(z => z.Supplier.Name))
                .ReverseMap();

            CreateMap<SubOrder, SubOrderDto>()
                .ForMember(x => x.CreatedUserFullName, y => y.MapFrom(z => $"{z.CreatedUser.FirstName} {z.CreatedUser.LastName}"))
                .ForMember(x => x.OrderName, y => y.MapFrom(z => z.Order.Name ?? ""))
                .ReverseMap();

            CreateMap<User, UserDto>()
                .ReverseMap();
        }
    }
}
