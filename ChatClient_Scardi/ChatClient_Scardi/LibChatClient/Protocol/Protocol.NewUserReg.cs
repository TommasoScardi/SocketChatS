using System.Net;

namespace LibChatClient.Protocol
{
    /// <summary>
    /// Insieme di tutte le costanti relative al protocollo NewUserReg
    /// </summary>
    public struct NewUserReg
    {
        /// <summary>
        /// Equivalente in stringa dell Enumerazione Protocols di NewUserReg (2)
        /// </summary>
        public const string ProtocolString = "<NEWUSERREG>";

        public const int ProtocolDataLenght = 3;

        public const int UserName = 0;
        public const int Password = 1;
        public const int IpUtente = 2;

        public static string Assemble(string UserName, string Password, IPAddress IpUtente)
        {
            return $"{ProtocolString}{UserName}{Protocols.DELIM}{Password}{Protocols.DELIM}{IpUtente}{EOF.ProtocolString}";
        }
    }
}
