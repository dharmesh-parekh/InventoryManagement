using InventoryManagement.Library.Services;
using InventoryManagement.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InventoryManagement.Core.Entities;
using InventoryManagement.Web.Extensions;
using InventoryManagementLibrary.Helpers;
using InventoryManagement.Web.Helpers;
using InventoryManagement.Library.Resources;
using InventoryManagement.Library.Enums;

namespace InventoryManagement.Web.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class UserController : Controller
    {
        #region Members

        private IUserService _userService;
        private IRoleService _roleService;

        #endregion

        #region Constructor
        public UserController(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        #endregion

        #region Action Methods
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetUserList(JQueryDataTableParams param)
        {
            try
            {
                string sortColumn = null;
                if (!(string.IsNullOrEmpty(param.sColumns)))
                {
                    var columns = param.sColumns.Split(',').ToArray();
                    if (param.iSortCol_0 < columns.Length)
                    {
                        sortColumn = columns[param.iSortCol_0];
                    }
                }
                var users = _userService.GetUserList(param.CurrentPage, param.iDisplayLength, sortColumn, param.sSortDir_0, param.sSearch);

                return Json(new
                {
                    sEcho = param.sEcho,
                    iTotalRecords = users.Any() ? users[0].TotalRows : 0,
                    iTotalDisplayRecords = users.Count(),
                    aaData = users
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, Message.GeneralError);
            }
            return Json(null);

        }

        public PartialViewResult UserModel(int userId = 0)
        {
            var model = new UserModel();
            try
            {
                if (userId > 0)
                {
                    var user = _userService.GetById(userId);
                    model.UserId = user.UserId;
                    model.FirstName = user.FirstName;
                    model.LastName = user.LastName;
                    model.Email = user.Email;
                    model.Password = user.Password;
                    model.RoleId = user.RoleId;
                    model.IsActive = user.IsActive;
                    model.MobileNo = user.MobileNo;
                }
                model.Roles = _roleService.GetRoles().Select(x => new DropdownHelper()
                {
                    Text = x.Name,
                    Value = x.RoleId
                });
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, Message.GeneralError);
            }
            return PartialView("_UserPartial", model);

        }

        [HttpPost]
        public ActionResult Save(UserModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return this.FailSaveResult();

                User user;
                if (model.UserId > 0)
                    user = _userService.GetById(model.UserId);
                else
                    user = new User();

                var isExists = _userService.CheckUserExists(model.Email, model.UserId);
                if (isExists)
                {
                    ModelState.AddModelError(string.Empty, "User with same email already exists! Please enter different value.");
                    return this.FailSaveResult();
                }
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.Password = model.Password;
                user.MobileNo = model.MobileNo;
                user.IsActive = model.IsActive;
                user.RoleId = model.RoleId;
                _userService.Save(user);
                this.AddSuccessMessage("User saved successfully.");
                return this.SuccessSaveResult();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, Message.GeneralError);
                ModelState.AddModelError(string.Empty, "Something is wrong! Please try again later");
                return this.FailSaveResult();
            }
        }

        [HttpPost]
        public JsonResult Delete(int userId)
        {
            try
            {
                var user = _userService.GetById(userId);
                if (user == null)
                {
                    this.AddErrorMessage("User does not exists or deleted.");
                    return this.FailSaveResult();
                }
                _userService.Delete(user);
                this.AddSuccessMessage("User deleted successfully.");
                return this.SuccessSaveResult();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, Message.GeneralError);
                this.AddErrorMessage("Something is wrong! Please try again later");
                return this.FailSaveResult();
            }
        }

        #endregion
    }
}