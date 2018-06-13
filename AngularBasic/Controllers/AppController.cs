using AngularBasic.Data;
using AngularBasic.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AngularBasic.Controllers
{
    public class AppController : Controller
    {
        private readonly AngularBasicContext _context;
        private readonly IAngularBasicRepository _angularBasicRepository;

        public AppController(AngularBasicContext context, IAngularBasicRepository angularBasicRepository)
        {
            _context = context;
            _angularBasicRepository = angularBasicRepository;
        }

        public IActionResult Index()
        {
            //***********

            //***********
            return View();
        }
        [Authorize]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";
            //throw new Exception("Bad things happen");
            return View();
        }

        public IActionResult Details(String Name, String Mobile) 
        {
            var model = new ContactViewModel();
            model.Name = Name;
            model.Mobile = Mobile;
            return View(model);
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {
            //return View();
            if (ModelState.IsValid)
            {
                return RedirectToAction("Details", new { Name = model.Name, Mobile = model.Mobile });
            }
            else
            {
                return View();
            }
            
        }

        //[Authorize]
        public IActionResult shop()
        {
            //var result = _angularBasicRepository.GetAllProducts();

            //return View(result);
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
        
    }
}
