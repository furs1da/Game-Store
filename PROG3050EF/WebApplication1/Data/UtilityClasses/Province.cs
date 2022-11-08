namespace GameStore.Data.UtilityClasses
{
    public class Province
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Province(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
