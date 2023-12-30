using FerozLeisureHub.Application.Common.Interfaces;
using FerozLeisureHub.Domain.Entities;
using FerozLeisureHub.Insfrastructure.Data;

namespace FerozLeisureHub.Insfrastructure.Repository
{
    public class VillaRepository(ApplicationDbContext dbContext) : Repository<Villa>(dbContext), IVillaRepository
    {
        //private readonly ApplicationDbContext _dbContext = dbContext;

        public void Update(Villa entity)
        {
            dbContext.Villas.Update(entity);
        }
    }
}