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

        public static void StartClient()
        {

            // Connect to a remote device.  
            try
            {

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public static string Send(FantacticsServer.Messages.Message message)
        {
            return Send(message.Serialize());
        }

        public static string Send(string data)
        {
            // Establish the remote endpoint for the socket.  
            // This example uses port 11000 on the local computer.  
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            _remoteEP = new IPEndPoint(ipAddress, 11000);

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

            DebugConsole.Output("Fantactics-Server", "Recieved Response From Server: " + response);

            return response;
        }
    }
}
