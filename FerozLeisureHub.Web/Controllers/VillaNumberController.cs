using FerozLeisureHub.Application;
using FerozLeisureHub.Domain.Entities;
using FerozLeisureHub.Web.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace FerozLeisureHub.Web.Controllers
{

    public class VillaNumberController(IUnitOfWork unitOfWork) : Controller
    {
       //private readonly IUnitOfWork unitOfWork = unitOfWork;

        public IActionResult Index()
        {
            var villaNumbers = unitOfWork.VillaNumber.GetAll(includeProperties:"Villa");
            return View(villaNumbers);
        }

        public IActionResult Create()
        {
            VillaNumberVM villaNumberVM = new()
            {
                VillaList = unitOfWork.Villa.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                })
            };

            return View(villaNumberVM);
        }
        [HttpPost]
        public IActionResult Create(VillaNumberVM obj)
        {
            bool isVillaNumberExists =obj.VillaNumber !=null &&  unitOfWork.VillaNumber.Any(v => v.Villa_Number == obj.VillaNumber.Villa_Number);

            // ModelState.Remove("Villa");
            if (ModelState.IsValid && !isVillaNumberExists)
            {
                unitOfWork.VillaNumber.Add(obj.VillaNumber);
                unitOfWork.Save();
                TempData["success"] = "Villa Number has been Created successfully.";
                return RedirectToAction("Index");
            }
            if (isVillaNumberExists)
            {
                TempData["error"] = "Villa number already exists";
            }
            obj.VillaList =  unitOfWork.Villa.GetAll().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
            return View(obj);
        }

        public IActionResult Update(int villaNumberId)
        {
            VillaNumberVM villaNumberVM = new()
            {
                VillaList = unitOfWork.Villa.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }),
                VillaNumber = unitOfWork.VillaNumber.Get(u => u.Villa_Number == villaNumberId)

            };
            if (villaNumberVM.VillaNumber == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(villaNumberVM);
        }

        [HttpPost]
        public IActionResult Update(VillaNumberVM villaNumberVM)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.VillaNumber.Update(villaNumberVM.VillaNumber);
                unitOfWork.Save();
                TempData["success"] = "Villa Number has been Updated successfully.";
                return RedirectToAction(nameof(Index)); 
            }
            villaNumberVM.VillaList = unitOfWork.Villa.GetAll().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
            return View(villaNumberVM);
        }

        public IActionResult Delete(int villaNumberId)
        {
            VillaNumberVM villaNumberVM = new()
            {
                VillaList = unitOfWork.Villa.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }),
                VillaNumber = unitOfWork.VillaNumber.Get(u => u.Villa_Number == villaNumberId)

            };
            if (villaNumberVM.VillaNumber == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(villaNumberVM);
        }
        [HttpPost]
        public IActionResult Delete(VillaNumberVM villaNumberVM )
        {
            VillaNumber? villafromdb = unitOfWork.VillaNumber.Get(u => u.Villa_Number == villaNumberVM.VillaNumber.Villa_Number);

            if (villafromdb is not null)
            {
                unitOfWork.VillaNumber.Remove(villafromdb);
                unitOfWork.Save();
                TempData["success"] = "The Villa numbers has been Deleted successfully";
                return RedirectToAction("Index");
            }
            TempData["error"] = "The Villa number could not be deleted.";
            return View();
        }
    }
}