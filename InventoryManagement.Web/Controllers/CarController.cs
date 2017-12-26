using InventoryManagement.Library.Resources;
using InventoryManagement.Library.Services;
using InventoryManagement.Web.Extensions;
using InventoryManagement.Web.Helpers;
using InventoryManagement.Web.Models;
using InventoryManagementLibrary.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InventoryManagement.Core.Entities;

namespace InventoryManagement.Web.Controllers
{
    [Authorize]
    public class CarController : Controller
    {
        // GET: Car
        #region Members

        ICarService _carService;

        #endregion

        #region Constructor

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        #endregion

        #region Action Methods

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetCarList(JQueryDataTableParams param)
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
                var cars = _carService.GetCarList(UserDataHelper.LoggedInUserInfo().UserID, param.CurrentPage, param.iDisplayLength, sortColumn, param.sSortDir_0, param.sSearch);
                return Json(new
                {
                    sEcho = param.sEcho,
                    iTotalRecords = cars.Any() ? cars[0].TotalRows : 0,
                    iTotalDisplayRecords = cars.Count(),
                    aaData = cars
                }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                Logger.LogError(ex, Message.GeneralError);
            }
            return Json(null);
        }

        public PartialViewResult CarModel(long carId = 0)
        {
            var model = new CarModel();
            try
            {
               
                if (carId > 0)
                {
                    var car = _carService.GetById(carId);
                    model.CarId = car.CarId;
                    model.Brand = car.Brand;
                    model.Model = car.Model;
                    model.Price = car.Price;
                    model.Year = car.Year;
                    model.New = car.New;
                }               
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, Message.GeneralError);
            }
            return PartialView("_CarPartial", model);
        }

        [HttpPost]
        public ActionResult Save(CarModel carModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return this.FailSaveResult();

                Car car;
                if (carModel.CarId > 0)
                    car = _carService.GetById(carModel.CarId);
                else
                    car = new Car();

                car.Brand = carModel.Brand;
                car.Model = carModel.Model;
                car.Price = carModel.Price;
                car.Year = carModel.Year;
                car.New = carModel.New;
                car.UserId = UserDataHelper.LoggedInUserInfo().UserID;
                _carService.Save(car);
                this.AddSuccessMessage("Car saved successfully.");
                return this.SuccessSaveResult();
            }
            catch(Exception ex)
            {
                Logger.LogError(ex, Message.GeneralError);
                ModelState.AddModelError(string.Empty, "Something is wrong! Please try again later");
                return this.FailSaveResult();
            }
        }

        [HttpPost]
        public JsonResult Delete(long carId)
        {
            try
            {
                var car = _carService.GetById(carId);
                if (car == null)
                {
                    this.AddErrorMessage("Car does not exists or deleted.");
                    return this.FailSaveResult();
                }
                _carService.Delete(car);
                this.AddSuccessMessage("Car deleted successfully.");
                return this.SuccessSaveResult();
            }
            catch(Exception ex)
            {
                Logger.LogError(ex, Message.GeneralError);
                this.AddErrorMessage("Something is wrong! Please try again later");
                return this.FailSaveResult();
            }
        }

        #endregion
    }
}