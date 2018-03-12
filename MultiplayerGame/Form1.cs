using System;
using System.Drawing;
using System.Windows.Forms;
using MultiplayerGame.Models;
using Newtonsoft.Json;

namespace MultiplayerGame
{
    public partial class Form1 : Form
    {
        private Game gameForm;
        private AsynchronousSocketListener serverSocket;
        private AsynchronousClient asynchronousClient;
        private GameModeType gameModeType;
        private int character = 1;
        
        public Form1()
        {
            InitializeComponent();
            
            startGame.Enabled = false;

            serverSocket = new AsynchronousSocketListener();
            serverSocket.OnClientConnected = Connected;
            serverSocket.OnClientDisconnected = OnClientDisconnected;
            serverSocket.OnMessageReceived = OnMessageReceived;
            
            asynchronousClient = new AsynchronousClient();
            asynchronousClient.OnConnected = Connected;
            asynchronousClient.OnMessageReceived = OnMessageReceived;
            asynchronousClient.OnClientDisconnected = OnClientDisconnected;
        }
        
        private void startServer_Click(object sender, EventArgs e)
        {
            gameModeType = GameModeType.SERVER;

            serverSocket.Initialize();
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

        private void OnClientDisconnected()
        {
            events.Text += "Szétkapcsolódva.";
            if (gameForm != null)
            {
                gameForm.Close();
            }
            startGame.Enabled = false;
        }
        
        private void OnMessageReceived(string message)
        {
            try
            {
                //TODO: megvalósítani a lövés animációját
                if (message.Contains("From"))
                {
                    Fire fire = JsonConvert.DeserializeObject<Fire>(message);
                    if (gameForm != null)
                    {
                        gameForm.Me.Life -= Constant.DamageFromFire;
                    }
                }
                else
                {
                    PlayerData data = JsonConvert.DeserializeObject<PlayerData>(message);
                    if (gameForm != null)
                    {
                        gameForm.Enemy = data;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}, Message: {1}", e.Message, message);
            }
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
            PlayerData me = new PlayerData();
            me.Name = textBox1.Text;
            me.Character = character;
            
            gameForm = new Game(parent: this, me: me);
            gameForm.Show();
            
            startGame.Enabled = false;
        }

        public void SendData(object @object)
        {
            string serializedObject = JsonConvert.SerializeObject(@object);

            if (gameModeType == GameModeType.CLIENT)
            {
                asynchronousClient.Send(serializedObject);
            }
            else if (gameModeType == GameModeType.SERVER)
            {
                serverSocket.Send(serializedObject);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (((Button)sender).Text == ">")
            {
                if (character < 5)
                {
                    character++;
                }
                else
                {
                    character = 1;
                }
            }
            else
            {
                if (character <= 1)
                {
                    character = 5;
                }
                else
                {
                    character--;
                }
            }

            pictureBox1.Image = GetCharacterImage(character);
        }
        
        public Bitmap GetCharacterImage(int id)
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