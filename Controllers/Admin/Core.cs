﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWebApplication.Domain;

namespace MyWebApplication.Controllers.Admin
{
    [Authorize(Roles = "admin")]
    public  partial class AdminController : Controller
    {
        private readonly DataManager _dataManager;

        public AdminController(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}
