using CRUD.Enum;

namespace CRUD.Core.DTO
{
    public class StudentDto
    {
        public int StudentId { get; set; }
        public string? StudentName { get; set; }
        public string? CollegeName { get; set; }
        public int YearOfPassOut { get; set; }
        public decimal Percentage { get; set; }
    }

    public class StudentResponse 
    {
        public List<StudentRecord>? student { get; set; }
        public ResponseModel Result { get; set; }
        public int TotalCount { get; set; }

        //public int Index { get; set; }
        //public int PageSize { get; set; }
        //public int PageCount { get; set; }
    }

    public class StudentRecord 
    {
        public int StudentId { get; set; }
        public string? StudentName { get; set; }
        public string? CollegeName { get; set; }
        public int YearOfPassOut { get; set; }
        public decimal Percentage { get; set; }
    }

    public class SaveResultResponse
    {
        public  int Id { get; set; }
        public ResponseModel Result { get; set; }
    }
}
