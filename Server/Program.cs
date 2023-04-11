using Autofac;
using Microsoft.Extensions.Logging;
using Server;
using Server.Persistence;
using Server.Repositories;
using Server.Strategies;

var builder = new ContainerBuilder();
ConfigureContainer(builder);
IContainer container = builder.Build();

using (var scope = container.BeginLifetimeScope())
{
    var server = scope.Resolve<IStudentServer>();
    server.StartListen();
}

static void ConfigureLogging(ILoggingBuilder log)
{
    log.ClearProviders();
    log.SetMinimumLevel(LogLevel.Debug);
    log.AddConsole();
}
static void ConfigureContainer(ContainerBuilder builder)
{
    builder.Register(handler => LoggerFactory.Create(ConfigureLogging))
        .As<ILoggerFactory>()
        .SingleInstance()
        .AutoActivate();

    builder.RegisterGeneric(typeof(Logger<>))
        .As(typeof(ILogger<>))
        .SingleInstance();
    
    // other registrations
    builder.RegisterType<StudentRepository>().As<IStudentRepository>();
    builder.RegisterType<DataSource>().AsSelf().SingleInstance();
    builder.RegisterType<StudentRequestHandler>().As<IStudentRequestHandlerStrategy>();
    builder.RegisterType<StudentServer>().As<IStudentServer>();
}