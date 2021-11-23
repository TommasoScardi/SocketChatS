namespace LibChatClient.Protocol
{
    /// <summary>
    /// Insieme di tutte le costanti relative al protocollo ReqContacts
    /// </summary>
    public struct ReqContacts
    {
        /// <summary>
        /// Equivalente in stringa dell Enumerazione Protocols di ReqContacts (3)
        /// </summary>
        public const string ProtocolString = "<REQCONTACTS>";

        public const int ProtocolDataLenght = 1;

        public const int UserID = 0;

        public static string Assemble(int UserID)
        {
            return $"{ProtocolString}{UserID}{EOF.ProtocolString}";
        }
    }
}
