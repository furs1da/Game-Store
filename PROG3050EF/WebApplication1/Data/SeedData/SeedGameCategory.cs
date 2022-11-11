using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStore.Data.SeedData
{
    public class SeedGameCategory : IEntityTypeConfiguration<GameCategory>
    {
        public void Configure(EntityTypeBuilder<GameCategory> entity)
        {
            entity.HasData(
                new GameCategory { Gameid = 1, Categoryid = 7 },

                new GameCategory { Gameid = 2, Categoryid = 1 },



                new GameCategory { Gameid = 3, Categoryid = 1 },
                new GameCategory { Gameid = 3, Categoryid = 4 },


                new GameCategory { Gameid = 4, Categoryid = 1 },
                new GameCategory { Gameid = 4, Categoryid = 4 },

                new GameCategory { Gameid = 5, Categoryid = 1 },
                new GameCategory { Gameid = 5, Categoryid = 4 },

                new GameCategory { Gameid = 6, Categoryid = 5 },
                new GameCategory { Gameid = 6, Categoryid = 3 },

                new GameCategory { Gameid = 7, Categoryid = 8 },
                new GameCategory { Gameid = 7, Categoryid = 4 },

                new GameCategory { Gameid = 8, Categoryid = 5 },
                new GameCategory { Gameid = 8, Categoryid = 3 },

                new GameCategory { Gameid = 9, Categoryid = 8 }
            );
        }
    }
}
