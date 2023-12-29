using FerozLeisureHub.Domain.Entities;

namespace FerozLeisureHub.Application;

public interface IVillaNumberRepository:IRepository<VillaNumber>
{
    void Update(VillaNumber villaNumber);
    
}
