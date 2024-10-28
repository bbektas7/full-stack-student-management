using Student.API.DataModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Student.API.Repositories
{
    public interface IStudentRepository
    {
        Task<List<Student.API.DataModels.Student>> GetStudentsAsync();

        Task<Student.API.DataModels.Student> GetStudentAsync(Guid studentID);

        Task<List<Student.API.DataModels.Gender>> GetGendersAsync();
        Task<Boolean>Exists(Guid studentID);

        Task<Student.API.DataModels.Student>UpdateStudent(Guid studentId, Student.API.DataModels.Student student);
        Task<Student.API.DataModels.Student> DeleteStudentAsync(Guid studentID);
        Task<Student.API.DataModels.Student> AddStudentAsync(Student.API.DataModels.Student student);
        Task<bool> UpdateProfileImage(Guid StudentId, string ProfileImageUrl);
    }
}
