using AspNetCore_10_4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore_10_4.Controllers
{
    public class ProfileController : Controller
    {
        public ProfileController()
        {
            profiles.Add(new Profile { Id = profiles.Count + 1, Tags = new[] { "asp", "Csharp" }, Birthday = DateTime.Now, Password = "Pass", CountryId = (int)Countries.England });
        }
        public static List<TestViewModel> models = new List<TestViewModel>();
        public static List<Profile> profiles = new List<Profile>();

        public IActionResult Index()
        {
            return View(profiles);

        }
        [HttpGet]
        public IActionResult Edit(int index)
        {
            return View(profiles[index]);
        }
        [HttpPost]
        public IActionResult Edit(Profile prof)
        {
            if (ModelState.IsValid)
            {
                profiles[prof.Id - 1] = prof;
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Edit), new { index = prof.Id });
        }
        [HttpGet]
        public IActionResult AddPhone()
        {
            ViewBag.Companies = (new List<Company> { new Company {Id=1,Name="Apple"},
            new Company {Id=2,Name="Microsoft"},
            new Company {Id=3,Name="IBM"}}).Select(x=>new SelectListItem { Text=x.Name,Value=x.Id.ToString()}); ;
            return View();
        }
        public IActionResult AddPhone(Phone p)
        {
            return RedirectToAction(nameof(AddPhone));
        }
        [HttpGet]
        public IActionResult CreateModel()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateModel(TestViewModel model)
        {
            return View(model);
        }
    }
}
