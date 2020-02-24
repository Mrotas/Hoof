using System;
using System.Web;
using DataAccess.Dao.Huntsman;
using DataAccess.Dao.User;
using DataAccess.Dto;

namespace Domain.Role
{
    public static class RoleManager
    {
        private static readonly IUserDao _userDao = new UserDao();
        private static readonly IHuntsmanDao _huntsmanDao = new HuntsmanDao();
        
        public static bool IsHeadHuntsman(HttpCookie userIdCookie)
        {
            if (userIdCookie == null)
            {
                return false;
            }

            string userIdString = userIdCookie["UserId"];
            if (!Int32.TryParse(userIdString, out int userId))
            {
                return false;
            }

            UserDto user = _userDao.GetById(userId);
            HuntsmanDto huntsman = _huntsmanDao.GetById(user.HuntsmanId);

            return huntsman.Role.Equals("Łowczy");
        }

        public static bool IsDirector(HttpCookie userIdCookie)
        {
            if (userIdCookie == null)
            {
                return false;
            }

            string userIdString = userIdCookie["UserId"];
            if (!Int32.TryParse(userIdString, out int userId))
            {
                return false;
            }

            UserDto user = _userDao.GetById(userId);
            HuntsmanDto huntsman = _huntsmanDao.GetById(user.HuntsmanId);

            return huntsman.Role.Equals("Zarząd");
        }
    }
}
