using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using Flame.Debug;

namespace FantacticsServer
{
    class Server
    {
        // Incoming data from the client.  
        public static string data = null;
        private static GameSession _session;

        public static void RestartServer()
        {
            DebugConsole.Output("Server", "Restarting...");
            _session = new GameSession();
        }

        public static void StartListening(string hostName, int port)
        {
            DebugConsole.AddChannel("Server", ConsoleColor.Green, ConsoleColor.Black);
            DebugConsole.AddChannel("Client", ConsoleColor.Red, ConsoleColor.Black);
            DebugConsole.ReleaseCancelKey();
            DebugConsole.Mirror = false;
            Console.CancelKeyPress += Console_CancelKeyPress;

            _session = new GameSession();
            // Data buffer for incoming data.  
            byte[] bytes = new byte[1024];

            // Establish the local endpoint for the socket.  
            // Dns.GetHostName returns the name of the   
            // host running the application.  
            IPHostEntry ipHostInfo = Dns.Resolve(hostName);
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, port);

            // Create a TCP/IP socket.  
            Socket listener = new Socket(AddressFamily.InterNetwork,SocketType.Stream, ProtocolType.Tcp);

            DebugConsole.Output("Server", string.Format("Starting Fantactics Server at {0}({2}) on port {1}...", hostName, port, ipAddress.ToString()));

            // Bind the socket to the local endpoint and   
            // listen for incoming connections.  
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(10);

                DebugConsole.Output("Server", "Socket bound...");

                // Start listening for connections.  
                while (true)
                {
                    //DebugConsole.Output("Server", "waiting...");
                    // Program is suspended while waiting for an incoming connection.  
                    Socket handler = listener.Accept();
                    data = null;

                    //DebugConsole.Output("Server", string.Format("Data recieved at {0} from {1}", DateTime.Now, "somewhere"));

                    // An incoming connection needs to be processed.  
                    while (true)
                    {
                        bytes = new byte[1024];
                        int bytesRec = handler.Receive(bytes);
                        data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                        if (data.IndexOf("<EOF>") > -1)
                        {
                            break;
                        }
                    }

                    // Show the data on the console.  
                    //Console.WriteLine("Text received : {0}", data);

                    string echoBack = _session.HandleMessage(data.Replace("<EOF>", ""));

                    // Echo the data back to the client.  
                    byte[] msg = Encoding.ASCII.GetBytes(echoBack + "<EOF>");

                    handler.Send(msg);
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("\nPress ENTER to continue...");
            Console.Read();

        }

        private static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            e.Cancel = true;
            RestartServer();
        }
    }
}
