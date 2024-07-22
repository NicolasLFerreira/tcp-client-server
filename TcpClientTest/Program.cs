using System;
using System.Net.Sockets;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            // Connect to a remote device
            int port = 13000;
            TcpClient client = new TcpClient("192.168.56.1", port);

            // Get a client stream for reading and writing
            NetworkStream stream = client.GetStream();

            while (true)
            {
                // Translate the passed message into ASCII and store it as a byte array
                string message = Console.ReadLine() ?? "Sup";
                byte[] data = Encoding.UTF8.GetBytes(message);

                // Send the message to the connected TcpServer
                stream.Write(data, 0, data.Length);
                Console.WriteLine("Sent: {0}", message);

                // Buffer to store the response bytes
                data = new byte[256];

                // String to store the response ASCII representation
                string responseData = string.Empty;

                // Read the first batch of the TcpServer response bytes
                int bytes = stream.Read(data, 0, data.Length);
                responseData = Encoding.UTF8.GetString(data, 0, bytes);
                Console.WriteLine("Received: {0}", responseData);
            }
            // Close everything
            stream.Close();
            client.Close();
        }
        catch (ArgumentNullException e)
        {
            Console.WriteLine("ArgumentNullException: {0}", e);
        }
        catch (SocketException e)
        {
            Console.WriteLine("SocketException: {0}", e);
        }

        Console.WriteLine("\nHit enter to continue...");
        Console.Read();
    }
}
