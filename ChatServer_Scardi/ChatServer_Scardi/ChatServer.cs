using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using LibChatServer;
using LibChatServer.Protocol;
using System.Text;

namespace ChatServer_Scardi
{
    class ChatServer
    {
        static void Main(string[] args)
        {
            IPAddress ServerIP;
            byte[] bytes = new byte[2048];
            Dictionary<int, User> serverUsers = new Dictionary<int, User>();
            string incomingProtocolClient = ""; //<NEWUSERREG>Tom,Tom,127.0.0.1<EOF> <AUTH>Tom,Tom,127.0.0.1<EOF>

            ProtocolsType protocol = ProtocolsType.Null;
            string[] protocolDatas = null;
            /*
             * Spiegazione Funzionamento Server
             * 
             * Il server funziona in modo tale da aprire all avvio una connessione socket che rimane in ascolto
             * di eventuali client connessi.
             * 
             * Alla connessione di un client il server si aspetta di ricevere un messaggio con il protocollo <AUTH>
             * contenente l'userName la password e il suo indirizzo IP, la connessione continua se nel DB del server
             * viene trovato il record dell utente.
             * Una volta autenticato il Cilent riceve il suo id per poter effetuare le connessioni al Server.
             * 
             * La seconda cosa che il cilent appena connesso richiederà saranno i contatti da lui salvati.
             * Quando il server si vede arrivare la richiesta di contatti salvati va a cercare sul database i contatti
             * associati al cilient nella tabella Contacts-$IDUTENTE e provvede a mandargeli secondo il formato XML
             * 
             * La connessione può continuare con il client che vuole caricare la chat tra lui ed un altro utente,
             * il Server a questo punto va nella tabella dedicata ai messaggi dell unente Messages-$IDUTENTE e filtrando
             * i record presenti nella tabella in base ai messaggi che appartengono alla chat tra utente e utente chat
             * gli manda tutto secondo il formato XML
             * 
             */

            Console.WriteLine("Server Chat Scardi!");
            Console.WriteLine("Avvio del Server...");

            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            for (int i = 0; i < ipHostInfo.AddressList.Length; i++)
            {
                Console.WriteLine($"{i}) {ipHostInfo.AddressList[i]}");
            }
            ServerIP = ipHostInfo.AddressList[int.Parse(Console.ReadLine())];
            IPEndPoint localEndPoint = new IPEndPoint(ServerIP, 11000);
            Socket listener = new Socket(ServerIP.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(localEndPoint);
            listener.Listen(10);

            while (true)
            {
                try
                {
                    Console.WriteLine("\r\nAttesa Ricezione Comandi ...");
                    Socket handler = listener.Accept();
                    handler.SendTimeout = 1000;
                    handler.ReceiveTimeout = 1000;
                    incomingProtocolClient = null;

                    while (true)
                    {
                        int bytesRec = handler.Receive(bytes);
                        incomingProtocolClient += Encoding.Unicode.GetString(bytes, 0, bytesRec);
                        if (incomingProtocolClient.IndexOf(EOF. ProtocolString) > -1 || incomingProtocolClient.IndexOf(EOS.ProtocolString) > -1)
                        {
                            break;
                        }
                    }

                    if (incomingProtocolClient is not null)
                    {
                        if (incomingProtocolClient != String.Empty)
                        {
                            protocol = Protocols.ParseDataProtocol(incomingProtocolClient, out protocolDatas);
                            if (protocol != ProtocolsType.Null)
                            {
                                Console.WriteLine(incomingProtocolClient);
                                try
                                {
                                    User tempUser;
                                    switch (protocol)
                                    {
                                        case ProtocolsType.Auth:
                                            {
                                                tempUser = User.Exist(protocolDatas[Auth.UserName]);//VERIFICA SE L'UTENTE ESISTE NEL DB
                                                if (tempUser is not null)//VERIFICA SE L'UTENTE ESISTE NEL DB
                                                {
                                                    //Se l'utente esiste verifico se si è gia autenticato
                                                    if (!serverUsers.ContainsKey(tempUser.UserID))
                                                    {
                                                        serverUsers.Add(tempUser.UserID, new User(protocolDatas));
                                                        incomingProtocolClient = tempUser.UserID.ToString() + "<EOF>"; //$"Utente Autenticato con successo, IdUtente -> {tempUser.UserID} | {(String.Equals(protocolDatas[Auth.IpUtente], tempUser.UserIpAddress.ToString()) ? "IP -> " + tempUser.UserIpAddress : "IP ultimo accesso -> " + tempUser.UserIpAddress + " IP attuale -> " + protocolDatas[Auth.IpUtente])}"
                                                        Console.WriteLine($"User Autenticated -> {tempUser.UserID}");
                                                    }
                                                    else
                                                        //Simulazione invio messaggio di errore al client
                                                        incomingProtocolClient = ErrorType.ToString(ErrorsType.AuthUserAlreadyAuth); //"Utente Già Autenticato o Non Registrato"
                                                }
                                                else
                                                    incomingProtocolClient = ErrorType.ToString(ErrorsType.AuthBadDatas); //"Utente Già Autenticato o Non Registrato"
                                            }
                                            break;
                                        case ProtocolsType.DeAuth:
                                            {
                                                tempUser = User.Exist(int.Parse(protocolDatas[DeAuth.UserID]));//VERIFICA SE L'UTENTE ESISTE NEL DB
                                                if (tempUser is not null)//VERIFICA SE L'UTENTE ESISTE NEL DB
                                                {
                                                    //Se l'utente esiste verifico se si è gia autenticato
                                                    if (serverUsers.ContainsKey(tempUser.UserID))
                                                    {
                                                        serverUsers.Remove(int.Parse(protocolDatas[DeAuth.UserID]));
                                                        incomingProtocolClient = "<OK><EOF>"; //Utente deAutenticato con successo, invio la conferma trasmettendo il suo ID negativo
                                                        Console.WriteLine($"User DeAutenticated -> {tempUser.UserID}");
                                                    }
                                                    else
                                                        //Simulazione invio messaggio di errore al client
                                                        incomingProtocolClient = ErrorType.ToString(ErrorsType.DeAuthUserNotFound);//"Utente Già DeAutenticato o Non Registrato"
                                                }
                                                else
                                                    incomingProtocolClient = ErrorType.ToString(ErrorsType.DeAuthUserNotFound);//"Utente Già DeAutenticato o Non Registrato"
                                            }
                                            break;
                                        case ProtocolsType.NewUserReg:
                                            //Simulazione invio messaggio di errore/successo al client
                                            incomingProtocolClient = User.UserCreation(protocolDatas) ? "<OK><EOF>" : ErrorType.ToString(ErrorsType.NewUserRegBadDatas); //Possibilità di aver inserito i dati male o presenza di un altro utente con le stesse credenziali
                                            Console.WriteLine($"User Created -> {protocolDatas[NewUserReg.UserName]}");
                                            break;
                                        case ProtocolsType.ReqContacts:
                                            {
                                                tempUser = User.Exist(int.Parse(protocolDatas[ReqContacts.UserID]));//VERIFICA SE L'UTENTE ESISTE NEL DB
                                                if (tempUser is not null)//VERIFICA SE L'UTENTE ESISTE NEL DB
                                                {
                                                    if (serverUsers.ContainsKey(tempUser.UserID))
                                                    {
                                                        if (serverUsers[int.Parse(protocolDatas[ReqContacts.UserID])].UserContacts.GetContact is not null)
                                                        {
                                                            //Simulazione dell invio tramite XML dei contatti dell utente che li richiede
                                                            //Console.WriteLine($"Contatti dell utente {serverUsers[int.Parse(protocolDatas[ReqContacts.UserID])]}");
                                                            //foreach (Contact contact in serverUsers[int.Parse(protocolDatas[ReqContacts.UserID])].UserContacts.GetContact)
                                                            //{
                                                            //    Console.WriteLine($"  {contact}");
                                                            //}
                                                            //Console.WriteLine("Fine Contatti.");
                                                            incomingProtocolClient = XmlAssembler.Contacts(serverUsers[int.Parse(protocolDatas[ReqContacts.UserID])].UserContacts) + "<EOF>";
                                                            Console.WriteLine($"Contact Requested From -> {tempUser.UserID}");
                                                        }
                                                        else
                                                            incomingProtocolClient = ErrorType.ToString(ErrorsType.ReqContactsInexistent); //$"Nessun Contatto presente per l'Utente: {serverUsers[int.Parse(protocolDatas[ReqContacts.UserID])]}"
                                                    }
                                                    else
                                                        //Simulazione invio messaggio di errore al client
                                                        incomingProtocolClient = ErrorType.ToString(ErrorsType.UserNotAuth); //"Effettuare il login prima di continuare"
                                                }
                                                else
                                                    incomingProtocolClient = ErrorType.ToString(ErrorsType.UserNotAuth); //"Utente Non Registrato"
                                            }
                                            break;
                                        case ProtocolsType.NewContact:
                                            {
                                                tempUser = User.Exist(int.Parse(protocolDatas[NewContact.UserID]));//VERIFICA SE L'UTENTE ESISTE NEL DB
                                                if (tempUser is not null)//VERIFICA SE L'UTENTE ESISTE NEL DB
                                                {
                                                    if (serverUsers.ContainsKey(tempUser.UserID))
                                                    {
                                                        serverUsers[int.Parse(protocolDatas[NewContact.UserID])].AddContact(new Contact(protocolDatas[NewContact.UserContactName]));
                                                        incomingProtocolClient = "<OK><EOF>"; //"Contatto Aggiunto all utente Selezionato"
                                                        Console.WriteLine($"New Contact Added -> {protocolDatas[NewContact.UserID]} To -> {tempUser.UserID}");
                                                    }
                                                    else
                                                        //Simulazione invio messaggio di errore al client
                                                        incomingProtocolClient = ErrorType.ToString(ErrorsType.UserNotAuth); //"Effettuare il login prima di continuare"
                                                }
                                                else
                                                    incomingProtocolClient = ErrorType.ToString(ErrorsType.UserNotAuth); //"Utente Non Registrato"
                                            }
                                            break;
                                        case ProtocolsType.LoadContact:
                                            {
                                                tempUser = User.Exist(int.Parse(protocolDatas[LoadContact.UserID]));//VERIFICA SE L'UTENTE ESISTE NEL DB
                                                if (tempUser is not null)//VERIFICA SE L'UTENTE ESISTE NEL DB
                                                {
                                                    if (serverUsers.ContainsKey(tempUser.UserID))
                                                    {
                                                        if (User.ContactUserExist(serverUsers[tempUser.UserID], serverUsers[tempUser.UserID].UserContacts[int.Parse(protocolDatas[LoadContact.ContactID])]))
                                                        {
                                                            if (serverUsers[int.Parse(protocolDatas[LoadContact.UserID])].UserContacts[int.Parse(protocolDatas[LoadContact.ContactID])].ContactMessages.GetMessages is not null)
                                                            {
                                                                //Simulazione dell invio tramite XML dei contatti dell utente che li richiede
                                                                //Console.WriteLine($"Messaggi del Contatto: {serverUsers[int.Parse(protocolDatas[LoadContact.UserID])]}");
                                                                //foreach (Message message in serverUsers[int.Parse(protocolDatas[LoadContact.UserID])].UserContacts[int.Parse(protocolDatas[LoadContact.ContactID])].ContactMessages.GetMessages)
                                                                //{
                                                                //    Console.WriteLine($"  {message}");
                                                                //}
                                                                //Console.WriteLine("Fine Messaggi.");
                                                                incomingProtocolClient = XmlAssembler.Messages(serverUsers[int.Parse(protocolDatas[LoadContact.UserID])].UserContacts[int.Parse(protocolDatas[LoadContact.ContactID])]) + "<EOF>";
                                                                Console.WriteLine($"Contact Loaded -> {protocolDatas[LoadContact.UserID]} From User -> {tempUser.UserID}");
                                                            }
                                                            else
                                                                incomingProtocolClient = ErrorType.ToString(ErrorsType.LoadContactMessageEmpty); //$"Nessun Messaggio presente per il Conatto: {serverUsers[int.Parse(protocolDatas[LoadContact.UserID])]}"

                                                        }
                                                        else
                                                            incomingProtocolClient = ErrorType.ToString(ErrorsType.LoadContactInexistent); //$"Nessun Contatto ({protocolDatas[LoadContact.ContactID]}) trovato associato all Utente Specificato ({protocolDatas[LoadContact.UserID]})"
                                                    }
                                                    else
                                                        //Simulazione invio messaggio di errore al client
                                                        incomingProtocolClient = ErrorType.ToString(ErrorsType.UserNotAuth); //"Effettuare il login prima di continuare"
                                                }
                                                else
                                                    incomingProtocolClient = ErrorType.ToString(ErrorsType.UserNotAuth); //"Utente Non Registrato"
                                            }
                                            break;
                                        case ProtocolsType.ReqNewMsg:
                                            {
                                                tempUser = User.Exist(int.Parse(protocolDatas[ReqNewMsg.UserID]));//VERIFICA SE L'UTENTE ESISTE NEL DB
                                                if (tempUser is not null)//VERIFICA SE L'UTENTE ESISTE NEL DB
                                                {
                                                    if (serverUsers.ContainsKey(tempUser.UserID))
                                                    {
                                                        if (User.ContactUserExist(serverUsers[tempUser.UserID], serverUsers[tempUser.UserID].UserContacts[int.Parse(protocolDatas[LoadContact.ContactID])]))
                                                        {
                                                            if (serverUsers[int.Parse(protocolDatas[ReqNewMsg.UserID])].UserContacts[int.Parse(protocolDatas[ReqNewMsg.ContactID])].GetMessagesFromDT(serverUsers[int.Parse(protocolDatas[ReqNewMsg.UserID])], DateTime.Parse(protocolDatas[ReqNewMsg.DTLastMessageReceived])) is not null)
                                                            {
                                                                //Simulazione dell invio tramite XML dei contatti dell utente che li richiede
                                                                //Console.WriteLine($"Nuovi Messaggi del Contatto: {serverUsers[int.Parse(protocolDatas[LoadContact.UserID])]} A PARTIRE DA: {DateTime.Parse(protocolDatas[ReqNewMsg.DTLastMessageReceived])}");
                                                                //foreach (Message message in serverUsers[int.Parse(protocolDatas[ReqNewMsg.UserID])].UserContacts[int.Parse(protocolDatas[ReqNewMsg.ContactID])].GetMessagesFromDT(serverUsers[int.Parse(protocolDatas[ReqNewMsg.UserID])], DateTime.Parse(protocolDatas[ReqNewMsg.DTLastMessageReceived])))
                                                                //{
                                                                //    Console.WriteLine($"  {message}");
                                                                //}
                                                                //Console.WriteLine("Fine Nuovi Messaggi.");
                                                                incomingProtocolClient = XmlAssembler.Messages(serverUsers[int.Parse(protocolDatas[ReqNewMsg.UserID])].UserContacts[int.Parse(protocolDatas[ReqNewMsg.ContactID])].GetMessagesFromDT(serverUsers[int.Parse(protocolDatas[ReqNewMsg.UserID])], DateTime.Parse(protocolDatas[ReqNewMsg.DTLastMessageReceived]))) + "<EOF>";
                                                                Console.WriteLine($"New Messages Loaded of Contact -> {protocolDatas[ReqNewMsg.UserID]} From User -> {tempUser.UserID}");
                                                            }
                                                            else
                                                                incomingProtocolClient = ErrorType.ToString(ErrorsType.ReqNewMsgNoNewMsg); //$"Nessun Nuovo ({protocolDatas[ReqNewMsg.DTLastMessageReceived]}) Messaggio trovato per il Contatto: ({protocolDatas[ReqNewMsg.ContactID]}) associato all Utente Specificato ({protocolDatas[ReqNewMsg.UserID]})"
                                                        }
                                                        else
                                                            incomingProtocolClient = ErrorType.ToString(ErrorsType.LoadContactInexistent); //$"Nessun Contatto ({protocolDatas[LoadContact.ContactID]}) trovato associato all Utente Specificato ({protocolDatas[LoadContact.UserID]})"
                                                    }
                                                    else
                                                        //Simulazione invio messaggio di errore al client
                                                        incomingProtocolClient = ErrorType.ToString(ErrorsType.UserNotAuth); //"Effettuare il login prima di continuare"
                                                }
                                                else
                                                    incomingProtocolClient = ErrorType.ToString(ErrorsType.UserNotAuth); //"Utente Non Registrato"
                                            }
                                            break;
                                        case ProtocolsType.SndMsg:
                                            {
                                                tempUser = User.Exist(int.Parse(protocolDatas[SndMsg.UserID]));//VERIFICA SE L'UTENTE ESISTE NEL DB
                                                if (tempUser is not null)//VERIFICA SE L'UTENTE ESISTE NEL DB
                                                {
                                                    if (serverUsers.ContainsKey(tempUser.UserID))
                                                    {
                                                        if (User.ContactUserExist(serverUsers[tempUser.UserID], serverUsers[tempUser.UserID].UserContacts[int.Parse(protocolDatas[SndMsg.ContactIDReceiver])]))
                                                        {
                                                            //Simulazione dell invio tramite XML dei contatti dell utente che li richiede
                                                            bool inputDateTime = true; //COSTANTE DI DEBUG
                                                            Message msgToSend = inputDateTime ? new Message(serverUsers[tempUser.UserID], serverUsers[tempUser.UserID].UserContacts[int.Parse(protocolDatas[SndMsg.ContactIDReceiver])], DateTime.Parse(protocolDatas[SndMsg.MessageDateTime]), protocolDatas[SndMsg.MessageText]) : new Message(serverUsers[tempUser.UserID], serverUsers[tempUser.UserID].UserContacts[int.Parse(protocolDatas[SndMsg.ContactIDReceiver])], protocolDatas[SndMsg.MessageText]);
                                                            serverUsers[tempUser.UserID].UserContacts[int.Parse(protocolDatas[SndMsg.ContactIDReceiver])].SendNewMsg(serverUsers[tempUser.UserID], msgToSend);
                                                            incomingProtocolClient = "<OK><EOF>"; //"Messaggio Inviato"
                                                            Console.WriteLine($"New Message Send To -> {protocolDatas[SndMsg.UserID]} From User -> {tempUser.UserID}");
                                                        }
                                                        else
                                                            incomingProtocolClient = ErrorType.ToString(ErrorsType.SndMsgContactInexistent); //$"Nessun Messaggio trovato per il Contatto associato ({protocolDatas[LoadContact.ContactID]}) all Utente Specificato ({protocolDatas[LoadContact.UserID]})"
                                                    }
                                                    else
                                                        //Simulazione invio messaggio di errore al client
                                                        incomingProtocolClient = ErrorType.ToString(ErrorsType.UserNotAuth); //"Effettuare il login prima di continuare"
                                                }
                                                else
                                                    incomingProtocolClient = ErrorType.ToString(ErrorsType.UserNotAuth); //"Utente Non Registrato"
                                            }
                                            break;
                                        case ProtocolsType.TestConn:
                                            {
                                                incomingProtocolClient = $"{protocolDatas[TestConn.IPAddress]}<EOF>";
                                            }
                                            break;
                                        default:
                                            {
                                                incomingProtocolClient = ErrorType.ToString(ErrorsType.CommandNotRecognized);
                                                Console.WriteLine(ErrorType.ToString(ErrorsType.CommandNotRecognized));
                                            }
                                            break;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    incomingProtocolClient = ex.Message;
                                    Console.WriteLine(ex.Message);
                                }

                                Console.WriteLine($"Response Send To Client -> {incomingProtocolClient}");
                                handler.Send(Encoding.Unicode.GetBytes(incomingProtocolClient));
                                handler.Shutdown(SocketShutdown.Both);
                                handler.Close();

                            }
                            else
                            {
                                incomingProtocolClient = ErrorType.ToString(ErrorsType.CommandNotRecognized);
                                Console.WriteLine(ErrorType.ToString(ErrorsType.CommandNotRecognized));
                            }

                        }
                        else
                        {
                            handler.Send(Encoding.Unicode.GetBytes(incomingProtocolClient));
                            handler.Shutdown(SocketShutdown.Both);
                            handler.Close();
                        }
                    }
                    else
                    {
                        handler.Shutdown(SocketShutdown.Both);
                        handler.Close();
                    }

                }
                catch (Exception exc)
                {
                    Console.WriteLine(Protocols.AssembleExceptionAsError(exc.Message));
                }
            }
            //Console.WriteLine("Chiusura Server Scardi!");

            /*
             * Lista di cose da fare:
             * 
             * Sistemare nel file Contact.cs la creazione di un contatto:
             * - Il costruttore con parametri userID e ContactNames è stato impostato da internal a public !!!!
             * 
             * overload dell operatore [] con la possibilità di cercare un utente/messaggio dato l'indice nel DB
             * overload dell operatore [] con la possibilità di cercare un contatto/messaggio dato il nome nel DB
             * 
             * IMPOSTARE AD internal TUTTE LE FUNZIONI DEL ServerDB;
             */
        }
    }
}
