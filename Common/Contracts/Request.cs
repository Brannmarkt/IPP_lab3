namespace Common.Contracts;

public static class Request
{
    public static class Student
    {
        private const string Base = "Students/";

        public const string GetAll = Base + "GetAll";
        public const string GetOver18 = Base + "GetOver18";
        public const string GetFellows = Base + "GetFellows";
    }
}