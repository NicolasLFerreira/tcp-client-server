using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        TcpListener listener = null;
        try
        {
            // Set the listener on the local IP address and port 13000
            int port = 13000;
            IPAddress localAddr = IPAddress.Parse("192.168.56.1");
            listener = new TcpListener(localAddr, port);

            // Start listening for client requests
            listener.Start();
            Console.WriteLine("Waiting for a connection...");

            // Buffer for reading data
            byte[] bytes = new byte[1024];
            string data = null;

            // Enter the listening loop
            while (true)
            {
                // Perform a blocking call to accept requests
                TcpClient client = listener.AcceptTcpClient();
                Console.WriteLine("Connected!");

                data = null;

                // Get a stream object for reading and writing
                NetworkStream stream = client.GetStream();

                int i;

                // Loop to receive all the data sent by the client
                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    // Translate data bytes to a UTF-8 string
                    data = Encoding.UTF8.GetString(bytes, 0, i);
                    Console.WriteLine("Received: {0}", data);

                    // Process the data sent by the client
                    data = data.ToUpper();

                    byte[] msg = Encoding.UTF8.GetBytes(data);

                    // Send back a response
                    stream.Write(msg, 0, msg.Length);
                    Console.WriteLine("Sent: {0}", data);
                }

                // Shutdown and end connection
                client.Close();
            }
        }
        catch (SocketException e)
        {
            Console.WriteLine("SocketException: {0}", e);
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
