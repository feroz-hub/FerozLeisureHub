using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FerozLeisureHub.Web.Models;
using FerozLeisureHub.Application;
using FerozLeisureHub.Web.ViewModel;

namespace FerozLeisureHub.Web.Controllers;

public class HomeController(IUnitOfWork unitOfWork) : Controller
{
    //private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public IActionResult Index()
    {
        HomeVM homeVM = new HomeVM
        {
            VillaList = unitOfWork.Villa.GetAll(includeProperties: "VillaAmenity"),
            Nights = 1,
            CheckInDate = DateOnly.FromDateTime(DateTime.Now),
        };
        return View(homeVM);
    }

    [HttpPost]
    public IActionResult Index(HomeVM homeVM)
    {
        homeVM.VillaList = unitOfWork.Villa.GetAll(includeProperties: "VillaAmenity");
        foreach (var villa in homeVM.VillaList)
        {
            if (villa.Id % 2 == 0)
            {
                villa.IsAvaliable = false;
            }
        }
        return View(homeVM);
    }

    public IActionResult GetVillasByDate(int nights, DateOnly checkInDate)
    {
        
        var villaList = unitOfWork.Villa.GetAll(includeProperties: "VillaAmenity").ToList();
        foreach (var villa in villaList)
        {
            if (villa.Id % 2 == 0)
            {
                villa.IsAvaliable = false;

            }
        }
        HomeVM homeVM = new()
        {
            CheckInDate = checkInDate,
            VillaList = villaList,
            Nights = nights
        };
        return PartialView("_VillaList",homeVM);
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

