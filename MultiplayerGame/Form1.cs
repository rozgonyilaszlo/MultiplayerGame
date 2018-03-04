using MultiplayerGame.Models;
using Newtonsoft.Json;
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

        private static AsynchronousSocketListener serverSocket;
        private static AsynchronousClient asynchronousClient;
        public static GameModeType gameModeType;
        public static PlayerData playerData;
        public static PlayerData otherPlayerData;

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
            asynchronousClient.OnMessageReceived = OnMessageReceived;
            asynchronousClient.OnMessageSent = OnMessageSent;
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

            try
            {
                PlayerData data = JsonConvert.DeserializeObject<PlayerData>(message);
                otherPlayerData = data;
            }
            catch (Exception exp)
            {
                Console.WriteLine("Nem player data volt.");
            }
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
            SendData(JsonConvert.SerializeObject(playerData));
            Game gameForm = new Game();
            gameForm.Show();
            
            startGame.Enabled = false;
        }

        public static void SendData(string data)
        {
            if (gameModeType == GameModeType.CLIENT)
            {
                asynchronousClient.Send(data);
            }
            else if (gameModeType == GameModeType.SERVER)
            {
                serverSocket.Send(data);
            }
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

            pictureBox1.Image = GetCharacterImage(playerData.Character);
        }

        public static Bitmap GetCharacterImage(int id)
        {
            switch (id)
            {
                case 1:
                    return Properties.Resources.player1;
                case 2:
                    return Properties.Resources.player2;
                case 3:
                    return Properties.Resources.player3;
                case 4:
                    return Properties.Resources.player4;
                case 5:
                    return Properties.Resources.player5;
                default:
                    return Properties.Resources.player1;
            }
        }
    }
}
