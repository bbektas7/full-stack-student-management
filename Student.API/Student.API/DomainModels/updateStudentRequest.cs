using System;

namespace Student.API.DomainModels
{
    public class updateStudentRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public string Email { get; set; }
        public long Mobile { get; set; }
        public Guid GenderId { get; set; }
        public string PhysicalAdress { get; set; }
        public string PostalAdress { get; set; }
    }
}
