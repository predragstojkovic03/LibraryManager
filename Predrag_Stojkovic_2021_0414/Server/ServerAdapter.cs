using Infrastructure.Communication;
using Infrastructure.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client.Server
{
    public class ServerAdapter
    {
        private Socket _socket;
        private CommunicationAdapter _communicationAdapter;

        public ServerAdapter() { }

        public void Connect()
        {
            if (_socket != null && _socket.Connected) { return; }

            _socket = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _socket.Connect("127.0.0.1", 3000);
            _communicationAdapter = new(_socket);
            Console.WriteLine("Connection to the server socket is established");
        }

        public void Disconnect()
        {
            if (_socket == null || !_socket.Connected) return;

            _socket.Shutdown(SocketShutdown.Both);
            _socket.Close();
        }

        public Response? MakeRequest(Operation o, object payload)
        {
            var request = new Request { Operation = o, Payload = payload };
            _communicationAdapter.Send(request);

            return _communicationAdapter.Receive<Response>();
        }

        public void Emit(Operation o, object payload)
        {
            var request = new Request { Operation = o, Payload = payload };
            _communicationAdapter.Send(request);
        }
    }
}
