using System.ComponentModel.DataAnnotations;
using GameStore.Models.ValidationAttributes;
using GameStore.Data.Static_Data;
using GameStore.Data.UtilityClasses;
using GameStore.Data;

namespace GameStore.Models.ViewModels
{
    public class GameDetailsViewModel
    {
        public Game Game { get; set; }

        public int Rating { get; set; }
    }
}
