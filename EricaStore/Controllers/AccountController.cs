using EricaStore.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EricaStore.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Register()
        {
            return View(new RegisterModel()) ;
        }

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

                    var user = new User()
                    {
                        UserName = model.EmailAddress,
                        Email = model.EmailAddress
                    };

                    IdentityResult result = manager.Create(user, model.Password);
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
                    SendGrid.Helpers.Mail.Content contents = new SendGrid.Helpers.Mail.Content("text/html", string.Format("<a href=\"{0}\"Confirm Account</a>", Request.Url.GetLeftPart(UriPartial.Authority) + "Account/Confirm/" + confirmationToken));

                    message.AddContent(contents.Type, contents.Type);

                    SendGrid.Response response = await client.SendEmailAsync(message);












                    if (result.Succeeded)
                    {
                        FormsAuthentication.SetAuthCookie(model.EmailAddress, true);
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

        public ActionResult ConfirmSent()
        {
            return View();
        }

        public ActionResult Confirm(string Id)
        {
            return View();
        }



       public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }








        public ActionResult Login()
        {

            return View(new LoginModel());
        }


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
                        return RedirectToAction("Index", "Home");
                    }
                    ModelState.AddModelError("EmailAddress", "Could not sign in with this username and/or password");
                }
            }
            return View(model);
        }
    }

}
