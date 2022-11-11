using GameStore.Data;

namespace GameStore.Models.DTOs
{
    public class GameDTO
    {
        public int GameId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int? GameStudio { get; set; }

        public int? Quantity { get; set; }
        public bool? IsDigital { get; set; }
        public decimal? Price { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public int? Discount { get; set; }

        public Dictionary<int, string> Categories { get; set; }

        public Dictionary<int, string> Platforms { get; set; }

        public Dictionary<int, string> GameFeatures { get; set; }


        public void Load(Game game)
        {
            GameId = game.GameId;
            Name = game.Name;
            Description = game.Description;
            GameStudio = game.GameStudio;

            Quantity = game.Pquantity;
            Price = game.Price;
            Discount = game.Discount;

            ReleaseDate = game.ReleaseDate;
            IsDigital = game.Digital;



            Categories = new Dictionary<int, string>();
            foreach (GameCategory gc in game.GameCategories)
            {
                Categories.Add(gc.Category.CategoryId, gc.Category.Name);
            }

            Platforms = new Dictionary<int, string>();
            foreach (PlatformGame pg in game.PlatformGames)
            {
                Categories.Add(pg.Platform.PlatformId, pg.Platform.Name);
            }

            GameFeatures = new Dictionary<int, string>();
            foreach (GameFeatureGame gfg in game.GameFeatureGames)
            {
                Categories.Add(gfg.GameFeature.FeatureId, gfg.GameFeature.Feature);
            }

        }

    }
}
