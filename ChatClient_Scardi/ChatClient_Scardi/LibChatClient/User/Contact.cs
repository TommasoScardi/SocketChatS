using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibChatClient.User
{
    public class Contact
    {
        private int _contactID;
        private int _contactUserID;
        private string _contactUserName;
        private bool _contactsAdded;
        private Message[] _contactsMessages;

        public int ID { get { return _contactID; } }
        public int UserID { get { return _contactUserID; } }
        public string UserName { get { return _contactUserName; } }
        public bool Added { get { return _contactsAdded; } }
        public Message[] Messages { get { return _contactsMessages; } set {if (value != null) _contactsMessages = value; } }
        public Message this[int Index] { get { return _contactsMessages[Index]; } }

        public Contact(int ID, int UserID, string UserName, bool Added)
        {
            _contactID = ID;
            _contactUserID = UserID;
            _contactUserName = UserName;
            _contactsAdded = Added;
            _contactsMessages = null;
        }

        public Contact(int ID, int UserID, string UserName, bool Added, Message[] Messages)
        {
            _contactID = ID;
            _contactUserID = UserID;
            _contactUserName = UserName;
            _contactsAdded = Added;
            _contactsMessages = Messages;
        }
    }
}
