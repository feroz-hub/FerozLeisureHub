using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FerozLeisureHub.Application.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FerozLeisureHub.Application.Services.Implentations
{
    public class RoleService(RoleManager<IdentityRole> roleManager) : IRoleService
    {

        public IEnumerable<SelectListItem> GetRolesAsSelectListItems()
        {
            return roleManager.Roles.Select(role => new SelectListItem
            {
                Text = role.Name,
                Value = role.Name
            });
        }
    }
}