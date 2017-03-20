using EricaStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EricaStore.Controllers
{
    public class StudentController : Controller
    {
       private static List<StudentModel> students = new List<StudentModel>();

        // GET: Student
        public ActionResult Index(string id)
        {
            if(students.Count == 0)
            {
                students.Add(new StudentModel { ID = 1, FirstName = "Ralph", LastName = "Comb", FavoriteFood = "Coffee" });
                students.Add(new StudentModel { ID = 2, FirstName = "JinSeong", LastName = "Kim", FavoriteFood = "Pasta" });
                students.Add(new StudentModel { ID = 3, FirstName = "Erica", LastName = "Wasilenko", FavoriteFood = "Hummus" });
                students.Add(new StudentModel { ID = 4, FirstName = "Sam", LastName = "Fessler", FavoriteFood = "Shrimp" });
                students.Add(new StudentModel { ID = 5, FirstName = "Will", LastName = "Mabry", FavoriteFood = "Ice Cream" });
                students.Add(new StudentModel { ID = 6, FirstName = "Joe", LastName = "Johnson", FavoriteFood = "Nachos" });
            }
            return View(students);
        }
        [HttpPost]
        public ActionResult Edit(StudentModel model)
        {
            var student = students.FirstOrDefault(x => x.ID == model.ID);
            student.FirstName = model.FirstName;
            student.LastName = model.LastName;
            student.FavoriteFood = model.FavoriteFood;

            return RedirectToAction("Index", new { edited = true });
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            return View(students.First(x => x.ID == id));
        }


    }
} 