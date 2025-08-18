using System.ComponentModel.DataAnnotations;

namespace NewZealandWalks.Api.Models.DTO
{
    public class UpdateRegionRequestDTO
    {
        [Required]
        [MinLength(3, ErrorMessage = "The Code has to be 3 characters")]
        [MaxLength(3, ErrorMessage = "The Code has to be 3 characters")]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
          public string? RegionImageUrl { get; set; }
    }
}
