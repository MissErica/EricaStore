using EricaStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EricaStore.Controllers
{
    public class CartController : Controller
    {

        // GET: Cart
        public ActionResult Index()
        {
            CartModel model = new CartModel();
            using (EricaStoreEntities entities = new EricaStoreEntities())
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
                        Guid ConfirmationNumber = Guid.Parse(Request.Cookies["ConfirmationNumber"].Value);
                        ord = entities.Orders.FirstOrDefault(x => x.Completed == null && x.ConfirmationNumber == ConfirmationNumber);
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

                model.Items = ord.OrderProducts.Select(x => new CartItemModel
                { 
                    Product = new ProductsModel
                    {
                        Description = x.Product.Description,
                        ID = x.Product.ID, 
                        ProductName = x.Product.Name,
                        Price = x.Product.Price,
                        Image = x.Product.ProductImages.Select(y => y.Path)

                    },
                    Quantity = x.Quantity
                }).ToArray();
                model.Subtotal = ord.OrderProducts.Sum(x => x.Product.Price * x.Quantity);
                ViewBag.PageGenerationTime = DateTime.UtcNow;
                return View(model);

            }


        }
    }
}

           

        

       // [Authorize]
      //  [HttpPost]
        //public ActionResult Checkout()
        //{
        //    return ViewBag.Message("You must be logged in!");
        //   RedirectToAction("");
        //}
    
