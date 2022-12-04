using System.ComponentModel.DataAnnotations;
using GameStore.Models.ValidationAttributes;
using GameStore.Data.Static_Data;
using GameStore.Data.UtilityClasses;
using GameStore.Data;

namespace GameStore.Models.ViewModels
{
    public class WriteReviewViewModel
    {
        public Game Game { get; set; }

        public int ReviewId { get; set; }
        public int GameId { get; set; }
        public int CustId { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rate { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public string? Title { get; set; }
    }
}
