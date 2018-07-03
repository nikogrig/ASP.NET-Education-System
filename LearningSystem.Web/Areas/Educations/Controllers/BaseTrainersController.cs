using LearningSystem.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningSystem.Web.Areas.Educations.Controllers
{
    [Area(AdminConstants.EDUCATIONS_AREA)]
    [Authorize(Roles = IdentitiesConstants.TRAINER_ROLE)]
    public abstract class BaseTrainersController : Controller
    {
        
    }
}
