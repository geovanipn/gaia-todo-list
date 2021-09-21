using System.Threading.Tasks;
using Gaia.ToDoList.Business.Interfaces.Repositories;
using Gaia.ToDoList.Business.Interfaces.Services;
using Gaia.ToDoList.Business.Models;

namespace Gaia.ToDoList.Business.Services
{
    public sealed class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Create(User user)
        {
            await _userRepository.Add(user);
            await _userRepository.Commit();

            return user;
        }

        public async Task<User> Update(User user)
        {
            await _userRepository.Update(user);
            await _userRepository.Commit();

            return user;
        }
    }
}
