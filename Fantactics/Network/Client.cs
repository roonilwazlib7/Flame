using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using Flame.Debug;

namespace Fantactics.Network
{
    class Client
    {
        private static Socket _sender;
        private static IPEndPoint _remoteEP;
        private static byte[] _bytes = new byte[1024]; // Data buffer for incoming data.
        private static string _hostName;
        private static int _port;
        private static int _connectionFailures = 0;
        
        public static void SetUpConnections(string hostName, int port)
        {
            if (hostName == null)
            {
                _hostName = Dns.GetHostName();
            }
            else
            {
                _hostName = hostName;
            }

            _port = port;

            // Establish the remote endpoint for the socket.  
            // This example uses port 11000 on the local computer.  
            IPHostEntry ipHostInfo = Dns.Resolve(_hostName);
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            _remoteEP = new IPEndPoint(ipAddress, _port);
        }  

        public static string Send(FantacticsServer.Messages.Message message)
        {
            try
            {
                return Send(message.Serialize());
            }
            catch (Exception e)
            {
                _connectionFailures++;
                DebugConsole.Output("Fantactics-Server", "Unable to connect to server");
                if (_connectionFailures >= 20)
                {
                    DebugConsole.Output("Fantactics-Server", "Giving up on server...");
                }
            }
            return "";
        }

        public static string Send(string data)
        {
            if (_connectionFailures >= 20) return "";
            // Create a TCP/IP  socket.  
            _sender = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);

            //connect
            _sender.Connect(_remoteEP);

            // Encode the data string into a byte array.  
            byte[] msg = Encoding.ASCII.GetBytes(data + "<EOF>");

            // Send the data through the socket.  
            int bytesSent = _sender.Send(msg);

            // Receive the response from the remote device.  
            int bytesRec = _sender.Receive(_bytes);
           

            // Release the socket.  
            _sender.Shutdown(SocketShutdown.Both);
            _sender.Close();

            //get the response
            string response = Encoding.ASCII.GetString(_bytes, 0, bytesRec).Replace("<EOF>", "");

            //DebugConsole.Output("Fantactics-Server", "Recieved Response From Server: " + response);

            return response;
        }
    }
}
