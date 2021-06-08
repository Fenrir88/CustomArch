using BlazorWebService.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWebService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestComponentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private static int autoIncremental;

        [HttpGet]
        public TestComponentModel Get()
        {
            
            TestComponentModel returnObj = new TestComponentModel();

            returnObj.Title = "Test Title #" + autoIncremental;
            returnObj.Content = "Next value for the title should increase";

            autoIncremental++;

            return returnObj; 

        }
    }
}
