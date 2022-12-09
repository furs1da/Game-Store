using System.ComponentModel.DataAnnotations;
using GameStore.Models.ValidationAttributes;
using GameStore.Data.Static_Data;
using GameStore.Data.UtilityClasses;

namespace GameStore.Models.ViewModels.Reports
{
    public class CustomerReportViewModel
    {
        public string NickName { get; set; }

        public string Email { get; set; }
        public string Name { get; set; }

        public CustomerReportViewModel(string customerNickname, string email, string customerName)
        {
            Name = customerName;
            NickName = customerNickname;
            Email = email;
        }
    }
}
