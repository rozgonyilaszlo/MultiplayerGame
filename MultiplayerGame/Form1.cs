using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultiplayerGame
{
    public partial class Form1 : Form
    {
        //server
        public AsynchronousSocketListener serverSocket;
        //client
        public AsynchronousClient asynchronousClient;

        void Connected()
        {
            events.Text += "Kapcsolódva.";
            startGame.Enabled = true;
        }

        public Form1()
        {
            InitializeComponent();

            startGame.Enabled = false;
        }

        private void startServer_Click(object sender, EventArgs e)
        {
            serverSocket = new AsynchronousSocketListener();

            serverSocket.OnClientConnected = OnClientConnected;
            serverSocket.OnClientDisconnected = OnClientDisconnected;
            serverSocket.OnMessageReceived = OnMessageReceived;
            serverSocket.OnMessageSent = OnMessageSent;

            serverSocket.Initialize();
            addressLabel.Text = serverSocket.LocalAddress.ToString();

            serverSocket.StartListening();
            
            addressTextBox.Enabled = false;
            connectToServer.Enabled = false;

            events.Text += "Szerver elindítva az alábbi címen: " + serverSocket.LocalAddress.ToString();
        }

        private void OnClientConnected(string client)
        {
            events.Text += "Kliens csatlakozva: " + client;
            startGame.Enabled = true;
        }

        private void OnClientDisconnected()
        {
            events.Text += "Kliens lecsatlakozott";
            startGame.Enabled = false;
        }

        private void OnMessageReceived(string message)
        {
            events.Text += "Message: " + message;
        }

        private void OnMessageSent()
        {
            events.Text += "Küldés sikerült.";
        }

        private void connectToServer_Click(object sender, EventArgs e)
        {
            asynchronousClient = new AsynchronousClient();
            asynchronousClient.OnConnected = Connected;
            try
            {
                startServer.Enabled = false;
                asynchronousClient.Connect(addressTextBox.Text);
            }
            catch (Exception exp)
            {
                events.Text += "Hiba: " + exp.Message;
                startServer.Enabled = true;
            }
        }

        
    }
}
