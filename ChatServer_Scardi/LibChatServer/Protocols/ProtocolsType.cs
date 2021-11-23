namespace LibChatServer
{
    /// <summary>
    /// Enumerazione dei vari protocolli
    /// </summary>
    public enum ProtocolsType : int
    {
        /// <summary>
        /// Protocollo nullo o assenza di protocollo in una stringa (-1)
        /// </summary>
        Null = -1,
        /// <summary>
        /// End Of Stream: Fine della trasmissione, un alternativa puo essere EOF(99) (98)
        /// </summary>
        EOS = 98,
        /// <summary>
        /// End Of File: Fine del file, un alternativa puo essere EOS(98) (99)
        /// </summary>
        EOF = 99,
        TestConn = 100,
        /// <summary>
        /// Protocollo di Autenticazione
        /// </summary>
        Auth = 1,
        /// <summary>
        /// Protocollo di DeAutenticazione o Log-Out
        /// </summary>
        DeAuth,
        /// <summary>
        /// Protocollo di Nuova Registrazione Utente
        /// </summary>
        NewUserReg,
        /// <summary>
        /// Protocollo di richiesta contatti
        /// </summary>
        ReqContacts,
        /// <summary>
        /// Protocollo di caricamento contatti
        /// </summary>
        LoadContact,
        /// <summary>
        /// Protocollo di creazione nuovo contatto nella casella dei contatti
        /// </summary>
        NewContact,
        /// <summary>
        /// Protocollo di Richiesta dei Messaggi
        /// </summary>
        ReqNewMsg,
        /// <summary>
        /// Protocollo per l'invio dei messaggi
        /// </summary>
        SndMsg
    }
}
