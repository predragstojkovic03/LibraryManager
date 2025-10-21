using Infrastructure.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Communication
{
    public class CommunicationAdapter
    {
        private readonly Socket _socket;
        private readonly NetworkStream _stream;
        private readonly StreamReader _reader;
        private readonly StreamWriter _writer;

        public CommunicationAdapter(Socket socket)
        {
            _socket = socket;
            _stream = new(_socket);
            _reader = new(_stream);
            _writer = new(_stream) { AutoFlush = true };
        }

        public void Send(object payload)
        {
            _writer.WriteLine(JsonSerializer.Serialize(payload));
        }

        public T? Receive<T>()
        {
            return JsonSerializer.Deserialize<T>(_reader.ReadLine());
        }

        public T ReadType<T>(object podaci)
        {
            return podaci == null ? default(T) : JsonSerializer.Deserialize<T>((JsonElement)podaci);
        }

        ~CommunicationAdapter()
        {
            _stream.Close();
            _reader.Close();
            _writer.Close();
        }
    }
}
