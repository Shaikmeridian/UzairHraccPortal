using HRACCPortal.Edmx;
using HRACCPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRACCPortal.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public HRACCDBEntities entities;
        clsCrud cls;
        public EmployeeController()
        {
            entities = new HRACCDBEntities();
            cls = new clsCrud();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddEmployee()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddEmployee(EmployeeModel employee)
        {


            string message = "";
            try
            {
                message = cls.AddEmployee(employee);
            }
            catch (Exception e)
            {
                message = e.Message;
            }


            return Json(new { message = message, JsonRequestBehavior.AllowGet });
        }


        public ActionResult ViewEmployees()
        {
            cls.GetEmployees();
            return View(cls);
        }
        public ActionResult EditEmployee(int id)
        {
            EmployeeModel cl = cls.GetEmployeeById(id);
            return Json(new { cl = cl, JsonRequestBehavior.AllowGet });
        }
    }
}
