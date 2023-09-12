using System.Net.Sockets;
using System.Net;
using System.Text;

namespace SocketProgramming
{
    internal class SocketServer
    {
        public void ExecuteServer()
        {
            IPHostEntry iPHost = Dns.GetHostEntry(Dns.GetHostName(), AddressFamily.InterNetwork);
            IPAddress iPAddress;
            if (iPHost.AddressList.Length == 1) iPAddress = iPHost.AddressList[0];
            else iPAddress = Program.GetIPAddress(iPHost.AddressList);

            IPEndPoint iPEndPoint = new(iPAddress, 11111);

            Socket listener = new(SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(iPEndPoint);
            listener.Listen(1000);

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Listen for connection on " +listener.LocalEndPoint);
                Socket socket = listener.Accept();
                byte[] buffer = new byte[4096];
                string? data = null;

                while (true)
                {
                    int numByte = socket.Receive(buffer);
                    data += Encoding.ASCII.GetString(buffer, 0, numByte);
                    if (data.Contains("<EOM>")) break;
                }

                string messageToClient = "Roger over";
                byte[] bytes = Encoding.ASCII.GetBytes(messageToClient);
                socket.Send(bytes);

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(data);
            }
            
        }
    }
}
