using System;

namespace LibChatServer
{
    public class Messages
    {
        private Message[] _messages;

        public Message this[int index]
        {
            get
            {
                return _messages[index];
            }
        }
        
        public Message[] GetMessages { get { return _messages; } }

        public Messages(User user, Contact contact)
        {
            if (user is null)
                throw new Exception(ErrorType.ToString(ErrorsType.AuthUserInexistent)); //"The user requesting contacts is inexistent, register a new user to continue"
            if (contact is null)
                throw new Exception(ErrorType.ToString(ErrorsType.LoadContactInexistent)); //"The contact that are associated the message is null"

            _messages = ServerDB.GetUserMessages(user, contact);
        }

        internal Messages(Message[] messages)
        {
            _messages = messages;
        }

        public Message GetMessageByID(int MessageID)
        {
            int indexTop, indexBot, indexMed;
            indexTop = 0;
            indexBot = _messages.Length - 1;
            do
            {
                indexMed = (int)((double)indexTop + (double)indexBot / 2.0);
                if (MessageID == _messages[indexMed].MessageID)
                {
                    return _messages[indexMed];
                }
                else if (MessageID < _messages[indexMed].MessageID)
                {
                    indexBot = indexMed - 1;
                }
                else if (MessageID > _messages[indexMed].MessageID)
                {
                    indexTop = indexMed + 1;
                }
            } while (indexTop <= indexBot);
            return null;
        }

        ~Messages()
        {
            _messages = null;
        }
    }
}
