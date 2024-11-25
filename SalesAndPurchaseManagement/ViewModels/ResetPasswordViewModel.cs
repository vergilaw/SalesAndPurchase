﻿using System.ComponentModel.DataAnnotations;

namespace SalesAndPurchaseManagement.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Email là bắt buộc.")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        public string Email { get; set; }
    }
}
