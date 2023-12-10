using ElectronicLibrary.Core.Interface.Repository;
using ElectronicLibrary.Core.Interface.Service;
using ElectronicLibrary.Core.Models;

namespace ElectronicLibrary.Controllers
{
    public class PublisherController : BaseController<Publisher>
    {
        private readonly IPublisherService _service;
        private readonly IPublisherRepo _repo;


        public PublisherController(IPublisherRepo repo, IPublisherService service) : base(repo, service)
        {
            _service = service;
            _repo = repo;
        }
    }
}
