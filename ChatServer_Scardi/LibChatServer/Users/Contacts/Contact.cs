using System;

namespace LibChatServer
{
    public class Contact
    {
        private int _contactID;
        private int _contactUserID;
        private string _contactUserName;
        private bool _contactsAdded;
        private Messages _contactMessages;

        public int ContactID { get { return _contactID; } }
        public int ContactUserID { get { return _contactUserID; } }
        public string ContactUserName { get { return _contactUserName; } }
        public bool ContactsAdded { get { return _contactsAdded; } }
        public Messages ContactMessages { get { return _contactMessages; } }
        internal Messages SetContactMessages { set { _contactMessages = value; } }

        /// <summary>
        /// Costruttore DI UN NUOVO Contatto da CREARE sul DB,
        /// Usare solo in quel caso
        /// </summary>
        /// <param name="ContactUserID">ID dell utente da aggiungere nella tabella Contact</param>
        /// <param name="ContactUserName">NomeUtente del contatto da aggiungere nella tabella User</param>
        //Costruttore di TESTING
        //Il modificatore di visibilità dovrebbe essere impostato su INTERNAL dopo il testing
        internal Contact(int ContactUserID, string ContactUserName, bool ContactsAdded)
        {
            _contactID = -1;
            _contactUserID = ContactUserID;
            _contactUserName = ContactUserName;
            _contactsAdded = ContactsAdded;
        }

        public Contact(User user)
        {
            if (!ServerDB.UserExist(user))
                throw new Exception(ErrorType.ToString(ErrorsType.AuthUserInexistent)); //"The user requesting contacts is inexistent, register a new user to continue"

            _contactID = -1;
            _contactUserID = user.UserID;
            _contactUserName = user.UserName;
            //ServerDB.AddNewContactToUser(user, this);
        }

        public Contact(string ContactName)
        {
            User contactUser = ServerDB.UserExist(ContactName);
            if (contactUser is null)
                throw new Exception(ErrorType.ToString(ErrorsType.AuthUserInexistent)); //"Il contatto che si sta tentando di aggiungere non esiste"

            _contactID = -1;
            _contactUserID = contactUser.UserID;
            _contactUserName = contactUser.UserName;
        }

        //Costruttore di TESTING
        //Il modificatore di visibilità dovrebbe essere impostato su INTERNAL dopo il testing
        internal Contact(int ContactID, int ContactUserID, string ContactUserName, bool ContactsAdded)
        {
            //VERIFICA GIA FATTA ALLA CREAZIONE DEL CONTATTO
            //if (ServerDB.UserExist(ContactID) is null)
            //    throw new Exception("The user requesting contacts is inexistent, register a new user to continue");

            _contactID = ContactID;
            _contactUserID = ContactUserID;
            _contactUserName = ContactUserName;
            _contactsAdded = ContactsAdded;
        }

        public void UpdateMessages(User user)
        {
            if (_contactID > 0)
                _contactMessages = new Messages(user, this);
        }

        public Message[] GetMessagesFromDT(User user, DateTime newMsgFromDateTime)
        {
            return ServerDB.GetUserMessages(user, this, newMsgFromDateTime);
        }

        public void SendNewMsg(User user, Message message)
        {
            if (!ServerDB.SendNewMessageToContact(user, this, message))
                throw new Exception(ErrorType.ToString(ErrorsType.SndMsgUnableToSend)); //$"Unable to send a New Message to {this}"
            UpdateMessages(user);
        }

        /// <summary>
        /// Returna una stringa che rappresenta il contatto
        /// </summary>
        /// <returns>Formato: Contact {ContactID}: {ContactUserID} - {ContactUserName}</returns>
        public override string ToString()
        {
            return $"Contact {_contactID}: {_contactUserID} - {_contactUserName} - Lo si è aggiunto {_contactsAdded}/ è stato aggiunto {!_contactsAdded}";
        }

        ~Contact()
        {
            _contactID = -1;
            _contactUserID = -1;
            _contactUserName = null;
            _contactsAdded = false;
            _contactMessages = null;
        }

    }
}
