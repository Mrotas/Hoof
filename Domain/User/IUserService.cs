using System.Collections.Generic;
using Domain.User.Models;
using Domain.User.ViewModels;

namespace Domain.User
{
    public interface IUserService
    {
        UserModel GetUserModelByEmail(string email);
        IList<UserViewModel> GetUserViewModels();
        void CreateAccount(CreateUserViewModel model, VerificationLinkModel verificationLinkModel);
        int VerifyAccount(string activationCode);
        void ChangePassword(int userId, string password);
    }
}