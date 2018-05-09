using System;
using System.Drawing;
using System.Windows.Forms;
using MultiplayerGame.Models;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace MultiplayerGame
{
    public partial class Form1 : Form
    {
        private Game gameForm;
        private AsynchronousSocketListener serverSocket;
        private AsynchronousClient asynchronousClient;
        private GameModeType gameModeType;
        private int character = 1;
        private Queue<string> queue;
        private string messages;
        
        public Form1()
        {
            InitializeComponent();

            queue = new Queue<string>();
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
        
        //Start server.
        private void startServer_Click(object sender, EventArgs e)
        {
            gameModeType = GameModeType.SERVER;

            serverSocket.Initialize();
            serverSocket.StartListening();

            addressTextBox.Enabled = false;
            connectToServer.Enabled = false;

            events.Text += "Szerver elindítva az alábbi címen: " + serverSocket.LocalAddress.ToString();
        }

        /// <summary>
        /// Callback when connected.
        /// </summary>
        private void Connected()
        {
            events.Text += "Kapcsolódva.";
            startGame.Enabled = true;
        }

        /// <summary>
        /// Callback when disconnected.
        /// </summary>
        private void OnClientDisconnected()
        {
            events.Text += "Szétkapcsolódva.";
            if (gameForm != null)
            {
                gameForm.Close();
            }
            startGame.Enabled = false;
        }

        /// <summary>
        /// Callback when message is received.
        /// </summary>
        /// <param name="message"></param>
        private void OnMessageReceived(string message)
        {
            messages += message;

            int index = messages.IndexOf(']');

            string serializableData = messages.Substring(0, index + 2);
            messages = messages.Substring(index + 2);

            queue.Enqueue(serializableData);

            try
            {
                Console.WriteLine(queue.Count);

                if (queue.Count > 0)
                {
                    PlayerData data = JsonConvert.DeserializeObject<PlayerData>(queue.Dequeue());
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
        
        //Connect to server.
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
        
        //Start game.
        private void startGame_Click(object sender, EventArgs e)
        {
            PlayerData me = new PlayerData();
            me.Name = textBox1.Text;
            me.Character = character;
            
            gameForm = new Game(parent: this, me: me);
            gameForm.Show();
            
            startGame.Enabled = false;
        }

        /// <summary>
        /// Send .NET object.
        /// </summary>
        /// <param name="object">.NET object</param>
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
        
        // Select character image.
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

        /// <summary>
        /// Get character image from id.
        /// </summary>
        /// <param name="id">Character id.</param>
        /// <returns>Returns the character image.</returns>
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