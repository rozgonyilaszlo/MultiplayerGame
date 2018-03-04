using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class AsynchronousSocketListener
{
    // public delegate functions
    public delegate void ClientConnected(string client);
    public delegate void ClientDisconnected();
    public delegate void MessageReceived(string message);
    public delegate void MessageSent();

    // public properties
    public IPAddress LocalAddress { private set; get; }

    // public callbacks
    public ClientConnected OnClientConnected { set; get; }
    public ClientDisconnected OnClientDisconnected { set; get; }
    public MessageReceived OnMessageReceived { set; get; }
    public MessageSent OnMessageSent { get; set; }


    // data members
    Socket listener;
    Socket handler;

    public const int BufferSize = 1024;
    public byte[] buffer = new byte[BufferSize];

    /// <summary>
    /// Default constructor
    /// </summary>
    public AsynchronousSocketListener()
    {
        handler = null;
    }

    public void Initialize()
    {
        // Data buffer for incoming data.  
        byte[] bytes = new Byte[1024];

        // Establish the local endpoint for the socket.  
        // The DNS name of the computer  
        IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
        StoreLocalIPAddress(ipHostInfo);
        IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, 11000);

        // Create a TCP/IP socket.  
        listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        // Bind the socket to the local endpoint and listen for incoming connections.  
        listener.Bind(localEndPoint);
        listener.Listen(100);
    }

    /// <summary>
    /// Starts listening for client connection
    /// </summary>
    public void StartListening()
    {
        try
        {
            // Start an asynchronous socket to listen for connections.  
            listener.BeginAccept(new AsyncCallback(AcceptCallback), listener);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    /// <summary>
    /// Stores IPv4 address as a local address
    /// </summary>
    /// <param name="host"></param>
    private void StoreLocalIPAddress(IPHostEntry host)
    {
        foreach (IPAddress address in host.AddressList)
        {
            if (address.AddressFamily == AddressFamily.InterNetwork)
            {
                LocalAddress = address;
            }
        }
    }

    /// <summary>
    /// Callback for client connection accept event
    /// </summary>
    /// <param name="ar"></param>
    public void AcceptCallback(IAsyncResult ar)
    {
        // Get the socket that handles the client request.  
        handler = listener.EndAccept(ar);

        if (OnClientConnected != null)
            SynchronizationContext.Current.Post(o => OnClientConnected(handler.RemoteEndPoint.ToString()), null);

        // start data reception
        handler.BeginReceive(buffer, 0, BufferSize, 0, new AsyncCallback(ReadCallback), null);
    }

    /// <summary>
    /// Callback for message reception
    /// </summary>
    /// <param name="ar"></param>
    public void ReadCallback(IAsyncResult ar)
    {
        try
        {

            // Read data from the client socket.   
            int bytesRead = handler.EndReceive(ar);

            if (bytesRead > 0)
            {
                // There  might be more data, so store the data received so far.  
                string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);

                if (OnMessageReceived != null)
                    SynchronizationContext.Current.Post(o => OnMessageReceived(message), null);
            }

            // Receive more data
            handler.BeginReceive(buffer, 0, BufferSize, 0, new AsyncCallback(ReadCallback), null);
        }
        catch
        {
            handler.Close();
            handler = null;
            StartListening();

            if (OnClientDisconnected != null)
                SynchronizationContext.Current.Post(o => OnClientDisconnected(), null);
        }
    }

    /// <summary>
    /// Sends message
    /// </summary>
    /// <param name="data"></param>
    public void Send(String data)
    {
        // Convert the string data to byte data using ASCII encoding.  
        byte[] byteData = Encoding.ASCII.GetBytes(data + "\r\n");

        // Begin sending the data to the remote device.  
        handler.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), null);
    }

    /// <summary>
    /// Callsback when message sending is complete
    /// </summary>
    /// <param name="ar"></param>
    private void SendCallback(IAsyncResult ar)
    {
        try
        {
            // Complete sending the data to the remote device.  
            int bytesSent = handler.EndSend(ar);

            if (OnMessageSent != null)
                SynchronizationContext.Current.Post(o => OnMessageSent(), null);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }
}