using System;
using System.Net;
using System.Threading.Tasks;
using Aiesec.Data.Model.IdentityModel;
using Aiesec.Data.ViewModel;
using Aiesec.Web.Helper.SelectListService;
using Aiesec.Web.Mail;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Aiesec.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMailSender _mailSender;
        private readonly ISelectListService _selectListService;

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ISelectListService selectListService,
            IMailSender mailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _selectListService = selectListService;
            _mailSender = mailSender;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password, bool isPersistent,
            string returnUrl = "")
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null) return RedirectToAction("Login");
            var signInResult = await _signInManager.PasswordSignInAsync(user, password, isPersistent, false);
            if (!signInResult.Succeeded) return RedirectToAction("Login");
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            ViewBag.RoleSelectList = _selectListService.Roles();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegistration userRegistration)
        {
            Console.WriteLine(userRegistration.Username);
            var doesExist = await _userManager.FindByNameAsync(userRegistration.Username);
            if (doesExist != null) return RedirectToAction("Register");
            var user = new ApplicationUser
            {
                UserName = userRegistration.Username,
                Email = userRegistration.Email,
                FirstName = userRegistration.FirstName,
                LastName = userRegistration.LastName
            };
            const string password = "Password123";
            await _userManager.CreateAsync(user, password);
            await _userManager.AddToRoleAsync(user, userRegistration.Role);
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var activationLink = Url.Action("ConfirmEmail", "Account", new {userId = user.Id, token}, Request.Scheme);
            var message = new Message(new[] {user.Email}, "AIESEC | Registration successful",
                "WELCOME\nUsername: " + user.UserName + "\nPassword: " + password + "\nTo activate your account, please follow: " +
                activationLink);
            await _mailSender.SendEmailAsync(message);
            return RedirectToAction("Register");
        }

        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string userId, [FromQuery] string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return RedirectToAction("Login", "Account");
            if (user.EmailConfirmed)
            {
                ViewBag.Error = "Email is already confirmed.";
                return View();
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                ViewBag.Error = "Token invalid or expired.";
                return View();
            }

            var message = new Message(new[] {user.Email}, "AIESEC | Email Confirmed",
                "Email confirmed successfully");
            await _mailSender.SendEmailAsync(message);
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        public async Task<IActionResult> Settings()
        {
            var userData = await _userManager.GetUserAsync(HttpContext.User);

            return View(userData);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(PasswordChange passwordChange)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null) return RedirectToAction("Settings");
            var result =
                await _userManager.ChangePasswordAsync(user, passwordChange.OldPassword, passwordChange.NewPassword);
            if (!result.Succeeded)
            {
                return new JsonResult(new
                    {StatusCode = HttpStatusCode.BadRequest, Message = "Action failed, please try again"});
            }

            return new JsonResult(new
                {StatusCode = HttpStatusCode.OK, Message = "Password changed successfully"});
        }
    }
}