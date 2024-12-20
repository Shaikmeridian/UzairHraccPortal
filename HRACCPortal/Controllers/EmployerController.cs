using HRACCPortal.Edmx;
using HRACCPortal.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace HRACCPortal.Controllers
{
    public class EmployerController : Controller
    {

        // GET: Employer
        public HRACCDBEntities entities;
        clsCrud cls;
        public EmployerController()
        {
            entities = new HRACCDBEntities();
            cls = new clsCrud();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddEmployer()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddEmployer(EmployerModel employer)
        {

            string message = "";
            try
            {
                message = cls.AddEmployer(employer);
            }
            catch (Exception e)
            {
                message = e.Message;
            }


            return Json(new { message = message, JsonRequestBehavior.AllowGet });
        }


        public ActionResult ViewEmployers()
        {
            cls.GetEmployers();
            return View(cls);
        }
        public ActionResult EditEmployer(int id)
        {
            EmployerModel cl = cls.GetEmployerById(id);
            return Json(new { cl = cl, JsonRequestBehavior.AllowGet });
        }
    }
}