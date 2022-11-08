using System;
using System.Collections.Generic;

namespace GameStore.Data
{
    public class PlatformGame
    {
        public int Platformid { get; set; }
        public int Gameid { get; set; }


        public Platform Platform { get; set; }
        public Game Game { get; set; }
    }
}
