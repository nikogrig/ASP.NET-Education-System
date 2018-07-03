using LearningSystem.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningSystem.Web.Areas.Admin.Models.Users
{
    public class EditUserProfileViewModel
    {
        public string Id { get; set; }

        [MinLength(DataConstants.NAME_MIN_LENGTH)]
        [MaxLength(DataConstants.NAME_MAX_LENGTH)]
        public string Username { get; set; }

        [Required]
        [MinLength(DataConstants.NAME_MIN_LENGTH)]
        [MaxLength(DataConstants.NAME_MAX_LENGTH)]
        [RegularExpression("^[A-Za-z]+", ErrorMessage = "Name must have only letters.")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Birthdate { get; set; }
    }
}
