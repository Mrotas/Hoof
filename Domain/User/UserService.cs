using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using DataAccess.Dao.Huntsman;
using DataAccess.Dao.User;
using DataAccess.Dto;
using Domain.Cryptography;
using Domain.Notification;
using Domain.Notification.Models;
using Domain.User.Models;
using Domain.User.ViewModels;

namespace Domain.User
{
    public class UserService : IUserService
    {
        private readonly IUserDao _userDao;
        private readonly IHuntsmanDao _huntsmanDao;
        private readonly INotificationService _notificationService;

        public UserService() : this(new UserDao(), new HuntsmanDao(), new NotificationService())
        {
        }

        public UserService(IUserDao userDao, IHuntsmanDao huntsmanDao, INotificationService notificationService)
        {
            _userDao = userDao;
            _huntsmanDao = huntsmanDao;
            _notificationService = notificationService;
        }

        public UserModel GetUserModelByEmail(string email)
        {
            UserDto userDto = _userDao.GetByEmail(email);
            if (userDto == null)
            {
                throw new Exception("Konto o podanym emailu nie istnieje.");
            }

            var userModel = new UserModel
            {
                UserId = userDto.Id,
                HuntsmanId = userDto.HuntsmanId,
                Email = userDto.Email,
                Password = userDto.Password,
                EmailVerified = userDto.EmailVerified,
                ActivationCode = userDto.ActivationCode,
                ActivationCodeTimeStamp = userDto.ActivationCodeTimeStamp,
                CreationTimeStamp = userDto.CreationTimeStamp
            };

            return userModel;
        }

        public IList<UserViewModel> GetUserViewModels()
        {
            IList<UserDto> users = _userDao.GetAll();
            IList<HuntsmanDto> huntsmans = _huntsmanDao.GetAll();

            List<UserViewModel> userViewModels = 
            (
                from user in users
                join huntsman in huntsmans on user.HuntsmanId equals huntsman.Id
                select new UserViewModel
                {
                    UserId = user.Id,
                    HuntsmanId = huntsman.Id,
                    Name = huntsman.Name,
                    LastName = huntsman.LastName,
                    Role = huntsman.Role,
                    Email = user.Email
                }
            ).ToList();

            return userViewModels;
        }

        public void CreateAccount(CreateUserViewModel model, VerificationLinkModel verificationLinkModel)
        {
            UserDto existingUser = _userDao.GetByEmail(model.Email);
            if (existingUser != null)
            {
                throw new Exception($"Użytkownik o emailu {model.Email} już istnieje!");
            }
            
            UserDto user = _userDao.Insert(model.Name, model.LastName, model.Role, model.Email, SetPassword());

            string verifyLink = $"/User/VerifyAccount/{user.ActivationCode}";
            string activationLink = verificationLinkModel.AbsoluteUri.Replace(verificationLinkModel.PathAndQuery, verifyLink);

            var createAccountNotificationMessage = new CreateAccountNotificationMessage
            {
                EmailTo = user.Email,
                ActivationLink = activationLink,
            };

            _notificationService.SendCreateAccountNotification(createAccountNotificationMessage);
        }

        public int VerifyAccount(string activationCode)
        {
            UserDto user = _userDao.GetByActivationCode(activationCode);
            if (user == null)
            {
                throw new Exception("Błędny kod aktywacji, skontaktuj się z administratorem.");
            }

            if (user.EmailVerified)
            {
                throw new Exception("Twoje konto jest już aktywne!");
            }

            user.EmailVerified = true;
            _userDao.Update(user);
            return user.Id;
        }

        public void ChangePassword(int userId, string password)
        {
            UserDto user = _userDao.GetById(userId);
            if (user == null)
            {
                throw new Exception("Wystąpił niespodziewany błąd podczas zmiany hasła, spróbuj ponowanie.");
            }

            user.Password = Encrypt.EncryptPassword(password);
            _userDao.Update(user);
        }

        private string SetPassword()
        {
            const int MINIMUM_PASSWORD_LENGTH = 14;
            const int NUMBER_OF_NON_ALPHANUMERIC_CHARACTERS = 4;

            string generatedPassword = Membership.GeneratePassword(MINIMUM_PASSWORD_LENGTH, NUMBER_OF_NON_ALPHANUMERIC_CHARACTERS);

            string hashedPassword = Encrypt.EncryptPassword(generatedPassword);

            return hashedPassword;
        }
    }
}
