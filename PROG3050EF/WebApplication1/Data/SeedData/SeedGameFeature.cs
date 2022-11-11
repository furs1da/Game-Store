using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStore.Data.SeedData
{
    public class SeedGameFeature : IEntityTypeConfiguration<GameFeature>
    {
        public void Configure(EntityTypeBuilder<GameFeature> entity)
        {
            entity.HasData(
                new GameFeature { FeatureId = 1, Feature = "Free-to-play" },
                new GameFeature { FeatureId = 2, Feature = "Multiplayer" },
                new GameFeature { FeatureId = 3, Feature = "Casual" },
                new GameFeature { FeatureId = 4, Feature = "Party-based" },
                new GameFeature { FeatureId = 5, Feature = "Anime" },
                new GameFeature { FeatureId = 6, Feature = "Co-Operative" },
                new GameFeature { FeatureId = 7, Feature = "LAN" },
                new GameFeature { FeatureId = 8, Feature = "Singleplayer" },
                new GameFeature { FeatureId = 9, Feature = "Sports" }
            );
        }
    }
}
