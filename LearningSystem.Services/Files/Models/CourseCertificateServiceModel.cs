using LearningSystem.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningSystem.Services.Files.Models
{
    public class CourseCertificateServiceModel
    {
        public string CourseName { get; set; }

        public DateTime CourseStartDate { get; set; }

        public DateTime CourseEndDate { get; set; }

        public string StudentName { get; set; }

        public Grade? StudentGrade { get; set; }

        public string Trainer { get; set; }
    }
}
