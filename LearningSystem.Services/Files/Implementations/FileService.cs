using LearningSystem.Data;
using LearningSystem.Data.Models;
using LearningSystem.Services.Files.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearningSystem.Constants;
using LearningSystem.Services.Files.Models;

namespace LearningSystem.Services.Files.Implementations
{
    public class FileService : IFileService
    {
        private readonly LearningSystemDbContext db;
        private readonly IPdfGeneratorService pdfService;

        public FileService(LearningSystemDbContext db, IPdfGeneratorService pdfService)
        {
            this.db = db;
            this.pdfService = pdfService;
        }

        public async Task<bool> SaveExamSubmissionAsync(int courseId, string studentId, byte[] examSubmission)
        {
            var studentInCourse = await this.db
                .FindAsync<StudentCourse>(studentId, courseId);

            if (studentInCourse == null)
            {
                return false;
            }

            studentInCourse.ExamSubmission = examSubmission;

            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<byte[]> GetExamSubmissionAsync(string studentId, int courseId)
        {
            var studentIncourse = await this.db
                .FindAsync<StudentCourse>(studentId, courseId);

            return studentIncourse?.ExamSubmission;
        }

        public async Task<byte[]> GetPdfCertificateAsync(string studentId, int courseId)
        {
            var studentInCourse = await this.db.FindAsync<StudentCourse>(studentId, courseId);

            if (studentInCourse == null)
            {
                return null;
            }

            var certificateData = await this.db
                .Courses
                .Where(c => c.Id == courseId)
                .Select(c => new 
                {
                    CourseName = c.Name,
                    CourseStartDate = c.StartDate,
                    CourseEndDate = c.EndDate,
                    StudentName = c.Students
                                   .Where(s => s.StudentId == studentId)
                                   .Select(s => s.Student.Name)
                                   .FirstOrDefault(),
                    StudentGrade = c.Students
                                   .Where(s => s.StudentId == studentId)
                                   .Select(s => s.Grade)
                                   .FirstOrDefault(),
                    Trainer = c.Trainer.Name

                })
                .FirstOrDefaultAsync();

            var generator = this.pdfService.GeneratePdfFromHtml(string.Format(
                                                                       PdfFormatConstants.PDF_CERTIFICATE_FORMAT,
                                                                       certificateData.CourseName,
                                                                       certificateData.CourseStartDate.ToShortDateString(),
                                                                       certificateData.CourseEndDate.ToShortDateString(),
                                                                       certificateData.StudentName,
                                                                       certificateData.StudentGrade,
                                                                       certificateData.Trainer,
                                                                       DateTime.UtcNow.ToShortDateString()));

            return generator;
        }
    }
}
