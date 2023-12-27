using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FerozLeisureHub.Domain.Entities;
using FerozLeisureHub.Insfrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FerozLeisureHub.Web.Controllers
{
    
    public class VillaController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public VillaController(ApplicationDbContext dbContext)
        {
            _dbContext=dbContext;
        }
        public IActionResult Index()
        {
            var villa=_dbContext.Villas.ToList();
            return View(villa);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Villa villa)
        {
            if(villa.Name == villa.Description)
            {
                ModelState.AddModelError("Villa","The description cannot exactly match the Name");

            }
            if (ModelState.IsValid)
            {
                _dbContext.Villas.Add(villa);
                _dbContext.SaveChanges();
                TempData["success"]="Villa Added successfully.";
                return RedirectToAction("Index", "Villa");
            }
            return View();
            

        }

        public IActionResult Update(int villaId){
            Villa? villa = _dbContext.Villas.FirstOrDefault(u => u.Id == villaId);
            
            if(villa==null){
                return RedirectToAction("Error","Home");
            }
            return View(villa);
        }

        [HttpPost]
        public IActionResult Update(Villa villa)
        {
            
            if (ModelState.IsValid)
            {
                _dbContext.Villas.Update(villa);
                _dbContext.SaveChanges();
                TempData["success"]="The villa has been updated successfully.";
                return RedirectToAction("Index", "Villa");
            }
            return View();
            

        }

        public IActionResult Delete (int villaId){
            Villa? villa = _dbContext.Villas.FirstOrDefault(u => u.Id == villaId);
            
            if(villa is null){
                return RedirectToAction("Error","Home");
            }
            return View(villa);
        }

        [HttpPost]
        public IActionResult Delete(Villa villa)
        {
            Villa? villafromdb = _dbContext.Villas.FirstOrDefault(u=>u.Id ==villa.Id);
            
            if (villafromdb is not null)
            {
                _dbContext.Villas.Remove(villafromdb);
                _dbContext.SaveChanges();
                TempData["success"]="Villa deleted successfully";
                return RedirectToAction("Index", "Villa");
            }
            TempData["error"]="Villa could not be deleted.";
            return View();
            

        }






    }
}