using System;
using System.Collections.Generic;

namespace GameStore.Data
{
    public partial class GameImage
    {
        public int GameImgId { get; set; }
        public int GameId { get; set; }
        public string? Path { get; set; }
        public string? Extention { get; set; }
        public bool IsCoverImage { get; set; }

        public virtual Game Game { get; set; } = null!;
    }
}
