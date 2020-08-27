using System;
using System.Collections.Generic;
using System.Linq;

namespace PremiumCalculatorUI.Helpers
{
    public class GlobalHelper
    {
        public static string FormatError(Exception ex)
        {
            string message = "Error Message: " + ex.Message + "\nStack Trace: " + ex.StackTrace;
            return message;
        }
    }
}