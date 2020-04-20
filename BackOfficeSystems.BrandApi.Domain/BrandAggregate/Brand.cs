using System.Collections.Generic;
using System.Linq;

namespace BackOfficeSystems.BrandApi.Domain.BrandAggregate
{
    public class Brand
    {
        public int BrandId { get; set; }

        public string Name => _name;
        private string _name;

        public IEnumerable<BrandQuantityTimeReceived> BrandQuantityTimeReceived => _brandQuantityTimeReceived;
        private ICollection<BrandQuantityTimeReceived> _brandQuantityTimeReceived;

        public Brand()
        {
            _brandQuantityTimeReceived = new HashSet<BrandQuantityTimeReceived>();
        }

        public Brand(string name) : this()
        {
            _name = name;
        }

        public int GetSumOfInventory() =>
            _brandQuantityTimeReceived.Sum(received => received.Quantity);
    }
}
