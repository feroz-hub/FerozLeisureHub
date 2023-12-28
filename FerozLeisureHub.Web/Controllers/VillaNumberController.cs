using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FerozLeisureHub.Domain.Entities;
using FerozLeisureHub.Insfrastructure.Data;
using FerozLeisureHub.Web.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FerozLeisureHub.Web.Controllers
{

    public class VillaNumberController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public VillaNumberController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            var villaNumbers = _dbContext.VillaNumbers.Include(u => u.Villa).ToList();
            return View(villaNumbers);
        }

        public IActionResult Create()
        {
            VillaNumberVM villaNumberVM = new()
            {
                VillaList = _dbContext.Villas.ToList().Select(x => new SelectListItem
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
            bool isVillaNumberExists =obj.VillaNumber !=null &&  _dbContext.VillaNumbers.Any(v => v.Villa_Number == obj.VillaNumber.Villa_Number);

            // ModelState.Remove("Villa");
            if (ModelState.IsValid && !isVillaNumberExists)
            {


                _dbContext.VillaNumbers.Add(obj.VillaNumber);
                _dbContext.SaveChanges();
                TempData["success"] = "Villa Number has been Created successfully.";
                return RedirectToAction("Index");
            }
            if (isVillaNumberExists)
            {
                TempData["error"] = "Villa number already exists";


            }
            obj.VillaList = _dbContext.Villas.ToList().Select(x => new SelectListItem
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
                VillaList = _dbContext.Villas.ToList().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }),
                VillaNumber = _dbContext.VillaNumbers.FirstOrDefault(u => u.Villa_Number == villaNumberId)

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
            //bool isVillaNumberExists= _dbContext.VillaNumbers.Any(v=>v.Villa_Number==  obj.VillaNumber.Villa_Number);

            // ModelState.Remove("Villa");
            if (ModelState.IsValid)
            {


                _dbContext.VillaNumbers.Update(villaNumberVM.VillaNumber);
                _dbContext.SaveChanges();
                TempData["success"] = "Villa Number has been Updated successfully.";
                return RedirectToAction(nameof(Index)); 
            }

            villaNumberVM.VillaList = _dbContext.Villas.ToList().Select(x => new SelectListItem
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
                VillaList = _dbContext.Villas.ToList().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }),
                VillaNumber = _dbContext.VillaNumbers.FirstOrDefault(u => u.Villa_Number == villaNumberId)

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
            VillaNumber? villafromdb = _dbContext.VillaNumbers
            .FirstOrDefault(u => u.Villa_Number == villaNumberVM.VillaNumber.Villa_Number);

            if (villafromdb is not null)
            {
                _dbContext.VillaNumbers.Remove(villafromdb);
                _dbContext.SaveChanges();
                TempData["success"] = "The Villa numbers has been Deleted successfully";
                return RedirectToAction("Index");
            }
            TempData["error"] = "The Villa number could not be deleted.";
            return View();


        }






    }
}