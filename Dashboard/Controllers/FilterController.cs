﻿using Dashboard.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

namespace Dashboard.Controllers
{
    public class FilterController : Controller
    {
        public FilterRepositories _filterRepositories;
        public FilterController(FilterRepositories filterRepositories)
        {
            _filterRepositories = filterRepositories;
        }
        public IActionResult Index(string type, string modulename,string control,string input)
        {
            /*var result = _filterRepositories.FilterImp(type,modulename, control, input);
            return View(result);*/
            return null;
        }
    }
}
