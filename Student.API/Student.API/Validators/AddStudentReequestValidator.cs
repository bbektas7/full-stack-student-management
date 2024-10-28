using FluentValidation;
using Student.API.DomainModels;
using Student.API.Repositories;
using System.Linq;

namespace Student.API.Validators
{
    public class AddStudentReequestValidator: AbstractValidator<AddStudentRequest>
    {
        public AddStudentReequestValidator(IStudentRepository studentRepository)
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.DateOfBirth).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Mobile).GreaterThan(99999).LessThan(10000000000);
            RuleFor(x => x.GenderId).NotEmpty().Must(id =>
            {
                var gender = studentRepository.GetGendersAsync().Result.ToList().FirstOrDefault(x => x.Id == id);

                if (gender != null)
                {
                    return true;
                }
                return false;
            }).WithMessage("Please select a valid Gender");
            RuleFor(x=> x.PhysicalAdress).NotEmpty();
            RuleFor(x => x.PostalAdress).NotEmpty();
        }
    }
}
