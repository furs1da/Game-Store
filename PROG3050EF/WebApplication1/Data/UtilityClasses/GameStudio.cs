namespace GameStore.Data.UtilityClasses
{
    public class GameStudio
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public GameStudio(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
