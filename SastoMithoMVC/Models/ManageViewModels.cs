using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using SastoMithoClassLibrary.Models;

namespace SastoMithoMVC.Models
{
    public class PendingOrderViewModel 
    {
        public Guid OrderId { get; set; }
        public decimal Price { get; set; }
        public DateTime OrderIssued { get; set; }
        public string AssingedDelivererFirstName { get; set; }
        public string AssingedDelivererLastName { get; set; }
        public string AssignedDelivererPhoneNumber { get; set; }
        public string OrderStatus { get; set; }
        public List<OrderedItemModel> OrderedItems { get; set; }
    }
    public class OrderHistoryViewModel
    {
        public Guid OrderId { get; set; }
        public decimal Price { get; set; }
        public DateTime OrderIssued { get; set; }
        public DateTime OrderCompleted { get; set; }
        public string OrderStatus { get; set; }
        public List<OrderedItemModel> OrderedItems { get; set; }
    }
    public class PrimaryAddressViewModel
    {
        public string PrimaryAddress { get; set; }
        public string PrimaryAddressMemo { get; set; }
    }
    public class SecondaryAddressViewModel
    {
        public string SecondaryAddress { get; set; }
        public string SecondaryAddressMemo { get; set; }
    }
    public class PrimaryAddressLocationViewModel
    {
        public decimal PrimaryAddressLatitude { get; set; }
        public decimal PrimaryAddressLongitude { get; set; }
    }
    public class SecondaryAddressLocationViewModel
    {
        public decimal SecondaryAddressLatitude { get; set; }
        public decimal SecondaryAddressLongitude { get; set; }
    }


    public class ManageLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }
        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }

    public class FactorViewModel
    {
        public string Purpose { get; set; }
    }

    public class SetPasswordViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }
    public class ChangeEmailAddressViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }
    }
    public class ProfileInfoViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
    public class PasswordInfoViewModel
    {
        public bool HasPassword { get; set; }
    }
    public class PhoneNumberInfoViewModel
    {
        public string PhoneNumber { get; set; }
        public bool IsPhoneNumberConfirmed { get; set; }
    }
    public class EmailAddressInfoViewModel
    {
        public string EmailAddress { get; set; }
        public bool HasEmailAddress { get; set; }
        public bool IsEmailAddressConfirmed { get; set; }
    }



    public class VerifyPhoneNumberViewModel
    {
        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }

    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
    }
}