using GameStore.Data.UtilityClasses;

namespace GameStore.Data.Static_Data
{
    public static class ProvinceList
    {
        public static List<Province> Provinces = new List<Province>() {
                new Province(1, "Alberta"),
                new Province(2, "British Columbia"),
                new Province(3, "Manitoba"),
                new Province(4, "New Brunswick"),
                new Province(5, "Newfoundland and Labrador"),
                new Province(6, "Northwest Territories"),
                new Province(7, "Nova Scotia"),
                new Province(8, "Nunavut"),
                new Province(9, "Ontario"),
                new Province(10, "Prince Edward Island"),
                new Province(11, "Quebec"),
                new Province(12, "Saskatchewan"),
                new Province(13, "Yukon")
        };
    }
}
