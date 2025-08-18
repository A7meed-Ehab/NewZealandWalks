using NewZealandWalks.Api.Models.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewZealandWalks.Api.Models.DTO
{
    public class WalkDto
    {

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        public DifficultyDto Difficulty { get; set; }
        public RegionDTO Region { get; set; }

    }
}