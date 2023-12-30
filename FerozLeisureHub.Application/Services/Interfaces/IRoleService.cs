using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FerozLeisureHub.Application.Services.Interfaces
{
    public interface IRoleService
    {
         IEnumerable<SelectListItem> GetRolesAsSelectListItems();
    }
}