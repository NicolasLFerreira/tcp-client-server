using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

using NetworkTestfield;

class Program
{
    static void Main(string[] args)
    {
        // Configures listener
        TcpListener listener = new(Configurator.EndPoint);

        try
        {
            // Start listening for client requests
            listener.Start();
            Console.WriteLine("Waiting for a connection...");

            // Buffer for reading data
            string message;
            byte[] messageBytes = new byte[1024];

            // Response
            string response = "OK";
            byte[] responseBytes = Encoding.UTF8.GetBytes(response);

            // Enter the listening loop
            while (true)
            {
                // Perform a blocking call to accept requests
                TcpClient client = listener.AcceptTcpClient();
                //Console.WriteLine(listener.Server.LocalEndPoint.ToString());
                Console.WriteLine("Connected!");

                // Get a stream object for reading and writing
                NetworkStream stream = client.GetStream();

                // Loop to receive all the data sent by the client
                int bytes;

                while ((bytes = stream.Read(messageBytes, 0, messageBytes.Length)) != 0)
                {
                    // Translate data bytes to a UTF-8 string
                    message = Encoding.UTF8.GetString(messageBytes, 0, bytes);
                    Console.WriteLine($"Received: {message}");

                    // Confirmation
                    stream.Write(responseBytes, 0, responseBytes.Length);
                    Console.WriteLine($"Sent: {response}");
                }

                // Close everything
                client.Close();
            }
        }
        catch (SocketException e)
        {
            Console.WriteLine($"SocketException: {e}");
        }
        finally
        {
            // Stop listening for new clients
            listener.Stop();
        }

        Console.WriteLine("\nHit enter to continue...");
        Console.Read();
    }
}
