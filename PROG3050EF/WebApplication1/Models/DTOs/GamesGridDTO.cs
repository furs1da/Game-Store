using Newtonsoft.Json;

namespace GameStore.Models.DTOs
{
    public class GamesGridDTO : GridDTO
    {
        [JsonIgnore]
        public const string DefaultFilter = "all";
        [JsonIgnore]
        public const string DefaultNameFilter = "";

        public string Category { get; set; } = DefaultFilter;
        public string Platform { get; set; } = DefaultFilter;
        public string GameFeature { get; set; } = DefaultFilter;
        public string Price { get; set; } = DefaultFilter;
        public string Name { get; set; } = DefaultNameFilter;
    }
}
