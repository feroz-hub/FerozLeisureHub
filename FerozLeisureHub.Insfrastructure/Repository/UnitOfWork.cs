using FerozLeisureHub.Application;
using FerozLeisureHub.Application.Common.Interfaces;
using FerozLeisureHub.Insfrastructure.Data;
using FerozLeisureHub.Insfrastructure.Repository;

namespace FerozLeisureHub.Insfrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;
    public IVillaRepository Villa {get;private set;}

    public IVillaNumberRepository  VillaNumber {get;private set;}

    public IAmenityRepository Amenity {get;private set;}

    public UnitOfWork(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        Villa=new VillaRepository(_dbContext);
        VillaNumber=new VillaNumberRepository(_dbContext); //
        Amenity=new AmenityRepository(_dbContext);
    }
    public void Save()
    {
        _dbContext.SaveChanges();
    }
}
