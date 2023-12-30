using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FerozLeisureHub.Domain.Entities;

namespace FerozLeisureHub.Web.ViewModel
{
    public class HomeVM
    {
        public  IEnumerable<Villa>? VillaList{get;set;}
        public DateOnly? CheckInDate{get;set;}
        public DateOnly? CheckOutDate{get;set;}

        public int Nights{get;set;}
        
    }
}