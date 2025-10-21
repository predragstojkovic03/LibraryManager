using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IEntityMapper<Domain, Persistence> where Persistence : new()
    {
        public Domain ToDomain(Persistence persistence);
        public Persistence ToPersistence(Domain domain);
    }
}
