using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibChatClient.User
{
    public class Message
    {
        private int _messageID;
        private int _messageUserSenderID;
        private int _messageContactReceiverID;
        private DateTime _messageDateTimeSend;
        private string _messageText;

        public int ID { get { return _messageID; } }
        public int UserSenderID { get { return _messageUserSenderID; } }
        public int ContactReceiverID { get { return _messageContactReceiverID; } }
        public DateTime DateTimeSend { get { return _messageDateTimeSend; } }
        public string Text { get { return _messageText; } }

        internal Message(int ID, int UserSenderID, int ContactReceiverID, DateTime DateTimeSend, string Text)
        {
            _messageID = ID;
            _messageUserSenderID = UserSenderID;
            _messageContactReceiverID = ContactReceiverID;
            _messageDateTimeSend = DateTimeSend;
            _messageText = Text;
        }
    }
}
