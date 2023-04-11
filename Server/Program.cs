using System.Net;
using Common.Contracts;
using Common.Models;
using Microsoft.Extensions.Logging;
using Server;
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

var names = new List<string>
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

var students = Enumerable.Range(0, 15).Select(x =>
{
    return new Student
    {
        Id = Guid.NewGuid(), FullName = names[x], Age = Random.Shared.Next(16, 24),
        GradePointAverage = Random.Shared.Next(3, 6)
    };
}).ToList();
var repository = new StudentRepository(students);
var studentRequestHandler = new StudentRequestHandler(loggerFactory.CreateLogger<StudentRequestHandler>(), repository);
var server = new StudentServer(IPAddress.Any, Port, loggerFactory.CreateLogger<StudentServer>(), studentRequestHandler);
server.StartListen();