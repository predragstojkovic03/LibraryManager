using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Mappers
{
    public class MssqlLibraryMapper : IEntityMapper<Library, MssqlLibrary>
    {
        public Library ToDomain(MssqlLibrary persistence)
        {
            return new Library { Address = persistence.Address, Name = persistence.Name };
        }

        public MssqlLibrary ToPersistence(Library domain)
        {
            return new MssqlLibrary
            {
                Address = domain.Address,
                Name = domain.Name
            };
        }
    }
}
