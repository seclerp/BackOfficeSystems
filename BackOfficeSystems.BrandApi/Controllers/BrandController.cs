﻿using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BackOfficeSystems.BrandApi.Api.Models;
using BackOfficeSystems.BrandApi.Domain.BrandAggregate;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackOfficeSystems.BrandApi.Controllers
{
    /// <summary>
    /// Controller that handles CRUD operations on brands
    /// </summary>
    [Route("brand")]
    [ApiController]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [InputValidation]
    public class BrandController : ControllerBase
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;

        public BrandController(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves brand data by ID
        /// </summary>
        /// <param name="id">ID of a brand</param>
        /// <returns>Brand data</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BrandResponseModel), StatusCodes.Status200OK)]
        public async Task<BrandResponseModel> Get(int id)
        {
            var brand = await _brandRepository.Get(id);

            return _mapper.Map<BrandResponseModel>(brand);
        }

        /// <summary>
        /// Creates brand with provided brand data
        /// </summary>
        /// <param name="createModel">Model that describes brand data</param>
        /// <returns>Created brand data</returns>
        [HttpPost("")]
        [ProducesResponseType(typeof(BrandResponseModel), StatusCodes.Status200OK)]
        public async Task<BrandResponseModel> Create(BrandCreateRequestModel createModel)
        {
            var brand = _mapper.Map<Brand>(createModel);
            var addedBrand = await _brandRepository.Add(brand);

            return _mapper.Map<BrandResponseModel>(addedBrand);
        }

        /// <summary>
        /// Update brand with provided brand data
        /// </summary>
        /// <param name="updateModel">Model that describes new brand data</param>
        /// <returns>Updated brand data</returns>
        [HttpPut("")]
        [ProducesResponseType(typeof(BrandResponseModel), StatusCodes.Status200OK)]
        public async Task<BrandResponseModel> Update(BrandUpdateRequestModel updateModel)
        {
            var brand = _mapper.Map<Brand>(updateModel);
            var updatedBrand = await _brandRepository.Update(brand);

            return _mapper.Map<BrandResponseModel>(updatedBrand);
        }

        /// <summary>
        /// Delete brand by id
        /// </summary>
        /// <param name="id">ID of a brand</param>
        /// <returns>Updated brand data</returns>
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _brandRepository.Delete(id);
        }

        /// <summary>
        /// Returns Sum of inventory for all brands
        /// </summary>
        /// <returns>Sum of inventory for all brands</returns>
        [HttpGet("sum-of-inventory")]
        [ProducesResponseType(typeof(IEnumerable<BrandResponseModel>), StatusCodes.Status200OK)]
        public async Task<IEnumerable<SumOfInventoryResponseModel>> GetSumOfInventory()
        {
            var allBrands = await _brandRepository.Get();

            return _mapper.Map<IEnumerable<SumOfInventoryResponseModel>>(allBrands);
        }
    }
}