using FerozLeisureHub.Application;
using FerozLeisureHub.Domain.Entities;
using FerozLeisureHub.Web.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace FerozLeisureHub.Web.Controllers
{

    public class AmenityController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public AmenityController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var amenities = _unitOfWork.Amenity.GetAll(includeProperties: "Villa");
            return View(amenities);
        }

        public IActionResult Create()
        {
            AmenityVM amenityVM = new()
            {
                VillaList = _unitOfWork.Villa.GetAll().Select(x => new SelectListItem
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
                _unitOfWork.Amenity.Add(obj.Amenity);
                _unitOfWork.Save();
                TempData["success"] = "Amenity has been Created successfully.";
                return RedirectToAction("Index");
            }

            obj.VillaList = _unitOfWork.Villa.GetAll().Select(x => new SelectListItem
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
                VillaList = _unitOfWork.Villa.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }),
                Amenity = _unitOfWork.Amenity.Get(u => u.Id == amenityId)

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
                _unitOfWork.Amenity.Update(amenityVm.Amenity);
                _unitOfWork.Save();
                TempData["success"] = "Amenity has been Updated successfully.";
                return RedirectToAction(nameof(Index));
            }
            amenityVm.VillaList = _unitOfWork.Villa.GetAll().Select(x => new SelectListItem
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
                VillaList = _unitOfWork.Villa.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }),
                Amenity = _unitOfWork.Amenity.Get(u => u.Id == amenityId)

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
            Amenity? amenity = _unitOfWork.Amenity.Get(u => u.Id == amenityVM.Amenity.Id);

            if (amenity is not null)
            {
                _unitOfWork.Amenity.Remove(amenity);
                _unitOfWork.Save();
                TempData["success"] = "The Amenitys has been Deleted successfully";
                return RedirectToAction("Index");
            }
            TempData["error"] = "The Amenity could not be deleted.";
            return View();
        }
    }
}