using GameStore.Data.UtilityClasses;

namespace GameStore.Data.Static_Data
{
    public static class GenderList
    {
        public static List<Gender> Genders = new List<Gender>() {
                new Gender(1, "Male"),
                new Gender(2, "Female"),
                new Gender(3, "Other")
        };
    }
}
