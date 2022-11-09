namespace GameStore.Data.UtilityClasses
{
    public class NumericValue
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public NumericValue(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
