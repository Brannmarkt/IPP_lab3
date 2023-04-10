namespace Server.Models;

public class Student
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public int Age { get; set; }
    public double GradePointAverage { get; set; }
}