using FerozLeisureHub.Application;
using FerozLeisureHub.Application.Common.Utility;
using FerozLeisureHub.Domain.Entities;
using FerozLeisureHub.Web.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace FerozLeisureHub.Web.Controllers
{

[Authorize(Roles =SD.Role_Admin)]
    public class AmenityController(IUnitOfWork unitOfWork) : Controller
    {
        //private readonly IUnitOfWork unitOfWork = unitOfWork;

        public IActionResult Index()
        {
            var amenities = unitOfWork.Amenity.GetAll(includeProperties: "Villa");
            return View(amenities);
        }

        public IActionResult Create()
        {
            AmenityVM amenityVM = new()
            {
                VillaList = unitOfWork.Villa.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                })
            };

            return View(amenityVM);
        }
        [HttpPost]
        public IActionResult Create(AmenityVM obj)
        {

            if (ModelState.IsValid)
            {
                unitOfWork.Amenity.Add(obj.Amenity);
                unitOfWork.Save();
                TempData["success"] = "Amenity has been Created successfully.";
                return RedirectToAction("Index");
            }

            obj.VillaList = unitOfWork.Villa.GetAll().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
            return View(obj);
        }

        public IActionResult Update(int amenityId)
        {
            AmenityVM amenityVM = new()
            {
                VillaList = unitOfWork.Villa.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }),
                Amenity = unitOfWork.Amenity.Get(u => u.Id == amenityId)

            };
            if (amenityVM.Amenity == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(amenityVM);
        }

        [HttpPost]
        public IActionResult Update(AmenityVM amenityVm)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Amenity.Update(amenityVm.Amenity);
                unitOfWork.Save();
                TempData["success"] = "Amenity has been Updated successfully.";
                return RedirectToAction(nameof(Index));
            }
            amenityVm.VillaList = unitOfWork.Villa.GetAll().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
            return View(amenityVm);
        }

        public IActionResult Delete(int amenityId)
        {
            AmenityVM amenityVM = new()
            {
                VillaList = unitOfWork.Villa.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }),
                Amenity = unitOfWork.Amenity.Get(u => u.Id == amenityId)

            };
            if (amenityVM.Amenity == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(amenityVM);
        }
        [HttpPost]
        public IActionResult Delete(AmenityVM amenityVM)
        {
            if (amenityVM.Amenity == null)
            {
                return RedirectToAction("Error", "Home");
            }
            Amenity? amenity = unitOfWork.Amenity.Get(u => u.Id == amenityVM.Amenity.Id);

            if (amenity is not null)
            {
                unitOfWork.Amenity.Remove(amenity);
                unitOfWork.Save();
                TempData["success"] = "The Amenitys has been Deleted successfully";
                return RedirectToAction("Index");
            }
            TempData["error"] = "The Amenity could not be deleted.";
            return View();
        }
    }
}