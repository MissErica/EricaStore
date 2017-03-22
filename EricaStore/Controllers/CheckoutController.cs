using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EricaStore.Models;

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
        public ActionResult Index(CheckoutModel model)
        {
            if (ModelState.IsValid)
            {
              using(Models.EricaStoreEntities1 entities = new EricaStoreEntities1())
                {
                    string uniqueName = Guid.NewGuid().ToString();
                    Order newOrder = new Order();
                    //newOrder.PurchaserEmail = uniqueName + "@gmail.com";
                    //newOrder.Address = uniqueName;
                    //newOrder.DeliveryAddressID = uniqueName;
                    entities.Orders.Add(newOrder);
                    entities.SaveChanges();

                    int id = newOrder.ID;
                    //entities.sp_CompleteOrder(id);  this is a stored procedure you create
                }
            }
            return View(model);
            {
        }
        }
    }
}