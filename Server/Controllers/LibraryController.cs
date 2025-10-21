using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Services;
using Domain.Entities;
using Infrastructure.Dto;
using Server.Dtos;

namespace Server.Controllers
{
    public class LibraryController
    {
        private readonly CrudService<Library> _libraryService;

        public LibraryController(CrudService<Library> libraryService)
        {
            _libraryService = libraryService;
        }

        public List<Library> GetLibraryList()
        {
            return _libraryService.GetAll();
        }

        public Library GetLibraryById(Guid id)
        {
            return _libraryService.GetById(id);
        }

        public Library CreateLibrary(LibraryDto libraryDto)
        {
            var library = new Library
            {
                Name = libraryDto.Name,
                Address = libraryDto.Address
            };
            return _libraryService.Create(library);
        }

        public Library UpdateLibrary(LibraryDto libraryDto)
        {
            var library = new Library
            {
                Id = libraryDto.Id,
                Name = libraryDto.Name,
                Address = libraryDto.Address
            };
            return _libraryService.Update(library);
        }
    }
}
