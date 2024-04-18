using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRUD_MVC_DotNet.Context; // Adjust namespace as needed

namespace CRUD_MVC_DotNet.Controllers
{
    public class EmployeeController : Controller
    {
        private db_adminEntities dbObj = new db_adminEntities();

        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Employee(tbl_Emp obj)
        {
            if (obj != null)
            {
                return View(obj);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult AddEmployee(tbl_Emp model)
        {
            if (ModelState.IsValid)
            {
                if (model.ID == 0)
                {
                    dbObj.tbl_Emp.Add(model);
                }
                else
                {
                    dbObj.Entry(model).State = EntityState.Modified;
                }
                dbObj.SaveChanges();
            }
            ModelState.Clear();
            return RedirectToAction("EmployeeList");
        }

        public ActionResult EmployeeList()
        {
            var employees = dbObj.tbl_Emp.ToList();
            return View(employees);
        }

        public ActionResult Delete(int id)
        {
            var employee = dbObj.tbl_Emp.FirstOrDefault(e => e.ID == id);
            if (employee != null)
            {
                dbObj.tbl_Emp.Remove(employee);
                dbObj.SaveChanges();
            }
            return RedirectToAction("EmployeeList");
        }
    }
}
