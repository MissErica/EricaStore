using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EricaStore.Models;

namespace EricaStore.Controllers
{
    public class ProductsController : Controller

    {
        public static List<ProductsModel> Products = new List<ProductsModel>();



        // GET: Products
        public ActionResult Index()

        {


            Products.Add(new ProductsModel
            {
                Id = 7,
                ProductName = "Ulitmate Juice Fix",
                Category = "Monthly Membership",
                Description = " Get your favorite juices delivered direct to your door daily at your selected time Monday-Friday",
                Image = new string[] {"/Content/Images/strawberrysmoothie.jpeg"},
                Price = 150.00m
            });
            
            Products.Add(new ProductsModel
            {
                Id = 8,
                ProductName = "Triple Booster Package",
                Category = "Monthly Membership",
                Description = "Get your favorite juices delivered direct to your door daily at your selected time Monday, Wednesday, Friday ",
                Image = new string[] {"/Content/Images/grapefruit.jpeg"},
                Price = 115.00m
            });

            Products.Add(new ProductsModel
            {
                Id = 9,
                ProductName = "Weekend Warrior",
                Category = "Monthly Membership",
                Description = "Get your favorite juices delivered direct to your door daily at your selected time Saturday and Sunday",
                Image = new string[] {"/Content/Images/rhubarbpear.jpeg"},
                Price = 55.00m
            });


            Products.Add(new ProductsModel
            {

                Id = 1,
                ProductName = "Rump Shaker",
                Category = "Energizer",
                Description = "amazing yummy great cool good",
                Ingredients = "",
                Image = new string[] { "/Content/Images/rhubarbpear.jpeg" },
                Price = 9.99m
            });

            Products.Add(new ProductsModel
            {
                Id = 2,
                ProductName = "Simply the Best",
                Category = "Energizer",
                Description = "simply the best",
                Ingredients = "",
                Image = new string[] { "/Content/Images/healthy-people-woman-girl.jpg" },
                Price = 9.99m
            });

            Products.Add(new ProductsModel
            {
                Id = 3,
                ProductName = "Welcome to the Jungle",
                Category = "Detox",
                Description = "better than all the rest",
                Ingredients = "",
                Image = new string[] { "/Content/Images/cucumbers.jpeg" },
                Price = 9.99m
            });

            Products.Add(new ProductsModel
            {
                Id = 4,
                ProductName = "Moondance",
                Category = "Detox",
                Description = "beautiful",
                Ingredients = "",
                Image = new string[] { "/Content/Images/carrotjuice.jpeg" },
                Price = 9.99m
            });

            Products.Add(new ProductsModel
            {
                Id = 5,
                ProductName = "Yellow Submarine",
                Category = "Beauty",
                Description = "too legit",
                Ingredients = "",
                Image = new string[] { "/Content/Images/strawberrysmoothie.jpeg" },
                Price = 9.99m
            });

            Products.Add(new ProductsModel
            {
                Id = 6,
                ProductName = "Uptown Girl",
                Category = "Pre-Workout",
                Description = "sassy pre pilates blend",
                Ingredients = "",
                Image = new string[] { "/Content/Images/flowersmoothie.jpeg" },
                Price = 9.99m
            });




            return View(Products);
            }
        
        [HttpGet]
        public ActionResult Show(int? id)
        {
            return View(Products.First(x => x.Id == id));
        }



        [HttpPost]
        public ActionResult Show(ProductsModel model)
        {


            List<ProductsModel> cart = this.Session["Cart"] as List<ProductsModel>;
            if (cart == null)
            {
                cart = new List<ProductsModel>();
            }

            cart.Add(model);

            this.Session.Add("Cart", cart );

            TempData.Add("AddedToCart", true);


            //Response.Cookies.Add(new HttpCookie("ProductId", model.Id.Value.ToString()));
            //Response.Cookies.Add(new HttpCookie("ProductName", model.ProductName));
            //Response.Cookies.Add(new HttpCookie("ProductPrice", model.Price.ToString()));


            return RedirectToAction("Index", "Cart");

        }
    }

   

    
}

 



 