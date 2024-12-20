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
    public class ContactController : Controller
    {

        // GET: Employer
        public HRACCDBEntities entities;
        clsCrud cls;
        public ContactController()
        {
            entities = new HRACCDBEntities();
            cls = new clsCrud();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddContact()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddContact(ContactModel contact)
        {

            string message = "";
            try
            {
                message = cls.AddContact(contact);
            }
            catch (Exception e)
            {
                message = e.Message;
            }


            return Json(new { message = message, JsonRequestBehavior.AllowGet });
        }


        public ActionResult ViewContact()
        {
            cls.GetContacts();
            return View(cls);
        }
        public ActionResult EditContact(int id)
        {
            ContactModel cl = cls.GetContactById(id);
            return Json(new { cl = cl, JsonRequestBehavior.AllowGet });
        }
    }
}