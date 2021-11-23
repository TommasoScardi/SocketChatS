
namespace ChatClient_Scardi
{
    partial class ChatClient
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.connessioneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nuovaConnessioneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.connettiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disconnettiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.utenteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.accediToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.esciToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.registratiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contattiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aggiornaContattiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.aggiungiContattoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainElement = new System.Windows.Forms.TableLayoutPanel();
            this.boxContacts = new System.Windows.Forms.GroupBox();
            this.lstContacts = new System.Windows.Forms.ListBox();
            this.btnAddContact = new System.Windows.Forms.Button();
            this.boxChat = new System.Windows.Forms.GroupBox();
            this.panelViewMsg = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panelSendMsg = new System.Windows.Forms.TableLayoutPanel();
            this.inputNewMsg = new System.Windows.Forms.RichTextBox();
            this.btnSendNewMsg = new System.Windows.Forms.Button();
            this.toolTipMessages = new System.Windows.Forms.ToolTip(this.components);
            this.mainMenu.SuspendLayout();
            this.mainElement.SuspendLayout();
            this.boxContacts.SuspendLayout();
            this.boxChat.SuspendLayout();
            this.panelViewMsg.SuspendLayout();
            this.panelSendMsg.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connessioneToolStripMenuItem,
            this.utenteToolStripMenuItem,
            this.contattiToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(800, 24);
            this.mainMenu.TabIndex = 0;
            this.mainMenu.Text = "Main Menu";
            // 
            // connessioneToolStripMenuItem
            // 
            this.connessioneToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nuovaConnessioneToolStripMenuItem,
            this.toolStripSeparator2,
            this.connettiToolStripMenuItem,
            this.disconnettiToolStripMenuItem});
            this.connessioneToolStripMenuItem.Name = "connessioneToolStripMenuItem";
            this.connessioneToolStripMenuItem.Size = new System.Drawing.Size(87, 20);
            this.connessioneToolStripMenuItem.Text = "Connessione";
            // 
            // nuovaConnessioneToolStripMenuItem
            // 
            this.nuovaConnessioneToolStripMenuItem.Name = "nuovaConnessioneToolStripMenuItem";
            this.nuovaConnessioneToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.nuovaConnessioneToolStripMenuItem.Text = "Nuova Connessione ...";
            this.nuovaConnessioneToolStripMenuItem.Click += new System.EventHandler(this.nuovaConnessioneToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(189, 6);
            // 
            // connettiToolStripMenuItem
            // 
            this.connettiToolStripMenuItem.Name = "connettiToolStripMenuItem";
            this.connettiToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.connettiToolStripMenuItem.Text = "Connetti Server";
            this.connettiToolStripMenuItem.Click += new System.EventHandler(this.connettiToolStripMenuItem_Click);
            // 
            // disconnettiToolStripMenuItem
            // 
            this.disconnettiToolStripMenuItem.Name = "disconnettiToolStripMenuItem";
            this.disconnettiToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.disconnettiToolStripMenuItem.Text = "Disconnetti Server";
            this.disconnettiToolStripMenuItem.Click += new System.EventHandler(this.disconnettiToolStripMenuItem_Click);
            // 
            // utenteToolStripMenuItem
            // 
            this.utenteToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.accediToolStripMenuItem,
            this.esciToolStripMenuItem,
            this.toolStripSeparator1,
            this.registratiToolStripMenuItem});
            this.utenteToolStripMenuItem.Name = "utenteToolStripMenuItem";
            this.utenteToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.utenteToolStripMenuItem.Text = "Utente";
            // 
            // accediToolStripMenuItem
            // 
            this.accediToolStripMenuItem.Name = "accediToolStripMenuItem";
            this.accediToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.accediToolStripMenuItem.Text = "Accedi";
            this.accediToolStripMenuItem.Click += new System.EventHandler(this.accediToolStripMenuItem_Click);
            // 
            // esciToolStripMenuItem
            // 
            this.esciToolStripMenuItem.Name = "esciToolStripMenuItem";
            this.esciToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.esciToolStripMenuItem.Text = "Esci";
            this.esciToolStripMenuItem.Click += new System.EventHandler(this.esciToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(120, 6);
            // 
            // registratiToolStripMenuItem
            // 
            this.registratiToolStripMenuItem.Name = "registratiToolStripMenuItem";
            this.registratiToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.registratiToolStripMenuItem.Text = "Registrati";
            this.registratiToolStripMenuItem.Click += new System.EventHandler(this.registratiToolStripMenuItem_Click);
            // 
            // contattiToolStripMenuItem
            // 
            this.contattiToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aggiornaContattiToolStripMenuItem,
            this.toolStripSeparator3,
            this.aggiungiContattoToolStripMenuItem});
            this.contattiToolStripMenuItem.Name = "contattiToolStripMenuItem";
            this.contattiToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.contattiToolStripMenuItem.Text = "Contatti";
            // 
            // aggiornaContattiToolStripMenuItem
            // 
            this.aggiornaContattiToolStripMenuItem.Name = "aggiornaContattiToolStripMenuItem";
            this.aggiornaContattiToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.aggiornaContattiToolStripMenuItem.Text = "Aggiorna Contatti";
            this.aggiornaContattiToolStripMenuItem.Click += new System.EventHandler(this.aggiornaContattiToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(170, 6);
            // 
            // aggiungiContattoToolStripMenuItem
            // 
            this.aggiungiContattoToolStripMenuItem.Name = "aggiungiContattoToolStripMenuItem";
            this.aggiungiContattoToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.aggiungiContattoToolStripMenuItem.Text = "Aggiungi Contatto";
            this.aggiungiContattoToolStripMenuItem.Click += new System.EventHandler(this.aggiungiContattoToolStripMenuItem_Click);
            // 
            // mainElement
            // 
            this.mainElement.ColumnCount = 2;
            this.mainElement.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.mainElement.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 571F));
            this.mainElement.Controls.Add(this.boxContacts, 0, 0);
            this.mainElement.Controls.Add(this.boxChat, 1, 0);
            this.mainElement.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainElement.Location = new System.Drawing.Point(0, 24);
            this.mainElement.Name = "mainElement";
            this.mainElement.RowCount = 1;
            this.mainElement.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainElement.Size = new System.Drawing.Size(800, 426);
            this.mainElement.TabIndex = 1;
            // 
            // boxContacts
            // 
            this.boxContacts.Controls.Add(this.lstContacts);
            this.boxContacts.Controls.Add(this.btnAddContact);
            this.boxContacts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.boxContacts.Location = new System.Drawing.Point(3, 3);
            this.boxContacts.Name = "boxContacts";
            this.boxContacts.Size = new System.Drawing.Size(200, 420);
            this.boxContacts.TabIndex = 0;
            this.boxContacts.TabStop = false;
            this.boxContacts.Text = "Contatti";
            // 
            // lstContacts
            // 
            this.lstContacts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstContacts.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstContacts.FormattingEnabled = true;
            this.lstContacts.ItemHeight = 16;
            this.lstContacts.Location = new System.Drawing.Point(3, 43);
            this.lstContacts.Name = "lstContacts";
            this.lstContacts.Size = new System.Drawing.Size(194, 374);
            this.lstContacts.TabIndex = 1;
            this.lstContacts.SelectedIndexChanged += new System.EventHandler(this.lstContacts_SelectedIndexChanged);
            // 
            // btnAddContact
            // 
            this.btnAddContact.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnAddContact.Location = new System.Drawing.Point(3, 16);
            this.btnAddContact.Name = "btnAddContact";
            this.btnAddContact.Size = new System.Drawing.Size(194, 27);
            this.btnAddContact.TabIndex = 0;
            this.btnAddContact.Text = "Aggiungi Contatto";
            this.btnAddContact.UseVisualStyleBackColor = true;
            this.btnAddContact.Click += new System.EventHandler(this.btnAddContact_Click);
            // 
            // boxChat
            // 
            this.boxChat.Controls.Add(this.panelViewMsg);
            this.boxChat.Controls.Add(this.panelSendMsg);
            this.boxChat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.boxChat.Location = new System.Drawing.Point(209, 3);
            this.boxChat.Name = "boxChat";
            this.boxChat.Size = new System.Drawing.Size(588, 420);
            this.boxChat.TabIndex = 1;
            this.boxChat.TabStop = false;
            this.boxChat.Text = "Chat con:";
            // 
            // panelViewMsg
            // 
            this.panelViewMsg.AutoScroll = true;
            this.panelViewMsg.AutoScrollMargin = new System.Drawing.Size(0, 5);
            this.panelViewMsg.Controls.Add(this.label4);
            this.panelViewMsg.Controls.Add(this.label2);
            this.panelViewMsg.Controls.Add(this.label3);
            this.panelViewMsg.Controls.Add(this.label1);
            this.panelViewMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelViewMsg.Location = new System.Drawing.Point(3, 16);
            this.panelViewMsg.Name = "panelViewMsg";
            this.panelViewMsg.Size = new System.Drawing.Size(582, 360);
            this.panelViewMsg.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(560, 97);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(14, 14);
            this.label4.TabIndex = 2;
            this.label4.Text = "A";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(230, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(342, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Testo di prova ricevuto da un contatto kjbasklbloaregboiergnlaebngilgn";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AccessibleDescription = "";
            this.label3.AccessibleName = "";
            this.label3.AutoSize = true;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Location = new System.Drawing.Point(222, 63);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(3);
            this.label3.Size = new System.Drawing.Size(350, 21);
            this.label3.TabIndex = 0;
            this.label3.Text = "Testo di prova ricevuto da un contatto kjbasklbloaregboiergnlaebngilgn";
            this.label3.MouseHover += new System.EventHandler(this.label1_MouseHover);
            // 
            // label1
            // 
            this.label1.AccessibleDescription = "";
            this.label1.AccessibleName = "";
            this.label1.AutoSize = true;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Location = new System.Drawing.Point(10, 15);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(3);
            this.label1.Size = new System.Drawing.Size(350, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Testo di prova ricevuto da un contatto kjbasklbloaregboiergnlaebngilgn";
            this.label1.MouseHover += new System.EventHandler(this.label1_MouseHover);
            // 
            // panelSendMsg
            // 
            this.panelSendMsg.ColumnCount = 2;
            this.panelSendMsg.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.panelSendMsg.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.panelSendMsg.Controls.Add(this.inputNewMsg, 0, 0);
            this.panelSendMsg.Controls.Add(this.btnSendNewMsg, 1, 0);
            this.panelSendMsg.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelSendMsg.Location = new System.Drawing.Point(3, 376);
            this.panelSendMsg.Name = "panelSendMsg";
            this.panelSendMsg.RowCount = 1;
            this.panelSendMsg.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panelSendMsg.Size = new System.Drawing.Size(582, 41);
            this.panelSendMsg.TabIndex = 0;
            // 
            // inputNewMsg
            // 
            this.inputNewMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inputNewMsg.Location = new System.Drawing.Point(3, 3);
            this.inputNewMsg.Name = "inputNewMsg";
            this.inputNewMsg.Size = new System.Drawing.Size(459, 35);
            this.inputNewMsg.TabIndex = 0;
            this.inputNewMsg.Text = "";
            // 
            // btnSendNewMsg
            // 
            this.btnSendNewMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSendNewMsg.Location = new System.Drawing.Point(468, 3);
            this.btnSendNewMsg.Name = "btnSendNewMsg";
            this.btnSendNewMsg.Size = new System.Drawing.Size(111, 35);
            this.btnSendNewMsg.TabIndex = 1;
            this.btnSendNewMsg.Text = "Invia Messaggio";
            this.btnSendNewMsg.UseVisualStyleBackColor = true;
            this.btnSendNewMsg.Click += new System.EventHandler(this.btnSendNewMsg_Click);
            // 
            // ChatClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.mainElement);
            this.Controls.Add(this.mainMenu);
            this.MainMenuStrip = this.mainMenu;
            this.Name = "ChatClient";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chat Client Scardi";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChatClient_FormClosing);
            this.Load += new System.EventHandler(this.ChatClient_Load);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.mainElement.ResumeLayout(false);
            this.boxContacts.ResumeLayout(false);
            this.boxChat.ResumeLayout(false);
            this.panelViewMsg.ResumeLayout(false);
            this.panelViewMsg.PerformLayout();
            this.panelSendMsg.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem connessioneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nuovaConnessioneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connettiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disconnettiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem utenteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem esciToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem accediToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem registratiToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel mainElement;
        private System.Windows.Forms.GroupBox boxContacts;
        private System.Windows.Forms.ListBox lstContacts;
        private System.Windows.Forms.Button btnAddContact;
        private System.Windows.Forms.GroupBox boxChat;
        private System.Windows.Forms.TableLayoutPanel panelSendMsg;
        private System.Windows.Forms.RichTextBox inputNewMsg;
        private System.Windows.Forms.Button btnSendNewMsg;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem contattiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aggiornaContattiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aggiungiContattoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolTip toolTipMessages;
        private System.Windows.Forms.Panel panelViewMsg;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
    }
}

