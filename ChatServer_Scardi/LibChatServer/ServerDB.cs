#pragma warning disable CS0162 // È stato rilevato codice non raggiungibile
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Configuration;
using System.Net;

namespace LibChatServer
{
    /// <summary>
    /// La Classe che gestisce l'interazione del Server con il DB
    /// </summary>
    internal class ServerDB
    {
        /// <summary>
        /// Converte un Intero in un Booleano
        /// </summary>
        /// <param name="intToParse">1 -> TRUE | 0 -> FALSE | ALTRI NUMERI -> ERRORE</param>
        /// <returns>Il valore dell'Intero in Booleano</returns>
        internal static bool ParseIntToBool(int intToParse)
        {
            return intToParse == 1 ? true : intToParse == 0 ? false : throw new ArgumentOutOfRangeException(nameof(intToParse), "L'intero fornito non è ne 1 ne 0");
        }

        /// <summary>
        /// Converte un Booleano in un Intero
        /// </summary>
        /// <param name="boolToParse">TRUE -> 1 | FALSE -> 0</param>
        /// <returns>Il valore del Booleano in Intero</returns>
        internal static int ParseBoolToInt(bool boolToParse)
        {
            return boolToParse ? 1 : 0;
        }

        /// <summary>
        /// Restituisce la stringa di connessione al DB
        /// </summary>
        /// <param name="id">Nome Nel App.config dell attributo id nel tag XML StringCoonnections</param>
        /// <returns>La Stringa di connessione al DB</returns>
        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }

        /// <summary>
        /// Cerca nel DB un utente secondo la classe User che gli viene fornita
        /// </summary>
        /// <param name="user">l'Utente da cercare</param>
        /// <returns>Un Bool che rappresenta se l'utente è stato trovato</returns>
        internal static bool UserExist(User user)
        {
            if (user is null)
                return false;

            using (SQLiteConnection dbConn = new SQLiteConnection(LoadConnectionString()))
            {
                dbConn.Open();

                using (SQLiteCommand dbCmd = new SQLiteCommand(dbConn))
                {
                    //Lettura di dei Record da una tabella secondo le condizioni specificate
                    string sql = "SELECT * FROM Users WHERE UsersID = @userID AND UsersName = @userName AND UsersPwd = @userPwd";
                    dbCmd.CommandText = sql;
                    dbCmd.Parameters.AddWithValue("userID", user.UserID);
                    dbCmd.Parameters.AddWithValue("userName", user.UserName);
                    dbCmd.Parameters.AddWithValue("userPwd", user.UserPwd);
                    using (SQLiteDataReader queryRes = dbCmd.ExecuteReader())
                    {
                        return queryRes.HasRows;
                    }
                }
                dbConn.Close();
            }
        }

        /// <summary>
        /// Cerca nel DB un utente secondo l'ID dell Utente che gli viene fornito
        /// </summary>
        /// <param name="userID">l'ID dell Utente da cercare</param>
        /// <returns>Un Utente se lo si è trovato o null viceversa</returns>
        internal static User UserExist(int userID)
        {
            if (userID <= 0)
                return null;

            using (SQLiteConnection dbConn = new SQLiteConnection(LoadConnectionString()))
            {
                dbConn.Open();

                using (SQLiteCommand dbCmd = new SQLiteCommand(dbConn))
                {
                    //Lettura di dei Record da una tabella secondo le condizioni specificate
                    string sql = "SELECT * FROM Users WHERE UsersID = @userID";
                    dbCmd.CommandText = sql;
                    dbCmd.Parameters.AddWithValue("userID", userID);
                    using (SQLiteDataReader queryRes = dbCmd.ExecuteReader())
                    {
                        if (queryRes.HasRows)
                        {
                            queryRes.Read();
                            return new User(int.Parse(queryRes["UsersID"].ToString()), queryRes["UsersName"].ToString(), queryRes["UsersPwd"].ToString(), IPAddress.Parse(queryRes["UsersIP"].ToString()));
                        }
                        else
                            return null;
                    }
                }

                dbConn.Close();
            }
        }

        /// <summary>
        /// Cerca nel DB un utente secondo l'ID dell Utente che gli viene fornito
        /// </summary>
        /// <param name="userName">il NomeUtente dell Utente da cercare</param>
        /// <returns>Un Utente se lo si è trovato o null viceversa</returns>
        internal static User UserExist(string userName)
        {
            if (userName is null)
                return null;

            using (SQLiteConnection dbConn = new SQLiteConnection(LoadConnectionString()))
            {
                dbConn.Open();

                using (SQLiteCommand dbCmd = new SQLiteCommand(dbConn))
                {
                    //Lettura di dei Record da una tabella secondo le condizioni specificate
                    string sql = "SELECT * FROM Users WHERE UsersName = @userName";
                    dbCmd.CommandText = sql;
                    dbCmd.Parameters.AddWithValue("userName", userName);
                    using (SQLiteDataReader queryRes = dbCmd.ExecuteReader())
                    {
                        if (queryRes.HasRows)
                        {
                            queryRes.Read();
                            return new User(int.Parse(queryRes["UsersID"].ToString()), queryRes["UsersName"].ToString(), queryRes["UsersPwd"].ToString(), IPAddress.Parse(queryRes["UsersIP"].ToString()));
                        }
                        else
                        {
                            return null;
                        }
                    }
                }

                dbConn.Close();
            }
        }

        /// <summary>
        /// Cerca nel DB un utente secondo l'ID dell Utente che gli viene fornito
        /// </summary>
        /// <param name="userIP">l'IPAddress dell Utente da cercare</param>
        /// <returns>Un Utente se lo si è trovato o null viceversa</returns>
        internal static User UserExist(IPAddress userIP)
        {
            if (userIP is null)
                return null;

            using (SQLiteConnection dbConn = new SQLiteConnection(LoadConnectionString()))
            {
                dbConn.Open();

                using (SQLiteCommand dbCmd = new SQLiteCommand(dbConn))
                {
                    //Lettura di dei Record da una tabella secondo le condizioni specificate
                    string sql = "SELECT * FROM Users WHERE UsersIP = @userIP";
                    dbCmd.CommandText = sql;
                    dbCmd.Parameters.AddWithValue("userIP", userIP.ToString());
                    using (SQLiteDataReader queryRes = dbCmd.ExecuteReader())
                    {
                        if (queryRes.HasRows)
                        {
                            queryRes.Read();
                            return new User(int.Parse(queryRes["UsersID"].ToString()), queryRes["UsersName"].ToString(), queryRes["UsersPwd"].ToString(), IPAddress.Parse(queryRes["UsersIP"].ToString()));
                        }
                        else
                        {
                            return null;
                        }
                    }
                }

                dbConn.Close();
            }
        }

        /// <summary>
        /// Cerca nel DB un utente secondo l'ID dell Utente che gli viene fornito
        /// </summary>
        /// <param name="userID">l'ID dell Utente da cercare</param>
        /// <param name="userName">il NomeUtente dell Utente da cercare</param>
        /// <returns>Un Utente se lo si è trovato o null viceversa</returns>
        internal static User UserExist(int userID, string userName)
        {
            if (userID <= 0 || userName is null)
                return null;

            using (SQLiteConnection dbConn = new SQLiteConnection(LoadConnectionString()))
            {
                dbConn.Open();

                using (SQLiteCommand dbCmd = new SQLiteCommand(dbConn))
                {
                    //Lettura di dei Record da una tabella secondo le condizioni specificate
                    string sql = "SELECT * FROM Users WHERE UsersID = @userID AND UsersName = @userName";
                    dbCmd.CommandText = sql;
                    dbCmd.Parameters.AddWithValue("userID", userID);
                    dbCmd.Parameters.AddWithValue("userName", userName);
                    using (SQLiteDataReader queryRes = dbCmd.ExecuteReader())
                    {
                        if (queryRes.HasRows)
                        {
                            queryRes.Read();
                            return new User(int.Parse(queryRes["UsersID"].ToString()), queryRes["UsersName"].ToString(), queryRes["UsersPwd"].ToString(), IPAddress.Parse(queryRes["UsersIP"].ToString()));
                        }
                        else
                        {
                            return null;
                        }
                    }
                }

                dbConn.Close();
            }
        }

        /// <summary>
        /// Cerca nel DB un utente secondo l'ID dell Utente che gli viene fornito
        /// </summary>
        /// <param name="userName">il NomeUtente dell Utente da cercare</param>
        /// <param name="userPwd">la Password dell Utente da cercare</param>
        /// <returns>Un Utente se lo si è trovato o null viceversa</returns>
        internal static User UserExist(string userName, string userPwd)
        {
            if (userName is null || userPwd is null)
                return null;

            using (SQLiteConnection dbConn = new SQLiteConnection(LoadConnectionString()))
            {
                dbConn.Open();

                using (SQLiteCommand dbCmd = new SQLiteCommand(dbConn))
                {
                    //Lettura di dei Record da una tabella secondo le condizioni specificate
                    string sql = "SELECT * FROM Users WHERE UsersName = @userName AND UsersPwd = @userPwd";
                    dbCmd.CommandText = sql;
                    dbCmd.Parameters.AddWithValue("userName", userName);
                    dbCmd.Parameters.AddWithValue("userPwd", userPwd);
                    using (SQLiteDataReader queryRes = dbCmd.ExecuteReader())
                    {
                        if (queryRes.HasRows)
                        {
                            queryRes.Read();
                            return new User(int.Parse(queryRes["UsersID"].ToString()), queryRes["UsersName"].ToString(), queryRes["UsersPwd"].ToString(), IPAddress.Parse(queryRes["UsersIP"].ToString()));
                        }
                        else
                        {
                            return null;
                        }
                    }
                }

                dbConn.Close();
            }
        }

        /// <summary>
        /// Cerca nel DB un utente secondo l'ID dell Utente che gli viene fornito
        /// </summary>
        /// <param name="userName">il NomeUtente dell Utente da cercare</param>
        /// <param name="userIP">l'IPAddress dell Utente da cercare</param>
        /// <returns>Un Utente se lo si è trovato o null viceversa</returns>
        internal static User UserExist(string userName, IPAddress userIP)
        {
            if (userName is null || userIP is null)
                return null;

            using (SQLiteConnection dbConn = new SQLiteConnection(LoadConnectionString()))
            {
                dbConn.Open();

                using (SQLiteCommand dbCmd = new SQLiteCommand(dbConn))
                {
                    //Lettura di dei Record da una tabella secondo le condizioni specificate
                    string sql = "SELECT * FROM Users WHERE UsersName = @userName AND UsersIP = @userIP";
                    dbCmd.CommandText = sql;
                    dbCmd.Parameters.AddWithValue("userName", userName);
                    dbCmd.Parameters.AddWithValue("userIP", userIP.ToString());
                    using (SQLiteDataReader queryRes = dbCmd.ExecuteReader())
                    {
                        if (queryRes.HasRows)
                        {
                            queryRes.Read();
                            return new User(int.Parse(queryRes["UsersID"].ToString()), queryRes["UsersName"].ToString(), queryRes["UsersPwd"].ToString(), IPAddress.Parse(queryRes["UsersIP"].ToString()));
                        }
                        else
                        {
                            return null;
                        }
                    }
                }

                dbConn.Close();
            }
        }

        /// <summary>
        /// Cerca nel DB un utente secondo l'ID dell Utente che gli viene fornito
        /// </summary>
        /// <param name="userID">l'ID dell Utente da cercare</param>
        /// <param name="userName">il NomeUtente dell Utente da cercare</param>
        /// <param name="userIP">l'IPAddress dell Utente da cercare</param>
        /// <returns>Un Utente se lo si è trovato o null viceversa</returns>
        internal static User UserExist(int userID, string userName, IPAddress userIP)
        {
            if (userID <= 0 || userName is null || userIP is null)
                return null;

            using (SQLiteConnection dbConn = new SQLiteConnection(LoadConnectionString()))
            {
                dbConn.Open();

                using (SQLiteCommand dbCmd = new SQLiteCommand(dbConn))
                {
                    //Lettura di dei Record da una tabella secondo le condizioni specificate
                    string sql = "SELECT * FROM Users WHERE UsersID = @userID AND UsersName = @userName AND UsersIP = @userIP";
                    dbCmd.CommandText = sql;
                    dbCmd.Parameters.AddWithValue("userID", userID);
                    dbCmd.Parameters.AddWithValue("userName", userName);
                    dbCmd.Parameters.AddWithValue("userIP", userIP.ToString());
                    using (SQLiteDataReader queryRes = dbCmd.ExecuteReader())
                    {
                        if (queryRes.HasRows)
                        {
                            queryRes.Read();
                            return new User(int.Parse(queryRes["UsersID"].ToString()), queryRes["UsersName"].ToString(), queryRes["UsersPwd"].ToString(), IPAddress.Parse(queryRes["UsersIP"].ToString()));
                        }
                        else
                        {
                            return null;
                        }
                    }
                }

                dbConn.Close();
            }
        }

        /// <summary>
        /// Funzione che va a sovrascrivere l'IPAddress di un utente
        /// </summary>
        /// <param name="userID">l'ID dell utente a cui sovrascrivere l'IPAddress</param>
        /// <param name="userIP">il nuovo IPAddress da scrivere nel DB</param>
        /// <returns>Un Bool che rappresenta se l'IPAddress è stato aggiornato</returns>
        internal static bool UpdateUserIP(int userID, IPAddress userIP)
        {
            if (userID <= 0 || userIP is null)
                return false;

            using (SQLiteConnection dbConn = new SQLiteConnection(LoadConnectionString()))
            {
                dbConn.Open();

                using (SQLiteCommand dbCmd = new SQLiteCommand(dbConn))
                {
                    string sql = "UPDATE Users SET UsersIP = @userIP WHERE UsersID = @userID;";
                    dbCmd.CommandText = sql;
                    dbCmd.Parameters.AddWithValue("userIP", userIP.ToString());
                    dbCmd.Parameters.AddWithValue("userID", userID);
                    if (dbCmd.ExecuteNonQuery() <= 0)
                        //throw new Exception("Inable to Update the IP of the user selected");
                        return false;

                }

                dbConn.Close();
                return true;
            }
        }

        /// <summary>
        /// Ottiene l'ID dell utente dato l'UserName
        /// </summary>
        /// <param name="userName">l'UserName dell utente da cercare nel DB</param>
        /// <returns>l'ID dell utente cercato o -1 se non si è trovato l'utente</returns>
        internal static int GetUserID(string userName)
        {
            User user = UserExist(userName);
            if (user is not null)
                return user.UserID;
            else
                return -1;
        }

        /// <summary>
        /// Ottiene l'UserName dell utente dato l'ID
        /// </summary>
        /// <param name="userID">l'ID dell utente da cercare nel DB</param>
        /// <returns>l'UserName dell utente cercato o null se non si è trovato l'utente</returns>
        internal static string GetUserName(int userID)
        {
            User user = UserExist(userID);
            if (user is not null)
                return user.UserName;
            else
                return null;
        }

        /// <summary>
        /// Crea un nuovo Utente nel DB
        /// </summary>
        /// <param name="userName">il NomeUtente del Nuovo Utente</param>
        /// <param name="userPwd">la Password del Nuovo Utente</param>
        /// <param name="userIP">l'IPAddress del Nuovo Utente</param>
        /// <returns>Un Bool che rappresenta l'exit code della funzione</returns>
        internal static bool CreateNewUser(string userName, string userPwd, IPAddress userIP)
        {
            if (userName is null || userPwd is null || userIP is null)
                return false;

            using (SQLiteConnection dbConn = new SQLiteConnection(LoadConnectionString()))
            {
                dbConn.Open();

                using (SQLiteCommand dbCmd = new SQLiteCommand(dbConn))
                {
                    //Lettura di dei Record da una tabella secondo le condizioni specificate
                    string sql = "SELECT * FROM Users WHERE UsersName = @userName AND UsersIP = @userIP";
                    dbCmd.CommandText = sql;
                    dbCmd.Parameters.AddWithValue("userName", userName);
                    dbCmd.Parameters.AddWithValue("userIP", userIP.ToString());
                    using (SQLiteDataReader queryRes = dbCmd.ExecuteReader())
                    {
                        if (queryRes.HasRows)
                            //throw new SQLiteException(SQLiteErrorCode.Row, $"An user already exist with the given username: {userName}");
                            return false;
                    }

                    sql = @"INSERT INTO Users ('UsersName', 'UsersPwd', 'UsersIP') VALUES (@userName, @userPwd, @userIp);";
                    dbCmd.CommandText = sql;
                    dbCmd.Parameters.AddWithValue("userName", userName);
                    dbCmd.Parameters.AddWithValue("userPwd", userPwd);
                    dbCmd.Parameters.AddWithValue("userIp", userIP.ToString());

                    if (dbCmd.ExecuteNonQuery() <= 0)
                        //throw new SQLiteException(SQLiteErrorCode.IoErr_Write, $"Error while creating a new user: {userName}");
                        return false;

                    sql = @"SELECT UsersID FROM Users WHERE UsersName = @userName";
                    dbCmd.CommandText = sql;
                    dbCmd.Parameters.AddWithValue("userName", userName);
                    int userID;
                    using (SQLiteDataReader queryRes = dbCmd.ExecuteReader())
                    {
                        if (!queryRes.HasRows)
                            //throw new SQLiteException(SQLiteErrorCode.IoErr_Read, $"Unable to find id of the given username: {userName}");
                            return false;
                        queryRes.Read();
                        userID = int.Parse(queryRes[0].ToString());
                    }

                    sql = @$"CREATE TABLE ""Contacts-{userID}"" (
                            ""ContactsID""    INTEGER NOT NULL UNIQUE,
                            ""ContactsUserID""    INTEGER NOT NULL UNIQUE,
                            ""ContactsUserName""  TEXT NOT NULL UNIQUE,
                            ""ContactsAdded""    INTEGER NOT NULL,
                            PRIMARY KEY(""ContactsID"" AUTOINCREMENT),
	                        FOREIGN KEY(""ContactsUserID"") REFERENCES ""Users""(""UsersID""),
	                        FOREIGN KEY(""ContactsUserName"") REFERENCES ""Users""(""UsersName"")
                        ); ";
                    dbCmd.CommandText = sql;

                    if (dbCmd.ExecuteNonQuery() <= 0)
                        //throw new SQLiteException(SQLiteErrorCode.IoErr_Write, $"Error while creating a new user Contacts Table: {userName}");
                        return false;
                    return true;
                }

                dbConn.Close();
            }
        }

        /// <summary>
        /// Cerca all interno del DB nella tabella contatti riferita all utente specificato se è presente un determinato contatto
        /// </summary>
        /// <param name="userID">l'ID dell utente dove cercare il contatto (Serve per il nome della tabella)</param>
        /// <param name="contactUserID">l'ID del contatto da cercare</param>
        /// <param name="contactUserName">il NomeUtente del contatto da cercare</param>
        /// <returns>Un Bool che rappresenta l'exit code della funzione</returns>
        internal static bool ContactUserExist(int userID, int contactUserID, string contactUserName)
        {
            if (userID <= 0 || contactUserID <= 0 || contactUserName is null)
                //throw new ArgumentNullException(userID < 0 ? nameof(userID) : nameof(contactUserID), userID < 0 ? "L'utente a cui ci si rifersice per verificare il contatto non puo esistere nel DB dato che ID < 0" : "Il contatto che si sta cercando non puo esistere nel DB dato che ID < 0");
                return false;

            using (SQLiteConnection dbConn = new SQLiteConnection(LoadConnectionString()))
            {
                dbConn.Open();

                using (SQLiteCommand dbCmd = new SQLiteCommand(dbConn))
                {
                    string sql = @$"SELECT * FROM 'Contacts-{userID}' WHERE ContactsUserID = @contactsUserID AND ContactsUserName = @contactsUserName;";
                    dbCmd.CommandText = sql;
                    dbCmd.Parameters.AddWithValue("contactsUserID", contactUserID);
                    dbCmd.Parameters.AddWithValue("contactsUserName", contactUserName);
                    using (SQLiteDataReader queryRes = dbCmd.ExecuteReader())
                    {
                        return queryRes.HasRows;
                    }
                }

                dbConn.Close();
            }
        }

        /// <summary>
        /// Cerca all interno del DB nella tabella Contatti riferita all Utente specificato se è presente un determinato contatto
        /// </summary>
        /// <param name="user">l'Utente dove cercare il Contatto (Serve per il nome della tabella)</param>
        /// <param name="contact">il Contatto da cercare</param>
        /// <returns>Un Bool che rappresenta l'exit code della funzione</returns>
        internal static bool ContactUserExist(User user, Contact contact)
        {
            if (user is null || contact is null)
                //throw new ArgumentNullException(nameof(user), "L'utente a cui ci si rifersice per verificare il contatto è null");
                return false;
            if (contact.ContactID <= 0) //Verifica se il contatto è stato inserito nel DB e quind di consequenza gli è stato assegnato un ID
                //throw new Exception("Si sta verificando l'esistenza di un contatto neanche ancora inserito nel database");
                return false;

            using (SQLiteConnection dbConn = new SQLiteConnection(LoadConnectionString()))
            {
                dbConn.Open();
                bool queryHasRows;

                using (SQLiteCommand dbCmd = new SQLiteCommand(dbConn))
                {
                    string sql = @$"SELECT * FROM 'Contacts-{user.UserID}' WHERE ContactsUserID = @contactsUserID AND ContactsUserName = @contactsUserName";
                    dbCmd.CommandText = sql;
                    dbCmd.Parameters.AddWithValue("contactsUserID", contact.ContactUserID);
                    dbCmd.Parameters.AddWithValue("contactsUserName", contact.ContactUserName);
                    using (SQLiteDataReader queryRes = dbCmd.ExecuteReader())
                    {
                        queryHasRows = queryRes.HasRows;

                    }
                }

                dbConn.Close();
                return queryHasRows;
            }
        }

        /// <summary>
        /// Ottiene TUTTI i Contatti di un determinato Utente
        /// </summary>
        /// <param name="user">l'Utente dove cercare i Contatti (Serve per il nome della tabella)</param>
        /// <returns>Se trova Contatti un array di Contatti viceversa null</returns>
        internal static Contact[] GetUserContacts(User user)
        {
            if (user is null)
                //throw new ArgumentNullException(nameof(user), "L'utente a cui ci si rifersice per caricare i contatti è null");
                return null;

            List<Contact> contacts = new List<Contact>();
            using (SQLiteConnection dbConn = new SQLiteConnection(LoadConnectionString()))
            {
                dbConn.Open();

                using (SQLiteCommand dbCmd = new SQLiteCommand(dbConn))
                {
                    //string sql = @$"SELECT * FROM 'Contacts-{user.UserID}' ORDER BY ContactsUserID ASC;";
                    string sql = @$"SELECT * FROM 'Contacts-{user.UserID}';";
                    dbCmd.CommandText = sql;
                    using (SQLiteDataReader queryRes = dbCmd.ExecuteReader())
                    {
                        if (!queryRes.HasRows)
                            return null; //Ritorno al chiamante il mio array di contatti vuoto (Molto meglio di un eccezione)
                            //throw new Exception("L'utente che sta tentendo di richiedere contatti non li ha");

                        while (queryRes.Read())
                            contacts.Add(new Contact(int.Parse(queryRes["ContactsID"].ToString()), int.Parse(queryRes["ContactsUserID"].ToString()), queryRes["ContactsUserName"].ToString(), ParseIntToBool(int.Parse(queryRes["ContactsAdded"].ToString()))));

                    }
                }

                dbConn.Close();
            }
            return contacts.ToArray();
        }

        /// <summary>
        /// Aggiunge un nuovo Contatto all Utente specificato
        /// </summary>
        /// <param name="user">l'Utente dove aggiungere il Contatto (Serve per il nome della tabella)</param>
        /// <param name="newContact">il Contatto da aggiungere</param>
        /// <returns>Un Bool che rappresenta l'exit code della funzione</returns>
        internal static bool AddNewContactToUser(User user, Contact newContact)
        {
            if (user is null || newContact is null)
                //throw new ArgumentNullException(user is null ? nameof(user) : nameof(newContact), user is null ? "L'utente a cui ci si riferisce per aggiungere il contatto è null" : "Il contatto da aggiungere è null");
                return false;
            if (newContact.ContactID >= 0) //Verifica se il contatto che si sta tentando di aggiungere ha gia un ID e quindi di conseguenza è gia stato aggiunto nel DB
                //throw new Exception("Il contatto che si sta tentando di aggiungere presenta gia un ContactID");
                return false;

            using (SQLiteConnection dbConn = new SQLiteConnection(LoadConnectionString()))
            {
                dbConn.Open();

                using (SQLiteCommand dbCmd = new SQLiteCommand(dbConn))
                {
                    //Verifica Esistenza del nuovo contatto
                    string sql = @$"SELECT * FROM 'Contacts-{user.UserID}' WHERE ContactsUserID = @contactsUserID OR ContactsUserName = @contactsUserName;";
                    dbCmd.CommandText = sql;
                    dbCmd.Parameters.AddWithValue("contactsUserID", newContact.ContactUserID);
                    dbCmd.Parameters.AddWithValue("contactsUserName", newContact.ContactUserName);
                    using (SQLiteDataReader queryRes = dbCmd.ExecuteReader())
                    {
                        if (queryRes.HasRows)
                            //throw new Exception("The New Contact is already in the DB");
                            return false;
                    }

                    //Scrittura del nuovo contatto
                    sql = @$"INSERT INTO 'Contacts-{user.UserID}' (ContactsUserID, ContactsUserName, ContactsAdded) VALUES (@contactUserID, @contactUserName, @contactAdded);";
                    dbCmd.CommandText = sql;
                    dbCmd.Parameters.AddWithValue("contactUserID", newContact.ContactUserID);
                    dbCmd.Parameters.AddWithValue("contactUserName", newContact.ContactUserName);
                    dbCmd.Parameters.AddWithValue("contactAdded", ParseBoolToInt(true));
                    if (dbCmd.ExecuteNonQuery() <= 0)
                        //throw new SQLiteException(SQLiteErrorCode.IoErr_Write, "Error writing the new Contact to the DB");
                        return false;

                    //Scrittura del nuovo contatto nella tabella del conttatto aggiunto
                    sql = @$"INSERT INTO 'Contacts-{newContact.ContactUserID}' (ContactsUserID, ContactsUserName, ContactsAdded) VALUES (@contactUserID, @contactUserName, @contactAdded);";
                    dbCmd.CommandText = sql;
                    dbCmd.Parameters.AddWithValue("contactUserID", user.UserID);
                    dbCmd.Parameters.AddWithValue("contactUserName", user.UserName);
                    dbCmd.Parameters.AddWithValue("contactAdded", ParseBoolToInt(false));
                    if (dbCmd.ExecuteNonQuery() <= 0)
                        //throw new SQLiteException(SQLiteErrorCode.IoErr_Write, "Error writing the new Contact to the DB");
                        return false;

                    sql = @$"CREATE TABLE ""Messages-{user.UserID}-{newContact.ContactUserID}""(
                            ""MessagesID""    INTEGER NOT NULL UNIQUE,
                         ""MessagesUserSenderID""   INTEGER NOT NULL,
                         ""MessagesContactReceiverID""   INTEGER NOT NULL,
                         ""MessagesDateTimeSend""	TEXT NOT NULL,
                            ""MessagesText""  TEXT NOT NULL,
                         PRIMARY KEY(""MessagesID"" AUTOINCREMENT),
                         FOREIGN KEY(""MessagesContactReceiverID"") REFERENCES ""Contacts-{user.UserID}""(""ContactsUserID"")
                        ); ";
                    dbCmd.CommandText = sql;

                    if (dbCmd.ExecuteNonQuery() <= 0)
                        //throw new SQLiteException(SQLiteErrorCode.IoErr_Write, $"Error while creating a new user Messages Table: {userName}");
                        return false;
                    return true;

                }

                dbConn.Close();
            }
        }

        /// <summary>
        /// Ottiene TUTTI i Messaggi di un determinato Contatto di un determinato Utente
        /// </summary>
        /// <param name="user">l'Utente dove cercare il Contatto (Serve per il nome della tabella)</param>
        /// <param name="contact">il Contatto dove cercare i Messaggi (Serve per il nome della atbella)</param>
        /// <returns>Se trova Messaggi un array di Messaggi viceversa null</returns>
        internal static Message[] GetUserMessages(User user, Contact contact)
        {
            if (user is null || contact is null)
                return null;
            if (user.UserID <= 0 || contact.ContactID <= 0) //Verifica che l'utente e il contatto abbiano un ID quindi esistano nel DB
                return null;

            List<Message> messages = new List<Message>();
            using (SQLiteConnection dbConn = new SQLiteConnection(LoadConnectionString()))
            {
                dbConn.Open();

                using (SQLiteCommand dbCmd = new SQLiteCommand(dbConn))
                {
                    string outputEvalContactAdded = contact.ContactsAdded ? $"{user.UserID}-{contact.ContactUserID}" : $"{contact.ContactUserID}-{user.UserID}";
                    string sql = @$"SELECT * FROM 'Messages-{outputEvalContactAdded}';";// WHERE MessagesContactReceiverID = @messagesContactReceiverID;
                    dbCmd.CommandText = sql;
                    //dbCmd.Parameters.AddWithValue("messagesContactReceiverID", contact.ContactUserID);
                    using (SQLiteDataReader queryRes = dbCmd.ExecuteReader())
                    {
                        if (!queryRes.HasRows)
                            return null; //L'utente non ha ancora scambiato messaggi con il suo contatto
                                         //throw new Exception("The user requesting contacts is inexistent, register a new user to continue");

                        while (queryRes.Read())
                            messages.Add(new Message(int.Parse(queryRes["MessagesID"].ToString()), int.Parse(queryRes["MessagesUserSenderID"].ToString()), int.Parse(queryRes["MessagesContactReceiverID"].ToString()), DateTime.Parse(queryRes["MessagesDateTimeSend"].ToString()), queryRes["MessagesText"].ToString(), int.Parse(queryRes["MessagesUserSenderID"].ToString()) == user.UserID ? true : false));
                        //Spiegazione BOOL (ultimo parametro del costruttore Message): se il mittente del messaggio sono io vado averificare che il destinatario esista all interno della mia tabella contatti mentre, se sono io il destinatario questa verifica non mi riguarda ma la farà l'utente che mi ha inviato il messaggio

                    }
                }

                dbConn.Close();
            }
            return messages.ToArray();
        }

        /// <summary>
        /// Ottiene i Messaggi oltre un tempo specificato di un determinato Contatto di un determinato Utente
        /// </summary>
        /// <param name="user">l'Utente dove cercare il Contatto (Serve per il nome della tabella)</param>
        /// <param name="contact">il Contatto dove cercare i Messaggi (Serve per il nome della atbella)</param>
        /// <param name="newMsgFromDateTime"> il Tempo specificato dopo cui prendere i Messaggi</param>
        /// <returns>Se trova Messaggi un array di Messaggi viceversa null</returns>
        internal static Message[] GetUserMessages(User user, Contact contact, DateTime newMsgFromDateTime)
        {
            if (user is null || contact is null)
                return null;
            if (user.UserID <= 0 || contact.ContactID <= 0) //Verifica che l'utente e il contatto abbiano un ID quindi esistano nel DB
                return null;

            List<Message> messages = new List<Message>();
            using (SQLiteConnection dbConn = new SQLiteConnection(LoadConnectionString()))
            {
                dbConn.Open();

                using (SQLiteCommand dbCmd = new SQLiteCommand(dbConn))
                {
                    string outputEvalContactAdded = contact.ContactsAdded ? $"{user.UserID}-{contact.ContactUserID}" : $"{contact.ContactUserID}-{user.UserID}";
                    string sql = @$"SELECT * FROM 'Messages-{outputEvalContactAdded}';";// WHERE MessagesDateTimeSend >= @messageDateTimeSend;
                    dbCmd.CommandText = sql;
                    dbCmd.Parameters.AddWithValue("messageDateTimeSend", newMsgFromDateTime.ToString());
                    using (SQLiteDataReader queryRes = dbCmd.ExecuteReader())
                    {
                        if (!queryRes.HasRows)
                            return null; //L'utente non ha ancora scambiato messaggi con il suo contatto
                                         //throw new Exception("The user requesting contacts is inexistent, register a new user to continue");
                        DateTime queryDateTime;
                        while (queryRes.Read())
                        {
                            queryDateTime = DateTime.Parse(queryRes["MessagesDateTimeSend"].ToString());
                            if (queryDateTime >= newMsgFromDateTime)
                                messages.Add(new Message(int.Parse(queryRes["MessagesID"].ToString()), int.Parse(queryRes["MessagesUserSenderID"].ToString()), int.Parse(queryRes["MessagesContactReceiverID"].ToString()), queryDateTime, queryRes["MessagesText"].ToString(), int.Parse(queryRes["MessagesUserSenderID"].ToString()) == user.UserID ? true : false));
                            else
                                continue;
                        }
                        //Spiegazione BOOL (ultimo parametro del costruttore Message): se il mittente del messaggio sono io vado averificare che il destinatario esista all interno della mia tabella contatti mentre, se sono io il destinatario questa verifica non mi riguarda ma la farà l'utente che mi ha inviato il messaggio

                    }
                }

                dbConn.Close();
            }
            return messages.Count > 0 ? messages.ToArray() : null;
        }

        /// <summary>
        /// Invia un Nuovo messaggio dato un determinato Contatto di un determinato Utente
        /// </summary>
        /// <param name="user">l'Utente dove cercare il Contatto (Serve per il nome della tabella)</param>
        /// <param name="contact">il Contatto dove cercare i Messaggi (Serve per il nome della atbella)</param>
        /// <param name="message">il Nuovo Messaggio da Inviare</param>
        /// <returns>Un Bool che rappresenta l'exit code della funzione</returns>
        internal static bool SendNewMessageToContact(User user, Contact contact, Message message)
        {
            if (user is null || contact is null || message is null)
                //throw new ArgumentNullException(user is null ? nameof(user) : contact is null ? nameof(contact):nameof(message), user is null ? "L'utente a cui ci si riferisce per ottenere la tabella relativa ai contatti di questo è null" : contact is null ? "Il contatto a cui si fa riferimento per Scrivere un nuovo messaggio su DB è null": "Il messaggio da scivere sul db è null");
                return false;
            if (contact.ContactID <= 0)
                //throw new Exception("Il contatto che si sta tentando di utilizzare per aggiungere un messaggio non esiste nella tabella Contacts");
                return false;
            if (message.MessageID >= 0)
                //throw new Exception("Il messaggio che si sta andando a scrivere ha gia un ID e pertanto potrebbe essere gia stato scritto");
                return false;

            using (SQLiteConnection dbConn = new SQLiteConnection(LoadConnectionString()))
            {
                dbConn.Open();

                using (SQLiteCommand dbCmd = new SQLiteCommand(dbConn))
                {
                    string outputEvalContactAdded = contact.ContactsAdded ? $"{user.UserID}-{contact.ContactUserID}" : $"{contact.ContactUserID}-{user.UserID}";
                    string sql = @$"INSERT INTO 'Messages-{outputEvalContactAdded}' (MessagesUserSenderID, MessagesContactReceiverID, MessagesDateTimeSend, MessagesText) VALUES (@messageUserSenderID, @messageContactReceiverID, @messageDateTimeSend, @messageText);";
                    dbCmd.CommandText = sql;
                    dbCmd.Parameters.AddWithValue("messageUserSenderID", user.UserID);
                    dbCmd.Parameters.AddWithValue("messageContactReceiverID", message.MessageContactReceiverID);
                    dbCmd.Parameters.AddWithValue("messageDateTimeSend", message.MessageDateTimeSend.ToString("dd/MM/yyyy HH:mm:ss"));
                    dbCmd.Parameters.AddWithValue("messageText", message.MessageText);
                    if (dbCmd.ExecuteNonQuery() <= 0)
                        //throw new SQLiteException(SQLiteErrorCode.IoErr_Write, "Error writing the new Contact to the DB");
                        return false;

                }

                dbConn.Close();
                return true;
            }

        }
    }
}
#pragma warning restore CS0162 // È stato rilevato codice non raggiungibile
