using CRUD.Core.Entities;

namespace CRUD.Core.IRepository
{
    public interface IStudentRepo
    {
        Task<int> AddStudent(StudentDetails student);

        Task<int> UpdateStudent(StudentDetails student);

        IQueryable<StudentDetails> GetAllStudents();

        Task<StudentDetails?> GetByStudentId(int studentId);

    }
}


