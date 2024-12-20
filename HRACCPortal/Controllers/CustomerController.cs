using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRACCPortal.Edmx;
using HRACCPortal.Models;

namespace HRACCPortal.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        // GET: Customer
        public HRACCDBEntities entities;
        clsCrud cls;
        public CustomerController()
        {
            entities = new HRACCDBEntities();
            cls = new clsCrud();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddCustomer()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCustomer(CustomerModel customer)
        {


            string message = "";
            try
            {
                message = cls.AddCustomer(customer);
            }
            catch (Exception e)
            {
                message = e.Message;
            }


            return Json(new { message = message, JsonRequestBehavior.AllowGet });
        }


        public ActionResult ViewCustomers()
        {
            cls.GetCustomers();
            return View(cls);
        }
        public ActionResult EditCustomer(int id)
        {
            CustomerModel cl = cls.GetCustomerById(id);
            return Json(new { cl = cl, JsonRequestBehavior.AllowGet });
        }

    }
}