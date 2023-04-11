using System.Net;
using System.Net.Sockets;
using Common.Contracts;
using Common.JsonExtensions;
using Common.Models;
using Microsoft.Extensions.Logging;
using Server.Strategies;

namespace Server;

public class StudentServer
{
    private readonly int _port;
    private readonly IPAddress _localAddr;
    private readonly ILogger<StudentServer> _logger;
    private readonly IStudentRequestHandlerStrategy _studentRequestHandler;
    public StudentServer(IPAddress ipAddress, int port, ILogger<StudentServer> logger, IStudentRequestHandlerStrategy studentRequestHandler)
    {
        _localAddr = ipAddress;
        _port = port;
        _logger = logger;
        _studentRequestHandler = studentRequestHandler;
    }

    public void StartListen()
    {
        var tcpListener = new TcpListener(_localAddr, _port);

        try
        {
            tcpListener.Start();
            _logger.LogInformation("Started listen!");
            
            // Buffer for reading data
            Byte[] bytes = new Byte[256];
            string request = string.Empty;

            // Enter the listening loop.
            while(true)
            {
                _logger.LogInformation("Waiting for a connection... ");

                // Perform a blocking call to accept requests.
                // You could also use server.AcceptSocket() here.
                using TcpClient client = tcpListener.AcceptTcpClient();
                _logger.LogInformation("Connected!");

                // Get a stream object for reading and writing
                NetworkStream stream = client.GetStream();
                
                int i = stream.Read(bytes, 0, bytes.Length);

                // Translate data bytes to a ASCII string.
                request = System.Text.Encoding.UTF8.GetString(bytes, 0, i);
                _logger.LogInformation("Received: {0}", request);

                // Process the data sent by the client.
                var result = _studentRequestHandler.HandleRequest(request);
                string response = string.Empty;
                if (result == null)
                {
                    response = ErrorMessages.InvalidRequest;
                }
                else
                {
                    response = result.Serialize<IEnumerable<Student>>();
                }
                    
                byte[] msg = System.Text.Encoding.UTF8.GetBytes(response);

                // Send back a response.
                stream.Write(msg, 0, msg.Length);
                _logger.LogInformation($"Request: {request} has been processed");
            }
        }
        catch (Exception e)
        {
            
            _logger.LogError(e, $"Error: {e.Message}");
            throw;
        }
        finally
        {
            tcpListener.Stop();
            _logger.LogInformation("Server are stopped!");
        }
    }
}