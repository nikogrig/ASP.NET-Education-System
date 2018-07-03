using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningSystem.Services.HTML.Contracts
{
    public interface IHtmlService
    {
        string DoSanitize(string htmlContent);
    }
}
