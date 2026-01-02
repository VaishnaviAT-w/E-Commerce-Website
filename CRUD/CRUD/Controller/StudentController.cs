using CRUD.Core.DTO;
using CRUD.Core.IService;
using Microsoft.AspNetCore.Mvc;

namespace CRUD.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpPost("AddOrUpdateStudents")]
        public async Task<SaveResultResponse> AddOrUpdateStudent(StudentDto studentDto)
        {
            return await _studentService.AddOrUpdateStudent(studentDto);
        }

        //[HttpPut("UpdateStudents")]
        //public async Task<StudentResponse> UpdateStudents([FromBody] StudentDto studentDto)
        //{
        //    return await _studentService.UpdateStudent(studentDto);
        //}

        [HttpGet("GetAllStudents")]
        public async Task<StudentResponse> GetAllStudents()
        {
              return await _studentService.GetAllStudents();
        }

        //[HttpPost("GetAllStudents")]
        //public async Task<StudentResponse> GetAllStudents([FromBody]PaginationRequest request)
        //{
        //    return await _studentService.GetAllStudents(request);
        //}

        [HttpPost("DeleteStudents")]
        public async Task<SaveResultResponse> DeleteStudent(int studentId)
        {
            return await _studentService.DeleteStudent(studentId);
        }   

        [HttpGet("GetByIdStudent")]
        public async Task<StudentResponse> GetByStudentId(int studentId)
        {
            return await _studentService.GetByStudentId(studentId);
        }
    }
}















