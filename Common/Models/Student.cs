namespace Common.Models;

public class Student
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public int Age { get; set; }
    public double GradePointAverage { get; set; }

    public override string ToString()
    {
        return $"Fullname: {FullName}, Age: {Age}, Grade point average: {GradePointAverage}";
    }
}