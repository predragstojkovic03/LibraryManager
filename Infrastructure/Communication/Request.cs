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
        public Operation Operation { get; set; }
        public object? Payload { get; set; }
    }
}
