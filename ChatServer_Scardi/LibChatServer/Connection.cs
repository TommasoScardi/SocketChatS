using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;

namespace LibChatServer
{
    public class Connection
    {

        // Incoming data from the client.  
        public static string data = null;

        public static void StartListening()
        {
            // Data buffer for incoming data.  
            byte[] bytes = new byte[1024];

            // Establish the local endpoint for the socket.  
            // Dns.GetHostName returns the name of the
            // host running the application.  
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

            // Create a TCP/IP socket.  
            Socket listener = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and
            // listen for incoming connections.  
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(10);

                // Start listening for connections.  
                while (true)
                {
                    Console.WriteLine("Waiting for a connection...");
                    // Program is suspended while waiting for an incoming connection.  
                    Socket handler = listener.Accept();
                    data = null;

                    // An incoming connection needs to be processed.  
                    while (true)
                    {
                        int bytesRec = handler.Receive(bytes);
                        data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                        if (data.IndexOf("<EOF>") > -1)
                        {
                            break;
                        }
                    }

                    // Show the data on the console.  
                    Console.WriteLine("Text received : {0}", data);

                    // Echo the data back to the client.  
                    byte[] msg = Encoding.ASCII.GetBytes(data);

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

        public static bool VerifyIpAddress(IPAddress iPAddressToVerify)
        {
            if (iPAddressToVerify is null)
                throw new ArgumentNullException("iPAddressToVerify", "L'IP Fornito era null");

            Ping verifyIP = new Ping();
            int timeout = 120;
            PingReply reply = verifyIP.Send(iPAddressToVerify, timeout);

            if (reply.Status == IPStatus.Success)
                return true;
            else
                return false;
        }

        public static IPAddress GetIPAddress()
        {
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress iPAddress in ipHostInfo.AddressList)
            {
                if (VerifyIpAddress(iPAddress))
                    return iPAddress;
                else
                    continue;
            }

            return null;
        }
    }
}
