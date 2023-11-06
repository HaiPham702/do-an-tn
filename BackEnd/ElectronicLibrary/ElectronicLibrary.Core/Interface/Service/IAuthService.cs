
using ElectronicLibrary.Core.Models;

namespace ElectronicLibrary.Core.Interface.Service
{
    public interface IAuthService
    {
        public Task<User> Login(string email, string password); 
        public User Register(User user);
    }
}
