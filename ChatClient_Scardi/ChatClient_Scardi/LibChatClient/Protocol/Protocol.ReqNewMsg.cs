using System;

namespace LibChatClient.Protocol
{
    /// <summary>
    /// Insieme di tutte le costanti relative al protocollo ReqNewMsg
    /// </summary>
    public struct ReqNewMsg
    {
        /// <summary>
        /// Equivalente in stringa dell Enumerazione Protocols di ReqNewMsg (6)
        /// </summary>
        public const string ProtocolString = "<REQNEWMSG>";

        public const int ProtocolDataLenght = 3;

        public const int UserID = 0;
        public const int ContactID = 1;
        public const int DTLastMessageReceived = 2;

        public static string Assemble(int UserID, int ContactID, DateTime DTLastMessageReceived)
        {
            return $"{ProtocolString}{UserID}{Protocols.DELIM}{ContactID}{Protocols.DELIM}{DTLastMessageReceived.ToString("dd/MM/yyyy hh:mm:ss")}{EOF.ProtocolString}";
        }
    }
}
