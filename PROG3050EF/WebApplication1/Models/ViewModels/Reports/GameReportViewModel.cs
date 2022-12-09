using System.ComponentModel.DataAnnotations;
using GameStore.Models.ValidationAttributes;
using GameStore.Data.Static_Data;
using GameStore.Data.UtilityClasses;

namespace GameStore.Models.ViewModels.Reports
{
    public class GameReportViewModel
    {
        public string Name { get; set; }
        public double Price { get; set; }

        public GameReportViewModel(string name, double price)
        {
            Name = name;
            Price = price;
        }
    }
}
