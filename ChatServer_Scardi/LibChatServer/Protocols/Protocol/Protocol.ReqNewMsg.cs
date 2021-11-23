namespace LibChatServer.Protocol
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
    }
}
