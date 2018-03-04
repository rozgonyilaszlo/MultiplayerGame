using MultiplayerGame.Models;
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
        //TODO: ha a szerver elérhetetlen lesz, valamit kezdeni vele.
        
        private AsynchronousSocketListener serverSocket;
        private AsynchronousClient asynchronousClient;
        private GameModeType gameModeType;
        private PlayerData playerData;
        
        public Form1()
        {
            InitializeComponent();

            playerData = new PlayerData();
            playerData.Character = 1;
            startGame.Enabled = false;
            
            //Server
            serverSocket = new AsynchronousSocketListener();

            serverSocket.OnClientConnected = OnClientConnected;
            serverSocket.OnClientDisconnected = OnClientDisconnected;
            serverSocket.OnMessageReceived = OnMessageReceived;
            serverSocket.OnMessageSent = OnMessageSent;

            //Client
            asynchronousClient = new AsynchronousClient();
            asynchronousClient.OnConnected = Connected;
        }

        private void startServer_Click(object sender, EventArgs e)
        {
            gameModeType = GameModeType.SERVER;
            serverSocket.Initialize();
            addressLabel.Text = serverSocket.LocalAddress.ToString();

            serverSocket.StartListening();
            
            addressTextBox.Enabled = false;
            connectToServer.Enabled = false;

            events.Text += "Szerver elindítva az alábbi címen: " + serverSocket.LocalAddress.ToString();
        }

        private void Connected()
        {
            events.Text += "Kapcsolódva.";
            startGame.Enabled = true;
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
            try
            {
                startServer.Enabled = false;
                asynchronousClient.Connect(addressTextBox.Text);
                gameModeType = GameModeType.CLIENT;
            }
            catch (Exception exp)
            {
                events.Text += "Hiba: " + exp.Message;
                startServer.Enabled = true;
            }
        }

        private void startGame_Click(object sender, EventArgs e)
        {
            playerData.Name = textBox1.Text;
            Game gameForm = new Game(ref serverSocket, ref asynchronousClient, gameModeType, playerData);
            gameForm.Show();

            startGame.Enabled = false;
        }

        //TODO: nem túl szép megoldás, valamit kellen még vele kezdeni :)
        private void button1_Click(object sender, EventArgs e)
        {
            if (((Button)sender).Text == ">")
            {
                //következő karakter
                if (playerData.Character < 5)
                {
                    playerData.Character++;
                }
                else
                {
                    playerData.Character = 1;
                }
            }
            else
            {
                //előző karakter
                if (playerData.Character <= 1)
                {
                    playerData.Character = 5;
                }
                else
                {
                    playerData.Character--;
                }
            }

            switch (playerData.Character)
            {
                case 1:
                    pictureBox1.Image = Properties.Resources.player1;
                    break;
                case 2:
                    pictureBox1.Image = Properties.Resources.player2;
                    break;
                case 3:
                    pictureBox1.Image = Properties.Resources.player3;
                    break;
                case 4:
                    pictureBox1.Image = Properties.Resources.player4;
                    break;
                case 5:
                    pictureBox1.Image = Properties.Resources.player5;
                    break;
                default:
                    pictureBox1.Image = Properties.Resources.player1;
                    break;
            }
        }
    }
}
