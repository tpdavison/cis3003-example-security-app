using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SecureApp.Models;
using SecureApp.Services;

namespace SecureApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IValuesService _values;

        public HomeController(ILogger<HomeController> logger,
                              IValuesService valuesService)
        {
            _logger = logger;
            _values = valuesService;
        }

        public async Task<IActionResult> Index()
        {
            var values = await _values.Get();
            return View(values);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
