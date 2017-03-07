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
        private List<StudentModel> students = new List<StudentModel>();
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
    }
} 