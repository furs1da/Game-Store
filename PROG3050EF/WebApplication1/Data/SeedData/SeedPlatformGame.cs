using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStore.Data.SeedData
{
    public class SeedPlatformGame : IEntityTypeConfiguration<PlatformGame>
    {

            public void Configure(EntityTypeBuilder<PlatformGame> entity)
            {
                entity.HasData(
                    new PlatformGame { Gameid = 1, Platformid = 1 },
                    new PlatformGame { Gameid = 1, Platformid = 7 },

                    new PlatformGame { Gameid = 2, Platformid = 1 },
                    new PlatformGame { Gameid = 2, Platformid = 7 },


                    new PlatformGame { Gameid = 3, Platformid = 1 },
                    new PlatformGame { Gameid = 3, Platformid = 2 },
                    new PlatformGame { Gameid = 3, Platformid = 3 },
                    new PlatformGame { Gameid = 3, Platformid = 7 },

                    new PlatformGame { Gameid = 4, Platformid = 1 },
                    new PlatformGame { Gameid = 4, Platformid = 2 },
                    new PlatformGame { Gameid = 4, Platformid = 3 },
                    new PlatformGame { Gameid = 4, Platformid = 7 },

                    new PlatformGame { Gameid = 5, Platformid = 1 },
                    new PlatformGame { Gameid = 5, Platformid = 2 },
                    new PlatformGame { Gameid = 5, Platformid = 3 },
                    new PlatformGame { Gameid = 5, Platformid = 7 },

                    new PlatformGame { Gameid = 6, Platformid = 1 },
                    new PlatformGame { Gameid = 6, Platformid = 2 },
                    new PlatformGame { Gameid = 6, Platformid = 3 },
                    new PlatformGame { Gameid = 6, Platformid = 7 },

                    new PlatformGame { Gameid = 7, Platformid = 1 },
                    new PlatformGame { Gameid = 7, Platformid = 6 },

                    new PlatformGame { Gameid = 8, Platformid = 1 },
                    new PlatformGame { Gameid = 8, Platformid = 7 },

                    new PlatformGame { Gameid = 9, Platformid = 1 },
                    new PlatformGame { Gameid = 9, Platformid = 2 },
                    new PlatformGame { Gameid = 9, Platformid = 3 },
                    new PlatformGame { Gameid = 9, Platformid = 7 }
                );
            }
        
    }
}
