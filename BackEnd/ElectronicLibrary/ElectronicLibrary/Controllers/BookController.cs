using ElectronicLibrary.Core.Interface.Repository;
using ElectronicLibrary.Core.Interface.Service;
using ElectronicLibrary.Core.Models;
using Microsoft.AspNetCore.Mvc;
using static Dapper.SqlMapper;

namespace ElectronicLibrary.Controllers
{
    public class BookController : BaseController<Book>
    {
        private readonly IBookService _service;
        private readonly IBookRepo _repo;


        public BookController(IBookRepo repo, IBookService service) : base(repo, service)
        {
            _service = service;
            _repo = repo;
        }
    }
}
