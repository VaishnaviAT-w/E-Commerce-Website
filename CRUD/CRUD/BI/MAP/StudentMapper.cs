using CRUD.Core.DTO;
using CRUD.Core.Entities;

namespace CRUD.BI.MAP
{
    public class StudentMapper
    {
        public StudentDetails AddMap(StudentDto dto)
        {
            return new StudentDetails
            {
                StudentName = dto.StudentName,
                CollegeName = dto.CollegeName,
                YearOfPassOut = dto.YearOfPassOut,
                Percentage = dto.Percentage,
                CreatedBy = 1,
                IsActive = true
            };
        }

        public StudentDetails UpdateMap(StudentDetails entity, StudentDto dto)
        {
            entity.StudentName = dto.StudentName;
            entity.CollegeName = dto.CollegeName;
            entity.YearOfPassOut = dto.YearOfPassOut;
            entity.Percentage = dto.Percentage;
            entity.UpdatedBy = 1;
            return entity;
        }

        public StudentDetails DeleteMap(StudentDetails entity)
        {
            entity.IsActive = false;
            entity.UpdatedBy = 1;
            return entity;
        }

    }
}
