using System;
using LibChatServer.Protocol;

namespace LibChatServer
{
    public static partial class Protocols
    {
        const char DELIM = ',';//§

        public static string GetProtocolString(ProtocolsType protocol)
        {
            switch (protocol)
            {
                case ProtocolsType.EOS:
                    return EOS.ProtocolString;
                case ProtocolsType.EOF:
                    return EOF.ProtocolString;
                case ProtocolsType.TestConn:
                    return TestConn.ProtocolString;
                case ProtocolsType.Auth:
                    return Auth.ProtocolString;
                case ProtocolsType.DeAuth:
                    return DeAuth.ProtocolString;
                case ProtocolsType.ReqContacts:
                    return ReqContacts.ProtocolString;
                case ProtocolsType.NewUserReg:
                    return NewUserReg.ProtocolString;
                case ProtocolsType.LoadContact:
                    return LoadContact.ProtocolString;
                case ProtocolsType.NewContact:
                    return NewContact.ProtocolString;
                case ProtocolsType.ReqNewMsg:
                    return ReqNewMsg.ProtocolString;
                case ProtocolsType.SndMsg:
                    return SndMsg.ProtocolString;
                default:
                    return null;
            }

            //throw new ArgumentNullException(nameof(protocol), "The Protocols Enum was not declared");
        }

        public static ProtocolsType GetProtocolEnum(string protocol)
        {
            switch (protocol)
            {
                case EOS.ProtocolString:
                    return ProtocolsType.EOS;
                case EOF.ProtocolString:
                    return ProtocolsType.EOF;
                case TestConn.ProtocolString:
                    return ProtocolsType.TestConn;
                case Auth.ProtocolString:
                    return ProtocolsType.Auth;
                case DeAuth.ProtocolString:
                    return ProtocolsType.DeAuth;
                case NewUserReg.ProtocolString:
                    return ProtocolsType.NewUserReg;
                case ReqContacts.ProtocolString:
                    return ProtocolsType.ReqContacts;
                case LoadContact.ProtocolString:
                    return ProtocolsType.LoadContact;
                case NewContact.ProtocolString:
                    return ProtocolsType.NewContact;
                case ReqNewMsg.ProtocolString:
                    return ProtocolsType.ReqNewMsg;
                case SndMsg.ProtocolString:
                    return ProtocolsType.SndMsg;
                default:
                    return ProtocolsType.Null;
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

        public static string RemoveProtocolInString(string strProtocolData)
        {
            if (!SearchProtocolInString(strProtocolData, GetProtocolEnumInString(strProtocolData)) && !(SearchProtocolInString(strProtocolData, ProtocolsType.EOS) || SearchProtocolInString(strProtocolData, ProtocolsType.EOF)))
                return null;

            bool terminatorEOF = SearchProtocolInString(strProtocolData, ProtocolsType.EOF);

            string protocolData;
            protocolData = strProtocolData;
            ProtocolsType protocol = GetProtocolEnumInString(protocolData);
            protocolData = protocolData.Replace(GetProtocolString(protocol), string.Empty); //Rimozione Protocollo in testa
            protocolData = protocolData.Replace(GetProtocolString(terminatorEOF ? ProtocolsType.EOF : ProtocolsType.EOS), string.Empty); //Rimozione Protocollo EOF-EOS
            return protocolData;
        }

        /// <summary>
        /// Funzione che determina e rimuove il protocollo all inizio della stringa di dati e alla fine
        /// </summary>
        /// <param name="strProtocolData">Stringa contenente i dati e i protocolli</param>
        /// <returns>Stringa senza il protocollo iniziale e quello finale (EOF/EOS)</returns>
        public static ProtocolsType RemoveProtocolInString(string strProtocolData, ref string strData)
        {
            if (!SearchProtocolInString(strProtocolData, GetProtocolEnumInString(strProtocolData)) && !(SearchProtocolInString(strProtocolData, ProtocolsType.EOS) || SearchProtocolInString(strProtocolData, ProtocolsType.EOF)))
                return ProtocolsType.Null;

            bool terminatorEOF = SearchProtocolInString(strProtocolData, ProtocolsType.EOF);

            string protocolData;
            protocolData = strProtocolData;
            ProtocolsType protocol = GetProtocolEnumInString(protocolData);
            protocolData = protocolData.Replace(GetProtocolString(protocol), string.Empty); //Rimozione Protocollo in testa
            protocolData = protocolData.Replace(GetProtocolString(terminatorEOF ? ProtocolsType.EOF : ProtocolsType.EOS), string.Empty); //Rimozione Protocollo EOF-EOS
            strData = protocolData;
            return protocol;
        }

        public static ProtocolsType GetProtocolEnumInString(string strProtocolData)
        {
            int protocolIndex, protocolLength;
            string strProtocolSearched;

            protocolIndex = strProtocolData.IndexOf('<', 0);
            protocolLength = protocolIndex + strProtocolData.IndexOf('>', 0) + 1; //Il +1 serve a comprendere la parentesi angolare che chiude il protocollo

            if ((protocolIndex > 0 || protocolIndex < 0) && (protocolLength > strProtocolData.Length - 1 || protocolLength < 0))
                return ProtocolsType.Null;

            strProtocolSearched = strProtocolData.Substring(protocolIndex, protocolLength);
            return GetProtocolEnum(strProtocolSearched);
        }

        public static string[] GetProtocolDataInString(string stringData, int numData)
        {
            string protocolData = RemoveProtocolInString(stringData);
            return protocolData is not null ? protocolData.Split(DELIM, numData, StringSplitOptions.TrimEntries) : null;
        }

        public static ProtocolsType ParseDataProtocol(string protocolReceived, out string[] protocolData)
        {
            switch (GetProtocolEnumInString(protocolReceived))
            {
                case ProtocolsType.Null:
                    protocolData = null;
                    return ProtocolsType.Null;
                case ProtocolsType.Auth:
                    protocolData = GetProtocolDataInString(protocolReceived, Auth.ProtocolDataLenght);
                    return ProtocolsType.Auth;
                case ProtocolsType.DeAuth:
                    protocolData = GetProtocolDataInString(protocolReceived, DeAuth.ProtocolDataLenght);
                    return ProtocolsType.DeAuth;
                case ProtocolsType.NewUserReg:
                    protocolData = GetProtocolDataInString(protocolReceived, NewUserReg.ProtocolDataLenght);
                    return ProtocolsType.NewUserReg;
                case ProtocolsType.ReqContacts:
                    protocolData = GetProtocolDataInString(protocolReceived, ReqContacts.ProtocolDataLenght);
                    return ProtocolsType.ReqContacts;
                case ProtocolsType.LoadContact:
                    protocolData = GetProtocolDataInString(protocolReceived, LoadContact.ProtocolDataLenght);
                    return ProtocolsType.LoadContact;
                case ProtocolsType.NewContact:
                    protocolData = GetProtocolDataInString(protocolReceived, NewContact.ProtocolDataLenght);
                    return ProtocolsType.NewContact;
                case ProtocolsType.ReqNewMsg:
                    protocolData = GetProtocolDataInString(protocolReceived, ReqNewMsg.ProtocolDataLenght);
                    return ProtocolsType.ReqNewMsg;
                case ProtocolsType.SndMsg:
                    protocolData = GetProtocolDataInString(protocolReceived, SndMsg.ProtocolDataLenght);
                    return ProtocolsType.SndMsg;
                case ProtocolsType.TestConn:
                    protocolData = GetProtocolDataInString(protocolReceived, TestConn.ProtocolDataLenght);
                    return ProtocolsType.TestConn;
                default:
                    protocolData = null;
                    return ProtocolsType.Null;
            }
        }

        public static string AssembleExceptionAsError(string exceptionMessage)
        {
            return "<ERROR>" + exceptionMessage + EOF.ProtocolString;
        }
    }
}
