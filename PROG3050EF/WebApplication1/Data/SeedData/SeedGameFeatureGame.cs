using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStore.Data.SeedData
{
    public class SeedGameFeatureGame : IEntityTypeConfiguration<GameFeatureGame>
    {
        public void Configure(EntityTypeBuilder<GameFeatureGame> entity)
        {
            entity.HasData(
                new GameFeatureGame { Gameid = 1, GameFeatureid = 1 },
                new GameFeatureGame { Gameid = 1, GameFeatureid = 2 },

                new GameFeatureGame { Gameid = 2, GameFeatureid = 1 },
                new GameFeatureGame { Gameid = 2, GameFeatureid = 2 },


                new GameFeatureGame { Gameid = 3, GameFeatureid = 1 },
                new GameFeatureGame { Gameid = 3, GameFeatureid = 2 },

                new GameFeatureGame { Gameid = 4, GameFeatureid = 1 },
                new GameFeatureGame { Gameid = 4, GameFeatureid = 2 },

                new GameFeatureGame { Gameid = 5, GameFeatureid = 2 },
                new GameFeatureGame { Gameid = 5, GameFeatureid = 6 },
                new GameFeatureGame { Gameid = 5, GameFeatureid = 8 },

                new GameFeatureGame { Gameid = 6, GameFeatureid = 6 },
                new GameFeatureGame { Gameid = 6, GameFeatureid = 8 },

                new GameFeatureGame { Gameid = 7, GameFeatureid = 1 },
                new GameFeatureGame { Gameid = 7, GameFeatureid = 2 },

                new GameFeatureGame { Gameid = 8, GameFeatureid = 6 },
                new GameFeatureGame { Gameid = 8, GameFeatureid = 8 },

                new GameFeatureGame { Gameid = 9, GameFeatureid = 2 },
                new GameFeatureGame { Gameid = 9, GameFeatureid = 7 },
                new GameFeatureGame { Gameid = 9, GameFeatureid = 8 },
                new GameFeatureGame { Gameid = 9, GameFeatureid = 9 }
            );
        }
    }
}
