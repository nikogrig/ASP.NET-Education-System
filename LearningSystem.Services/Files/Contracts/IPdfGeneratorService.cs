using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningSystem.Services.Files.Contracts
{
    public interface IPdfGeneratorService
    {
        byte[] GeneratePdfFromHtml(string html);
    }
}
