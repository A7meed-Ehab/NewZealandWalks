using NewZealandWalks.Api.Models.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewZealandWalks.Api.Models.DTO
{
    public class updateWalkDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }
    }
}
