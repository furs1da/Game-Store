using GameStore.Data;

namespace GameStore.Models.ViewModels
{
    public class PreferencesViewModel
    {
        public int? CustomerId { get; set; }
        public int? CategoryId { get; set; }
        public int? PlatformId { get; set; }

        public List<Category> Categories { get; set; }
        public List<Platform> Platforms { get; set; }
    }
}
