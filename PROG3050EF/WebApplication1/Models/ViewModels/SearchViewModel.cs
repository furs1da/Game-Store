using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GameStore.Data;

namespace GameStore.Models.ViewModels
{
    public class SearchViewModel
    {
        public IEnumerable<Game> Games { get; set; }

        [Required(ErrorMessage = "Please enter a search term.")]
        public string SearchTerm { get; set; }
        public string Type { get; set; }
        public string Header { get; set; }
    }
}
