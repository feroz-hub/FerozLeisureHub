using FerozLeisureHub.Application;
using FerozLeisureHub.Domain.Entities;
using FerozLeisureHub.Insfrastructure.Data;

namespace FerozLeisureHub.Insfrastructure;

public class VillaNumberRepository(ApplicationDbContext dbContext) : Repository<VillaNumber>(dbContext), IVillaNumberRepository
{

    private readonly ApplicationDbContext _dbContext = dbContext;

    public void Update(VillaNumber villaNumber)
    {
        dbContext.VillaNumbers.Update(villaNumber);
    }
}
