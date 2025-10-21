using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    internal class Server
    {
        private readonly Socket _serverSocket =
            new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private readonly List<ClientHandler> _clients = new();
        private object _lock = new object();

        public void Listen(int port)
        {
            try
            {
                _serverSocket.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), port));
                Thread listener = new(_serverSocket.Listen) { IsBackground = true };
                listener.Start();

                while (true)
                {
                    var clientSocket = _serverSocket.Accept();
                    ClientHandler handler = new(clientSocket, this);
                    _clients.Add(handler);
                    Thread handlerThread = new(handler.Handle) { IsBackground = true };
                    handlerThread.Start();
                }

            }
            catch (Exception ex) { }
        }

        public void RemoveClient(ClientHandler handler)
        {
            lock (_lock)
            {
                _clients.Remove(handler);
            }
        }
    }
}
