namespace GameStore.Data
{
    public class GameFeatureGame
    {
        public int GameFeatureid { get; set; }
        public int Gameid { get; set; }

        public GameFeature GameFeature { get; set; }
        public Game Game { get; set; }
    }
}
