using Server.Models;

namespace Server.Repositories;

public interface IStudentRepository
{
    IEnumerable<Student> GetAllStudents();
}