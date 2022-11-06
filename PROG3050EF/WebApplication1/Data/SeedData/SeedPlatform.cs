using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStore.Data.SeedData
{
    internal class SeedPlatform : IEntityTypeConfiguration<Platform>
    {
        public void Configure(EntityTypeBuilder<Platform> entity)
        {
            entity.HasData(
                new Platform { PlatformId = 1, Name = "PC" },
                new Platform { PlatformId = 2, Name = "Xbox" },
                new Platform { PlatformId = 3, Name = "PlayStation" },
                new Platform { PlatformId = 4, Name = "Nintendo" },
                new Platform { PlatformId = 5, Name = "VR" },
                new Platform { PlatformId = 6, Name = "Mobile" },
                new Platform { PlatformId = 7, Name = "Steam OS" }
            );
        }
    }
}
