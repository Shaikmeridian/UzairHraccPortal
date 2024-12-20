using HRACCPortal.Edmx;
using HRACCPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRACCPortal.Controllers
{
    public class SubContractorController : Controller
    {
        public HRACCDBEntities entities;
        clsCrud cls;
        public SubContractorController()
        {
            entities = new HRACCDBEntities();
            cls = new clsCrud();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddSubContractor()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddSubContractor(SubContractorModel subcontractor)
        {


            string message = "";
            try
            {
                message = cls.AddSubContractor(subcontractor);
            }
            catch (Exception e)
            {
                message = e.Message;
            }


            return Json(new { message = message, JsonRequestBehavior.AllowGet });
        }


        public ActionResult ViewSubContractors()
        {
            cls.GetSubContractors();
            return View(cls);
        }
        public ActionResult EditSubContractor(int id)
        {
            SubContractorModel cl = cls.GetSubContractorById(id);
            return Json(new { cl = cl, JsonRequestBehavior.AllowGet });
        }

    }
}