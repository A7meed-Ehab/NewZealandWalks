using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NewZealandWalks.Api.Data;
using NewZealandWalks.Api.Models.Domain;

namespace NewZealandWalks.Api.Repositories
{
    public class SqlRegionRepository : IRegionRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public SqlRegionRepository(ApplicationDbContext applicationDbContext)
        {
            this._dbContext = applicationDbContext;
        }

        public async Task<Region> CreateAsync(Region region)
        {
            if (region == null)
            {
                return null;
            }
             await _dbContext.Regions.AddAsync(region);
             await _dbContext.SaveChangesAsync();
             return region;
        }
        public async Task<List<Region>> GetAllAsync()
        {
          return await _dbContext.Regions.ToListAsync();
        }
        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Regions.FirstOrDefaultAsync(r=>r.Id==id);
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            if(region is null)
            {
                return null;
            };
            var existingRegion=await GetByIdAsync(id);
            existingRegion.Name = region.Name;
            existingRegion.Code=region.Code;
            existingRegion.RegionImageUrl = region.RegionImageUrl;
            await _dbContext.SaveChangesAsync();
            return existingRegion;
        }
        public async Task<Region?> DeleteAsync(Guid id)
        {
            var domainModel=await GetByIdAsync(id);
            if(id==Guid.Empty || domainModel is null)
            {
                return null;
            }
            _dbContext.Regions.Remove(domainModel);
            await _dbContext.SaveChangesAsync();
            return domainModel;
        }
    }
}
