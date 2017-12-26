using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagement.Web.Controllers
{

    [Authorize]
    public class HomeController : Controller
    {

        #region Action Methods
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        #endregion
    }
}