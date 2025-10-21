using Infrastructure.Communication;
using Infrastructure.Dto;
using System.Net.Sockets;

namespace Server
{
    internal class ClientHandler
    {
        private Socket _socket;
        private Server _server;
        private CommunicationAdapter _adapter;

        public ClientHandler(Socket clientSocket, Server server)
        {
            _socket = clientSocket;
            _server = server;
            _adapter = new(_socket);
        }

        public void Handle()
        {
            var req = _adapter.Receive<Request>();
            try
            {
                var res = HandleRequest(req);
                _adapter.Send(res);
            }
            catch (Exception ex)
            {
                _adapter.Send(new Response { Message = ex.Message, Status = 1 });
            }
        }

        private Response HandleRequest(Request request)
        {
            Response res = request.Operation switch
            {
                Operation.AuthLogout => new Response(),
                _ => new Response()
            };
        }
    }
}
