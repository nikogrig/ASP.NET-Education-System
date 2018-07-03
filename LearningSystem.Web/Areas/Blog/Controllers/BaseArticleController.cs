﻿using LearningSystem.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningSystem.Web.Areas.Blog.Controllers
{
    [Area(AdminConstants.BLOG_AREA)]
    public abstract class BaseArticleController : Controller
    {
        
    }
}
