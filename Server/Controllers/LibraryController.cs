using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Services;

namespace Server.Controllers
{
    public class LibraryController
    {
        private readonly LibraryService _libraryService;

        public LibraryController(LibraryService libraryService)
        {
            _libraryService = libraryService;
        }

    }
}
