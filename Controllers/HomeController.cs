﻿using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using LightCV.BL.Auth;
using LightCV.Models;

namespace LightCV.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICurrentUser currentUser;

        public HomeController(ILogger<HomeController> logger,
            ICurrentUser currentUser)
        {
            _logger = logger;
            this.currentUser = currentUser;
        }

        public IActionResult Index()
        {
            return View(currentUser.IsLoggedIn());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}