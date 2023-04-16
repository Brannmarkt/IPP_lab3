using Microsoft.Extensions.Configuration;
using Autofac;
using Microsoft.Extensions.Logging;
using Server.Extensions;
using Server.Persistence;
using Server.Repositories;
using Server.Strategies;

namespace Server;

public class Startup
{
    public IConfiguration Configuration { get; init; }
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IContainer ConfigureServices(ContainerBuilder builder)
    {
        /* Logger registration */
        builder.Register(handler => LoggerFactory.Create(log =>
            {
                log.ClearProviders();
                log.SetMinimumLevel(Configuration.GetLogLevel());
                log.AddConsole();
            }))
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
        
        return builder.Build();
    }
}