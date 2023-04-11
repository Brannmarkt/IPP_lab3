using Common.Models;
using Microsoft.Extensions.Logging;
using Server.Persistence;

namespace Server.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly List<Student> _students;
    public StudentRepository(DataSource dataSource)
    {
        _students = dataSource.GetStudents().ToList();
    }

    public IEnumerable<Student> GetAllStudents()
    {
        return _students;
    }
}