using System;
using System.Net;

namespace LibChatServer
{
    public class User
    {
        private int _userID;
        private string _userName;
        private string _userPwd;
        private IPAddress _userIpAddress;

        private Contacts _userContacts;

        //Da aggiungere classe Contacts

        public int UserID { get { return _userID; } }
        public string UserName { get { return _userName; } }
        public string UserPwd { get { return _userPwd; } }
        public IPAddress UserIpAddress { get { return _userIpAddress; } }
        public Contacts UserContacts { get { UpdateContacts();  return _userContacts; } }

        public User(string[] protocolDatas)
        {
            if (protocolDatas is null)
                throw new Exception(ErrorType.ToString(ErrorsType.AuthBadDatas)); //"La stringa contenente i dati di autenticazione è null"

            User userSearch = ServerDB.UserExist(protocolDatas[Protocol.Auth.UserName], protocolDatas[Protocol.Auth.Password]);

            if (userSearch is null)
                throw new Exception(ErrorType.ToString(ErrorsType.AuthUserInexistent)); //"The User dont exist on the DB"

            _userID = userSearch.UserID;
            _userName = userSearch.UserName;
            _userPwd = userSearch.UserPwd;
            if (userSearch.UserIpAddress == IPAddress.Parse(protocolDatas[Protocol.Auth.IpUtente]))
                _userIpAddress = userSearch.UserIpAddress;
            else
            {
                if (!ServerDB.UpdateUserIP(userSearch.UserID, IPAddress.Parse(protocolDatas[Protocol.Auth.IpUtente])))
                    throw new Exception(ErrorType.ToString(ErrorsType.AuthIPChange)); //$"Unable to change the IP Address of {userSearch}"
                _userIpAddress = IPAddress.Parse(protocolDatas[Protocol.Auth.IpUtente]);
            }

            UpdateContacts();
        }

        public User(string userName, string userPwd, IPAddress userIP)
        {
            User userSearch = ServerDB.UserExist(userName,userPwd);

            if (userSearch is null)
                throw new Exception(ErrorType.ToString(ErrorsType.AuthUserInexistent)); //"The User dont exist on the DB"

            _userID = userSearch.UserID;
            _userName = userSearch.UserName;
            _userPwd = userSearch.UserPwd;
            if (userSearch.UserIpAddress == userIP)
                _userIpAddress = userSearch.UserIpAddress;
            else
            {
                if (!ServerDB.UpdateUserIP(userSearch.UserID, userIP))
                    throw new Exception(ErrorType.ToString(ErrorsType.AuthIPChange)); //$"Unable to change the IP Address of {userSearch}"
                _userIpAddress = userIP;
            }

            UpdateContacts();
        }


        internal User(int userID, string userName, string userPwd, IPAddress userIpAddress)
        {
            _userID = userID;
            _userName = userName;
            _userPwd = userPwd;
            _userIpAddress = userIpAddress;
        }

        private void UpdateContacts()
        {
            _userContacts = new Contacts(this);
        }

        public void AddContact(Contact newContact)
        {
            if (!ServerDB.AddNewContactToUser(this, newContact))
                throw new Exception(ErrorType.ToString(ErrorsType.NewContactUnableToAdd)); //$"Unable to Add a new Contact to {this}"
            UpdateContacts();
        }

        public override string ToString()
        {
            return $"{_userID}) {_userName} ({_userIpAddress})";
        }

        ~User() //DISTRUTTORE
        {
            _userID = -1;
            _userName = null;
            _userPwd = null;
            _userIpAddress = null;
        }     

        public static User Exist(int UserID)
        {
            return ServerDB.UserExist(UserID);
        }

        public static User Exist(string UserName)
        {
            return ServerDB.UserExist(UserName);
        }

        public static bool ContactUserExist(User user, Contact contact)
        {
            return ServerDB.ContactUserExist(user, contact);
        }

        public static bool VerifyContactIsNotSameUser(User user, Contact contact)
        {
            return user.UserID == contact.ContactUserID || user.UserName == contact.ContactUserName;
        }

        //Funzione STATICA per la creazione di nuovi utenti, DA REVISIONARE
        public static bool UserCreation(string userName, string userPwd, IPAddress userIpAddress)
        {
            return ServerDB.CreateNewUser(userName, userPwd, userIpAddress);
        }

        public static bool UserCreation(string[] protocolDatas)
        {
            if (protocolDatas is null)
                throw new Exception(ErrorType.ToString(ErrorsType.NewUserRegBadDatas)); //"La stringa contenente i dati di registrazione è null"
            return ServerDB.CreateNewUser(protocolDatas[Protocol.NewUserReg.UserName], protocolDatas[Protocol.NewUserReg.Password], IPAddress.Parse(protocolDatas[Protocol.NewUserReg.IpUtente]));
        }
    }
}
