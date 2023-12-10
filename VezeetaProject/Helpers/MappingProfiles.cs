using AutoMapper;
using core.Models;
using VezeetaProject.Dtos;
using core.Enums;
namespace VezeetaProject.Helpers
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Doctor, DoctorToReturnDto>()
        .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender.ToString()));

            CreateMap<DoctorToReturnDto, Doctor>()
        .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => Enum.Parse<Gender>(src.Gender)))
        .ReverseMap()
        .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender.ToString()));

            //       CreateMap<DoctorToReturnDto, Doctor>()
            //.ForMember(dest => dest.AdminId, opt => opt.MapFrom(src => src.AdminId))
            //.ForMember(dest => dest.Admin, opt => opt.MapFrom(src => new Admin { UserName = src.AdminUserName })) // Map Admin property
            //.ForMember(dest => dest.Gender, opt => opt.MapFrom(src => Enum.Parse<Gender>(src.Gender))) // Map Gender property
            //.ReverseMap();



           // CreateMap<Patient, PatientToReturnDto>()
           //.ForMember(d => d.Doctor, o => o.MapFrom(src => src.Doctors.full));

        }
    }
}
