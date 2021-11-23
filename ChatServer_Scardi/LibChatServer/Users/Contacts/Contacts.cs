using System;
using System.Collections.Generic;

namespace LibChatServer
{
    //Espansione della classe a IEnumerator e a IEnumerable dovuta all implementazione del FOREACH nella classe Contacts
    public class Contacts
    {
        private Contact[] _contacts;

        /// <summary>
        /// Cerca All interno dell Array di Contatti il contatto con l'ID Specificato
        /// </summary>
        /// <param name="ContactID">L'ID del Contatto da Cercare</param>
        /// <returns>Il contatto cercato o se non è stato trovato nessun contatto con l'ID fornito null</returns>
        public Contact this[int ContactID]
        {
            get //L'ordine dell array corrisponde alla colonna ContactsUserID
            {
                int indexTop, indexBot, indexMed;
                indexTop = 0;
                indexBot = _contacts.Length - 1;
                do
                {
                    indexMed = (indexTop + indexBot) / 2;
                    if (ContactID == _contacts[indexMed].ContactID)
                    {
                        return _contacts[indexMed];
                    }
                    else if (ContactID < _contacts[indexMed].ContactID)
                    {
                        indexBot = indexMed - 1;
                    }
                    else if (ContactID > _contacts[indexMed].ContactID)
                    {
                        indexTop = indexMed + 1;
                    }
                } while (indexTop <= indexBot);
                return null;
            }
        }

        public Contact[] GetContact { get { return _contacts; } }


        public Contacts(User user)
        {
            if (user is null)
                throw new Exception(ErrorType.ToString(ErrorsType.AuthUserInexistent)); //"The user requesting contacts is inexistent, register a new user to continue"

            _contacts = ServerDB.GetUserContacts(user); //inserisce contatti nell array di contact
            if (_contacts is not null && _contacts.Length > 0) //Verifica se ci sono contatti
            {
                for (int i = 0; i < _contacts.Length; i++)
                {
                    Messages m = new Messages(ServerDB.GetUserMessages(user, _contacts[i]));
                    _contacts[i].SetContactMessages = m;
                }
            }
        }

        internal Contacts(Contact[] contacts)
        {
            _contacts = contacts;
        }

        ~Contacts()
        {
            _contacts = null;
        }
    }
}
