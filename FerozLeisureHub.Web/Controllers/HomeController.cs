﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FerozLeisureHub.Web.Models;
using FerozLeisureHub.Application;
using FerozLeisureHub.Web.ViewModel;

namespace FerozLeisureHub.Web.Controllers;

public class HomeController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    public HomeController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        HomeVM homeVM=new HomeVM{
            VillaList=_unitOfWork.Villa.GetAll(includeProperties:"VillaAmenity"),
            Nights=1,
            CheckinDate=DateOnly.FromDateTime(DateTime.Now),
        };
        return View(homeVM);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Error()
    {
        return View();
    }
}

