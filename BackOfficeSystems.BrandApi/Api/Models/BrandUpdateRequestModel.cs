using System.ComponentModel.DataAnnotations;

namespace BackOfficeSystems.BrandApi.Api.Models
{
    public class BrandUpdateRequestModel
    {
        [Required]
        public int Id { get; set; }

        [StringLength(maximumLength: 100, MinimumLength = 1)]
        [Required]
        public string Name { get; set; }
    }
}