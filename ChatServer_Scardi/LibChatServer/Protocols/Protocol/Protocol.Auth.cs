namespace LibChatServer.Protocol
{
    /// <summary>
    /// Insieme di tutte le costanti relative al protocollo Auth
    /// </summary>
    public struct Auth
    {
        /// <summary>
        /// Equivalente in stringa dell Enumerazione Protocols di Auth (1)
        /// </summary>
        public const string ProtocolString = "<AUTH>";

        public const int ProtocolDataLenght = 3;

        public const int UserName = 0;
        public const int Password = 1;
        public const int IpUtente = 2;
    }
}
