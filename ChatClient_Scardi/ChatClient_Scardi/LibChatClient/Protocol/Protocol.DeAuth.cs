namespace LibChatClient.Protocol
{
    /// <summary>
    /// Insieme di tutte le costanti relative al protocollo Auth
    /// </summary>
    public struct DeAuth
    {
        /// <summary>
        /// Equivalente in stringa dell Enumerazione Protocols di Auth (1)
        /// </summary>
        public const string ProtocolString = "<DEAUTH>";

        public const int ProtocolDataLenght = 1;

        public const int UserID = 0;

        public static string Assemble(int UserID)
        {
            return $"{ProtocolString}{UserID}{EOF.ProtocolString}";
        }
    }
}
