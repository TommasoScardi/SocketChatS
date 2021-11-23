using System;
using System.Xml;

namespace LibChatServer
{
    public static class XmlAssembler
    {
        public static string Contacts(Contacts contacts)
        {
            XmlDocument xmlAssembler = new XmlDocument();

            XmlDeclaration docDeclaration = xmlAssembler.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement docRoot = xmlAssembler.DocumentElement;
            xmlAssembler.InsertBefore(docDeclaration, docRoot);

            XmlElement docContacts = xmlAssembler.CreateElement("Contacts");
            xmlAssembler.AppendChild(docContacts);

            for (int i = 0; i < contacts.GetContact.Length; i++)
            {
                XmlElement docContact = xmlAssembler.CreateElement("Contact");
                docContact.SetAttribute("ContactID", contacts.GetContact[i].ContactID.ToString());
                docContact.SetAttribute("ContactUserID", contacts.GetContact[i].ContactUserID.ToString());
                docContact.SetAttribute("ContactUserName", contacts.GetContact[i].ContactUserName.ToString());
                docContact.SetAttribute("ContactAdded", contacts.GetContact[i].ContactsAdded.ToString());
                docContacts.AppendChild(docContact);
            }

            return xmlAssembler.OuterXml;
        }

        public static string Messages(Contact contact)
        {
            XmlDocument xmlAssembler = new XmlDocument();

            XmlDeclaration docDeclaration = xmlAssembler.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement docRoot = xmlAssembler.DocumentElement;
            xmlAssembler.InsertBefore(docDeclaration, docRoot);

            XmlElement docMessages = xmlAssembler.CreateElement("Messages");
            xmlAssembler.AppendChild(docMessages);

            for (int i = 0; i < contact.ContactMessages.GetMessages.Length; i++)
            {
                XmlElement docMessage = xmlAssembler.CreateElement("Message");
                docMessage.SetAttribute("MessageID", contact.ContactMessages[i].MessageID.ToString());
                docMessage.SetAttribute("MessageUserSenderID", contact.ContactMessages[i].MessageUserSenderID.ToString());
                docMessage.SetAttribute("MessageContactReceiverID", contact.ContactMessages[i].MessageContactReceiverID.ToString());
                docMessage.SetAttribute("MessageDateTimeSend", contact.ContactMessages[i].MessageDateTimeSend.ToString("dd/MM/yyyy hh:mm:ss"));

                XmlElement docText = xmlAssembler.CreateElement("MessageText");
                XmlText docMessageText = xmlAssembler.CreateTextNode(contact.ContactMessages[i].MessageText);
                docText.AppendChild(docMessageText);
                docMessage.AppendChild(docText);
                docMessages.AppendChild(docMessage);
            }

            return xmlAssembler.OuterXml;
        }
        
        public static string Messages(Message[] messages)
        {
            XmlDocument xmlAssembler = new XmlDocument();

            XmlDeclaration docDeclaration = xmlAssembler.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement docRoot = xmlAssembler.DocumentElement;
            xmlAssembler.InsertBefore(docDeclaration, docRoot);

            XmlElement docMessages = xmlAssembler.CreateElement("Messages");
            xmlAssembler.AppendChild(docMessages);

            for (int i = 0; i < messages.Length; i++)
            {
                XmlElement docMessage = xmlAssembler.CreateElement("Message");
                docMessage.SetAttribute("MessageID", messages[i].MessageID.ToString());
                docMessage.SetAttribute("MessageUserSenderID", messages[i].MessageUserSenderID.ToString());
                docMessage.SetAttribute("MessageContactReceiverID", messages[i].MessageContactReceiverID.ToString());
                docMessage.SetAttribute("MessageDateTimeSend", messages[i].MessageDateTimeSend.ToString("dd/MM/yyyy hh:mm:ss"));

                XmlElement docText = xmlAssembler.CreateElement("MessageText");
                XmlText docMessageText = xmlAssembler.CreateTextNode(messages[i].MessageText);
                docText.AppendChild(docMessageText);
                docMessage.AppendChild(docText);
                docMessages.AppendChild(docMessage);
            }

            return xmlAssembler.OuterXml;
        }
    }
}
