using LearningSystem.Constants;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningSystem.Infrastructure.TempDataMessages
{
    public static class TempDataMessageExtensions
    {
        public static void AddSuccessMessage(this ITempDataDictionary tempData, string message)
        {
            tempData[MessageConstants.TEMPDATA_SUCCESS_MESSAGE_KEY] = message;
        }

        public static void AddErrorMessage(this ITempDataDictionary tempData, string message)
        {
            tempData[MessageConstants.TEMPDATA_ERROR_MESSAGE_KEY] = message;
        }
    }
}
