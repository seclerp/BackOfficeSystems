using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackOfficeSystems.BrandApi.Domain.BrandAggregate
{
    public interface IBrandRepository
    {
        Task<Brand> Add(Brand brand);
        Task<Brand> Update(Brand brand);
        Task<Brand> Get(int id);
        Task<IEnumerable<Brand>> Get();
        Task Delete(int id);
    }
}