using System.Collections.Generic;
using System.Threading.Tasks;
using BackOfficeSystems.BrandApi.Domain.BrandAggregate;
using BackOfficeSystems.BrandApi.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BackOfficeSystems.BrandApi.Infrastructure.DataAccess.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly BackOfficeSystemsContext _context;

        public BrandRepository(BackOfficeSystemsContext context)
        {
            _context = context;
        }

        public async Task<Brand> Add(Brand brand)
        {
            await _context.Brands.AddAsync(brand);
            await _context.SaveChangesAsync();

            return brand;
        }

        public async Task<Brand> Update(Brand brand)
        {
            _context.Entry(brand).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return brand;
        }

        public async Task<Brand> Get(int id)
        {
            var brandFromDb = await _context.Brands
                .Include(brand => brand.BrandQuantityTimeReceived)
                .FirstOrDefaultAsync(brand => brand.BrandId == id);

            if (brandFromDb is null)
            {
                throw new BrandNotFoundException(id);
            }

            return brandFromDb;
        }

        public async Task<IEnumerable<Brand>> Get()
        {
            return await _context.Brands
                .Include(brand => brand.BrandQuantityTimeReceived)
                .ToListAsync();
        }

        public async Task Delete(int id)
        {
            var brand = await Get(id);

            foreach (var receivedData in brand.BrandQuantityTimeReceived)
            {
                _context.BrandQuantityTimeReceived.Remove(receivedData);
            }
            _context.Brands.Remove(brand);

            await _context.SaveChangesAsync();
        }
    }
}