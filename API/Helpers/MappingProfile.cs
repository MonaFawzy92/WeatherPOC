using AutoMapper;
using Domain.Entities;
using Service.DTO;
using Util.Models;
using Util.DTO;

namespace API.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EmployeeDTO, Employee>().ReverseMap();
            CreateMap(typeof(PagedResponse<>), typeof(PagedResponseDTO<>)).ReverseMap();

        }

    }
}
