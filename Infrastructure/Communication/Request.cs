using Infrastructure.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Dto
{
    public class Request
    {
        private readonly Operation _operation;
        private readonly object _payload;

        public Operation Operation { get => _operation; }
        public object? Payload { get => _payload; }

        public Request(Operation o, object payload)
        {
            _operation = o;
            _payload = payload;
        }
    }
}
