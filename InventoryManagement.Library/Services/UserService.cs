using InventoryManagement.Core.Entities;
using InventoryManagement.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Library.Services
{
    public interface IUserService
    {
        User ValidateUserLogin(string email, string password);
        List<GetUsers_Result> GetUserList(int pageNo, int pageSize, string sortColumn, string sortOrder, string searchValue = "");
        User GetById(int Id);
        int Save(User user);
        void Delete(User user);

        bool CheckUserExists(string email, int userId);

    }

    public class UserService : IUserService
    {
        IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public User GetById(int Id)
        {
            return _userRepository.GetById(Id);
        }


        public bool CheckUserExists(string email, int userId)
        {
            return _userRepository.Table.Any(x => x.Email == email && x.UserId != userId);
        }

        public int Save(User user)
        {
            if (user.UserId > 0)
                _userRepository.Update(user);
            else
                _userRepository.Insert(user);
            return user.UserId;
        }

        public void Delete(User user)
        {
            _userRepository.Delete(user);
        }

        public User ValidateUserLogin(string email, string password)
        {
            return _userRepository.Table.Where(x => x.Email == email && x.Password == password).FirstOrDefault();
        }

        public List<GetUsers_Result> GetUserList(int pageNo, int pageSize, string sortColumn, string sortOrder, string searchValue = "")
        {        
            return _userRepository.Context.Database.SqlQuery<GetUsers_Result>("exec GetUsers {0},{1},{2},{3},{4}", pageNo, pageSize, sortColumn, sortOrder, searchValue).ToList();
        }
    }
}
