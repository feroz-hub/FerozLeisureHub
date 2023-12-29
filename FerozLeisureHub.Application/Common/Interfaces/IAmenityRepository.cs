using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FerozLeisureHub.Domain.Entities;

namespace FerozLeisureHub.Application.Common.Interfaces
{
    public interface IAmenityRepository:IRepository<Amenity>
    {
        void Update(Amenity amenity);
        
    }
}