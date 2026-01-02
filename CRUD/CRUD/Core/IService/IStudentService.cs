using CRUD.Core.DTO;
using MFA.Core.DTO;
using static CRUD.Core.DTO.StudentDto;

namespace CRUD.Core.IService
{
    public interface IStudentService
    {
        Task<SaveResultResponse> AddOrUpdateStudent(StudentDto studentDto);
        
        //Task<StudentResponse> UpdateStudent(StudentDto studentDto);
        Task<SaveResultResponse> DeleteStudent(int studentId);

        Task<StudentResponse> GetAllStudents();

        Task<StudentResponse> GetByStudentId(int studentId);

      //  Task<StudentResponse> GetAllStudents(PaginationRequest request);
    }
}
