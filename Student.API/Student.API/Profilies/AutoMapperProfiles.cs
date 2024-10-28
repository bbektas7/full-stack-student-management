using AutoMapper;
using Student.API.DomainModels;
using Student.API.Profilies.AfterMap;

namespace Student.API.Profilies
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Student.API.DataModels.Student, Student.API.DomainModels.Student>().ReverseMap();
            CreateMap<Student.API.DataModels.Gender, Student.API.DomainModels.Gender>().ReverseMap();
            CreateMap<Student.API.DataModels.Adress, Student.API.DomainModels.Adress>().ReverseMap();
            CreateMap<updateStudentRequest, Student.API.DataModels.Student>().AfterMap<updateStudentRequestAfterMap>();
            CreateMap<AddStudentRequest, Student.API.DataModels.Student>().AfterMap<AddStudentRequestAfterMap>();
        }
    }
}
