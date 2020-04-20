using System.ComponentModel.DataAnnotations;

namespace BackOfficeSystems.BrandApi.Api.Models
{
    public class BrandCreateRequestModel
    {
        [Required]
        [StringLength(maximumLength: 100, MinimumLength = 1)]
        public string Name { get; set; }
    }
}