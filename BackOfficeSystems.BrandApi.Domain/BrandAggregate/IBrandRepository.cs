using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackOfficeSystems.BrandApi.Domain.BrandAggregate
{
    /// <summary>
    /// Repository contract for <see cref="Brand"/> aggregate
    /// </summary>
    public interface IBrandRepository
    {
        /// <summary>
        /// Adds new brand to data storage
        /// </summary>
        /// <param name="brand">Brand data</param>
        /// <returns>Added brand data</returns>
        Task<Brand> Add(Brand brand);

        /// <summary>
        /// Updates existing brand with new brand data
        /// </summary>
        /// <param name="brand">Brand data</param>
        /// <returns>Added brand data</returns>
        Task<Brand> Update(Brand brand);

        /// <summary>
        /// Retrieves brand data by ID
        /// </summary>
        /// <param name="id">ID of a brand</param>
        /// <returns>Added brand data</returns>
        Task<Brand> Get(int id);

        /// <summary>
        /// Retrieves all brand data
        /// </summary>
        /// <returns>Added brand data</returns>
        Task<IEnumerable<Brand>> Get();

        /// <summary>
        /// Deletes brand data by ID
        /// </summary>
        /// <param name="id">ID of a brand</param>
        Task Delete(int id);
    }
}