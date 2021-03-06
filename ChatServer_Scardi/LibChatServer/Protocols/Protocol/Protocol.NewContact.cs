namespace LibChatServer.Protocol
{
    /// <summary>
    /// Insieme di tutte le costanti relative al protocollo NewContact
    /// </summary>
    public struct NewContact
    {
        /// <summary>
        /// Equivalente in stringa dell Enumerazione Protocols di NewContact (5)
        /// </summary>
        public const string ProtocolString = "<NEWCONTACT>";

        public const int ProtocolDataLenght = 2;

        public const int UserID = 0;
        public const int UserContactName = 1;
    }
}
