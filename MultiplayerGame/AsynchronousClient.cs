using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;

public class AsynchronousClient
{
    // Public delegate functions
    public delegate void Connected();
    public delegate void MessageReceived(string data);
    public delegate void MessageSent();
    public delegate void ClientDisconnected();

    // The port number for the remote device.
    private const int port = 11000;

    // data members
    private Socket socket;

    public const int BufferSize = 1024;
    public byte[] buffer = new byte[BufferSize];

    // public callbacks
    public Connected OnConnected { set; get; }
    public MessageReceived OnMessageReceived { get; set; }
    public MessageSent OnMessageSent { set; get; }
    public ClientDisconnected OnClientDisconnected { get; set; }

    /// <summary>
    /// Default constructor
    /// </summary>
    public void Connect(string hostname)
    {
        // Connect to a remote device.
        try
        {
            // Establish the remote endpoint for the socket.
            // The name of the 
            IPHostEntry ipHostInfo = Dns.GetHostEntry(hostname);
            IPAddress ipAddress = GetIPV4Address(ipHostInfo);
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);

            // Create a TCP/IP socket.
            socket = new Socket(remoteEP.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            // Connect to the remote endpoint.
            socket.BeginConnect(remoteEP, new AsyncCallback(ConnectCallback), socket);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    /// <summary>
    /// Gets IPv4 address from host address
    /// </summary>
    /// <param name="host"></param>
    /// <returns></returns>
    private IPAddress GetIPV4Address(IPHostEntry host)
    {
        foreach (IPAddress address in host.AddressList)
        {
            if (address.AddressFamily == AddressFamily.InterNetwork)
            {
                return address;
            }
        }

        return null;
    }

    /// <summary>
    /// Callback when connected to the server
    /// </summary>
    /// <param name="ar"></param>
    private void ConnectCallback(IAsyncResult ar)
    {
        try
        {
            // Complete the connection.
            socket.EndConnect(ar);

            if (OnConnected != null)
                SynchronizationContext.Current.Post(o => OnConnected(), null);

            // Begin receiving the data from the remote device.
            socket.BeginReceive(buffer, 0, BufferSize, 0, new AsyncCallback(ReceiveCallback), null);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    /// <summary>
    /// Callback when socket data is received
    /// </summary>
    /// <param name="ar"></param>
    private void ReceiveCallback(IAsyncResult ar)
    {
        // check for conenction alive
        if (socket == null || !socket.Connected)
            return;

        try
        {
            // Read data from the remote device.
            int bytesRead = socket.EndReceive(ar);

            if (bytesRead > 0)
            {
                // There  might be more data, so store the data received so far.  
                string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);

                if (OnMessageReceived != null)
                    SynchronizationContext.Current.Post(o => OnMessageReceived(message), null);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());

            if (OnClientDisconnected != null)
                SynchronizationContext.Current.Post(o => OnClientDisconnected(), null);
        }

        // Receive more data
        if (socket.Connected)
            socket.BeginReceive(buffer, 0, BufferSize, 0, new AsyncCallback(ReceiveCallback), null);
    }

    /// <summary>
    /// Send message to the server
    /// </summary>
    /// <param name="data"></param>
    public void Send(String data)
    {
        // Convert the string data to byte data using ASCII encoding.
        byte[] byteData = Encoding.ASCII.GetBytes(data + "\r\n");

        // Begin sending the data to the remote device.
        socket.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), socket);
    }

    /// <summary>
    /// Receives message from the server
    /// </summary>
    /// <param name="ar"></param>
    private void SendCallback(IAsyncResult ar)
    {
        try
        {
            // Complete sending the data to the remote device.
            int bytesSent = socket.EndSend(ar);

            if (OnMessageSent != null)
                SynchronizationContext.Current.Post(o => OnMessageSent(), null);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }
}