using System.Net;
using Common.Contracts;
using Microsoft.Extensions.Logging;
using Server;
using Server.Models;
using Server.Repositories;
using Server.Strategies;

const int Port = ConnectionInformation.ServerPort;

using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder
        .AddFilter("Microsoft", LogLevel.Warning)
        .AddFilter("System", LogLevel.Warning)
        .AddFilter("NonHostConsoleApp.Program", LogLevel.Debug)
        .AddConsole();
});
ILogger logger = loggerFactory.CreateLogger<Program>();

var studentRepository = new StudentRepository(new List<Student>
{
    new Student { Id = Guid.NewGuid(), FullName = "Vitaliy Minaev", Age = 19, GradePointAverage = 5},
    new Student { Id = Guid.NewGuid(), FullName = "Aleksey Kutniy", Age = 20, GradePointAverage = 5},
    new Student { Id = Guid.NewGuid(), FullName = "Michail Kutniy", Age = 20, GradePointAverage = 5}
});
var studentRequestHandler = new StudentRequestHandler(loggerFactory.CreateLogger<StudentRequestHandler>(), studentRepository);
var server = new StudentServer(IPAddress.Any, Port, loggerFactory.CreateLogger<StudentServer>(), studentRequestHandler);
server.StartListen();