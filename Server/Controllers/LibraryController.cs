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

        public async Task<List<Library>> GetLibraryListAsync()
        {
            return await Task.Run(() => _libraryService.GetAll());
        }

        public async Task<Library> GetLibraryByIdAsync(Guid id)
        {
            return await Task.Run(() => _libraryService.GetById(id));
        }

        public async Task<Library> CreateLibraryAsync(LibraryDto libraryDto)
        {
            var library = new Library
            {
                Name = libraryDto.Name,
                Address = libraryDto.Address
            };

            return await Task.Run(() => _libraryService.Create(library));
        }

        public async Task<Library> UpdateLibraryAsync(LibraryDto libraryDto)
        {
            var library = new Library
            {
                Id = libraryDto.Id,
                Name = libraryDto.Name,
                Address = libraryDto.Address
            };

            return await Task.Run(() => _libraryService.Update(library));
        }
    }
}
