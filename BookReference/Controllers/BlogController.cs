using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BookReference.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SingleBlogPost(int? year, int? month, int? day, string slug)
        {
            ViewData["year"] = year;
            ViewData["month"] = month;
            ViewData["day"] = day;
            ViewData["title"] = slug;

            return View();
        }

        public IActionResult BlogCategory(string categoryName)
        {
            ViewData["categoryName"] = categoryName;

            return View();
        }
    }
}
