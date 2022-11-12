namespace GameStore.Models.ViewModels
{
    public class EventRegisterViewModel
    {
        public int EventId { get; set; }
        public string Name { get; set; } = null!;
        public DateTime? Date { get; set; }
        public string? Description { get; set; }
        public string? Duration { get; set; }
        public bool IsRegistered { get; set; }
    }
}
