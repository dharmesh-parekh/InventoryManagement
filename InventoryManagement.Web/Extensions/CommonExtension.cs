using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.Web.Extensions
{
    public static class CommonExtension
    {
        public static string ToCleanString(this object obj)
        {
            if (obj == null)
            {
                return string.Empty;
            }

            try
            {
                return obj.ToString().Trim();
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}