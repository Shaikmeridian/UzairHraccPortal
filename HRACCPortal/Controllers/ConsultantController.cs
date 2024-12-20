using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using HRACCPortal.Edmx;
using HRACCPortal.Models;
using HRACCPortal.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Runtime.CompilerServices;

namespace HRACCPortal.Controllers
{
    [Authorize]
    public class ConsultantController : Controller
    {

        public HRACCDBEntities entities;
        private AccountController _accountController;
        clsCrud cls;
        private ConsultantService _consultantService;



        public ConsultantController()
        {
            entities = new HRACCDBEntities();
            cls = new clsCrud();


        }

        private ApplicationUserManager UserManager
        {
            get
            {
                if (HttpContext != null && HttpContext.GetOwinContext() != null)
                {
                    return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                }
                return null;
            }
        }

        private ApplicationSignInManager SignInManager
        {
            get
            {
                if (HttpContext != null && HttpContext.GetOwinContext() != null)
                {
                    return HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
                }
                return null;
            }
        }

        private ConsultantService ConsultantService
        {
            get
            {
                if (_consultantService == null)
                {
                    var userManager = UserManager;
                    var signInManager = SignInManager;
                    if (userManager != null && signInManager != null)
                    {
                        _consultantService = new ConsultantService(userManager, signInManager);
                    }
                }
                return _consultantService;
            }
        }

        // GET: Consultant
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult AddConsultant()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> AddConsultant(ConsultantModel consultant)
        {
            string message = "";
            string mess = "";
            try
            {
                message = cls.AddConsultant(consultant);
                string randomPassword = GenerateRandomPassword();

                if (message == "success")
                {
                    // Create a RegisterViewModel and pass it to the service for user registration
                    var registerViewModel = new RegisterViewModel
                    {
                        Email = consultant.Email,
                        Password = randomPassword,
                        ConfirmPassword = randomPassword,
                        PhoneNumber = consultant.Phone,
                        Active = consultant.Active,
                        UserName = consultant.UserName
                    };


                    // Debugging RegisterConsultantAsync call
                    mess = await ConsultantService.RegisterConsultantAsync(registerViewModel);

                    if(mess =="success")
                    {
                        await SendWelcomeEmail(consultant.Email, consultant.UserName, randomPassword);
                    }
                    // Call the service directly

                }
                else
                {
                    mess = "Error adding consultant to the database.";
                }


            }
            catch (Exception e)
            {
                message = e.Message;
                mess = e.Message;
            }

            // Return the message in a JsonResult
            return Json(new { message = message }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult ViewConsultants()
        {
            cls.GetConsultants();
            return View(cls);
        }

        public ActionResult EditConsultant(int id)
        {
            ConsultantModel cl = cls.GetConsultantById(id);
            return Json(new { cl = cl, JsonRequestBehavior.AllowGet });
        }
        public ActionResult ConsultantPositionDetails()
        {
            cls.GetconsultantPositionDetails();
            return View(cls);
        }
        public ActionResult EditConsultantPositionDetails(int id)
        {
            ConsultantPositionDetailsModel cl = cls.GetConsultantPositionDetailsById(id);
            return Json(new { cl = cl, JsonRequestBehavior.AllowGet });
        }
        [HttpPost]
        public ActionResult AddConsultantPositionDetails(ConsultantPositionDetailsModel consultantPositionDetail)
        {
            string message = "";
            try
            {
                message = cls.AddConsultantPositionDetails(consultantPositionDetail);
            }
            catch (Exception e)
            {
                message = e.Message;
            }


            return Json(new { message = message, JsonRequestBehavior.AllowGet });
        }
        private string GenerateRandomPassword()
        {
            const int passwordLength = 12;
            const string upperCaseChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string lowerCaseChars = "abcdefghijklmnopqrstuvwxyz";
            const string digits = "0123456789";
            const string specialChars = "!@#$%^&*()_+[]{}|;:,.<>?";
            const string allChars = upperCaseChars + lowerCaseChars + digits + specialChars;

            Random random = new Random();
            StringBuilder password = new StringBuilder();

            // Ensure at least one character from each required set
            password.Append(upperCaseChars[random.Next(upperCaseChars.Length)]);
            password.Append(lowerCaseChars[random.Next(lowerCaseChars.Length)]);
            password.Append(digits[random.Next(digits.Length)]);
            password.Append(specialChars[random.Next(specialChars.Length)]);

            // Fill the remaining characters randomly
            for (int i = 4; i < passwordLength; i++)
            {
                password.Append(allChars[random.Next(allChars.Length)]);
            }

            // Shuffle the characters to ensure randomness
            return new string(password.ToString().OrderBy(_ => random.Next()).ToArray());
        }

        private async Task SendWelcomeEmail(string email, string UserName ,  string temporaryPassword)
        {
            try
            {
                // Use the correct SMTP host for Gmail
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587, // Port for TLS
                    Credentials = new NetworkCredential("shaikuzairuzair@gmail.com", "fuuanbubiwfpfavl"), // Use your app password
                    EnableSsl = true, // Enable SSL/TLS
                };

                string subject = "Welcome to HRACC Portal";
                string body = $"" +
                             $"<h1>Welcome to HRACC Portal</h1>" +
                             $"<p>Dear Consultant,</p>" +
                             $"<p>We are excited to welcome you to the HRACC Portal. Below are your login details:</p>" +
                             $"<p><strong>Login Link:</strong> <a href='https://your-portal-login-url.com'>Login Here</a></p>" +
                             $"<p><strong> User Name : </strong> {UserName} </p>"+
                             $"<p><strong> Email :</strong>{email} </p>"+
                             $"<p><strong>Temporary Password:</strong> {temporaryPassword}</p>" +
                             $"<p>Please use this password to log in and change it immediately for security purposes.</p>" +
                             $"<p>Best Regards,<br>HRACC Portal Team</p>";

                var mailMessage = new MailMessage
                {
                    From = new MailAddress("shaikuzairuzair@gmail.com"),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true,
                };

                // Add recipient
                mailMessage.To.Add(email);

                // Send the email
                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                // Log or handle the email sending failure
                Console.WriteLine("Error sending email: " + ex.Message);
            }
        }

    }
}