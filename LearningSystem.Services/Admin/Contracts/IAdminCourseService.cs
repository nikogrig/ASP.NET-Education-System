using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningSystem.Services.Admin.Contracts
{
    public interface IAdminCourseService
    {
        Task CreateAsync(string name, string description, DateTime startDate, DateTime endDate, string trainerId);
    }
}
