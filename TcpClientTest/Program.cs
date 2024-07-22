using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

using NetworkTestfield;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Attempting connection...");
        try
        {
            // Create client and stream
            TcpClient client = new(Configurator.IPPlain, Configurator.Port);
            NetworkStream stream = client.GetStream();

            Console.WriteLine($"Connected!");
            Console.WriteLine($"IP: {Configurator.IPPlain} / Port: {Configurator.Port}");

            // Inside loop variables
            string message = "Hello World!";
            byte[] messageByte;
            string response;
            byte[] responseByte;
            int bytes;


            while (message != string.Empty)
            {
                // Encode message
                message = Console.ReadLine() ?? string.Empty;
                messageByte = Encoding.UTF8.GetBytes(message);

                // Send to TCP listener
                stream.Write(messageByte, 0, messageByte.Length);
                Console.WriteLine($"Sent: {message}");

                // Response data
                response = string.Empty;
                responseByte = new byte[256];

                // Read the first batch of the TcpServer response bytes
                bytes = stream.Read(responseByte, 0, responseByte.Length);
                response = Encoding.UTF8.GetString(responseByte, 0, bytes);
                Console.WriteLine($"Received: {response}");
            }
            // Close everything
            stream.Close();
            client.Close();
        }
        catch (ArgumentNullException e)
        {
            Console.WriteLine($"Failed to connect!");
            Console.WriteLine($"ArgumentNullException: {e}");
        }
        catch (SocketException e)
        {
            Console.WriteLine($"Failed to connect!");
            Console.WriteLine($"SocketException: {e}");
        }

        Console.WriteLine("\nHit enter to continue...");
        Console.Read();
    }
}
