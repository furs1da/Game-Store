using GameStore.Data.UtilityClasses;

namespace GameStore.Data.Static_Data
{
    public static class GameStudioList
    {
        public static List<GameStudio> Genders = new List<GameStudio>() {
                new GameStudio(1, "Ubisoft"),
                new GameStudio(2, "EA"),
                new GameStudio(3, "Microsoft"),
                new GameStudio(4, "Vavle"),
                new GameStudio(5, "Epic Games"),
                new GameStudio(6, "Techland"),
                new GameStudio(7, "Kitka Games"),
                new GameStudio(8, "CyberLight Game Studio")
        };
    }
}
