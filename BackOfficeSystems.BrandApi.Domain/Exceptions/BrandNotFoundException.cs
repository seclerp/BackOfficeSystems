using System;

namespace BackOfficeSystems.BrandApi.Domain.Exceptions
{
    public class BrandNotFoundException : Exception
    {
        public BrandNotFoundException(int brandId)
        {
            BrandId = brandId;
        }

        public int BrandId { get; }
    }
}