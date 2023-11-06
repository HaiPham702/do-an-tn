using ElectronicLibrary.Core.Interface.Repository;
using ElectronicLibrary.Core.Interface.Service;
using ElectronicLibrary.Core.Models;

namespace ElectronicLibrary.Core.Service
{
    public class UserService : BaseService<User>, IUserService
    {
        public UserService(IBaseRepo<User> repo) : base(repo)
        {
        }
    }
}
