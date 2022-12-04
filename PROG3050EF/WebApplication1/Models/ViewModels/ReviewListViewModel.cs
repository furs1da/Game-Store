using System.ComponentModel.DataAnnotations;
using GameStore.Models.ValidationAttributes;
using GameStore.Data.Static_Data;
using GameStore.Data.UtilityClasses;
using GameStore.Data;

namespace GameStore.Models.ViewModels
{
    public class ReviewListViewModel
    {
        public List<Review> Reviews { get; set; }
        public List<Customer> Customers { get; set; }
        public Game Game   { get; set; }
        public double OverallRating { get; set; }
    }
}
