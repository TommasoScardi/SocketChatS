using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibChatClient.Connections
{
    internal class Connection
    {
        internal static string SendReceiveFromServer(IPAddress IpServer, string strToSend)
        {
            byte[] bytes = new byte[2048]; //500KB 0.5MB
            string incomingResponseServer = String.Empty;

            try
            {
                IPEndPoint remoteEP = new IPEndPoint(IpServer, 11000);
                Socket sender = new Socket(IpServer.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                sender.SendTimeout = 1000;
                sender.ReceiveTimeout = 2500;
                try
                {
                    sender.Connect(remoteEP);

                    //Console.WriteLine("Socket connected to {0}",
                    //    sender.RemoteEndPoint.ToString());

                    if (strToSend.IndexOf("<EOF>") < 0)
                        strToSend += "<EOF>";

                    byte[] msg = Encoding.Unicode.GetBytes(strToSend);
                    int bytesSent = sender.Send(msg);

                    while (true)
                    {
                        int bytesRec = sender.Receive(bytes);
                        incomingResponseServer += Encoding.Unicode.GetString(bytes, 0, bytesRec);
                        if (incomingResponseServer.IndexOf(Protocol.EOF.ProtocolString) > -1 || incomingResponseServer.IndexOf(Protocol.EOS.ProtocolString) > -1)
                        {
                            break;
                        }
                    }

                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();

                    bytes = null;
                    return incomingResponseServer;

                }
                catch (SocketException se)
                {
                    MessageBox.Show(se.Message, "Errore Socket Connessione Server", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Errore Invio Comando al Server", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Errore Connessione Server", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            bytes = null;
            return null;
        }

        internal static bool VerifyIpAddress(IPAddress iPAddressToVerify)
        {
            if (iPAddressToVerify is null)
                throw new ArgumentNullException("iPAddressToVerify", "L'IP Fornito era null");

            Ping verifyIP = new Ping();
            int timeout = 120;
            PingReply reply = verifyIP.Send(iPAddressToVerify, timeout);
            //PingReply reply = verifyIP.Send(IPAddress.Parse("1.1.1.1"), timeout); //Attenzione, questa stringa limita l'uso dei client a quelli connessi a internet

            if (reply.Status == IPStatus.Success)
                return true;
            else
                return false;
        }

        internal static IPAddress GetIPAddress()
        {
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress iPAddress in ipHostInfo.AddressList)
            {
                if (iPAddress.ToString().Count(c => c == '.') == 3)
                {
                    if (VerifyIpAddress(iPAddress))
                        return iPAddress;
                    else
                        continue;
                }
                else
                    continue;
            }
            return null;
        }
    }
}
