using System.Collections.Generic;
using DataAccess.Dto;

namespace DataAccess.Dao.User
{
    public interface IUserDao
    {
        IList<UserDto> GetAll();
        UserDto GetById(int userId);
        UserDto GetByEmail(string email);
        UserDto GetByActivationCode(string activationCode);
        UserDto Insert(string name, string lastName, string role, string email, string password);
        void Update(UserDto dto);
    }
}