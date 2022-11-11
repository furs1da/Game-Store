using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStore.Data.SeedData
{
    public class SeedGames : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> entity)
        {
            entity.HasData(
                new Game { GameId = 1, Name = "Dota 2", Price = 0, Pquantity = 10, Description = "The best MOBA game", Discount = 0, ReleaseDate = new DateTime(2013, 7, 9), GameStudio = 4, Digital = true},
                new Game { GameId = 2, Name = "CS:GO", Price = 0, Pquantity = 15, Description = "The best first-person shooter game", Discount = 0, ReleaseDate = new DateTime(2012, 8, 21), GameStudio = 4, Digital = true },
                new Game { GameId = 3, Name = "Fortnite", Price = 0, Pquantity = 100, Description = "The best game", Discount = 0, ReleaseDate = new DateTime(2017, 7, 21), GameStudio = 5, Digital = true },
                new Game { GameId = 4, Name = "Apex Legends", Price = 0, Pquantity = 50, Description = "The best battle royal-hero game", Discount = 0, ReleaseDate = new DateTime(2021, 3, 1), GameStudio = 2, Digital = true },
                new Game { GameId = 5, Name = "Call of Duty®: Modern Warfare® II", Price = 89.99m, Pquantity = 25, Description = "Play a new COD game from EA Arts!", Discount = 0, ReleaseDate = new DateTime(2022, 10, 28), GameStudio = 2, Digital = true },
                new Game { GameId = 6, Name = "Dying Light 2", Price = 43.99m, Pquantity = 15, Description = "You are a wanderer with the power to change the fate of The City.", Discount = 0, ReleaseDate = new DateTime(2022, 2, 3), GameStudio = 6, Digital = true },
                new Game { GameId = 7, Name = "Stumble Guys", Price = 0, Pquantity = 10, Description = "Better than Fall Guys!", Discount = 0, ReleaseDate = new DateTime(2021, 10, 7), GameStudio = 7, Digital = true },
                new Game { GameId = 8, Name = "BIGFOOT", Price = 26, Pquantity = 24, Description = "BIGFOOT is a survival horror game about hunting Bigfoot.", Discount = 0, ReleaseDate = new DateTime(2017, 01, 31), GameStudio = 8, Digital = true },
                new Game { GameId = 9, Name = "FIFA 22", Price = 89.99m, Pquantity = 125, 
                    Description = "EA SPORTS™ FIFA 22 brings the game even closer to the real thing with fundamental gameplay advances and a new season of innovation across every mode.", 
                    Discount = 0, ReleaseDate = new DateTime(2021, 10, 1), GameStudio = 2, Digital = true }
            );
        }
    }
}
