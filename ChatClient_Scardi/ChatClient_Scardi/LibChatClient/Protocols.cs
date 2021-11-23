using System;
using LibChatClient.Protocol;

namespace LibChatClient
{
    public static partial class Protocols
    {
        public const char DELIM = ',';//§

        public static string GetProtocolString(ProtocolsType protocol)
        {
            switch (protocol)
            {
                case ProtocolsType.EOS:
                    return EOS.ProtocolString;
                case ProtocolsType.EOF:
                    return EOF.ProtocolString;
                case ProtocolsType.OK:
                    return "<OK>";
                case ProtocolsType.Error:
                    return Error.ProtocolString;
                default:
                    return null;
            }
        }

        public static bool SearchProtocolInString(string message, ProtocolsType protocolSearched)
        {
            int protocolIndex, protocolLength;
            string strMessageProtocol, strProtocolSearched;

            strProtocolSearched = GetProtocolString(protocolSearched);
            if (strProtocolSearched is null)
                return false;

            protocolIndex = message.IndexOf(strProtocolSearched, 0, StringComparison.CurrentCultureIgnoreCase);
            if (protocolIndex < 0)
                return false;
            protocolLength = strProtocolSearched.Length;
            strMessageProtocol = message.Substring(protocolIndex, protocolLength);

            if (strMessageProtocol.Length == strProtocolSearched.Length)
                return true;
            else
                return false;
        }

        public static bool GetStatus(string message)
        {
            return SearchProtocolInString(message, ProtocolsType.OK);
        }

        public static bool VerifyServerError(string message)
        {
            return SearchProtocolInString(message, ProtocolsType.Error);
        }

        public static string RemoveTerminator(string message)
        {
            bool terminatorEOF = SearchProtocolInString(message, ProtocolsType.EOF);
            bool terminatorEOS = SearchProtocolInString(message, ProtocolsType.EOS);

            if (terminatorEOF || terminatorEOS)
            {
                return message.Substring(0, message.Length - (terminatorEOF ? EOF.ProtocolString.Length : EOS.ProtocolString.Length));
            }
            return null;
        }
        
        public static string RemoveErrorMark(string message)
        {
            if (SearchProtocolInString(message, ProtocolsType.Error))
            {
                return message.Substring(Error.ProtocolString.Length);
            }
            return null;
        }
    }
}
