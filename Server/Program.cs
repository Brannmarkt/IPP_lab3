using Autofac;
using Microsoft.Extensions.Configuration;
using Server;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();
var startup = new Startup(configuration);

var builder = new ContainerBuilder();
var container = startup.ConfigureServices(builder);

using (var scope = container.BeginLifetimeScope())
{
    var server = scope.Resolve<IStudentServer>();
    server.StartListen();
}