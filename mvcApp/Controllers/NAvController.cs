using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace mvcApp.Controllers
{
    public class NAvController : Controller
    {
        public IActionResult TopMenu()
        {
            return View();
        }
    }
}