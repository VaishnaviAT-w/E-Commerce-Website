using CRUD.BI.MAP;
using CRUD.Core.DTO;
using CRUD.Core.IRepository;
using CRUD.Core.IService;
using CRUD.Enum;
using Microsoft.EntityFrameworkCore;

namespace CRUD.BI.Service
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepo _studentRepo;
        private readonly StudentMapper _mapper = new StudentMapper();

        public StudentService(IStudentRepo studentRepo)
        {
            _studentRepo = studentRepo;
        }

        /// <summary>
        /// Add or Update Student
        /// </summary>
        /// <param name="studentDto"></param>
        /// <returns></returns>

        // This method performs both Add (insert) and Update (modify) operations
        // for Student records based on whether the student already exists or not.
        public async Task<SaveResultResponse> AddOrUpdateStudent(StudentDto studentDto)
        {
            // Create a response object that will store the operation result
            var response = new SaveResultResponse();

            try
            {
                // Step 1: Check if a student already exists by their unique StudentId
                // _studentRepo.GetAllStudents() returns all students from the database (IQueryable)
                // FirstOrDefaultAsync(...) checks asynchronously if any student has the given ID
                // If found → returns that student entity, otherwise returns null

                // Check if student already exists by StudentId
                var entity = await _studentRepo.GetAllStudents()
                    .FirstOrDefaultAsync(x => x.StudentId == studentDto.StudentId);

                // Step 2: If the student record already exists in the database
                // If entity found → Update existing student
                if (entity != null)
                {
                    // Use AutoMapper (or custom mapper) to copy updated values
                    // from the studentDto object into the existing entity object
                    entity = _mapper.UpdateMap(entity, studentDto);

                    // Call repository UpdateStudent method to save updated data to DB
                    // (No await → assuming UpdateStudent handles saving internally)

                    response.Id = await _studentRepo.UpdateStudent(entity);
                    // After successful update, set the result status to "Success"
                    response.Result = ResponseModel.Success;
                }
                else
                {
                    // Step 3: If student is not found by ID → it means new student to add
                    // Before adding new student, check if another student with the same name exists
                    // to prevent duplicate entries
                    // If not found → Check if name already exists (to avoid duplicate)
                    var isExist = await _studentRepo
                        .GetAllStudents()
                        .AnyAsync(x => x.StudentName == studentDto.StudentName);

                    // If student name already exists → return "AlreadyExists" response
                    if (isExist)
                    {
                        response.Result = ResponseModel.AlreadyExists;
                        return response;   // Exit method without adding new record
                    }

                    // Step 4: Map (convert) StudentDto to Student entity for database insertion
                    // The mapper converts DTO properties into entity properties
                    // Map new student entity
                    var newEntity = _mapper.AddMap(studentDto);

                    // Step 5: Add the newly mapped student entity to the database asynchronously
                    // Add to database
                    response.Id = await _studentRepo.AddStudent(newEntity);

                    // Step 6: Check if the StudentId generated after insertion > 0
                    // This confirms that the insert was successful
                    // Check if student added successfully
                    response.Result = response.Id > 0
                        ? ResponseModel.Success
                        : ResponseModel.Failed;
                }
            }
            catch (Exception)
            {
                // Step 7: If any unexpected error occurs (DB, mapping, etc.)
                // catch block ensures program doesn’t crash and sets result to "Failed"
                // Handle unexpected errors
                response.Result = ResponseModel.Failed;
            }

            // Step 8: Finally, return the response object containing operation result
            return response;
        }


        //public async Task<StudentResponse> AddStudent(StudentDto studentDto)
        //{
        //    var response = new StudentResponse();

        //    try
        //    {
        //        var isExist = await _studentRepo
        //            .GetAllStudents()
        //            .AnyAsync(x => x.StudentName == studentDto.StudentName);

        //        if (isExist)
        //        {
        //            response.Result = ResponseModel.AlreadyExists; 
        //            return response;
        //        }

        //        var entity = _mapper.AddMap(studentDto);

        //        var createdStudent = await _studentRepo.AddStudent(entity);

        //        response.Result = entity.StudentId > 0
        //            ? ResponseModel.Success
        //            : ResponseModel.Failed;
        //    }
        //    catch (Exception)
        //    {
        //        response.Result = ResponseModel.Failed;
        //    }

        //    return response;
        //}

        //public async Task<StudentResponse> UpdateStudent(StudentDto studentDto)
        //        {
        //            var response = new StudentResponse();
        //            try
        //            {
        //var entity = await _studentRepo.GetAllStudents()
        //    .FirstOrDefaultAsync(x => x.StudentId == studentDto.StudentId);
        //                if (entity != null)
        //                {
        //                    entity = _mapper.UpdateMap(entity, studentDto);
        //                    var stud = _studentRepo.UpdateStudent(entity); 
        //                    response.Result = ResponseModel.Success;
        //                }
        //                else
        //                {
        //                    response.Result = ResponseModel.NotFound;
        //                }
        //            }
        //            catch (Exception)
        //            {
        //                response.Result = ResponseModel.Failed;
        //            }
        //            return response;
        //        }

        //public async Task<StudentResponse> GetAllItUserMaster(PaginationRequest request )
        //{
        //    var response = new StudentResponse();

        //    try
        //    {
        //        var query = _studentRepo.GetAllStudents();

        //        query = query.Where(x => x.IsDeleted == false);
        //        response.TotalCount = await query.CountAsync();
        //        response.Index = request.Index;
        //        response.PageSize = request.PageSize;
        //        response.PageCount = (int)Math.Ceiling(response.TotalCount / (double)request.PageSize);

        //         response.student = await query
        //            .Skip(request.PageSize * (request.Index - 1))
        //            .Take(request.PageSize)
        //            .Select(x => new StudentRecord
        //            {
        //                StudentId = x.StudentId,
        //                StudentName = x.StudentName,
        //                CollegeName = x.CollegeName,
        //                YearOfPassOut = x.YearOfPassOut,
        //                Percentage = x.Percentage

        //            })
        //            .ToListAsync();

        //        response.Result = response.student.Count > 0
        //                        ? ResponseModel.Success
        //                        : ResponseModel.Failed;
        //    }
        //    catch (Exception)
        //    {
        //        response.Result = ResponseModel.Failed;
        //        response.student = new List<StudentRecord>();
        //    }
        //    return response;
        //}





        // Service method to fetch all student records from the database


        /// <summary>
        /// Get All Students 
        /// </summary>
        /// <returns></returns>

        public async Task<StudentResponse> GetAllStudents()
        {   
            // Create a response object to store fetched data and result status
            var response = new StudentResponse();

            try
            {
                // Get the base query (IQueryable) from repository
                var query = _studentRepo.GetAllStudents();

                // Execute LINQ query asynchronously and project (select) only required fields
                var data = await query
                    .Select(x => new StudentRecord
                    {
                        StudentId = x.StudentId,
                        StudentName = x.StudentName,
                        CollegeName = x.CollegeName,
                        YearOfPassOut = x.YearOfPassOut,
                        Percentage = x.Percentage
                    })
                    .ToListAsync(); // Converts query result to a List

                // Store fetched list in response
                response.student = data;

                // If list has data → Success; otherwise → NotFound
                response.Result = data.Count > 0 ? ResponseModel.Success : ResponseModel.NotFound;

                // Store total count of fetched records
                response.TotalCount = data.Count;
            }
            catch (Exception)
            {
                // In case of any runtime or database error, mark operation as Failed
                response.Result = ResponseModel.Failed;

                // Return empty list to avoid null reference
                response.student = new List<StudentRecord>();
            }

            // Return final response object to controller
            return response;
        }


        // This version supports pagination (fetching limited records per page)
        //// Currently commented, but here’s how it works:
        //public async Task<StudentResponse> GetAllStudents(PaginationRequest request)
        //{
        //    var response = new StudentResponse();

        //    try
        //    {
        //        // Step 1: Get all students from repository
        //        var query = _studentRepo.GetAllStudents();

        //        // Step 2: Apply filter - here it's getting only inactive students
        //        query = query.Where(x => x.IsActive == false);

        //        // Step 3: Count total records (for page count calculation)
        //        var totalCount = await query.CountAsync();

        //        // Step 4: Apply pagination logic
        //        // Skip = how many records to skip
        //        // Take = how many records to show per page
        //        var paginatedData = await query
        //            .Skip(request.PageSize * (request.Index - 1)) // e.g., page 2 skips 10 if pagesize=10
        //            .Take(request.PageSize)
        //            .Select(x => new StudentRecord
        //            {
        //                StudentId = x.StudentId,
        //                StudentName = x.StudentName,
        //                CollegeName = x.CollegeName,
        //                YearOfPassOut = x.YearOfPassOut,
        //                Percentage = x.Percentage,
        //            })
        //            .ToListAsync();

        //        // Step 5: Store paginated data and counts in response
        //        response.student = paginatedData;
        //        response.Result = paginatedData.Count > 0 ? ResponseModel.Success : ResponseModel.Failed;
        //        response.TotalCount = totalCount;

        //        // Step 6: Pagination info
        //        response.Index = request.Index;
        //        response.PageSize = request.PageSize;

        //        // Calculate total number of pages (round up)
        //        response.PageCount = (int)Math.Ceiling(totalCount / (double)request.PageSize);
        //    }
        //    catch (Exception)
        //    {
        //        // In case of error, mark as failed and return empty list
        //        response.Result = ResponseModel.Failed;
        //        response.student = new List<StudentRecord>();
        //    }
        //    return response;
        //}












        // Method to perform soft-delete (deactivate) of a student record by ID


        /// <summary>
        /// Delete Students
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public async Task<SaveResultResponse> DeleteStudent(int studentId)
        {
            // Create response object to hold result
            var response = new SaveResultResponse();

            try
            {
                // Step 1: Find the student in database by their StudentId
                var entity = await _studentRepo.GetAllStudents()
                    .FirstOrDefaultAsync(x => x.StudentId == studentId);

                // Step 2: If student not found → return "NotFound"
                if (entity == null)
                {
                    response.Result = ResponseModel.NotFound;
                    return response;
                }

                // Step 4: Soft delete logic — mark record as inactive
                // Usually sets IsActive = false, UpdatedBy = userId, UpdatedDate = current time
                entity = _mapper.DeleteMap(entity);

                // Step 5: Save updated entity back to database
                response.Id = await _studentRepo.UpdateStudent(entity);

                // Step 6: Return success response
                response.Result = ResponseModel.Success;
            }
            catch (Exception)
            {
                // Step 7: Handle any error (DB issue, mapping error, etc.)
                response.Result = ResponseModel.Failed;
            }

            // Step 8: Return response
            return response;
        }

        public async Task<StudentResponse> GetByStudentId(int studentId)
        {
            // Create an empty response object to store result data and status
            var response = new StudentResponse();

            try
            {
                // Call repository method to fetch student details by ID
                var studentdetails = await _studentRepo.GetAllStudents().Where(x=>x.StudentId==studentId).FirstOrDefaultAsync();

                // If student found in database
                if (studentdetails != null)
                {
                    // Map entity (DB model) → DTO / Record object
                    var studentRecord = new StudentRecord
                    {
                        StudentId = studentdetails.StudentId,
                        StudentName = studentdetails.StudentName,
                        CollegeName = studentdetails.CollegeName,
                        YearOfPassOut = studentdetails.YearOfPassOut,
                        Percentage = studentdetails.Percentage
                    };

                    response.student = new List<StudentRecord> { studentRecord };                     // Assign record to response
                    response.Result = ResponseModel.Success;                                         // Set result status as success
                }
                else
                {
                    response.Result = ResponseModel.NotFound;                            // If student not found, send NotFound response
                }
            }
            catch (Exception)
            {
                // If any error occurs during process, set Failed status
                response.Result = ResponseModel.Failed;
            }

            // Return response in case of exception or after processing
            return response;
        }
    }
}

