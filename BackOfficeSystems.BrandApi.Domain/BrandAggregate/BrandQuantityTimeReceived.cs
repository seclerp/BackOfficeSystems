using System;

namespace BackOfficeSystems.BrandApi.Domain.BrandAggregate
{
    /// <summary>
    /// Brand quantity received data
    /// </summary>
    public class BrandQuantityTimeReceived
    {
        public int BrandId { get; set; }

        /// <summary>
        /// Quantity received in a certain time
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// That when that quantity was received
        /// </summary>
        public DateTime TimeReceived { get; set; }

        public virtual Brand Brand { get; set; }
    }
}
