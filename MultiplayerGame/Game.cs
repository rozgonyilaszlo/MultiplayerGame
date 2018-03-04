using System;
using System.Windows.Forms;
using System.Collections.Generic;
using MultiplayerGame.Models;

namespace MultiplayerGame
{
    public partial class Game : Form
    {
        private AsynchronousSocketListener serverSocket;
        private AsynchronousClient asynchronousClient;
        private GameModeType gameModeType;
        private PlayerData playerData = new PlayerData();
        //a játékosok adatai
        private List<PlayerData> playerDatas = new List<PlayerData>();
        

        public Game(ref AsynchronousSocketListener serverSocket, ref AsynchronousClient asynchronousClient, GameModeType gameModeType, PlayerData playerData)
        {
            InitializeComponent();
            this.gameModeType = gameModeType;
            this.playerData = playerData;
            
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
