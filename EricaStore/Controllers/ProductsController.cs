using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EricaStore.Models;

namespace EricaStore.Controllers
{
    public class ProductsController : Controller

    {
        



        // GET: Products
        public ActionResult Index()
        {
            using (EricaStoreEntities1 entities = new EricaStoreEntities1())
            {
                var Products = entities.Products.Select(x => new ProductsModel
                {
                    Category = "",
                    Description = x.Description,
                    Id = x.ID,
                    Ingredients = "",
                    Price = x.Price,
                    ProductName = x.Name,
                    Image = x.ProductImages.Select(y => y.Path)
                }).ToArray();


                return View(Products);
            }
        }
        
        [HttpGet]
        public ActionResult Show(int? id)
        {

            using (EricaStoreEntities1 entities = new EricaStoreEntities1())
            {
                var product = entities.Products.Find(id);
                if (product != null)
                {
                    ProductsModel model = new ProductsModel();
                    model.Category = "";
                    model.Description = product.Description;
                    model.Id = product.ID;
                    model.Image = product.ProductImages.Select(x => x.Path);
                    model.Ingredients = "";
                    model.Price = product.Price;
                    model.ProductName = product.Name;
                    return View(model);
                }
                
            }

            return HttpNotFound();
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

 



 