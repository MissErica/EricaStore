using EricaStore.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EricaStore.Controllers
{
    public class CheckoutController : Controller
    {
        // GET: Checkout
        public ActionResult Index()
        {

              CheckoutModel model = new CheckoutModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(CheckoutModel model)
        {
            if (ModelState.IsValid)
            {
                //TODO: persist order to database and redirect to a receipt page
                //Validated
                //TODO: send an email indicating order was places

                string sendGridApiKey = System.Configuration.ConfigurationManager.AppSettings["SendGrid.ApiKey"];

                SendGrid.SendGridClient client = new SendGrid.SendGridClient(sendGridApiKey);
                SendGrid.Helpers.Mail.SendGridMessage message = new SendGrid.Helpers.Mail.SendGridMessage();
                message.SetTemplateId("713abba9-42f1-41bb-be32-86e3f08347a8");

                //TODO: string.Format("Recepit for order {0}, ord.Id);
                message.Subject = "Receipt for order #000000";
                message.From = new SendGrid.Helpers.Mail.EmailAddress("admin@freshstartjuiceco.com", "Fresh Start Juice Co");
                message.AddTo(new SendGrid.Helpers.Mail.EmailAddress(model.ShippingEmail));
                SendGrid.Helpers.Mail.Content contents = new SendGrid.Helpers.Mail.Content("text/html", "Thank you for placing your order with Fresh Start Juice Co");
                
                message.AddContent(contents.Type, contents.Type);

                await client.SendEmailAsync(message);

                using (Models.EricaStoreEntities1 entities = new EricaStoreEntities1())
                {
                    Order ord = null;
                    if (User.Identity.IsAuthenticated)
                    {
                        AspNetUser currentUser = entities.AspNetUsers.Single(x => x.UserName == User.Identity.Name);
                        //ord = currentUser.Order.FirstOrDefault(x => x.Completed == null);
                        if (ord == null)
                        {
                            ord = new Order();
                            ord.ConfirmationNumber = Guid.NewGuid();
                           // currentUser.Order.Add(ord);
                            entities.SaveChanges();
                        }
                    }
                    else
                    {
                        if (Request.Cookies.AllKeys.Contains("ConfirmationNumber"))
                        {
                            Guid orderNumber = Guid.Parse(Request.Cookies["orderNumber"].Value);
                            ord = entities.Orders.FirstOrDefault(x => x.Completed == null && x.ConfirmationNumber == orderNumber);
                        }
                        if (ord == null)
                        {
                            ord = new Order();
                            ord.ConfirmationNumber = Guid.NewGuid();
                            entities.Orders.Add(ord);
                            Response.Cookies.Add(new HttpCookie("ConfirmationNumber", ord.ConfirmationNumber.ToString()));
                            entities.SaveChanges();
                        }
                    }
                    if (ord.OrderProducts.Sum(x => x.Quantity) == 0)
                    {
                        return RedirectToAction("Index", "Cart");
                    }

                    ord.EmailAddress = User.Identity.Name;
                    Address newShippingAddress = new Address();
                    newShippingAddress.Address1 = model.ShippingAddress1;
                    newShippingAddress.Address2 = model.ShippingAddress2;
                    newShippingAddress.City = model.ShippingCity;
                    newShippingAddress.State = model.ShippingState;
                   // newShippingAddress.Zipcode = model.ZipCode;
                    ord.Address1 = newShippingAddress;



                    //entities.sp_CompleteOrder(ord.Id);

                

                }
                return RedirectToAction("Index", "Receipt");
            }
            return View(model);
            
        }
        //[HttpPost]
        //public ActionResult States()
        //{
        //    using (EricaStoreEntities1 entities = new EricaStoreEntities1())
        //    {
        //        return Json(
        //    }
        //}
        }
    }
