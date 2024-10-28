using AutoMapper;
using Student.API.DomainModels;
using System.Net.NetworkInformation;

namespace Student.API.Profilies.AfterMap
{
    public class updateStudentRequestAfterMap : IMappingAction<updateStudentRequest, DataModels.Student>
    {
        public void Process(updateStudentRequest source, DataModels.Student destination, ResolutionContext context)
        {
            destination.Adress = new DataModels.Adress();
            {

                destination.Adress.PhysicalAdress = source.PhysicalAdress;
                destination.Adress.PostalAdress = source.PostalAdress;
            }

        }
    }
}
