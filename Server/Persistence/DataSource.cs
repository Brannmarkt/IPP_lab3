using Common.Models;

namespace Server.Persistence;

public class DataSource
{
    private readonly List<string> _names;
    public DataSource()
    {
        _names = new List<string>
        {
            "Aleksandra Kovalenko",
            "Ivan Petrov",
            "Katarina Novak",
            "Andrey Pavlov",
            "Elena Kuznetsova",
            "Pavel Sokolov",
            "Olga Shirokova",
            "Viktoriya Ivanova",
            "Sergei Gorbachev",
            "Nataliya Belyaeva",
            "Dmitriy Tarasov",
            "Marina Volkova",
            "Anton Popov",
            "Anastasia Orlova",
            "Roman Gromov"
        };
    }
    
    public IEnumerable<Student> GetStudents()
    {
        var students = Enumerable.Range(0, 15)
            .Select(x => new Student
            {
                Id = Guid.NewGuid(), FullName = _names[x], Age = Random.Shared.Next(16, 24),
                GradePointAverage = Random.Shared.Next(3, 6)
            }).ToList();
        return students;
    }
}