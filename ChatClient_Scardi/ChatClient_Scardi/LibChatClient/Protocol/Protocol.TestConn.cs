using System.Net;

namespace LibChatClient.Protocol
{
    public struct TestConn
    {
        public const string ProtocolString = "<TESTCONN>";

        public const int ProtocolDataLenght = 1;

        public const int serverIp = 0;

        public static string Assemble(IPAddress serverIp)
        {
            return $"{ProtocolString}{serverIp}{EOF.ProtocolString}";
        }
    }
}
