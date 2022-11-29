using Newtonsoft.Json;

namespace GameStore.Models.DTOs
{
    public class MerchandiseGridDTO : GridDTO
    {
        [JsonIgnore]
        public const string DefaultFilter = "all";
        [JsonIgnore]
        public const string DefaultNameFilter = "";

        public string Game { get; set; } = DefaultFilter;

        public string Name { get; set; } = DefaultNameFilter;

        public string Price { get; set; } = DefaultFilter;
    }
}
