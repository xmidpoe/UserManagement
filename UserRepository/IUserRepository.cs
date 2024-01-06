using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserRepository.Models;

namespace UserRepository
{
    public interface IUserRepository<T> where T : class
    {
        RepoResponseMessage<User> CreateUser(User user);

        RepoResponseMessage<User> UpdateUser(User user);

        RepoResponseMessage<User> DeleteUser(User user);

        RepoResponseMessage<User> GetUserById(User user);

        RepoResponseMessage<User> GetUserByUserNameAndPassword(User user);
    }
}
