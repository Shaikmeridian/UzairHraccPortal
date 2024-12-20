using HRACCPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRACCPortal.Controllers
{
    [Authorize]
    public class BalanceSheetController : Controller
    {
        private readonly BalanceSheetModel bsmodel;

        public BalanceSheetController()
        {
            bsmodel = new BalanceSheetModel();
        }
        // GET: BalanceSheet
        [HttpGet]
        public ActionResult ViewBalanceSheet()
        {
            bsmodel.GetBalanceSheetList();
            return View(bsmodel);
        }

        //POST: Add BalanceSheet
        [HttpPost]
        public ActionResult AddBalanceSheet(BalanceSheetModel bsmodel)
        {
            try
            {
                string status = bsmodel.AddEditBalanceSheet(bsmodel);
                return Json(new { message = status, JsonRequestBehavior.AllowGet });
            }
            catch (Exception e)
            {
                return Json(new { message = e.Message, JsonRequestBehavior.AllowGet });
            }

        }
        //POST: Get BalanceSheet for Edit View
        public ActionResult EditBalanceSheet(int? id)
        {
            try
            {
                BalanceSheetModel balancesheetData = bsmodel.GetBalanceSheetDetailsById(id);

                return Json(new { balancesheetData = balancesheetData, JsonRequestBehavior.AllowGet });
            }
            catch (Exception e)
            {
                return View(e.Message);
            }

        }
    }
}