using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManagementWindow
{
    public partial class MWindow : Form
    {
        Type windowType;
        Dictionary<string, Label> lblDesc = new Dictionary<string, Label>();
        Dictionary<string, TextBox> txtInput = new Dictionary<string, TextBox>();
        Button btnConfirm;
        string output = null;

        internal MWindow()
        {
            InitializeComponent();
        }

        public MWindow(Type WindowType)
        {
            InitializeComponent();

            this.Width = 350;
            this.Height = 350;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            windowType = WindowType;
        }

        public MWindow(int Width, int Height, Type WindowType)
        {
            InitializeComponent();

            this.Width = Width;
            this.Height = Height;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            windowType = WindowType;
        }

        private void ManagementWindow_Load(object sender, EventArgs e)
        {
            switch (windowType)
            {
                case Type.Connection:
                    {
                        this.Text = "Connessione Al Server";
                        lblDesc.Add("descConn", new Label());
                        lblDesc["descConn"].Text = "Inserisci qui l'IP del Chat Server";
                        lblDesc["descConn"].AutoSize = true;
                        lblDesc["descConn"].Location = new Point((this.Width / 2) - (lblDesc["descConn"].Size.Width) + 10, (this.Height / 2) - (lblDesc["descConn"].Size.Height) - 30);
                        txtInput.Add("inputConnIPAddress", new TextBox());
                        txtInput["inputConnIPAddress"].Size = lblDesc["descConn"].Size;
                        txtInput["inputConnIPAddress"].Location = new Point((this.Width / 2) - (lblDesc["descConn"].Size.Width) + 35, (this.Height / 2) - (lblDesc["descConn"].Size.Height));
                        btnConfirm = new Button();
                        btnConfirm.Text = "Connetti";
                        btnConfirm.Location = new Point((this.Width / 2) - (btnConfirm.Size.Width) + 30, (this.Height / 2) - (btnConfirm.Size.Height) + 30);
                        btnConfirm.Click += BtnConfirm_Click;
                        this.Controls.Add(lblDesc["descConn"]);
                        this.Controls.Add(txtInput["inputConnIPAddress"]);
                        this.Controls.Add(btnConfirm);
                    }
                    break;
                case Type.UserRegistration:
                    {
                        this.Text = "Registrazione Nuovo Utente";
                        lblDesc.Add("descUserReg", new Label());
                        lblDesc["descUserReg"].Text = "Inserisci qui le informazioni per creare un Nuovo Utente: \r\n Username, Password e Ripetizione Password";
                        lblDesc["descUserReg"].AutoSize = true;
                        lblDesc["descUserReg"].Location = new Point((this.Width / 2) - (lblDesc["descUserReg"].Size.Width) - 40, (this.Height / 2) - (lblDesc["descUserReg"].Size.Height) - 35);

                        txtInput.Add("inputUName", new TextBox());
                        txtInput["inputUName"].Location = new Point((this.Width / 2) - (lblDesc["descUserReg"].Size.Width) + 70, (this.Height / 2) - (lblDesc["descUserReg"].Size.Height));
                        lblDesc.Add("descInputUname", new Label());
                        lblDesc["descInputUname"].Text = "Username";
                        lblDesc["descInputUname"].AutoSize = true;
                        lblDesc["descInputUname"].Location = new Point((this.Width / 2) - (lblDesc["descUserReg"].Size.Width), (this.Height / 2) - (lblDesc["descUserReg"].Size.Height) + 2);

                        txtInput.Add("inputPwd", new TextBox());
                        txtInput["inputPwd"].UseSystemPasswordChar = true;
                        txtInput["inputPwd"].PasswordChar = '*';
                        txtInput["inputPwd"].Location = new Point((this.Width / 2) - (lblDesc["descUserReg"].Size.Width) + 70, (this.Height / 2) - (lblDesc["descUserReg"].Size.Height) + 25);
                        lblDesc.Add("descInputPwd", new Label());
                        lblDesc["descInputPwd"].Text = "Password";
                        lblDesc["descInputPwd"].AutoSize = true;
                        lblDesc["descInputPwd"].Location = new Point((this.Width / 2) - (lblDesc["descUserReg"].Size.Width), (this.Height / 2) - (lblDesc["descUserReg"].Size.Height) + 25 + 2);

                        txtInput.Add("inputRPwd", new TextBox());
                        txtInput["inputRPwd"].UseSystemPasswordChar = true;
                        txtInput["inputRPwd"].PasswordChar = '*';
                        txtInput["inputRPwd"].Location = new Point((this.Width / 2) - (lblDesc["descUserReg"].Size.Width) + 70, (this.Height / 2) - (lblDesc["descUserReg"].Size.Height) + 50);
                        lblDesc.Add("descInputRPwd", new Label());
                        lblDesc["descInputRPwd"].Text = "Ripeti Password";
                        lblDesc["descInputRPwd"].AutoSize = true;
                        lblDesc["descInputRPwd"].Location = new Point((this.Width / 2) - (lblDesc["descUserReg"].Size.Width) - 10, (this.Height / 2) - (lblDesc["descUserReg"].Size.Height) + 50 + 2);

                        btnConfirm = new Button();
                        btnConfirm.Text = "Registrati";
                        btnConfirm.Location = new Point((this.Width / 2) - (btnConfirm.Size.Width) + 30, (this.Height / 2) - (btnConfirm.Size.Height) + 80);
                        btnConfirm.Click += BtnConfirm_Click;
                        this.Controls.Add(lblDesc["descUserReg"]);

                        this.Controls.Add(txtInput["inputUName"]);
                        this.Controls.Add(lblDesc["descInputUname"]);

                        this.Controls.Add(txtInput["inputPwd"]);
                        this.Controls.Add(lblDesc["descInputPwd"]);

                        this.Controls.Add(txtInput["inputRPwd"]);
                        this.Controls.Add(lblDesc["descInputRPwd"]);

                        this.Controls.Add(btnConfirm);
                    }
                    break;
                case Type.UserLogIn:
                    {
                        this.Text = "Accesso Utente";
                        lblDesc.Add("descUserLogIn", new Label());
                        lblDesc["descUserLogIn"].Text = "Inserisci qui le informazioni per accedere";
                        lblDesc["descUserLogIn"].AutoSize = true;
                        lblDesc["descUserLogIn"].Location = new Point((this.Width / 2) - (lblDesc["descUserLogIn"].Size.Width) + 20, (this.Height / 2) - (lblDesc["descUserLogIn"].Size.Height) - 30);

                        txtInput.Add("inputUName", new TextBox());
                        txtInput["inputUName"].Location = new Point((this.Width / 2) - (lblDesc["descUserLogIn"].Size.Width) + 70, (this.Height / 2) - (lblDesc["descUserLogIn"].Size.Height));
                        lblDesc.Add("descInputUName", new Label());
                        lblDesc["descInputUName"].Text = "Username";
                        lblDesc["descInputUName"].AutoSize = true;
                        lblDesc["descInputUName"].Location = new Point((this.Width / 2) - (lblDesc["descUserLogIn"].Size.Width), (this.Height / 2) - (lblDesc["descUserLogIn"].Size.Height) + 2);

                        txtInput.Add("inputPwd", new TextBox());
                        txtInput["inputPwd"].UseSystemPasswordChar = true;
                        txtInput["inputPwd"].PasswordChar = '*';
                        txtInput["inputPwd"].Location = new Point((this.Width / 2) - (lblDesc["descUserLogIn"].Size.Width) + 70, (this.Height / 2) - (lblDesc["descUserLogIn"].Size.Height) + 25);
                        lblDesc.Add("descInputPwd", new Label());
                        lblDesc["descInputPwd"].Text = "Password";
                        lblDesc["descInputPwd"].AutoSize = true;
                        lblDesc["descInputPwd"].Location = new Point((this.Width / 2) - (lblDesc["descUserLogIn"].Size.Width), (this.Height / 2) - (lblDesc["descUserLogIn"].Size.Height) + 25 + 2);

                        btnConfirm = new Button();
                        btnConfirm.Text = "Accedi";
                        btnConfirm.Location = new Point((this.Width / 2) - (btnConfirm.Size.Width) + 30, (this.Height / 2) - (btnConfirm.Size.Height) + 55);
                        btnConfirm.Click += BtnConfirm_Click;
                        this.Controls.Add(lblDesc["descUserLogIn"]);

                        this.Controls.Add(lblDesc["descInputUName"]);
                        this.Controls.Add(txtInput["inputUName"]);

                        this.Controls.Add(lblDesc["descInputPwd"]);
                        this.Controls.Add(txtInput["inputPwd"]);

                        this.Controls.Add(btnConfirm);
                    }
                    break;
                case Type.NewContact:
                    {
                        this.Text = "Aggiunta Nuovo Contatto";
                        lblDesc.Add("descNewContact", new Label());
                        lblDesc["descNewContact"].Text = "Inserisci qui il Nome Del nuovo contatto che vuoi aggiungere";
                        lblDesc["descNewContact"].AutoSize = true;
                        lblDesc["descNewContact"].Location = new Point((this.Width / 2) - (lblDesc["descNewContact"].Size.Width) -50, (this.Height / 2) - (lblDesc["descNewContact"].Size.Height) - 30);
                        txtInput.Add("inputContactUname", new TextBox());
                        txtInput["inputContactUname"].Size = lblDesc["descNewContact"].Size;
                        txtInput["inputContactUname"].Location = new Point((this.Width / 2) - (lblDesc["descNewContact"].Size.Width) + 35, (this.Height / 2) - (lblDesc["descNewContact"].Size.Height));
                        btnConfirm = new Button();
                        btnConfirm.Text = "Aggiungi";
                        btnConfirm.Location = new Point((this.Width / 2) - (btnConfirm.Size.Width) + 30, (this.Height / 2) - (btnConfirm.Size.Height) + 30);
                        btnConfirm.Click += BtnConfirm_Click;
                        this.Controls.Add(lblDesc["descNewContact"]);
                        this.Controls.Add(txtInput["inputContactUname"]);
                        this.Controls.Add(btnConfirm);
                    }
                    break;
            }
        }

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            switch (windowType)
            {
                case Type.Connection:
                    {
                        try
                        {
                            IPAddress serverIP = IPAddress.Parse(txtInput["inputConnIPAddress"].Text.Trim());
                            Ping testIP = new Ping();
                            PingReply resTestIP = testIP.Send(serverIP);
                            //output = resTestIP.Status == IPStatus.Success ? serverIP.ToString() : string.Empty;

                            if (resTestIP.Status == IPStatus.Success)
                            {
                                output = serverIP.ToString();
                                MessageBox.Show("Connessione al server avvenuta con successo!", "Connessione Avvenuta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Close();
                            }
                            else if (resTestIP.Status == IPStatus.DestinationHostUnreachable || resTestIP.Status == IPStatus.DestinationNetworkUnreachable || resTestIP.Status == IPStatus.TimedOut)
                                MessageBox.Show("Impossibile raggiungere il Server", "Errore IP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            else
                                MessageBox.Show("Errore nell inserimento dell indirizzo IP del server", "Errore IP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    break;
                case Type.UserRegistration:
                    {
                        string userName = String.Empty;
                        string password = String.Empty;

                        if (txtInput["inputUName"].Text == String.Empty || txtInput["inputPwd"].Text == String.Empty || txtInput["inputRPwd"].Text == String.Empty)
                        {
                            MessageBox.Show("Campi vuoti, riempire tutti i campi e riprovare", "Errore: Campi vuoti");
                            return;
                        }

                        if (!String.Equals(txtInput["inputPwd"].Text, txtInput["inputRPwd"].Text))
                        {
                            MessageBox.Show("Le password non coincidono", "Errore Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        userName = txtInput["inputUName"].Text;
                        password = txtInput["inputPwd"].Text;

                        userName.Replace((char)32, '_');
                        output = $"{userName},{password},";
                        this.Close();
                    }
                    break;
                case Type.UserLogIn:
                    {
                        string userName = String.Empty;
                        string password = String.Empty;

                        if (txtInput["inputUName"].Text == String.Empty || txtInput["inputPwd"].Text == String.Empty)
                        {
                            MessageBox.Show("Campi vuoti, riempire tutti i campi e riprovare", "Errore: Campi vuoti");
                            return;
                        }

                        userName = txtInput["inputUName"].Text;
                        password = txtInput["inputPwd"].Text;

                        userName.Replace((char)32, '_');
                        output = $"{userName},{password},";
                        this.Close();
                    }
                    break;
                case Type.NewContact:
                    {
                        string userNameContact = String.Empty;

                        if (txtInput["inputContactUname"].Text == String.Empty)
                        {
                            MessageBox.Show("Campi vuoti, riempire tutti i campi e riprovare", "Errore: Campi vuoti");
                            return;
                        }

                        userNameContact = txtInput["inputContactUname"].Text.Trim();

                        userNameContact.Replace((char)32, '_');
                        output = $",{userNameContact}";
                        this.Close();
                    }
                    break;
            }
        }

        public string GetResult()
        {
            return output;
        }
    }

    public enum Type
    {
        Connection = 50,
        UserLogIn = 100,
        UserRegistration = 150,
        NewContact = 300
    }
}
