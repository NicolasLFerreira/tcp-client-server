using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

using NetworkTestfield;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            // Create client and stream
            TcpClient client = new(Configurator.IPPlain, Configurator.Port);
            NetworkStream stream = client.GetStream();

            // Inside loop variables
            string message = "Hello World!";
            string responseData;
            int bytes;
            byte[] data;

            while (message != string.Empty)
            {
                // Encode message
                message = Console.ReadLine() ?? string.Empty;
                data = Encoding.UTF8.GetBytes(message);

                // Send to TcpListener
                stream.Write(data, 0, data.Length);
                Console.WriteLine($"Sent: {message}");

                // Response storage
                data = new byte[256];
                responseData = string.Empty;

                // Read the first batch of the TcpServer response bytes
                bytes = stream.Read(data, 0, data.Length);
                responseData = Encoding.UTF8.GetString(data, 0, bytes);
                Console.WriteLine($"Received: {responseData}");
            }
            // Close everything
            stream.Close();
            client.Close();
        }
        catch (ArgumentNullException e)
        {
            Console.WriteLine($"ArgumentNullException: {e}");
        }
        catch (SocketException e)
        {
            Console.WriteLine($"SocketException: {e}");
        }

        Console.WriteLine("\nHit enter to continue...");
        Console.Read();
    }
}
