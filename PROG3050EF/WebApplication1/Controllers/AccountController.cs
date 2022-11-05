using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using GameStore.Data;
using GameStore.Models.ViewModels;
using GameStore.Models.Recaptcha;
using GameStore.Helpers;
using Microsoft.Extensions.Options;
using GameStore.Interfaces;
using GameStore.Models.EmailSender;
using static System.Net.WebRequestMethods;


namespace GameStore.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;
        private readonly IMailService mailService;
        private readonly RecaptchaOption _option;
        private readonly RecaptchaHelper _helper;
        private StoreContext _storeContext;


        public AccountController(UserManager<User> userMngr, SignInManager<User> signInMngr, IOptions<RecaptchaOption> option, IMailService mailService, StoreContext storeContext)
        {
            userManager = userMngr;
            signInManager = signInMngr;
            _option = option.Value;
            _helper = new RecaptchaHelper(option);
            _storeContext = storeContext;
            this.mailService = mailService;
        }


        #region Registration
        [HttpGet]
        public IActionResult Register()
        {
            var model = new RegisterViewModel()
            {
                SiteKey = _option.SiteKey
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            string captchaResponse = Request.Form["g-recaptcha-response"].ToString();
            var validate = _helper.ValidateCaptcha(captchaResponse);

            if (!validate.Success)
            {
                ModelState.AddModelError("", "Finish captcha");
                return View("Register", model);
            }

           

            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.Nickname, Email = model.Email};
                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    try
                    {
                        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                        var confirmationLink = Url.Action(nameof(ConfirmEmail), "Account", new { token, email = user.Email }, Request.Scheme);


                        MailRequest request = new MailRequest() { ToEmail = model.Email, Body = "Hi there! Confirmation email link: " + confirmationLink, Subject = "Welcome Letter - GameStore PROG-3050" };
                        await mailService.SendEmailAsync(request);
                        return RedirectToAction("SuccessRegistration", "Account");
                    }
                    catch (Exception ex)
                    {
                        return RedirectToAction("Error", "Account");
                    }
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            string messages = string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
           
            ModelState.AddModelError("", messages);
            
            return View("Register", model);
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(email);
                if (user == null)
                    return View("Error");
                var result = await userManager.ConfirmEmailAsync(user, token);

                Customer customer = new Customer() { Email = user.Email, Nickname = user.UserName, Password = user.PasswordHash };

                _storeContext.Customers.Add(customer);
                _storeContext.SaveChanges();

                return View(result.Succeeded ? nameof(ConfirmEmail) : "Error");
            }
            catch(Exception ex)
            {
                return RedirectToAction("Error", "Account");
            }
        }

        [HttpGet]
        public async Task<IActionResult> SuccessRegistration()
        {
            return View();
        }
        #endregion


        [HttpGet]
        public IActionResult Error()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult LogIn(string returnURL = "")
        {
            var model = new LoginViewModel { ReturnUrl = returnURL };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Username, model.Password,
                            isPersistent: model.RememberMe, lockoutOnFailure: true);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) &&
                        Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                if (result.IsLockedOut)
                {
                    var user = await userManager.FindByNameAsync(model.Username);
                    var forgotPassLink = Url.Action(nameof(ForgotPassword), "Account", new { }, Request.Scheme);


                    MailRequest request = new MailRequest() { ToEmail = user.Email, Body = string.Format("Your account is locked out, to reset your password, please click this link: {0}", forgotPassLink), Subject = "Locked out account information - GameStore PROG-3050" };
                    await mailService.SendEmailAsync(request);
                    ModelState.AddModelError("", "The account is locked out");

                    return View(model);
                }
            }
           
            string messages = string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));

            ModelState.AddModelError("", messages);

            ModelState.AddModelError("", "Invalid Login Attempt.");
            return View(model);
        }



        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel forgotPasswordModel)
        {
            if (!ModelState.IsValid)
                return View(forgotPasswordModel);

            var user = await userManager.FindByEmailAsync(forgotPasswordModel.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "This email is not registered in the system.");
                return View(forgotPasswordModel);

            }
              
            try
            {
                var token = await userManager.GeneratePasswordResetTokenAsync(user);
                var confirmationLink = Url.Action(nameof(ResetPassword), "Account", new { token, email = user.Email }, Request.Scheme);


                MailRequest request = new MailRequest() { ToEmail = forgotPasswordModel.Email, Body = "Hi there! Password recovery link: " + confirmationLink, Subject = "Welcome Letter - GameStore PROG-3050" };
                await mailService.SendEmailAsync(request);
                return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Account");
            }
        }

        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {
            var model = new ResetPasswordModel { Token = token, Email = email };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            if (!ModelState.IsValid)
                return View(resetPasswordModel);

            var user = await userManager.FindByEmailAsync(resetPasswordModel.Email);
            if (user == null)
            {
                return RedirectToAction("Error", "Account");
            }
               
            var resetPassResult = await userManager.ResetPasswordAsync(user, resetPasswordModel.Token, resetPasswordModel.Password);
            if (!resetPassResult.Succeeded)
            {
                foreach (var error in resetPassResult.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return View();
            }
            return RedirectToAction(nameof(ResetPasswordConfirmation));
        }
        
        [HttpGet]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        public ViewResult AccessDenied()
        {
            return View();
        }
    }
}
