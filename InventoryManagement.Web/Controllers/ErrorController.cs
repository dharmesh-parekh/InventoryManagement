using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagement.Web.Controllers
{
    public class ErrorController : Controller
    {

        #region Action Methods

        // GET: Error
        public ActionResult Error()
        {
            return View();
        }

        public ActionResult Unauthorized()
        {
            return View();
        }

        public ActionResult PageNotFound()
        {
            return View();
        }

        #endregion
    }
}