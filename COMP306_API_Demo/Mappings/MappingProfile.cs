using AutoMapper;
using COMP306_API_Demo.Models;
using ProductLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COMP306_API_Demo.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductSummaryDto>();
            CreateMap<Product, ProductDetailDto>();
            CreateMap<Product, ProductCreateUpdateDto>();
            CreateMap<ProductCreateUpdateDto, Product>();

            CreateMap<Employee, EmployeeSummaryDto>();
            CreateMap<Employee, EmployeeDetailDto>();
            CreateMap<Employee, EmployeeCreateUpdateDto>();
            CreateMap<EmployeeCreateUpdateDto, Employee>();

        }
    }
}
