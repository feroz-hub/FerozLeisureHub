using FerozLeisureHub.Application.Common.Interfaces;

namespace FerozLeisureHub.Application;

public interface IUnitOfWork
{
    void Save();
    IVillaRepository Villa{get;}
    IVillaNumberRepository  VillaNumber {get;} //

    IAmenityRepository Amenity {get;} //

}
