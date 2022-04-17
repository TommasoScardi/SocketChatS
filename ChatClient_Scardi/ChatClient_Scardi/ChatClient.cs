using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ManagementWindow;
using LibChatClient;
using LibChatClient.Protocol;
using LibChatClient.Connections;
using LibChatClient.User;

namespace ChatClient_Scardi
{
    public partial class ChatClient : Form
    {
        IPAddress ServerIP, ClientIP;
        bool ServerConnection = false;

        int UserID = -1;
        string UserName = null;
        Contact[] UserContacts;
        int indexContactSelected = -1;

        bool usoTimerFetchNewMsg = true;
        Timer fetchNewMsg;
        DateTime lastFetchNewMsg = DateTime.MinValue;
        int lastHeightMsg = 0;

        public ChatClient()
        {
            InitializeComponent();
            //ManagementWindow.ManagementWindow m = new ManagementWindow.ManagementWindow(ManagementWindow.Type.UserRegistration);
            //m.ShowDialog();
        }

        private void InizializeInterface()
        {
            accediToolStripMenuItem.Enabled = true;
            esciToolStripMenuItem.Enabled = false;
            registratiToolStripMenuItem.Enabled = true;

            DeactivateContactsControls();

            panelViewMsg.Controls.Clear();
        }

        private void DeactivateToolStripLogin()
        {
            nuovaConnessioneToolStripMenuItem.Enabled = true;
            connettiToolStripMenuItem.Enabled = true;
            disconnettiToolStripMenuItem.Enabled = false;

            utenteToolStripMenuItem.Enabled = false;
            accediToolStripMenuItem.Enabled = false;
            registratiToolStripMenuItem.Enabled = false;
            esciToolStripMenuItem.Enabled = false;
        }

        private void ActivateToolStripLogin()
        {
            nuovaConnessioneToolStripMenuItem.Enabled = false;
            connettiToolStripMenuItem.Enabled = false;
            disconnettiToolStripMenuItem.Enabled = true;

            utenteToolStripMenuItem.Enabled = true;
            accediToolStripMenuItem.Enabled = true;
            registratiToolStripMenuItem.Enabled = true;
            //esciToolStripMenuItem.Enabled = true;
        }

        private void DeactivateContactsControls()
        {

            btnAddContact.Enabled = false;

            contattiToolStripMenuItem.Visible = false;
            contattiToolStripMenuItem.Enabled = false;
            aggiungiContattoToolStripMenuItem.Enabled = false;
            aggiornaContattiToolStripMenuItem.Enabled = false;

            messaggiToolStripMenuItem.Visible = false;
            messaggiToolStripMenuItem.Enabled = false;
            interrompiTimerMessaggiToolStripMenuItem.Visible = false;
            interrompiTimerMessaggiToolStripMenuItem.Enabled = false;

        }
        
        private void ActivateContactsControls()
        {

            btnAddContact.Enabled = true;

            contattiToolStripMenuItem.Visible = true;
            contattiToolStripMenuItem.Enabled = true;
            aggiungiContattoToolStripMenuItem.Enabled = true;
            aggiornaContattiToolStripMenuItem.Enabled = true;

            messaggiToolStripMenuItem.Visible = true;
            messaggiToolStripMenuItem.Enabled = true;
            interrompiTimerMessaggiToolStripMenuItem.Visible = true;
            interrompiTimerMessaggiToolStripMenuItem.Enabled = true;

        }

        private void FillContactsList()
        {
            try
            {
                string[] datas = { UserID.ToString() };
                string strContacts = ReqContacts.Assemble(int.Parse(datas[ReqContacts.UserID]));
                string strServerRes = Connection.SendReceiveFromServer(ServerIP, strContacts);
                strServerRes = Protocols.RemoveTerminator(strServerRes);

                if (!Protocols.VerifyServerError(strServerRes))
                {
                    UserContacts = XmlParser.ParseContacts(strServerRes);
                    lstContacts.Items.Clear();
                    foreach (Contact contact in UserContacts)
                    {
                        lstContacts.Items.Add(contact.UserName);
                    }
                }
                else
                {
                    ErrorsType error;
                    if (ErrorType.TryParse(strServerRes, out error))
                    {
                        if (error == ErrorsType.ReqContactsInexistent)
                            MessageBox.Show("Hey, La tua lista contatti sembra essere vuota, Usa il pulsante Aggiungi Nuovo Contatto per riempirla", "Nessun Contatto Presente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                            MessageBox.Show(error.ToString(), "Errore Ottenimento Contatti", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                        MessageBox.Show("Errore Sconosciuto sul server durante l'ottenimento dei contatti dell utente", "Errore Sconosciuto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Errore Ottenimento Contatti", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EmptyContactsList()
        {
            lstContacts.Items.Clear();
            indexContactSelected = -1;
            panelViewMsg.Controls.Clear();
            boxChat.Visible = false;
        }

        private void ChatClient_Load(object sender, EventArgs e)
        {
            try
            {
                while (!ServerConnection)
                {
                    MWindow m = new MWindow(ManagementWindow.Type.Connection);
                    m.ShowDialog();
                    
                    if (m.GetResult() == null)
                        continue;

                    ServerIP = IPAddress.Parse(m.GetResult());
                    nuovaConnessioneToolStripMenuItem.Enabled = false;
                    connettiToolStripMenuItem.Enabled = false;
                    disconnettiToolStripMenuItem.Enabled = true;
                    boxChat.Visible = false;

                    ServerConnection = true;
                    MessageBox.Show("Verifica Connessione al server tramite protocollo di test connessione effettuato con successo, risposta ottenuta", "Verifica IP Server Avvenuta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Test Connessione al server fallito, Impossibile Raggiungere il Server con l'IP Fornito", "Errore Connessione", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            ClientIP = Connection.GetIPAddress();
            InizializeInterface();
        }

        private void ChatClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ServerConnection)
                disconnettiToolStripMenuItem_Click(sender, e);
        }

        private void nuovaConnessioneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                MWindow m = new MWindow(ManagementWindow.Type.Connection);
                m.ShowDialog();

                if (m.GetResult() == null)
                {
                    DeactivateToolStripLogin();
                    MessageBox.Show("Verifica Connessione al server tramite protocollo di test connessione fallito, risposta non ottenuta", "Errore Verifica IP Server", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                ServerIP = IPAddress.Parse(m.GetResult());
                nuovaConnessioneToolStripMenuItem.Enabled = false;
                connettiToolStripMenuItem.Enabled = false;
                disconnettiToolStripMenuItem.Enabled = true;
                boxChat.Visible = false;

                ServerConnection = true;
                ActivateToolStripLogin();
                MessageBox.Show("Verifica Connessione al server tramite protocollo di test connessione effettuato con successo, risposta ottenuta", "Verifica IP Server Avvenuta", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Test Connessione al server fallito, Impossibile Raggiungere il Server con l'IP Fornito", "Errore Riconnessione", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void connettiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                IPStatus statusTestServer = Connection.VerifyConnectionToServer(ServerIP);

                if (statusTestServer == IPStatus.Success)
                {
                    ServerConnection = true;
                    ActivateToolStripLogin();
                    MessageBox.Show("Riconnessione al server avvenuta con Successo", "Riconnessione Avvenuta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (statusTestServer == IPStatus.DestinationHostUnreachable || statusTestServer == IPStatus.DestinationNetworkUnreachable || statusTestServer == IPStatus.TimedOut)
                {
                    ServerConnection = false;
                    DeactivateToolStripLogin();
                    MessageBox.Show("Test Connessione al server fallito, Impossibile Raggiungere il Server", "Errore Riconnessione", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (statusTestServer == IPStatus.Unknown)
                {
                    ServerConnection = false;
                    DeactivateToolStripLogin();
                    MessageBox.Show("Il Server all IP fornito non supporta il Client Chat", "Server Errato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    ServerConnection = false;
                    DeactivateToolStripLogin();
                    MessageBox.Show("Errore nell inserimento dell indirizzo IP del server", "Errore IP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (NullReferenceException)
            {
                ServerConnection = false;
                DeactivateToolStripLogin();
                MessageBox.Show("Impossibile raggiungere il Server", "Errore Connessione al Server", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                ServerConnection = false;
                DeactivateToolStripLogin();
                MessageBox.Show(ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void disconnettiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ServerConnection)
            {
                if (UserID > 0)
                    esciToolStripMenuItem_Click(sender, e);
                ServerConnection = false;
                DeactivateContactsControls();

                DeactivateToolStripLogin();

                MessageBox.Show("Disconnessione dal server avvenuta con successo, ogni utente loggato è stato disconnesso", "Disconnessione Avvenuta", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Impossibile Disconnettere, nessuna connessione attiva !", "Errore Disconnessione", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void accediToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManagementWindow.MWindow m = new ManagementWindow.MWindow(ManagementWindow.Type.UserLogIn);
            m.ShowDialog();
            if (m.GetResult() != null)
            {
                try
                {
                    string res = m.GetResult();
                    res += ClientIP.ToString();
                    string[] datas = res.Split(new char[] { Protocols.DELIM }, Auth.ProtocolDataLenght, StringSplitOptions.RemoveEmptyEntries);
                    string strAuth = Auth.Assemble(datas[Auth.UserName], datas[Auth.Password], IPAddress.Parse(datas[Auth.IpUtente]));
                    string strServerRes = Connection.SendReceiveFromServer(ServerIP, strAuth);
                    strServerRes = Protocols.RemoveTerminator(strServerRes);

                    int tempUserID;
                    if (int.TryParse(strServerRes, out tempUserID))
                    {
                        if (tempUserID > 0)
                        {
                            UserID = tempUserID;
                            UserName = datas[Auth.UserName];

                            accediToolStripMenuItem.Enabled = false;
                            esciToolStripMenuItem.Enabled = true;
                            registratiToolStripMenuItem.Enabled = false;

                            ActivateContactsControls();

                            this.Text = $"Chat Client Scardi -> {UserName}";

                            MessageBox.Show($"Accesso effettuato con successo\r\nBenvenuto {UserName}", "Accesso Effettuato con Successo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            FillContactsList();

                        }
                        else
                            MessageBox.Show("Errore nell'Accesso, Impossibile ottenere l'ID dell utente", "Errore Accesso", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    else
                    {
                        ErrorsType error;
                        if (ErrorType.TryParse(strServerRes, out error))
                            MessageBox.Show(error.ToString(), "Errore Accesso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        else
                            MessageBox.Show("Nel server si è verificato un errore sconosciuto.", "Errore Sconosciuto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                catch (NullReferenceException)
                {
                    MessageBox.Show("Impossibile Contattare il Server", "Errore Comunicazione", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Errore Accesso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                MessageBox.Show("Errore nell'Accesso, Campi non Riempiti o Finestra chiusa", "Errore Accesso", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void esciToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string[] datas = { $"{UserID}" };
                string strDeAuth = DeAuth.Assemble(int.Parse(datas[DeAuth.UserID]));
                string strServerRes = Connection.SendReceiveFromServer(ServerIP, strDeAuth);
                strServerRes = Protocols.RemoveTerminator(strServerRes);

                if (Protocols.GetStatus(strServerRes))
                {
                    if (UserID > 0)
                    {
                        if (fetchNewMsg != null) fetchNewMsg.Enabled = false;
                        fetchNewMsg = null;

                        string tempUName = UserName;
                        UserID = -1;
                        UserName = null;

                        accediToolStripMenuItem.Enabled = true;
                        esciToolStripMenuItem.Enabled = false;
                        registratiToolStripMenuItem.Enabled = true;

                        DeactivateContactsControls();

                        this.Text = "Chat Client Scardi";
                        EmptyContactsList();

                        MessageBox.Show($"Disconnessione effettuata con successo\r\nArrivederci {tempUName}", "Disconnessione effettuata con successo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                        MessageBox.Show("Utente Già Disconnesso, Nessun Utente Collegato", "Errore Disconnessione", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else
                {
                    ErrorsType error;
                    if (ErrorType.TryParse(strServerRes, out error))
                        MessageBox.Show(error.ToString(), "Errore Disconnessione", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                        MessageBox.Show("Nel server si è verificato un errore sconosciuto.", "Errore Sconosciuto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Errore Accesso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void registratiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MWindow m = new MWindow(ManagementWindow.Type.UserRegistration);
            m.ShowDialog();
            if (m.GetResult() != null)
            {
                try
                {
                    string res = m.GetResult();
                    res += ClientIP.ToString();
                    string[] datas = res.Split(new char[] { Protocols.DELIM }, NewUserReg.ProtocolDataLenght, StringSplitOptions.RemoveEmptyEntries);
                    string strNewReg = NewUserReg.Assemble(datas[NewUserReg.UserName], datas[NewUserReg.Password], IPAddress.Parse(datas[NewUserReg.IpUtente]));
                    string strServerRes = Connection.SendReceiveFromServer(ServerIP, strNewReg);
                    strServerRes = Protocols.RemoveTerminator(strServerRes);

                    if (Protocols.GetStatus(strServerRes))
                    {
                        MessageBox.Show("Registrazione effettuata con successo, ora è possibile Accedere con le credenziali appena create", "Registrazione Effettuata con Successo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        ErrorsType error;
                        if (ErrorType.TryParse(strServerRes, out error))
                            MessageBox.Show(error.ToString(), "Errore Registrazione Nuovo Utente", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        else
                            MessageBox.Show("Nel server si è verificato un errore sconosciuto.", "Errore Sconosciuto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Errore Registrazione", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                MessageBox.Show("Errore nella Registrazione, Campi non Riempiti o Finestra chiusa", "Errore Registrazione", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void aggiornaContattiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FillContactsList();
        }

        private void aggiungiContattoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnAddContact_Click(sender, e);
        }

        private void interrompiTimerMessaggiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (usoTimerFetchNewMsg)
            {
                if (fetchNewMsg != null) fetchNewMsg.Enabled = false;
                fetchNewMsg = null;

                usoTimerFetchNewMsg = false;
                interrompiTimerMessaggiToolStripMenuItem.Checked = true;
                //interrompiTimerMessaggiToolStripMenuItem.Text = "Avvia Timer Messaggi";
            }
            else
            {
                usoTimerFetchNewMsg = true;
                interrompiTimerMessaggiToolStripMenuItem.Checked = false;
                //interrompiTimerMessaggiToolStripMenuItem.Text = "Interrompi Timer Messaggi";
            }
        }

        private void btnAddContact_Click(object sender, EventArgs e)
        {
            ManagementWindow.MWindow m = new ManagementWindow.MWindow(ManagementWindow.Type.NewContact);
            m.ShowDialog();
            if (m.GetResult() != null)
            {
                if (UserID > 0)
                {
                    string res = UserID.ToString() + m.GetResult();
                    string[] datas = res.Split(new char[] { Protocols.DELIM }, NewContact.ProtocolDataLenght, StringSplitOptions.RemoveEmptyEntries);
                    string strNewContact = NewContact.Assemble(int.Parse(datas[NewContact.UserID]), datas[NewContact.UserContactName]);
                    string strServerRes = Connection.SendReceiveFromServer(ServerIP, strNewContact);
                    strServerRes = Protocols.RemoveTerminator(strServerRes);

                    if (Protocols.GetStatus(strServerRes))
                    {
                        MessageBox.Show("Nuovo Contatto aggiunto con successo alla lista contatti personale", "Contatto Aggiunto con Successo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        FillContactsList();
                    }
                    else
                    {
                        ErrorsType error;
                        if (ErrorType.TryParse(strServerRes, out error))
                            MessageBox.Show(error.ToString(), "Errore Aggiunta Nuovo Contatto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        else
                            MessageBox.Show("Nel server si è verificato un errore sconosciuto.", "Errore Sconosciuto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    esciToolStripMenuItem_Click(sender, e);
                    MessageBox.Show("ERRORE: l'ID associato all utente è invalido.\r\nQuesto potrebbe essere perche si è effettuato il logout o il server potrebbe aver riscontrato un errore.\r\nPer Sicurezza dei propri dati è stato effettuato il logout forzato.", "Errore ID Utente", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            /*
             * Invio al server il nome del nuovo contatto e mi aspetto lo status della azione
             * Confermo o meno tutto quanto con un messagebox per l'utente
             */
        }

        private void lstContacts_SelectedIndexChanged(object sender, EventArgs e)
        {
            const int POSXL = 10, 
                STARTPOSY = 10, 
                MSGMAXLENGTH = 70, 
                DELTAMSGMAXL = 5, 
                LBLPADDING = 3, 
                MSGWIDTHFIT = 422, //La costante di lunghezza che riempie senza sbordare bene la riga di un messaggio allineato a destra
                DELTAXRMOVER_MSGWIDTHFIT = 50;

            try
            {
                indexContactSelected = -1;
                if (usoTimerFetchNewMsg)
                {
                    if (fetchNewMsg != null) fetchNewMsg.Enabled = false;
                    fetchNewMsg = null;
                    fetchNewMsg = new Timer();
                    fetchNewMsg.Interval = 2000;
                    fetchNewMsg.Tick += FetchMsg_Tick;
                }

                int indexContact = lstContacts.SelectedIndex;
                string[] datas = { UserID.ToString() , UserContacts[indexContact].ID.ToString() };
                string strContacts = LoadContact.Assemble(int.Parse(datas[LoadContact.UserID]), int.Parse(datas[LoadContact.ContactID]));
                string strServerRes = Connection.SendReceiveFromServer(ServerIP, strContacts);
                strServerRes = Protocols.RemoveTerminator(strServerRes);

                boxChat.Visible = false;
                if (!Protocols.VerifyServerError(strServerRes))
                {
                    UserContacts[indexContact].Messages = XmlParser.ParseMessages(strServerRes);

                    boxChat.Text = $"Chat con: {UserContacts[indexContact].UserName}";
                    panelViewMsg.Controls.Clear();

                    List<Label> messages = new List<Label>();
                    int deltaXR = 0, posY = STARTPOSY, msgWidth = 0;
                    int strRow = 0, adderHeigth = 30;
                    string msgText = String.Empty;

                    foreach (LibChatClient.User.Message message in UserContacts[indexContact].Messages)
                    {
                        msgText = String.Empty;
                        if (message.Text.Length > MSGMAXLENGTH + DELTAMSGMAXL)
                        {
                            int newLineToInsert = message.Text.Length / (MSGMAXLENGTH + DELTAMSGMAXL);
                            for (strRow = 0; strRow < newLineToInsert; strRow++)
                            {
                                msgText += message.Text.Substring((MSGMAXLENGTH * strRow), (MSGMAXLENGTH * (strRow + 1)) - (MSGMAXLENGTH * strRow)) + "\r\n";
                            }
                            msgText += message.Text.Substring(MSGMAXLENGTH * strRow);
                        }
                        else
                        {
                            msgText = message.Text;
                        }

                        Label tempLblMessage = new Label();
                        tempLblMessage.Font = new Font(new FontFamily("Consolas"), 8.50f);

                        tempLblMessage.MouseHover += (a, b) =>
                        {
                            toolTipMessages.Show($"ID: {message.ID}\r\nMitt: {(message.UserSenderID == UserID ? UserName : UserContacts[indexContact].UserName)}\r\nDest: {(message.ContactReceiverID == UserID ? UserName : UserContacts[indexContact].UserName)}\r\nDT invio: {message.DateTimeSend}", tempLblMessage);
                        };
                        tempLblMessage.Text = msgText;
                        tempLblMessage.AutoSize = true;
                        tempLblMessage.Padding = new Padding(LBLPADDING);
                        tempLblMessage.BorderStyle = BorderStyle.FixedSingle;

                        if (message.UserSenderID == UserID)
                        {
                            using (Graphics g = CreateGraphics())
                            {
                                SizeF size = g.MeasureString(tempLblMessage.Text, tempLblMessage.Font);
                                msgWidth = (int)Math.Ceiling(size.Width);
                                deltaXR = msgWidth * DELTAXRMOVER_MSGWIDTHFIT / MSGWIDTHFIT;
                                if (deltaXR < DELTAXRMOVER_MSGWIDTHFIT) deltaXR += 10;
                            }
                            tempLblMessage.Location = new Point(panelViewMsg.Size.Width - (msgWidth + deltaXR), posY);
                        }
                        else
                            tempLblMessage.Location = new Point(POSXL, posY);

                        tempLblMessage.Anchor = message.ContactReceiverID == UserID ? AnchorStyles.Top | AnchorStyles.Left : AnchorStyles.Top | AnchorStyles.Right;

                        posY += (adderHeigth * (strRow > 0 ? strRow : 1));
                        messages.Add(tempLblMessage);
                        strRow = 0;
                    }

                    panelViewMsg.Controls.AddRange(messages.ToArray());
                    boxChat.Visible = true;
                    panelViewMsg.AutoScrollPosition = new Point(0,boxChat.Height);
                    lastHeightMsg = posY;
                    lastFetchNewMsg = DateTime.Now;
                    indexContactSelected = indexContact;
                    if (usoTimerFetchNewMsg) fetchNewMsg.Enabled = true;
                }
                else
                {
                    ErrorsType error;
                    if (ErrorType.TryParse(strServerRes, out error))
                    {
                        if (error == ErrorsType.LoadContactMessageEmpty)
                        {
                            boxChat.Text = $"Chat con: {UserContacts[indexContact].UserName}";

                            panelViewMsg.Controls.Clear();

                            Label tempLblMessage = new Label();
                            tempLblMessage.Text = "Nessun messaggio presente in questa chat.\r\nPer iniziare la conversazione scrivi nella casella qua sotto e invia il messaggio";
                            tempLblMessage.TextAlign = ContentAlignment.MiddleCenter;
                            tempLblMessage.AutoSize = true;
                            tempLblMessage.Padding = new Padding(LBLPADDING);
                            tempLblMessage.BorderStyle = BorderStyle.FixedSingle;
                            tempLblMessage.Location = new Point(panelViewMsg.Size.Width / 2 - tempLblMessage.Size.Width, STARTPOSY);

                            panelViewMsg.Controls.Add(tempLblMessage);
                            boxChat.Visible = true;
                            lastHeightMsg = 0;
                            lastFetchNewMsg = DateTime.MinValue;
                            indexContactSelected = indexContact;
                            if (usoTimerFetchNewMsg) fetchNewMsg.Enabled = true;
                        }
                        else
                            MessageBox.Show(error.ToString() + " per il contatto selezionato", "Errore ottenimento nuovi messaggi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                        MessageBox.Show("Nel server si è verificato un errore sconosciuto.", "Errore Sconosciuto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Errore Ottenimento Messaggi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void FetchMsg_Tick(object sender, EventArgs e)
        {
            const int POSXL = 10,
                   STARTPOSY = 10,
                   MSGMAXLENGTH = 70,
                   DELTAMSGMAXL = 5,
                   LBLPADDING = 3,
                   MSGWIDTHFIT = 422, //La costante di lunghezza che riempie senza sbordare bene la riga di un messaggio allineato a destra
                   DELTAXRMOVER_MSGWIDTHFIT = 50;

            try
            {
                string[] datas = { UserID.ToString(), UserContacts[indexContactSelected].ID.ToString() };
                string strContacts = ReqNewMsg.Assemble(int.Parse(datas[ReqNewMsg.UserID]), int.Parse(datas[ReqNewMsg.ContactID]), lastFetchNewMsg);
                string strServerRes = Connection.SendReceiveFromServer(ServerIP, strContacts);
                strServerRes = Protocols.RemoveTerminator(strServerRes);

                if (!Protocols.VerifyServerError(strServerRes))
                {
                    UserContacts[indexContactSelected].Messages = XmlParser.ParseMessages(strServerRes);

                    List<Label> messages = new List<Label>();
                    int deltaXR = 0, posY = STARTPOSY, msgWidth = 0;
                    int strRow = lastHeightMsg, adderHeigth = 30;
                    string msgText = String.Empty;

                    foreach (LibChatClient.User.Message message in UserContacts[indexContactSelected].Messages)
                    {
                        msgText = String.Empty;
                        if (message.Text.Length > MSGMAXLENGTH + DELTAMSGMAXL)
                        {
                            int newLineToInsert = message.Text.Length / (MSGMAXLENGTH + DELTAMSGMAXL);
                            for (strRow = 0; strRow < newLineToInsert; strRow++)
                            {
                                msgText += message.Text.Substring((MSGMAXLENGTH * strRow), (MSGMAXLENGTH * (strRow + 1)) - (MSGMAXLENGTH * strRow)) + "\r\n";
                            }
                            msgText += message.Text.Substring(MSGMAXLENGTH * strRow);
                        }
                        else
                        {
                            msgText = message.Text;
                        }

                        Label tempLblMessage = new Label();
                        tempLblMessage.Font = new Font(new FontFamily("Consolas"), 8.50f);

                        tempLblMessage.MouseHover += (a, b) =>
                        {
                            toolTipMessages.Show($"ID: {message.ID}\r\nMitt: {(message.UserSenderID == UserID ? UserName : UserContacts[indexContactSelected].UserName)}\r\nDest: {(message.ContactReceiverID == UserID ? UserName : UserContacts[indexContactSelected].UserName)}\r\nDT invio: {message.DateTimeSend}", tempLblMessage);
                        };
                        tempLblMessage.Text = msgText;
                        tempLblMessage.AutoSize = true;
                        tempLblMessage.Padding = new Padding(LBLPADDING);
                        tempLblMessage.BorderStyle = BorderStyle.FixedSingle;

                        if (message.UserSenderID == UserID)
                        {
                            using (Graphics g = CreateGraphics())
                            {
                                SizeF size = g.MeasureString(tempLblMessage.Text, tempLblMessage.Font);
                                msgWidth = (int)Math.Ceiling(size.Width);
                                deltaXR = msgWidth * DELTAXRMOVER_MSGWIDTHFIT / MSGWIDTHFIT;
                                if (deltaXR < DELTAXRMOVER_MSGWIDTHFIT) deltaXR += 10;
                            }
                            tempLblMessage.Location = new Point(panelViewMsg.Size.Width - (msgWidth + deltaXR), posY);
                        }
                        else
                            tempLblMessage.Location = new Point(POSXL, posY);

                        tempLblMessage.Anchor = message.ContactReceiverID == UserID ? AnchorStyles.Top | AnchorStyles.Left : AnchorStyles.Top | AnchorStyles.Right;

                        posY += (adderHeigth * (strRow > 0 ? strRow : 1));
                        messages.Add(tempLblMessage);
                        strRow = 0;
                    }

                    panelViewMsg.Controls.AddRange(messages.ToArray());
                    panelViewMsg.AutoScrollPosition = new Point(0, boxChat.Height);
                    lastHeightMsg = posY;
                    lastFetchNewMsg = DateTime.Now;
                }
                else
                {
                    ErrorsType error;
                    if (ErrorType.TryParse(strServerRes, out error))
                    {
                        if (error == ErrorsType.LoadContactMessageEmpty)
                        {
                            boxChat.Text = $"Chat con: {UserContacts[indexContactSelected].UserName}";

                            panelViewMsg.Controls.Clear();

                            Label tempLblMessage = new Label();
                            tempLblMessage.Text = "Nessun messaggio presente in questa chat.\r\nPer iniziare la conversazione scrivi nella casella qua sotto e invia il messaggio";
                            tempLblMessage.TextAlign = ContentAlignment.MiddleCenter;
                            tempLblMessage.AutoSize = true;
                            tempLblMessage.Padding = new Padding(LBLPADDING);
                            tempLblMessage.BorderStyle = BorderStyle.FixedSingle;
                            tempLblMessage.Location = new Point(panelViewMsg.Size.Width / 2 - tempLblMessage.Size.Width, STARTPOSY);

                            panelViewMsg.Controls.Add(tempLblMessage);
                            lastHeightMsg = 0;
                            lastFetchNewMsg = DateTime.MinValue;
                        }
                        else if (error == ErrorsType.ReqNewMsgNoNewMsg)
                        { }
                        else
                            MessageBox.Show(error.ToString() + " per il contatto selezionato", "Errore ottenimento nuovi messaggi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                        MessageBox.Show("Nel server si è verificato un errore sconosciuto.", "Errore Sconosciuto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Errore ottenimento nuovi messaggi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSendNewMsg_Click(object sender, EventArgs e)
        {
            try
            {
                if (indexContactSelected >= 0)
                {
                    LibChatClient.User.Message newMsg;
                    string msgText = inputNewMsg.Text;
                    newMsg = new LibChatClient.User.Message(-1, UserID, UserContacts[indexContactSelected].ID, DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss")), msgText);
                    string strContacts = SndMsg.Assemble(newMsg);
                    string strServerRes = Connection.SendReceiveFromServer(ServerIP, strContacts);
                    strServerRes = Protocols.RemoveTerminator(strServerRes);

                    if (Protocols.GetStatus(strServerRes))
                    {
                        inputNewMsg.Text = String.Empty;
                        lstContacts_SelectedIndexChanged(sender, e);
                    }
                    else
                    {
                        ErrorsType error;
                        if (ErrorType.TryParse(strServerRes, out error))
                            MessageBox.Show(error.ToString(), "Errore Invio Messaggio", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        else
                            MessageBox.Show("Nel server si è verificato un errore sconosciuto.", "Errore Sconosciuto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                    MessageBox.Show("Prima di Inviare il Messaggio Selezionare un contatto a cui mandarlo adalla barra contatti a sinistra.", "Errore Invio Messaggio", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Errore Invio Messaggio", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
