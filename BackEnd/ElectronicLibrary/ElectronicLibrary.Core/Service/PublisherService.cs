using ElectronicLibrary.Core.Interface.Repository;
using ElectronicLibrary.Core.Interface.Service;
using ElectronicLibrary.Core.Models;

namespace ElectronicLibrary.Core.Service
{
    public class PublisherService : BaseService<Publisher>, IPublisherService
    {
        public PublisherService(IBaseRepo<Publisher> repo) : base(repo)
        {
        }
    }
}
