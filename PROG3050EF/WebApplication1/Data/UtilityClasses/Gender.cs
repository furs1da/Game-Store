namespace GameStore.Data.UtilityClasses
{
    public class Gender
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Gender(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
