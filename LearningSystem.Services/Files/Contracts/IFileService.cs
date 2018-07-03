using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningSystem.Services.Files.Contracts
{
    public interface IFileService
    {
        Task<bool> SaveExamSubmissionAsync(int courseId, string studentId, byte[] examSubmission);

        Task<byte[]> GetExamSubmissionAsync(string studentId, int courseId);

        Task<byte[]> GetPdfCertificateAsync(string studentId, int courseId);
    }
}
