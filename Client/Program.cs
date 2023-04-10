// See https://aka.ms/new-console-template for more information

using System.Net;
using System.Net.Sockets;
using Common.Contracts;

const string serverIp = ConnectionInformation.ServerIp;
const int port = ConnectionInformation.ServerPort;

string message = Request.Student.GetAll;

try
{
    // Prefer a using declaration to ensure the instance is Disposed later.
    IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse(serverIp), port);
    using TcpClient client = new TcpClient();
    client.Connect(serverEndPoint);
    
    // Translate the passed message into ASCII and store it as a Byte array.
    Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

    // Get a client stream for reading and writing.
    NetworkStream stream = client.GetStream();

    // Send the message to the connected TcpServer.
    stream.Write(data, 0, data.Length);

    Console.WriteLine("Sent: {0}", message);

    // Receive the server response.

    // Buffer to store the response bytes.
    data = new Byte[512];

    // String to store the response ASCII representation.
    string responseData = string.Empty;

    // Read the first batch of the TcpServer response bytes.
    int bytes = stream.Read(data, 0, data.Length);
    responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
    Console.WriteLine("Received: {0}", responseData);

    Console.ReadKey();

    // Explicit close is not necessary since TcpClient.Dispose() will be
    // called automatically.
    // stream.Close();
    // client.Close();
}
catch (ArgumentNullException e)
{
    Console.WriteLine("ArgumentNullException: {0}", e);
}
catch (SocketException e)
{
    Console.WriteLine("SocketException: {0}", e);
}