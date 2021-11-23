using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using LibChatClient.User;

namespace LibChatClient
{
    public static class XmlParser
    {
        public static Contact[] ParseContacts(string toParseXml)
        {
            List<Contact> contacts = new List<Contact>();
            XmlDocument xmlReader = new XmlDocument();
            xmlReader.LoadXml(toParseXml);

            XmlNode xmlElem = xmlReader.DocumentElement.SelectSingleNode("/Contacts");
            foreach (XmlNode xmlNode in xmlElem.ChildNodes)
            {
                contacts.Add(new Contact(int.Parse(xmlNode.Attributes["ContactID"].InnerText), int.Parse(xmlNode.Attributes["ContactUserID"].InnerText), xmlNode.Attributes["ContactUserName"].InnerText, bool.Parse(xmlNode.Attributes["ContactAdded"].InnerText)));
            }
            return contacts.ToArray();
        }

        public static Message[] ParseMessages(string toParseXml)
        {
            List<Message> messages = new List<Message>();
            XmlDocument xmlReader = new XmlDocument();
            xmlReader.LoadXml(toParseXml);

            XmlNode xmlElem = xmlReader.DocumentElement.SelectSingleNode("/Messages");
            foreach (XmlNode xmlNode in xmlElem.ChildNodes)
            {
                messages.Add(new Message(int.Parse(xmlNode.Attributes["MessageID"].InnerText), int.Parse(xmlNode.Attributes["MessageUserSenderID"].InnerText), int.Parse(xmlNode.Attributes["MessageContactReceiverID"].InnerText), DateTime.Parse(xmlNode.Attributes["MessageDateTimeSend"].InnerText), xmlNode.FirstChild.InnerText));
            }
            return messages.ToArray();
        }

        public static bool ParseMessages(ref Contact messageContact, string toParseXml)
        {
            if (messageContact != null)
            {
                if (messageContact.ID > 0)
                {
                    List<Message> messages = new List<Message>();
                    XmlDocument xmlReader = new XmlDocument();
                    xmlReader.LoadXml(toParseXml);

                    XmlNode xmlElem = xmlReader.DocumentElement.SelectSingleNode("/Messages");
                    foreach (XmlNode xmlNode in xmlElem.ChildNodes)
                    {
                        messages.Add(new Message(int.Parse(xmlNode.Attributes["MessageID"].InnerText), int.Parse(xmlNode.Attributes["MessageUserSenderID"].InnerText), int.Parse(xmlNode.Attributes["MessageContactReceiverID"].InnerText), DateTime.Parse(xmlNode.Attributes["MessageDateTimeSend"].InnerText), xmlNode.FirstChild.InnerText));
                    }
                    messageContact.Messages = messages.ToArray();
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }
    }
}
