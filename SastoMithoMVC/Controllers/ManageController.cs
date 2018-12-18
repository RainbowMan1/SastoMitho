using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using SastoMithoMVC.Models;

namespace SastoMithoMVC.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ManageController()
        {
        }

        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Manage/Index
        public async Task<ActionResult> Index()
        {
            
            
            var userId = Guid.Parse(User.Identity.GetUserId());
            var model = new List<PendingOrderViewModel>();
            model = await DataAccess.DataAccess.GetPendingOrders(userId);

            return View(model);
        }

      


        //
        // GET: /Manage/VerifyPhoneNumber
        public async Task<ActionResult> VerifyPhoneNumber(string phoneNumber)
        {
            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(Guid.Parse(User.Identity.GetUserId()), phoneNumber);
            // Send an SMS through the SMS provider to verify the phone number
            return phoneNumber == null ? View("Error") : View(new VerifyPhoneNumberViewModel { PhoneNumber = phoneNumber });
        }

        //
        // POST: /Manage/VerifyPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyPhoneNumber(VerifyPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePhoneNumberAsync(Guid.Parse(User.Identity.GetUserId()), model.PhoneNumber, model.Code);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(Guid.Parse(User.Identity.GetUserId()));
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.AddPhoneSuccess });
            }
            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "Failed to verify phone");
            return View(model);
        }


        //If password = null set change "change password" to Set Password
        // GET: /Manage/SetPassword
        public ActionResult SetPassword()
        {
            return View();
        }
        public ActionResult ProfileInfo()
        {
           
            var model = new ProfileInfoViewModel{
                FirstName = GetFirstName(),
                LastName = GetLastName()
            };
            //model = await DataAccess.DataAccess.GetProfileInfo(userId);
            return View();
        }
        public PartialViewResult PhoneNumberInfo()
        {
            var model = new PhoneNumberInfoViewModel {
                PhoneNumber=GetPhoneNumber(),
                IsPhoneNumberConfirmed = IsPhoneNumberConfirmed()
            };
            return PartialView(model);
        }
        public PartialViewResult PasswordInfo()
        {

            var model = new PasswordInfoViewModel {
                HasPassword  = HasPassword()
            };
            return PartialView(model);
        }
        public PartialViewResult EmailAddressInfo()
        {

            var model = new EmailAddressInfoViewModel {
                EmailAddress = GetEmail(),
                HasEmailAddress = HasEmailAddress(),
                IsEmailAddressConfirmed = IsEmailAddressConfirmed()
            };
            return PartialView(model);
        }
        //
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(Guid.Parse(User.Identity.GetUserId()), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(Guid.Parse(User.Identity.GetUserId()));
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("ProfileInfo");
            }
            AddErrors(result);
            return View(model);
        }
        public ActionResult ChangePhoneNumber()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePhoneNumber(ChangePhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.SetPhoneNumberAsync(Guid.Parse(User.Identity.GetUserId()), model.PhoneNumber);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(Guid.Parse(User.Identity.GetUserId()));
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("VerifyPhoneNumber", new { PhoneNumber = model.PhoneNumber });
            }
            AddErrors(result);
            return View(model);
        }

        public ActionResult ChangeEmailAddress()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangeEmailAddress(ChangeEmailAddressViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.SetEmailAsync(Guid.Parse(User.Identity.GetUserId()), model.EmailAddress);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(Guid.Parse(User.Identity.GetUserId()));
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("ProfileInfo");
            }
            AddErrors(result);
            return View(model);
        }





        public async Task<ActionResult> OrderHistoryAsync()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var model = new List<OrderHistoryViewModel>();
            model = await DataAccess.DataAccess.GetOrderHistory(userId);

            return View(model);
        }
        public ActionResult Addresses()
        {

            var model = new PrimaryAddressViewModel
            {
                PrimaryAddress = GetPrimaryAddress(),
                PrimaryAddressMemo = GetPrimaryAddressMemo()
            };

            return View(model);
        }
        public PartialViewResult PrimaryAddressLocation()
        {
            var model = new PrimaryAddressLocationViewModel
            {
                PrimaryAddressLatitude = GetPrimaryAddressLatitude(),
                PrimaryAddressLongitude = GetPrimaryAddressLongitude()
            };
            return PartialView(model);
        }
        public PartialViewResult SecondaryAddress()
        {

            var model = new SecondaryAddressViewModel
            {
                SecondaryAddress = GetSecondaryAddress(),
                SecondaryAddressMemo = GetSecondaryAddressMemo()
            };
            return PartialView(model);
        }
        public PartialViewResult SecondaryAddressLocation()
        {

            var model = new SecondaryAddressLocationViewModel
            {
                SecondaryAddressLatitude = GetSecondaryAddressLatitude(),
                SecondaryAddressLongitude = GetSecondaryAddressLongitude()
            };
            return PartialView(model);
        }
        //
        // POST: /Manage/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await UserManager.AddPasswordAsync(Guid.Parse(User.Identity.GetUserId()), model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(Guid.Parse(User.Identity.GetUserId()));
                    if (user != null)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }
                    return RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

#region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
        private string GetFirstName()
        {
            var user = UserManager.FindById(Guid.Parse(User.Identity.GetUserId()));
            if (user != null)
            {
                return user.FirstName;
            }
            return null;
        }
        private string GetLastName()
        {
            var user = UserManager.FindById(Guid.Parse(User.Identity.GetUserId()));
            if (user != null)
            {
                return user.LastName;
            }
            return null;
        }
        private string GetEmail()
        {
            var user = UserManager.FindById(Guid.Parse(User.Identity.GetUserId()));
            if (user != null)
            {
                return user.EmailAddress;
            }
            return null;
        }
        private string GetPhoneNumber()
        {
            var user = UserManager.FindById(Guid.Parse(User.Identity.GetUserId()));
            if (user != null)
            {
                return user.PhoneNumber;
            }
            return null;
        }
        private string GetPrimaryAddress()
        {
            var user = UserManager.FindById(Guid.Parse(User.Identity.GetUserId()));
            if (user != null)
            {
                return user.PrimaryAddress.AddressName;
            }
            return null;
        }
        private string GetPrimaryAddressMemo()
        {
            var user = UserManager.FindById(Guid.Parse(User.Identity.GetUserId()));
            if (user != null)
            {
                return user.PrimaryAddress.AddressMemo;
            }
            return null;
        }
        private decimal GetPrimaryAddressLatitude()
        {
            var user = UserManager.FindById(Guid.Parse(User.Identity.GetUserId()));
            if (user != null)
            {
                return user.PrimaryAddress.Location.Latitude;
            }
            return 0;
        }
        private decimal GetPrimaryAddressLongitude()
        {
            var user = UserManager.FindById(Guid.Parse(User.Identity.GetUserId()));
            if (user != null)
            {
                return user.PrimaryAddress.Location.Longitude;
            }
            return 0;
        }
        private string GetSecondaryAddress()
        {
            var user = UserManager.FindById(Guid.Parse(User.Identity.GetUserId()));
            if (user != null)
            {
                return user.SecondaryAddress.AddressName;
            }
            return null;
        }
        private string GetSecondaryAddressMemo()
        {
            var user = UserManager.FindById(Guid.Parse(User.Identity.GetUserId()));
            if (user != null)
            {
                return user.SecondaryAddress.AddressMemo;
            }
            return null;
        }
        private decimal GetSecondaryAddressLatitude()
        {
            var user = UserManager.FindById(Guid.Parse(User.Identity.GetUserId()));
            if (user != null)
            {
                return user.SecondaryAddress.Location.Latitude;
            }
            return 0;
        }
        private decimal GetSecondaryAddressLongitude()
        {
            var user = UserManager.FindById(Guid.Parse(User.Identity.GetUserId()));
            if (user != null)
            {
                return user.SecondaryAddress.Location.Longitude;
            }
            return 0;
        }
        private bool HasPassword()
        { 
            var user = UserManager.FindById(Guid.Parse(User.Identity.GetUserId()));
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }
        private bool HasEmailAddress()
        {
            var user = UserManager.FindById(Guid.Parse(User.Identity.GetUserId()));
            if (user != null)
            {
                return user.EmailAddress != null;
            }
            return false;
        }

        private bool IsPhoneNumberConfirmed()
        {
            var user = UserManager.FindById(Guid.Parse(User.Identity.GetUserId()));
            if (user != null)
            {
                return user.PhoneNumberConfirmed;
            }
            return false;
        }
        private bool IsEmailAddressConfirmed()
        {
            var user = UserManager.FindById(Guid.Parse(User.Identity.GetUserId()));
            if (user != null)
            {
                return user.EmailConfirmed == true;
            }
            return false;
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

#endregion
    }
}