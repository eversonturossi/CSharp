using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class UDPMessageListener
{
    static void Main()
    {
        int port = 45123;
        // Cria um socket UDP
        UdpClient udpClient = new UdpClient(port);

        try
        {
            Console.WriteLine("Listening for UDP messages...");

            // Aguarda por pacotes UDP
            IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, port);
            while (true)
            {
                byte[] receiveBytes = udpClient.Receive(ref remoteEP);
                string lldpMessage = Encoding.ASCII.GetString(receiveBytes);

                Console.WriteLine($"Received UDP message from {remoteEP.Address}: {lldpMessage}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            udpClient.Close();
        }
    }
}
