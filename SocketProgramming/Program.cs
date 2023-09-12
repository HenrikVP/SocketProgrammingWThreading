using System.Net;

namespace SocketProgramming
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            //We start making a seperate thread for our server
            //so it doesnt interfere with our client
            Thread serverThread = new Thread(ServerThread);
            serverThread.Start();

            //We let our main thread sleep a bit, and ask for
            //acceptance to continue our main thread, the client.
            Thread.Sleep(1000);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Start client? (Y/N)");
            while (Console.ReadKey().Key != ConsoleKey.Y) ;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Starting Client");
            SocketClient client = new SocketClient();
            client.ExecuteClient();
        }

        /// <summary>
        /// We run the Serverthread method as a delegate when we start
        /// the thread. This method will instantiate our server object,
        /// and run the ExecuteServer.
        /// </summary>
        /// <param name="o"></param>
        static void ServerThread(object o)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Starting Server");
            SocketServer server = new SocketServer();
            server.ExecuteServer();
        }

        /// <summary>
        /// In case we have multiple network interfaces,
        /// we can select from a list of IPs
        /// </summary>
        /// <param name="ipList"></param>
        /// <returns></returns>
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