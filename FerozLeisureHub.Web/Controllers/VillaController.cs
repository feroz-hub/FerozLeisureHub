using FerozLeisureHub.Application;
using FerozLeisureHub.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FerozLeisureHub.Web.Controllers
{
    [Authorize]
    public class VillaController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment) : Controller
    {
       // private readonly IUnitOfWork _unitOfWork = unitOfWork;
       // private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;

        public IActionResult Index()
        {
            var villa=unitOfWork.Villa.GetAll();
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
                if(villa.Image!=null)
                {
                    string filename=Guid.NewGuid().ToString()+ Path.GetExtension(villa.Image.FileName);
                    string imagepath = Path.Combine(webHostEnvironment.WebRootPath,@"Images/VillaImages");

                    using var fileStream = new FileStream(Path.Combine(imagepath, filename), FileMode.Create);
                    villa.Image.CopyTo(fileStream);
                    villa.ImageUrl = @"/Images/VillaImages/"+filename;

                }
                else{
                    villa.ImageUrl ="https://placehold.co/600x400";
                }
                unitOfWork.Villa.Add(villa);
                unitOfWork.Save();
                TempData["success"]="Villa Added successfully.";
                return RedirectToAction("Index", "Villa");
            }
            return View();
        }

        public IActionResult Update(int villaId){
            Villa? villa = unitOfWork.Villa.Get(u => u.Id == villaId);
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
                 if(villa.Image!=null)
                {
                    string filename=Guid.NewGuid().ToString()+Path.GetExtension(villa.Image.FileName);
                    string imagepath = Path.Combine(webHostEnvironment.WebRootPath,@"Images/VillaImages");

                    if(!string.IsNullOrEmpty(villa.ImageUrl))
                    {
                        var oldImagePath= Path.Combine(webHostEnvironment.WebRootPath,villa.ImageUrl.TrimStart('/'));
                        if(System.IO.File.Exists(oldImagePath)){
                            System.IO.File.Delete(oldImagePath);
                        };

                    }

                    using var fileStream = new FileStream(Path.Combine(imagepath, filename), FileMode.Create);
                    villa.Image.CopyTo(fileStream);
                    villa.ImageUrl = @"/Images/VillaImages/"+filename;

                }
                else{
                    villa.ImageUrl ="https://placehold.co/600x400";
                }
                unitOfWork.Villa.Update(villa);
                unitOfWork.Save();
                TempData["success"]="The villa has been updated successfully.";
                return RedirectToAction("Index", "Villa");
            }
            return View(); 
        }
        public IActionResult Delete (int villaId){
            Villa? villa = unitOfWork.Villa.Get(u => u.Id == villaId);
            
            if(villa is null){
                return RedirectToAction("Error","Home");
            }
            return View(villa);
        }

        [HttpPost]
        public IActionResult Delete(Villa villa)
        {
            Villa? villafromdb = unitOfWork.Villa.Get(u=>u.Id ==villa.Id);
            
            if (villafromdb is not null)
            {
                if(!string.IsNullOrEmpty(villafromdb.ImageUrl))
                    {
                        var oldImagePath= Path.Combine(webHostEnvironment.WebRootPath,villafromdb.ImageUrl.TrimStart('/'));
                        if(System.IO.File.Exists(oldImagePath)){
                            System.IO.File.Delete(oldImagePath);
                        };

                    }

                unitOfWork.Villa.Remove(villafromdb);
                unitOfWork.Save();
                TempData["success"]="Villa deleted successfully";
                return RedirectToAction("Index", "Villa");
            }
            TempData["error"]="Villa could not be deleted.";
            return View();         
        }
    }
}