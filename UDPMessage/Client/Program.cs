using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class UDPMessageClient
{
    static void Main()
    {
        var port = 45123;
        // Endereço IP do servidor (substitua pelo IP do servidor real)
        var serverIP = "127.0.0.1";
        string udpMessage = "UDP Message Example";

        // Converte a mensagem em bytes
        byte[] sendBytes = Encoding.ASCII.GetBytes(udpMessage);

        // Cria um socket UDP
        UdpClient udpClient = new UdpClient();

        try
        {
            // Define o destino para o servidor
            IPEndPoint serverEP = new IPEndPoint(IPAddress.Parse(serverIP), port);

            // Envia a mensagem LLDP para o servidor
            udpClient.Send(sendBytes, sendBytes.Length, serverEP);

            Console.WriteLine($"UDP message sent to {serverEP.Address}");
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
