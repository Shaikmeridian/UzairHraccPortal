using HRACCPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRACCPortal.Controllers
{
    public class PaymentsReceivedController : Controller
    {
        // GET: BalanceSheet
        public ActionResult Index()
        {
            return View();
        }

        private readonly PaymentsReceivedModel prmodel;

        public PaymentsReceivedController()
        {
            prmodel = new PaymentsReceivedModel();
        }
        // GET: Position
        [HttpGet]
        public ActionResult ViewPaymentsReceived()
        {
            prmodel.GetPaymentsReceivedList();
            return View(prmodel);
        }

        //POST: Add Position
        [HttpPost]
        public ActionResult AddPaymentsReceived(PaymentsReceivedModel prmodel)
        {
            try
            {
                string status = prmodel.AddEditPaymentsReceived(prmodel);
                return Json(new { message = status, JsonRequestBehavior.AllowGet });
            }
            catch (Exception e)
            {
                return Json(new { message = e.Message, JsonRequestBehavior.AllowGet });
            }

        }
        //POST: Get Position for Edit View
        public ActionResult EditPaymentsReceived(int? id)
        {
            try
            {
                PaymentsReceivedModel paymentsreceivedData = prmodel.GetPaymentsReceivedDetailsById(id);

                return Json(new { paymentsreceivedData = paymentsreceivedData, JsonRequestBehavior.AllowGet });
            }
            catch (Exception e)
            {
                return View(e.Message);
            }

        }

    }
}