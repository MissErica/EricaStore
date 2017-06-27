using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EricaStore.Models;
using System;

namespace EricaStore.Controllers
{
    public class ProductsController : Controller

    {
        



        // GET: Products
        public ActionResult Index()
        {
            using (EricaStoreEntities entities = new EricaStoreEntities())
            {
                var Products = entities.Products.Select(x => new ProductsModel
                {
                    Category = x.Category,
                    Description = x.Description,
                    ID = x.ID,
                    Ingredients = x.Ingredients,
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

            using (EricaStoreEntities entities = new EricaStoreEntities())
            {
                var product = entities.Products.Find(id);
                if (product != null)
                {
                    ProductsModel model = new ProductsModel();
                    model.Category = product.Category;
                    model.Description = product.Description;
                    model.ID = product.ID;
                    model.Image = product.ProductImages.Select(x => x.Path);
                    model.Ingredients = product.Ingredients;
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

                    }
                }

                ord.OrderProducts.Add(new OrderProduct { ProductID = model.ID ?? 0, Quantity = 1 });
                entities.SaveChanges();
                TempData.Add("AddedToCart", true);

            }
                return RedirectToAction("Index", "Cart");

            }
        }
    }

   

    



 



 