using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Student.API.DomainModels;
using Student.API.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Student.API.Controllers
{
    [ApiController]
    public class StudentController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IImageRepository _IImageRepository;
        private readonly IMapper _mapper;
        public StudentController(IStudentRepository studentRepository, IMapper mapper, IImageRepository imageRepository)
        {
            this._studentRepository = studentRepository;
            this._IImageRepository = imageRepository;
            this._mapper = mapper;
        }
        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> GetAllStudentAsync()
        {
            var students = await _studentRepository.GetStudentsAsync();


            return  Ok(_mapper.Map<List<Student.API.DomainModels.Student>>(students));
            //var domainModelStudents = new List<Student.API.DomainModels.Student>();

            //foreach (var student in students) 
            //{
            //    domainModelStudents.Add(new DomainModels.Student()
            //    {
            //        Id = student.Id,
            //        FirstName = student.FirstName,
            //        LastName = student.LastName,
            //        DateOfBirth = student.DateOfBirth,
            //        Email = student.Email,
            //        Mobile = student.Mobile,
            //        ProfileImageUrl = student.ProfileImageUrl,
            //        GenderId = student.GenderId,
            //        Adress = new DomainModels.Adress()
            //        {
            //            Id = student.Adress.Id,
            //            PhysicalAdress = student.Adress.PhysicalAdress,
            //            PostalAdress = student.Adress.PostalAdress,
            //        },
            //        Gender = new DomainModels.Gender()
            //        {
            //            Id=student.Gender.Id,
            //            Description = student.Gender.Description,
            //        }
            //    });
            //}

        }
        [HttpGet]
        [Route("[controller]/{studentId:guid}"), ActionName("GetStudentAsync")]
        public async Task<IActionResult> GetStudentAsync([FromRoute] Guid studentId)
        {
            var student = await _studentRepository.GetStudentAsync(studentId);

            if (student == null) 
            {
                return NotFound();
            }
            return Ok(_mapper.Map<Student.API.DomainModels.Student>(student));
        }
        [HttpPut]
        [Route("[controller]/{studentId:guid}")]
        public async Task<IActionResult> UpdateStudentAsync([FromRoute] Guid studentId, [FromBody] updateStudentRequest request)
        {
            if (await _studentRepository.Exists(studentId))
            {
              var updatedStudent = await _studentRepository.UpdateStudent(studentId, _mapper.Map<DataModels.Student>(request));
                if (updatedStudent != null)
                {
                    return Ok(_mapper.Map<DomainModels.Student>(updatedStudent));
                }
            }
            return NotFound();
        }
        [HttpDelete]
        [Route("[controller]/{studentId:guid}")]
        public async Task<IActionResult> DeleteStudentAsync([FromRoute] Guid studentId)
        {
            if (await _studentRepository.Exists(studentId))
            {
                var student = await _studentRepository.DeleteStudentAsync(studentId);
                if (student != null)
                {
                    return Ok(_mapper.Map<DomainModels.Student>(student));
                }
            }
            return NotFound();
        }

        [HttpPost]
        [Route("[controller]/Add")]
        public async Task<IActionResult> AddStudentAsync( [FromBody] AddStudentRequest request)
        {
            var student = await _studentRepository.AddStudentAsync(_mapper.Map<DataModels.Student>(request));
            return CreatedAtAction(nameof(GetStudentAsync),new {studentId=student.Id},_mapper.Map<DomainModels.Student>(student));
        }

        [HttpPost]
        [Route("[controller]/{studentId:guid}/uploud-image")]
        public async Task<IActionResult> UploudImage([FromRoute] Guid studentId,IFormFile profileImage)
        {
            var validExtensions = new List<string>
            {
                ".jpeg",
                ".png",
                ".gif",
                ".jpg"
            };
            if (profileImage != null && profileImage.Length > 0)
            {
                var extension = Path.GetExtension(profileImage.FileName);
                if (validExtensions.Contains(extension))
                {
                    if(await _studentRepository.Exists(studentId))
                    {
                        var fileName = Guid.NewGuid() + Path.GetExtension(profileImage.FileName);
                        var fileImagePath = await _IImageRepository.Uploud(profileImage,fileName);
                        if (await _studentRepository.UpdateProfileImage(studentId, fileImagePath))
                        {
                            return Ok(fileImagePath);
                        }
                        return StatusCode(StatusCodes.Status500InternalServerError,"Error uploading image");
                    }
                }
                return BadRequest("This is not valid Image format");
            }
            return NotFound();
            //var student = await _studentRepository.AddStudentAsync(_mapper.Map<DataModels.Student>(request));
            //return CreatedAtAction(nameof(GetStudentAsync), new { studentId = student.Id }, _mapper.Map<DomainModels.Student>(student));
        }
    }
}
