using System.Net.Sockets;
using System.Net;
using System.Text;

namespace SocketProgramming
{
    internal class SocketServer
    {
        public void ExecuteServer()
        {
            //In order to get the server started, we need a IP.
            //This can be wound in our IPHostEntry that shows our network interfaces
            //and the respective IP addresses.
            IPHostEntry iPHost = Dns.GetHostEntry(Dns.GetHostName(), AddressFamily.InterNetwork);
            IPAddress iPAddress;
            if (iPHost.AddressList.Length == 1) iPAddress = iPHost.AddressList[0];
            else iPAddress = Program.GetIPAddress(iPHost.AddressList);

            IPEndPoint iPEndPoint = new(iPAddress, 11111);

            //We create a listener socket, whos only function is to accept connections to
            //a dedicated socket for that connection.
            Socket listener = new(SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(iPEndPoint);
            listener.Listen(1000);

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Listen for connection on " +listener.LocalEndPoint);
                
                //When we have a connection a new socket is created, that will 
                //act as the endpoint for the client endpoint. (Peer to peer)
                Socket socket = listener.Accept();
                byte[] buffer = new byte[4096];
                string? data = null;

                //With our buffer we recieve and translate our BYTE data to ASCII code
                //ASCII characters are up to 1/4 the size of Unicode character,
                //but doesnt contain national letters.
                //The loop continues until we recieve the <EOM> tag.
                while (true)
                {
                    int numByte = socket.Receive(buffer);
                    data += Encoding.ASCII.GetString(buffer, 0, numByte);
                    if (data.Contains("<EOM>")) break;
                }

                //We send back a message to the client, confirming everything went ok
                string messageToClient = "Roger over";
                byte[] bytes = Encoding.ASCII.GetBytes(messageToClient);
                socket.Send(bytes);

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(data);
            }
        }
    }
}
