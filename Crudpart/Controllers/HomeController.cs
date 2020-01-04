using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyAppModels;
using MyAppDb.DbOperatuon;

namespace Crudpart.Controllers
{
    public class HomeController : Controller
    {
        EmployeeRepository repository = null;
        public HomeController()
        {
            repository = new EmployeeRepository();
        }
        //Insert Action methods 
        //      insert All The All The Records At time
        // GET: Home
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(EmployeeModel model)
        
{
            if (ModelState.IsValid)
            {
                int id = repository.AddEmployee(model);
                if (id>0)
                {
                    ModelState.Clear();
                    ViewBag.IsSuccess = "Data Added";
                }
            }
            return View();
        }
        //Display alll The Records At at time
        public ActionResult GetAllRecords()
        {
            var result = repository.GetAllEmployees();
            return View(result);
        }
        //Display the reords single records at a time
        public ActionResult Details(int id)
        {
            var employee = repository.GetEmployee(id);
            return View(employee);
        }
        //update Action performs
        public ActionResult Edit(int id)
        {
            var employee = repository.GetEmployee(id);
            return View(employee);
        }
        [HttpPost]
        public ActionResult Edit(EmployeeModel model)
        {
            if (ModelState.IsValid)
            {
                repository.UpDateEmployee(model.Id, model);
                return RedirectToAction("GetAllRecords");
            }
            return View();
        }

        //Delete the records from database
        
        public ActionResult Delete(int id)
        {
            repository.DeleteEmployee(id);
            return RedirectToAction("GetAllRecords");
        }
    }
}