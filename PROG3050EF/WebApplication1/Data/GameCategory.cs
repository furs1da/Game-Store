using System;
using System.Collections.Generic;

namespace GameStore.Data
{
    public class GameCategory
    {
        public int Categoryid { get; set; }
        public int Gameid { get; set; }

        public Category Category { get; set; }
        public Game Game { get; set; }
    }
}
