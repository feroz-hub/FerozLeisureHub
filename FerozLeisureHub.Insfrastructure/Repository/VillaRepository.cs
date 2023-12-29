using FerozLeisureHub.Application.Common.Interfaces;
using FerozLeisureHub.Domain.Entities;
using FerozLeisureHub.Insfrastructure.Data;

namespace FerozLeisureHub.Insfrastructure.Repository
{
    public class VillaRepository :Repository<Villa>, IVillaRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public VillaRepository(ApplicationDbContext dbContext):base(dbContext)
        {
            _dbContext = dbContext;
        }
        public void Update(Villa entity)
        {
            _dbContext.Villas.Update(entity);
        }
    }
}