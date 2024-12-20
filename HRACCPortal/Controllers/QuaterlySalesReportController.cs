using HRACCPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRACCPortal.Controllers
{
    public class QuaterlySalesReportController : Controller
    {
        private readonly QuaterlySalesReportModel qsrmodel;

        public QuaterlySalesReportController()
        {
            qsrmodel = new QuaterlySalesReportModel();
        }
        // GET: Position
        [HttpGet]
        public ActionResult ViewQuaterlySalesReport()
        {
            qsrmodel.GetQuaterlySalesReportList();
            return View(qsrmodel);
        }

        //POST: Add Position
        [HttpPost]
        public ActionResult AddQuaterlySalesReport(QuaterlySalesReportModel qsrmodel)
        {
            try
            {
                string status = qsrmodel.AddEditQuaterlySalesReport(qsrmodel);
                return Json(new { message = status, JsonRequestBehavior.AllowGet });
            }
            catch (Exception e)
            {
                return Json(new { message = e.Message, JsonRequestBehavior.AllowGet });
            }

        }
        //POST: Get Position for Edit View
        public ActionResult EditQuaterlySalesReport(int? id)
        {
            try
            {
                QuaterlySalesReportModel quaterlysalesreportData = qsrmodel.GetQuaterlySalesReportDetailsById(id);

                return Json(new { quaterlysalesreportData = quaterlysalesreportData, JsonRequestBehavior.AllowGet });
            }
            catch (Exception e)
            {
                return View(e.Message);
            }

        }
    }
}