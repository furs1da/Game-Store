using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GameStore.Data;


namespace GameStore.Models.ViewModels
{
    public class GameViewModel : IValidatableObject
    {
        public Game Game { get; set; }
        public IEnumerable<GameFeature> GameFeatures { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Platform> Platforms { get; set; }


        public int[] SelectedGameFeatures { get; set; }

        public int[] SelectedCategories { get; set; }

        public int[] SelectedPlatforms { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext ctx)
        {
            if (SelectedGameFeatures == null)
            {
                yield return new ValidationResult(
                  "Please select at least one game feature.",
                  new[] { nameof(SelectedGameFeatures) });
            }
            if (SelectedCategories == null)
            {
                yield return new ValidationResult(
                  "Please select at least one game feature.",
                  new[] { nameof(SelectedPlatforms) });
            }
            if (SelectedPlatforms == null)
            {
                yield return new ValidationResult(
                  "Please select at least one game feature.",
                  new[] { nameof(SelectedPlatforms) });
            }
        }
    }
}
