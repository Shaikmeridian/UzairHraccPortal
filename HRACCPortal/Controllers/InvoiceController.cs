using HRACCPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRACCPortal.Controllers
{
    [Authorize]
    public class InvoiceController : Controller
    {
        private readonly InvoiceModel model;
        private readonly clsCrud cls;

        public InvoiceController()
        {
            model = new InvoiceModel();
            cls = new clsCrud();
        }
        // GET: Invoice
        public ActionResult ViewInvoice()
        {
            model.GetInvoiceList();
            return View(model);
        }
        public ActionResult GetInvoice(int id)
        {
            InvoicePdfModel invoicePdfModel = cls.GenratePdf(id);

            return View(invoicePdfModel);
        }
        //POST: Add/Edit Position Rate
        [HttpPost]
        public ActionResult AddEditInvoice(InvoiceModel model)
        {
            try
            {
                string status = model.AddEditInvoice(model);
                return Json(new { message = status, JsonRequestBehavior.AllowGet });
            }
            catch (Exception e)
            {
                return Json(new { message = e.Message, JsonRequestBehavior.AllowGet });
            }
        }

        //POST: Get Position Rate Edit View
        public ActionResult EditInvoice(int? id)
        {
            try
            {
                InvoiceModel invoiceData = model.GetInvoiceDetailsById(id);

                return Json(new { invoiceData = invoiceData, JsonRequestBehavior.AllowGet });
            }
            catch (Exception e)
            {
                return View(e.Message);
            }

        }
    }
}