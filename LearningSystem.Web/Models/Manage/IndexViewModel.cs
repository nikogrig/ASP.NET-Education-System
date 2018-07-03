using LearningSystem.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LearningSystem.Data.Models.ManageViewModels
{
    public class IndexViewModel
    {
        public string Username { get; set; }

        [Required]
        [MinLength(DataConstants.NAME_MIN_LENGTH)]
        [MaxLength(DataConstants.NAME_MAX_LENGTH)]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime Birthdate { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        public string StatusMessage { get; set; }
    }
}
