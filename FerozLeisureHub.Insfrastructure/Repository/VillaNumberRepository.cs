using FerozLeisureHub.Application;
using FerozLeisureHub.Domain.Entities;
using FerozLeisureHub.Insfrastructure.Data;

namespace FerozLeisureHub.Insfrastructure;

public class VillaNumberRepository :Repository<VillaNumber>, IVillaNumberRepository
{

    private readonly ApplicationDbContext _dbContext;
    public VillaNumberRepository(ApplicationDbContext dbContext):base(dbContext)
    {
        _dbContext = dbContext;
    }

    public void Update(VillaNumber villaNumber)
    {
        _dbContext.VillaNumbers.Update(villaNumber);
    }
}
