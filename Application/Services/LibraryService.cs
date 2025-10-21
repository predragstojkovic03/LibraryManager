using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class LibraryService
    {
        private readonly IRepository<Library> _repository;

        public LibraryService(IRepository<Library> repository)
        {
            _repository = repository;
        }

        public Library GetLibraryById(Guid id)
        {
            return _repository.FindOne(id);
        }
    }
}
