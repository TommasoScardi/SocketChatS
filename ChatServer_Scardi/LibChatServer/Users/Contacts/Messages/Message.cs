using System;

namespace LibChatServer
{
    public class Message
    {
        private int _messageID;
        private int _messageUserSenderID;
        private int _messageContactReceiverID;
        private DateTime _messageDateTimeSend;
        private string _messageText;

        public int MessageID { get { return _messageID; } }

        //internal int SetMessageID { set {
        //        if (value <= 0)
        //            throw new ArgumentOutOfRangeException(nameof(value), "L'id del messaggio che si sta tentando di settare non può essere inferiore a 0 o uguale");
        //        _messageID = value;
        //    } }

        /// <summary>
        /// Flag per rappresentare se il messaggio è stato INVIATO -> 1 o RICEVUTO -> 0
        /// </summary>
        public int MessageUserSenderID { get { return _messageUserSenderID; } }
        public int MessageContactReceiverID { get { return _messageContactReceiverID; } }
        public DateTime MessageDateTimeSend { get { return _messageDateTimeSend; } }
        public string MessageText { get { return _messageText; } }

        /// <summary>
        /// Costruttore DI UN NUOVO Messaggio da CREARE sul DB,
        /// Usare solo in quel caso
        /// </summary>
        /// <param name="MessageSend">FLAG per stabilire se il MSG è stato inviato o ricevuto</param>
        /// <param name="MessageContactReceiverID">ID dell utente a cui spedire il messaggio</param>
        /// <param name="MessageDateTimeSend">DATETIME invio messaggio</param>
        /// <param name="MessageText">Testo Messaggio Inviato</param>//Costruttore di TESTING
        //Il modificatore di visibilità dovrebbe essere impostato su INTERNAL dopo il testing
        internal Message(int MessageUserSenderID, int MessageContactReceiverID, DateTime MessageDateTimeSend, string MessageText)
        {
            _messageID = -1;
            _messageUserSenderID = MessageUserSenderID;
            _messageContactReceiverID = MessageContactReceiverID;
            _messageDateTimeSend = MessageDateTimeSend;
            _messageText = MessageText;
        }

        public Message(User user, Contact contact, string MessageText)
        {
            if (!ServerDB.UserExist(user))
                throw new Exception(ErrorType.ToString(ErrorsType.AuthUserInexistent)); //"The user requesting contacts is inexistent, register a new user to continue"
            if (!ServerDB.ContactUserExist(user, contact))
                throw new Exception(ErrorType.ToString(ErrorsType.LoadContactInexistent)); //"The contact abbined with this message was not found"

            _messageID = -1;
            _messageUserSenderID = user.UserID;
            _messageContactReceiverID = contact.ContactUserID;
            _messageDateTimeSend = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            _messageText = MessageText;
        }

        public Message(User user, Contact contact, DateTime MessageDateTimeSend, string MessageText)
        {
            if (!ServerDB.UserExist(user))
                throw new Exception(ErrorType.ToString(ErrorsType.AuthUserInexistent)); //"The user requesting contacts is inexistent, register a new user to continue"
            if (!ServerDB.ContactUserExist(user, contact))
                throw new Exception(ErrorType.ToString(ErrorsType.LoadContactInexistent)); //"The contact abbined with this message was not found"

            _messageID = -1;
            _messageUserSenderID = user.UserID;
            _messageContactReceiverID = contact.ContactUserID;
            _messageDateTimeSend = MessageDateTimeSend;
            _messageText = MessageText;
        }

        internal Message(int MessageID, int MessageUserSenderID, int MessageContactReceiverID, DateTime MessageDateTimeSend, string MessageText, bool verifyContactUserExist)
        {
            if (ServerDB.UserExist(MessageContactReceiverID) is null)
                throw new Exception(ErrorType.ToString(ErrorsType.AuthUserInexistent)); //"The user requesting contacts is inexistent, register a new user to continue"
            if (verifyContactUserExist)
                if (!ServerDB.ContactUserExist(MessageUserSenderID, MessageContactReceiverID, ServerDB.GetUserName(MessageContactReceiverID)))
                    throw new Exception(ErrorType.ToString(ErrorsType.LoadContactInexistent)); //"The contact abbined with this message was not found"
            _messageID = MessageID;
            _messageUserSenderID = MessageUserSenderID;
            _messageContactReceiverID = MessageContactReceiverID;
            _messageDateTimeSend = MessageDateTimeSend;
            _messageText = MessageText;
        }

        /// <summary>
        /// Returna una stringa che rappresenta il Messaggio e i dati associati
        /// </summary>
        /// <returns>Formato: Message {MessageID}: {MessageText} (Info Message: From: {MessageContactReceiverID} DateTime Send: {MessageDateTimeSend} Send/Received: {MessageFlagSend}</returns>
        public override string ToString()
        {
            return $"Messaggio {_messageID}: Inviato IL: {_messageDateTimeSend} DA: {_messageUserSenderID} A: {_messageContactReceiverID} - {_messageText}";
        }

        ~Message()
        {
            _messageID = -1;
            _messageUserSenderID = -1;
            _messageContactReceiverID = -1;
            _messageDateTimeSend = DateTime.MinValue;
            _messageText = null;
        }
    }
}
