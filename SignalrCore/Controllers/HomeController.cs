using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using SignalrCore.Infrastructure.SignalR;
using SignalrCore.Models;

namespace SignalrCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            ViewBag.ClientName = $@"網路登入{new Random().Next(1, 99999)}";
            var onLineUserList = ChatHub.OnLineUsers.Select(u => new SelectListItem() { Text = u.Value, Value = u.Key }).ToList();
            onLineUserList.Insert(0, new SelectListItem() { Text = "-所有人-", Value = "" });
            ViewBag.OnLineUsers = onLineUserList;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}