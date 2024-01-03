using AutoMapper;
using Company.DAL.Models;
using Company.PL.ViewModels;

namespace Company.PL.Helpers
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
            //.ForMember(d => d.Name, o => o.MapFrom(s => s.EmpName);

            CreateMap<DepartmentViewModel, Department>().ReverseMap();
        }

    }
}
