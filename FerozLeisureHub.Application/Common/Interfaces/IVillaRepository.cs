using FerozLeisureHub.Domain.Entities;

namespace FerozLeisureHub.Application.Common.Interfaces
{
    public interface IVillaRepository:IRepository<Villa>
    {
        void Update(Villa entity);
    }

}