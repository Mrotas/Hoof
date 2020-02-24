using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Domain.Cryptography;
using Domain.User;
using Domain.User.Models;
using Domain.User.ViewModels;

namespace Hoof.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController() : this(new UserService())
        {
        }

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public ActionResult Index()
        {
            IList<UserViewModel> userViewModels = _userService.GetUserViewModels();
            return View(userViewModels);
        }

        [HttpPost]
        public JsonResult Create(CreateUserViewModel model)
        {
            var verificationLinkModel = new VerificationLinkModel
            {
                AbsoluteUri = Request.Url.AbsoluteUri,
                PathAndQuery = Request.Url.PathAndQuery
            };

            bool created = true;
            string message = String.Empty;
            try
            {
                _userService.CreateAccount(model, verificationLinkModel);
            }
            catch (Exception ex)
            {
                message = ex.Message;
                created = false;
            }
            
            ViewBag.Created = created;
            ViewBag.Message = message;

            return Json(new { message, created}, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Edit(CreateUserViewModel model)
        {
            string message = String.Empty;
            try
            {

            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Json(new { message }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            string message = String.Empty;
            try
            {

            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Json(new { message }, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult VerifyAccount(string id)
        {
            string message = String.Empty;
            bool verified = true;
            try
            {
                int userId = _userService.VerifyAccount(id);
                Session["UserId"] = userId;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                verified = false;
            }

            ViewBag.Status = verified;
            ViewBag.Message = message;
            return View();
        }

        public JsonResult ChangePassword(string password)
        {
            string message = String.Empty;
            
            try
            {
                int userId = Convert.ToInt32(Session["UserId"]);
                _userService.ChangePassword(userId, password);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            return Json(message);
        }
        
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel login, string returnUrl = "")
        {
            string message = String.Empty;
            try
            {
                UserModel user = _userService.GetUserModelByEmail(login.Email);
                if (String.Equals(Encrypt.EncryptPassword(login.Password), user.Password, StringComparison.Ordinal))
                {
                    int credentialsExpiration = login.RememberMe ? 525000 : 1440;
                    var ticket = new FormsAuthenticationTicket(login.Email, login.RememberMe, credentialsExpiration);
                    string encrypted = FormsAuthentication.Encrypt(ticket);
                    var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted)
                    {
                        Expires = DateTime.Now.AddMinutes(credentialsExpiration),
                        HttpOnly = true
                    };
                    Response.Cookies.Add(cookie);

                    var userCookie = new HttpCookie("User")
                    {
                        Expires = DateTime.Now.AddMinutes(credentialsExpiration),
                        HttpOnly = true
                    };
                    userCookie["UserId"] = user.UserId.ToString();
                    Response.Cookies.Add(userCookie);

                    return RedirectToAction("Index", "Home");
                }
                
                message = "Niepoprawny login lub hasło.";
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            ViewBag.Message = message;
            return View();
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "User");
        }
    }
}