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

        //[HttpGet]
        //public ActionResult AssignContact(int customerId)
        //{
        //    ViewBag.CustomerId = customerId;

        //    var contacts = entities.Contacts
        //                     .Select(c => new ContactModel
        //                     {
        //                         ContactIdPK = c.ContactIdPK,
        //                         ContactName = c.ContactName,
        //                         ContactEmail = c.ContactEmail,
                                 
        //                     })
        //                     .ToList();

        //    return View(contacts);
        //}

        //[HttpPost]
        //public JsonResult SaveCustomerContacts(int customerId, List<int> selectedContactIds)
        //{
        //    // Get the existing contacts for the customer
        //    var existingContacts = entities.CustomerContacts.Where(cc => cc.CustomerId == customerId).ToList();

        //    // Remove existing contacts
        //    if (existingContacts.Count > 0)
        //    {
        //        // Remove existing contacts
        //        foreach (var contact in existingContacts)
        //        {
        //            entities.CustomerContacts.DeleteObject(contact);
        //        }
        //    }
        //    // Add new contacts
        //    if (selectedContactIds != null && selectedContactIds.Count > 0)
        //    {
        //        foreach (var contactId in selectedContactIds)
        //        {
        //            var newContact = new CustomerContact
        //            {
        //                CustomerId = customerId,
        //                ContactId = contactId
        //            };
        //            entities.CustomerContacts.AddObject(newContact);
        //        }
        //    }

        //    // Save changes to the database
        //    entities.SaveChanges();

        //    return Json(new { success = true, message = "Contacts successfully assigned!" });
        //}


        //[HttpGet]
        //public ActionResult ViewAssignedContact(int customerId)
        //{
        //    var assignedContacts = entities.CustomerContacts
        //                             .Where(cc => cc.CustomerId == customerId)
        //                             .Select(cc => new ContactModel
        //                             {
        //                                 ContactIdPK = cc.ContactId,
        //                                 ContactName = cc.Contact.ContactName,
        //                                 ContactEmail = cc.Contact.ContactEmail
        //                             })
        //                             .ToList();

        //    return View(assignedContacts);
        //}

    }
}