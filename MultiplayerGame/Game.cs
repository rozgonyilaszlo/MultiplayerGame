using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using Newtonsoft.Json;
using MultiplayerGame.Models;

namespace MultiplayerGame
{
    public partial class Game : Form
    {
        private AsynchronousSocketListener serverSocket;
        private AsynchronousClient asynchronousClient;
        private GameModeType gameModeType;
        
        public Game(ref AsynchronousSocketListener serverSocket, ref AsynchronousClient asynchronousClient, GameModeType gameModeType)
        {
            InitializeComponent();
            this.gameModeType = gameModeType;

            if (gameModeType == GameModeType.SERVER)
            {
                this.serverSocket = serverSocket;
                serverSocket.OnMessageReceived = OnMessageReceived;
            }
            else if (gameModeType == GameModeType.CLIENT)
            {
                this.asynchronousClient = asynchronousClient;
                asynchronousClient.OnMessageReceived = OnMessageReceived;
            }
            else
            {
                Console.WriteLine("GameModeType not found.");
            }
        }

        private void OnMessageReceived(string data)
        {
            MessageBox.Show(data, "Adat jött", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
