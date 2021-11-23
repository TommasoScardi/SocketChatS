using LibChatClient.User;
using System;

namespace LibChatClient.Protocol
{
    /// <summary>
    /// Insieme di tutte le costanti relative al protocollo SndMsg
    /// </summary>
    public struct SndMsg
    {
        /// <summary>
        /// Equivalente in stringa dell Enumerazione Protocols di SndMsg (7)
        /// </summary>
        public const string ProtocolString = "<SNDMSG>";

        public const int ProtocolDataLenght = 4;

        public const int UserID = 0;
        public const int ContactIDReceiver = 1;
        public const int MessageDateTime = 2;
        public const int MessageText = 3;

        public static string Assemble(int UserID, int ContactIDReceiver, DateTime MessageDateTime, string MessageText)
        {
            return $"{ProtocolString}{UserID}{Protocols.DELIM}{ContactIDReceiver}{Protocols.DELIM}{MessageDateTime.ToString("dd/MM/yyyy hh:mm:ss")}{Protocols.DELIM}{MessageText}{EOF.ProtocolString}";
        }
        public static string Assemble(Message msg)
        {
            return $"{ProtocolString}{msg.UserSenderID}{Protocols.DELIM}{msg.ContactReceiverID}{Protocols.DELIM}{msg.DateTimeSend.ToString("dd/MM/yyyy hh:mm:ss")}{Protocols.DELIM}{msg.Text}{EOF.ProtocolString}";
        }
    }
}
