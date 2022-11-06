using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStore.Data.SeedData
{
    internal class SeedCategory : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> entity)
        {
            entity.HasData(
                new Category { CategoryId = 1, Name= "Shooter" },
                new Category { CategoryId = 2, Name = "Fighting" },
                new Category { CategoryId = 3, Name = "Stealth" },
                new Category { CategoryId = 4, Name = "RPG" },
                new Category { CategoryId = 5, Name = "Survival" },
                new Category { CategoryId = 6, Name = "Platformer" },
                new Category { CategoryId = 7, Name = "MOBA" },
                new Category { CategoryId = 8, Name = "Simulator" },
                new Category { CategoryId = 9, Name = "Puzzler" }
            );
        }
    }
}
