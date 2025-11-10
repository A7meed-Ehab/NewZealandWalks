using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NewZealandWalks.Api.Data
{
    public class NZWalksAuthDbContext: IdentityDbContext
    {
        public NZWalksAuthDbContext(DbContextOptions<NZWalksAuthDbContext> options):base(options)
        {
           
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var readerRoleId = "e701d9ba-343b-4225-ace4-605be2be40c9";
            var writerRoleId = "c9aa529d-a2f0-4ca4-baac-d2b3e2282b3b";

            var roles = new List<IdentityRole>
            {
                new IdentityRole{
                    Id = writerRoleId,
                    ConcurrencyStamp=writerRoleId,
                    Name ="Writer",
                    NormalizedName="Writer".ToUpper(),
                },new IdentityRole{
                    Id =  readerRoleId,
                    ConcurrencyStamp=readerRoleId,
                    Name ="Reader",
                    NormalizedName="reader".ToUpper(),
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
