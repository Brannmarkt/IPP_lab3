using Common.Models;
using Microsoft.Extensions.Logging;

namespace Server.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly List<Student> _students;
    
    public StudentRepository(List<Student> students)
    {
        _students = students;
    }

    public IEnumerable<Student> GetAllStudents()
    {
        return _students;
    }
}