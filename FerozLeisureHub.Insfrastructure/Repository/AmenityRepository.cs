using FerozLeisureHub.Application;
using FerozLeisureHub.Application.Common.Interfaces;
using FerozLeisureHub.Domain.Entities;
using FerozLeisureHub.Insfrastructure.Data;

namespace FerozLeisureHub.Insfrastructure;

public class AmenityRepository :Repository<Amenity>, IAmenityRepository
{

    private readonly ApplicationDbContext _dbContext;
    public AmenityRepository(ApplicationDbContext dbContext):base(dbContext)
    {
        _dbContext = dbContext;
    }

    public void Update(Amenity amenity )
    {
        _dbContext.Amenities.Update(amenity);
    }
}
