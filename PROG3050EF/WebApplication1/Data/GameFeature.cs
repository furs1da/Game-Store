using System;
using System.Collections.Generic;

namespace GameStore.Data
{
    public partial class GameFeature
    {
        public GameFeature()
        {
            Games = new HashSet<Game>();
        }

        public int FeatureId { get; set; }
        public string Feature { get; set; } = null!;

        public virtual ICollection<GameFeatureGame> GameFeatureGames { get; set; }
        public virtual ICollection<Game> Games { get; set; }
    }
}
