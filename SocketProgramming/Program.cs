using System.Net;

namespace SocketProgramming
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            Thread serverThread = new Thread(ServerThread);
            serverThread.Start();

            Thread.Sleep(1000);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Start client? (Y/N)");
            while (Console.ReadKey().Key != ConsoleKey.Y) ;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Starting Client");
            SocketClient client = new SocketClient();
            client.ExecuteClient();
        }

        static void ServerThread(object o)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Starting Server");
            SocketServer server = new SocketServer();
            server.ExecuteServer();
        }

        public static IPAddress GetIPAddress(IPAddress[] ipList)
        {
           

            for (int i = 0; i < ipList.Length; i++)
            {
                Console.WriteLine($"{i} {ipList[i]}");
            }

            int result;
            while (!int.TryParse(Console.ReadLine(), out result)) ;
            return ipList[result];
        }
    }
}