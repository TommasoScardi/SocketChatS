namespace LibChatServer.Protocol
{
    /// <summary>
    /// Insieme di tutte le costanti relative al protocollo LoadContact
    /// </summary>
    public struct LoadContact
    {
        /// <summary>
        /// Equivalente in stringa dell Enumerazione Protocols di LoadContact (4)
        /// </summary>
        public const string ProtocolString = "<LOADCONTACT>";

        public const int ProtocolDataLenght = 2;

        public const int UserID = 0;
        public const int ContactID = 1;
    }
}
