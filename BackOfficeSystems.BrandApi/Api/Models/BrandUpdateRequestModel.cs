using System.ComponentModel.DataAnnotations;

namespace BackOfficeSystems.BrandApi.Api.Models
{
    public class BrandUpdateRequestModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: 100, MinimumLength = 1)]
        public string Name { get; set; }
    }
}