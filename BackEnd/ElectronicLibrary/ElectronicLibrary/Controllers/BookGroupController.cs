using ElectronicLibrary.Core.Interface.Repository;
using ElectronicLibrary.Core.Interface.Service;
using ElectronicLibrary.Core.Models;

namespace ElectronicLibrary.Controllers
{
    public class BookGroupController : BaseController<BookGroup>
    {
        private readonly IBookGroupService _service;
        private readonly IBookGroupRepo _repo;


        public BookGroupController(IBookGroupRepo repo, IBookGroupService service) : base(repo, service)
        {
            _service = service;
            _repo = repo;
        }
    }
}
