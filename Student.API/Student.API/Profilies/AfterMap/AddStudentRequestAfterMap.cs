using AutoMapper;
using Student.API.DomainModels;
using System;

namespace Student.API.Profilies.AfterMap
{
    public class AddStudentRequestAfterMap : IMappingAction<AddStudentRequest, DataModels.Student>
    {
        public void Process(AddStudentRequest source, DataModels.Student destination, ResolutionContext context)
        {
            destination.Id = Guid.NewGuid();
            destination.Adress = new DataModels.Adress();
            {
                destination.Id = Guid.NewGuid();
                destination.Adress.PhysicalAdress = source.PhysicalAdress;
                destination.Adress.PostalAdress = source.PostalAdress;
            }
        }
    }
}
