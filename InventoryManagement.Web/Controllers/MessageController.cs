using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InventoryManagement.Web.Extensions;

namespace InventoryManagement.Web.Controllers
{
    public class MessageController : Controller
    {
        #region Action Methods

        [HttpGet]
        public JsonResult Messages()
        {
            return Json(this.GetMessages(), JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}