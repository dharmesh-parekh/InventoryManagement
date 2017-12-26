using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace InventoryManagement.Web.Extensions
{
    public static class MakeActiveExtension
    {
        public static string MakeActive(this UrlHelper urlHelper, string controller)
        {
            string result = "active";

            var controllers = controller.Split(',').Where(x => !string.IsNullOrEmpty(x)).Select(x => x.ToLower()).ToList();
            string controllerName = urlHelper.RequestContext.RouteData.Values["controller"].ToString().ToLower();
            if (!controllers.Contains(controllerName))
            {
                result = null;
            }
            return result;
        }
    }
}
