using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewZealandWalks.Api.Data;
using NewZealandWalks.Api.Models.Domain;
using NewZealandWalks.Api.Models.DTO;

namespace NewZealandWalks.Api.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly ApplicationDbContext _context;
        public SQLWalkRepository(ApplicationDbContext context)
        {
            this._context = context;
        }
        public async Task<Walk> CreateAsync(Walk obj)
        {
            if (obj == null)
            {
                return null;
            }
            await _context.Walks.AddAsync(obj);
            await _context.SaveChangesAsync();
            return obj;
        }
        public async Task<Walk?> DeleteAsync(Guid? Id)
        {
            var domainModel = await _context.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == Id);
            if (domainModel == null)
            {
                return null;
            }
            _context.Walks.Remove(domainModel);
            await _context.SaveChangesAsync();
            return domainModel;
        }


        public async Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null,
             string? sortBy = null, bool isAscending = true, int pageNumber=1, int pageSize = 5 )
        {
            //Filtering
            var walks = _context.Walks.Include("Region").Include("Difficulty").AsQueryable();
            if (string.IsNullOrEmpty(filterOn) == false && string.IsNullOrEmpty(filterQuery) == false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
            }
            //Sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);

                }
                else if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
                }
            }
            //Pagination
            int skipResults = (pageNumber - 1) * pageSize;
            return await walks.Skip(skipResults).Take(pageSize).ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync(Guid? Id)
        {
            if (Id == Guid.Empty)
            {
                return null;
            }
            var obj = await _context.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(e => e.Id == Id) ?? null;
            return obj;
        }

        public async Task<Walk?> UpdateAsync(Guid? Id, updateWalkDto? dto)
        {
            if (Id is null || Id == Guid.Empty || dto is null)
            {
                return null;
            }
            var domainModel = await _context.Walks.FirstOrDefaultAsync(w => w.Id == Id) ?? null;
            if (domainModel == null)
            {
                return null;
            }
            domainModel.Description = dto.Description;
            domainModel.Name = dto.Name;
            domainModel.LengthInKm = dto.LengthInKm;
            domainModel.DifficultyId = dto.DifficultyId;
            domainModel.WalkImageUrl = dto.WalkImageUrl;
            domainModel.RegionId = dto.RegionId;
            await _context.SaveChangesAsync();
            return domainModel;
        }
    }
}
