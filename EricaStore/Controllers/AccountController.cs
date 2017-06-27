using EricaStore.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EricaStore.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            AccountModel model = new AccountModel();
            EricaStoreEntities entities = new EricaStoreEntities();
            var email = User.Identity.Name;
            var user = entities.AspNetUsers.Single(x => x.Email == email);
            var activePlan = user.MembershipTypeUsers.FirstOrDefault(x => x.EndDate == null);
            if (activePlan != null)
            {
                model.DaysAvailable = ConvertStringToDayOfWeekArray(activePlan.MembershipType.Days);
                model.SelectedMembership = new MembershipTypeModel { Name = activePlan.MembershipType.Name };
            }
            else
            {
                model.DaysAvailable = new DayOfWeek[0];
            }
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View(new RegisterModel()) ;
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            if(ModelState.IsValid)
            {
                using (IdentityModels entities = new IdentityModels())
                {


                    var userStore = new UserStore<User>(entities);

                    var manager = new UserManager<User>(userStore);
                    manager.UserTokenProvider = new EmailTokenProvider<User>();

                    var user = new User()
                    {
                        UserName = model.EmailAddress,
                        Email = model.EmailAddress
                    };


                    IdentityResult result = manager.Create(user, model.Password);
                    
                    if (result.Succeeded)
                    {

                    
                    User u = manager.FindByName(model.EmailAddress);

                    //create a customer record in braintree

                    string merchantId = ConfigurationManager.AppSettings["Braintree.MerchantID"];
                    string publicKey = ConfigurationManager.AppSettings["Braintree.PublicKey"];
                    string privateKey = ConfigurationManager.AppSettings["Braintree.PrivateKey"];
                    string environment = ConfigurationManager.AppSettings["Braintree.Environment"];

                    Braintree.BraintreeGateway braintree = new Braintree.BraintreeGateway(environment, merchantId, publicKey, privateKey);

                    Braintree.CustomerRequest customer = new Braintree.CustomerRequest();
                    customer.CustomerId = u.Id;
                    customer.Email = u.Email;

                    var r = await braintree.Customer.CreateAsync(customer);


                    string confirmationToken = manager.GenerateEmailConfirmationToken(u.Id);



                    string sendGridApiKey = System.Configuration.ConfigurationManager.AppSettings["SendGrid.ApiKey"];

                    SendGrid.SendGridClient client = new SendGrid.SendGridClient(sendGridApiKey);
                    SendGrid.Helpers.Mail.SendGridMessage message = new SendGrid.Helpers.Mail.SendGridMessage();
                    //TODO: string.Format("Recepit for order {0}, ord.Id);
                    message.Subject = "Please confirm your account";
                    message.From = new SendGrid.Helpers.Mail.EmailAddress("admin@freshstartjuiceco.com", "Fresh Start Juice Co");
                    message.AddTo(new SendGrid.Helpers.Mail.EmailAddress(model.EmailAddress));
                    SendGrid.Helpers.Mail.Content contents = new SendGrid.Helpers.Mail.Content("text/html", string.Format("<a href=\"{0}\">Confirm Account</a>", Request.Url.GetLeftPart(UriPartial.Authority) + "Account/Login/" + confirmationToken));

                        
                    message.AddContent(contents.Type, contents.Value);

                        
                    SendGrid.Response response = await client.SendEmailAsync(message);



                    
                        return RedirectToAction("ConfirmSent");

                    }
                    else
                    {
                        ModelState.AddModelError("EmailAddress", "Unable to register with this email address");
                    }
                }
                

            }
            return View(model);
        }






        [AllowAnonymous]
        public ActionResult ConfirmSent()
        {
            return View();
        }




        [AllowAnonymous]
        public ActionResult Confirm(string id, string email)
        {
            using (IdentityModels entities = new IdentityModels())
            {
                var userStore = new UserStore<User>(entities);

                var manager = new UserManager<User>(userStore);
                manager.UserTokenProvider = new EmailTokenProvider<User>();
                var user = manager.FindByName(email);
                if (user != null)
                {
                    var result = manager.ConfirmEmail(user.Id, id);
                    if (result.Succeeded)
                    {
                        TempData.Add("AccountConfirmed", true);
                        return RedirectToAction("Login");
                    }
                }
            }


            return RedirectToAction("Index", "Home");
        }


        [AllowAnonymous]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }


        [AllowAnonymous]
        public ActionResult Login()
        {

            return View(new LoginModel());
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                using (IdentityModels entities = new IdentityModels())
                {
                    var userStore = new UserStore<User>(entities);

                    var manager = new UserManager<User>(userStore);

                    var user = manager.FindByEmail(model.EmailAddress);
                    if (manager.CheckPassword(user, model.Password))
                    {
                        FormsAuthentication.SetAuthCookie(model.EmailAddress, true);
                        return RedirectToAction("Index", "Account");
                    }
                    ModelState.AddModelError("EmailAddress", "Could not sign in with this username and/or password");
                }
            }
            return View(model);
        }


        public ActionResult MembershipType()
        {
            using (EricaStoreEntities entities = new EricaStoreEntities())
            {
                MembershipTypeModel[] model = entities.MembershipTypes.Select(x => new MembershipTypeModel
                {
                    ID = x.ID,
                    Name = x.Name,
                    Price = x.Price,
                    Description = x.Description,
                    Image = x.Image,
                    DaysString = x.Days,
                    Products = x.Products.Select(y =>  new ProductsModel {  ProductName = y.Name})
                }).ToArray();
                foreach (var m in model)
                {
                    m.DaysOfWeek = ConvertStringToDayOfWeekArray(m.DaysString);
                    
                }

                ViewBag.Message = "Select Your Membership";
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult MembershipType(int? ID)
        {
            using (EricaStoreEntities entities = new EricaStoreEntities())
            {
                string email = User.Identity.Name;
                var user = entities.AspNetUsers.FirstOrDefault(x => x.Email == email);
                var currentMembership = user.MembershipTypeUsers.FirstOrDefault(x => x.EndDate == null);
                if(currentMembership != null)
                {
                    currentMembership.EndDate = DateTime.UtcNow;
                    entities.SaveChanges();
                }

                var newMembership = new MembershipTypeUser();
                newMembership.StartDate = DateTime.UtcNow;
                newMembership.UserID = user.Id;
                newMembership.MembershipID = ID.Value;
                user.MembershipTypeUsers.Add(newMembership);
                
                entities.SaveChanges();
                
            }
            return RedirectToAction("Index");
        }

        private DayOfWeek[] ConvertStringToDayOfWeekArray(string days)
        { 
            List<DayOfWeek> d = new List<DayOfWeek>();
            if (days.Contains("M"))
                d.Add(DayOfWeek.Monday);
            if (days.Contains("Tu"))
                d.Add(DayOfWeek.Tuesday);
            if (days.Contains("W"))
                d.Add(DayOfWeek.Wednesday);
            if (days.Contains("Th"))
                d.Add(DayOfWeek.Thursday);
            if (days.Contains("F"))
                d.Add(DayOfWeek.Friday);
            if (days.Contains("Sat"))
                d.Add(DayOfWeek.Saturday);
            if (days.Contains("Sun"))
                d.Add(DayOfWeek.Sunday);
            return d.ToArray();
        }



       
    }
}




