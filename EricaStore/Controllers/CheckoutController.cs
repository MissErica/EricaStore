using EricaStore.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
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
                //TODO: send an email indicating order was placed

                string sendGridApiKey = System.Configuration.ConfigurationManager.AppSettings["SendGrid.ApiKey"];

                SendGrid.SendGridClient client = new SendGrid.SendGridClient(sendGridApiKey);
                SendGrid.Helpers.Mail.SendGridMessage message = new SendGrid.Helpers.Mail.SendGridMessage();
                message.SetTemplateId("713abba9-42f1-41bb-be32-86e3f08347a8");

                //TODO: string.Format("Receipt for order {0}, ord.Id);
                message.Subject = "Receipt for order #000000";
                message.From = new SendGrid.Helpers.Mail.EmailAddress("admin@freshstartjuiceco.com", "Fresh Start Juice Co");
                message.AddTo(new SendGrid.Helpers.Mail.EmailAddress(model.ShippingEmail));
                SendGrid.Helpers.Mail.Content contents = new SendGrid.Helpers.Mail.Content("text/html", "Thank you for placing your order with Fresh Start Juice Co");
                
                message.AddContent(contents.Type, contents.Type);

                await client.SendEmailAsync(message);

                using (Models.EricaStoreEntities entities = new EricaStoreEntities())
                {
                    Order ord = null;
                    if (User.Identity.IsAuthenticated)
                    {
                        AspNetUser currentUser = entities.AspNetUsers.Single(x => x.UserName == User.Identity.Name);
                        ord = currentUser.Orders.FirstOrDefault(x => x.Completed == null);
                        if (ord == null)
                        {
                            ord = new Order();
                            ord.ConfirmationNumber = Guid.NewGuid();
                           currentUser.Orders.Add(ord);
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

                    ord.PurchaseEmailAddress = User.Identity.Name;
                    Address newShippingAddress = new Address();
                    newShippingAddress.Address1 = model.ShippingAddress1;
                    newShippingAddress.Address2 = model.ShippingAddress2;
                    newShippingAddress.City = model.ShippingCity;
                    newShippingAddress.State = model.ShippingState;
                    //newShippingAddress.Zipcode = model.ZipCode;
                    ord.Address1 = newShippingAddress;



                    //entities.sp_CompleteOrder(ord.Id);

                    string merchantId = ConfigurationManager.AppSettings["Braintree.MerchantID"];
                    string publicKey = ConfigurationManager.AppSettings["Braintree.PublicKey"];
                    string privateKey = ConfigurationManager.AppSettings["Braintree.PrivateKey"];
                    string environment = ConfigurationManager.AppSettings["Braintree.Environment"];


                    Braintree.BraintreeGateway braintree = new Braintree.BraintreeGateway(environment,merchantId,publicKey,privateKey);

                    Braintree.TransactionRequest newTransaction = new Braintree.TransactionRequest();
                    newTransaction.Amount = ord.OrderProducts.Sum(x => x.Quantity * x.Product.Price);

                    Braintree.TransactionCreditCardRequest creditCard = new Braintree.TransactionCreditCardRequest();
                    creditCard.CardholderName = model.CreditCardName;
                    creditCard.CVV = model.CreditCardVerificationValue; ;
                    creditCard.ExpirationMonth = model.CreditCardExpiration.Value.Month.ToString().PadLeft(2,'0'); //always needs to be two characters
                    creditCard.ExpirationYear = model.CreditCardExpiration.Value.Year.ToString();
                    creditCard.Number = model.CreditCardNumber;

                    newTransaction.CreditCard = creditCard;

                    //if user is logged in, associate this transaction with their account

                    if (User.Identity.IsAuthenticated)
                    {
                        Braintree.CustomerSearchRequest search = new Braintree.CustomerSearchRequest();
                        search.Email.Is(User.Identity.Name);
                        var customers = braintree.Customer.Search(search);
                        newTransaction.CustomerId = customers.FirstItem.Id;

                    }

                    Braintree.Result<Braintree.Transaction> result = await braintree.Transaction.SaleAsync(newTransaction);

                    if (!result.IsSuccess())
                    {
                        ModelState.AddModelError("CreditCard", "Could not authorize payment");
                        return View(model);
                    }
                

                }
                return RedirectToAction("Index", "Receipt");
            }
            return View(model);
            
        }

        [HttpPost]
        public ActionResult ValidateAddress(string street1, string street2, string city, string state, string zip)
        {
            string authId = ConfigurationManager.AppSettings["SmartyStreets.AuthID"];
            string authToken = ConfigurationManager.AppSettings["SmartyStreets.AuthToken"];
            SmartyStreets.USStreetApi.ClientBuilder builder = new SmartyStreets.USStreetApi.ClientBuilder(authId, authToken);
            SmartyStreets.USStreetApi.Client client = builder.Build();
            SmartyStreets.USStreetApi.Lookup lookup = new SmartyStreets.USStreetApi.Lookup();
            lookup.City = city;
            lookup.State = state;
            lookup.Street = street1;
            lookup.Street2 = street2;
            lookup.ZipCode = zip;
            client.Send(lookup);
            return Json(lookup.Result);
        }

    }
    }
