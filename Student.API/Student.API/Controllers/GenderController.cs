using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Student.API.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Student.API.Controllers
{
    [ApiController]
    public class GenderController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;
        public GenderController(IStudentRepository studentRepository, IMapper mapper)
        {
            this._studentRepository = studentRepository;
            this._mapper = mapper;
        }

        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> GetAllGendersAsync()
        {
            var genders = await _studentRepository.GetGendersAsync();

            if (genders == null || !genders.Any() ) 
            {
                return NotFound();
            }


            return Ok(_mapper.Map<List<Student.API.DomainModels.Gender>>(genders));
        }
    }
}
