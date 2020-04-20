using System;

namespace BackOfficeSystems.BrandApi.Domain.BrandAggregate
{
    public class BrandQuantityTimeReceived
    {
        public int BrandId { get; set; }
        public int Quantity { get; set; }
        public DateTime TimeReceived { get; set; }

        public virtual Brand Brand { get; set; }
    }
}
