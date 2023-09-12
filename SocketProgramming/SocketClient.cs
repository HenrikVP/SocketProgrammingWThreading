using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketProgramming
{
    internal class SocketClient
    {
        public void ExecuteClient()
        {
            //string ip = "127.0.0.1";
            IPHostEntry iPHost = Dns.GetHostEntry(Dns.GetHostName(), AddressFamily.Unspecified);
            //iPHost.AddressList[0];
            IPAddress iPAddress = Program.GetIPAddress(iPHost.AddressList);
            IPEndPoint iPEndPoint = new(iPAddress, 11111);

            //We create a socket, that is used to connect to our server socket.
            //TODO Input IP address of server manually.
            Socket sender = new Socket(iPAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            sender.Connect(iPEndPoint);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Client connected to : {sender.RemoteEndPoint}");
            
            //Creates message and send it to server
            string message = "This is the message<EOM>";
            byte[] bytes = Encoding.ASCII.GetBytes(message);
            sender.Send(bytes);

            //Recieves answer from server
            byte[] bytesRecieved = new byte[4042];
            sender.Receive(bytesRecieved);

            string messageReceived = Encoding.ASCII.GetString(bytesRecieved);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(messageReceived);
        }
    }
}
