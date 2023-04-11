using Common.Contracts;
using Common.Models;
using Microsoft.Extensions.Logging;
using Server.Repositories;

namespace Server.Strategies;

public class StudentRequestHandler : IStudentRequestHandlerStrategy
{
    private readonly ILogger<StudentRequestHandler> _logger;
    private readonly IStudentRepository _studentRepository;
    
    public StudentRequestHandler(ILogger<StudentRequestHandler> logger, IStudentRepository studentRepository)
    {
        _logger = logger;
        _studentRepository = studentRepository;
    }

    public IEnumerable<Student> HandleRequest(string request)
    {
        if (request == Request.Student.GetAll)
        {
            return _studentRepository.GetAllStudents();
        }
        else if (request == Request.Student.GetOver18)
        {
            return _studentRepository.GetAllStudents().Where(s => s.Age >= 18);
        }
        else if (request == Request.Student.GetFellows)
        {
            return _studentRepository.GetAllStudents().Where(s => s.GradePointAverage >= 4);
        }
        else
        {
            _logger.LogWarning($"Inappropriate request: {request}");
            return new List<Student>();
        }
    }
}