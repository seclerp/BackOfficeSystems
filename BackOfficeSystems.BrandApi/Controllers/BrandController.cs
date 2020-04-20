using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BackOfficeSystems.BrandApi.Api.Models;
using BackOfficeSystems.BrandApi.Domain.BrandAggregate;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackOfficeSystems.BrandApi.Controllers
{
    [Route("brand")]
    [ApiController]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public class BrandController : ControllerBase
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;

        public BrandController(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BrandResponseModel), StatusCodes.Status200OK)]
        public async Task<BrandResponseModel> Get(int id)
        {
            var brand = await _brandRepository.Get(id);

            return _mapper.Map<BrandResponseModel>(brand);
        }

        [HttpPost("")]
        [ProducesResponseType(typeof(BrandResponseModel), StatusCodes.Status200OK)]
        public async Task<BrandResponseModel> Create(BrandCreateRequestModel createModel)
        {
            var brand = _mapper.Map<Brand>(createModel);
            var addedBrand = await _brandRepository.Add(brand);

            return _mapper.Map<BrandResponseModel>(addedBrand);
        }

        [HttpPut("")]
        [ProducesResponseType(typeof(BrandResponseModel), StatusCodes.Status200OK)]
        public async Task<BrandResponseModel> Update(BrandUpdateRequestModel updateModel)
        {
            var brand = _mapper.Map<Brand>(updateModel);
            var updatedBrand = await _brandRepository.Update(brand);

            return _mapper.Map<BrandResponseModel>(updatedBrand);
        }

        [HttpDelete("{id}")]
        public async Task Update(int id)
        {
            await _brandRepository.Delete(id);
        }

        [HttpGet("sum-of-inventory")]
        [ProducesResponseType(typeof(IEnumerable<BrandResponseModel>), StatusCodes.Status200OK)]
        public async Task<IEnumerable<SumOfInventoryResponseModel>> GetSumOfInventory()
        {
            var allBrands = await _brandRepository.Get();

            return _mapper.Map<IEnumerable<SumOfInventoryResponseModel>>(allBrands);
        }
    }
}