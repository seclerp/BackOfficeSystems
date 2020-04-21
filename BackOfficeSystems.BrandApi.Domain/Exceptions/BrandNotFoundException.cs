using System;

namespace BackOfficeSystems.BrandApi.Domain.Exceptions
{
    /// <summary>
    /// Exception that occurs when Brand with specified ID is not found
    /// </summary>
    public class BrandNotFoundException : Exception
    {
        public BrandNotFoundException(int brandId)
        {
            BrandId = brandId;
        }

        public int BrandId { get; }
    }
}