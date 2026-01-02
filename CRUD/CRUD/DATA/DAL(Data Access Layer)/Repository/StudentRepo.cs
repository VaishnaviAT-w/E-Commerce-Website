using CRUD.Core.Entities;
using CRUD.Core.IRepository;
using CRUD.DATA.DAL_Data_Access_Layer_.AppDBContext;

namespace CRUD.DATA.DAL_Data_Access_Layer_.Repository
{
    public class StudentRepo : IStudentRepo
    {
        private readonly ApplicationDBContext _context;
        public StudentRepo(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<int> AddStudent(StudentDetails entity)
        {
            _context.studentdetails.Add(entity);   
            await _context.SaveChangesAsync();  
            return entity.StudentId;
        }

        public async Task<int> UpdateStudent(StudentDetails entity)
        {
            _context.studentdetails.Update(entity);
            await _context.SaveChangesAsync();
            return entity.StudentId;
        }

        public IQueryable<StudentDetails> GetAllStudents()
        {
            return _context.studentdetails.Where(x=>x.IsActive==true);
        }

        public async Task<StudentDetails?> GetByStudentId(int studentId)
        {
            return await _context.studentdetails.FindAsync(studentId);
        }
    }
}
    