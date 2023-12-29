using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FerozLeisureHub.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FerozLeisureHub.Web.ViewModel
{
    public class AmenityVM
    {
        public Amenity?  Amenity { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> VillaList { get; set; }
    }
}