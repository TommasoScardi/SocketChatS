namespace LibChatServer.Protocol
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
    }
}
