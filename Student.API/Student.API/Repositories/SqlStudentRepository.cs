using Microsoft.EntityFrameworkCore;
using Student.API.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Student.API.Repositories
{
    public class SqlStudentRepository : IStudentRepository
    {
        private readonly StudentAdminContext context;
        public SqlStudentRepository(StudentAdminContext context)
        {
            this.context = context;
        }


        public async Task<List<DataModels.Student>> GetStudentsAsync()
        {
            return await context.Student.Include(nameof(Gender)).Include(nameof(Adress)).ToListAsync();
        }


        public async Task<DataModels.Student> GetStudentAsync(Guid studentId)
        {
            return await context.Student.Include(nameof(Gender)).Include(nameof(Adress)).FirstOrDefaultAsync(x => x.Id == studentId);
        }

        public async Task<List<Gender>> GetGendersAsync()
        {
            return await context.Gender.ToListAsync();
        }

        public async Task<bool> Exists(Guid studentID)
        {
            return await context.Student.AnyAsync(x => x.Id == studentID);
        }

        public async Task<DataModels.Student> UpdateStudent(Guid studentId, DataModels.Student request)
        {
            var existingStdudent = await GetStudentAsync(studentId);
            if (existingStdudent != null)
            {
                existingStdudent.FirstName = request.FirstName;
                existingStdudent.LastName = request.LastName;
                existingStdudent.DateOfBirth = request.DateOfBirth;
                existingStdudent.Email = request.Email;
                existingStdudent.Mobile = request.Mobile;
                existingStdudent.GenderId = request.GenderId;
                existingStdudent.Adress.PhysicalAdress = request.Adress.PhysicalAdress;
                existingStdudent.Adress.PostalAdress = request.Adress.PostalAdress;

                await context.SaveChangesAsync();
                return existingStdudent;
            }
            return null;
        }

        public async Task<DataModels.Student> DeleteStudentAsync(Guid studentID)
        {
            var existingStdudent = await GetStudentAsync(studentID);
            if (existingStdudent != null)
            {
                context.Student.Remove(existingStdudent);
                await context.SaveChangesAsync();
                return existingStdudent;
            }
            return null;
        }

        public async Task<DataModels.Student> AddStudentAsync(DataModels.Student request)
        {
           var student = await context.Student.AddAsync(request);
            await context.SaveChangesAsync();
            return student.Entity;
        }

        public async Task<bool> UpdateProfileImage(Guid StudentId, string ProfileImageUrl)
        {
            var student = await GetStudentAsync(StudentId);
            if (student != null) 
            { 
                student.ProfileImageUrl = ProfileImageUrl;
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
