using LearningSystem.Constants;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LearningSystem.Data.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [MinLength(DataConstants.USERNAME_MIN_LENGTH)]
        [MaxLength(DataConstants.USERNAME_MAX_LENGTH)]
        public string Username { get; set; }

        [Required]
        [MinLength(DataConstants.NAME_MIN_LENGTH)]
        [MaxLength(DataConstants.NAME_MAX_LENGTH)]
        [RegularExpression("^[A-Za-z]+", ErrorMessage = "Name must have only letters.")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Birthdate { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
