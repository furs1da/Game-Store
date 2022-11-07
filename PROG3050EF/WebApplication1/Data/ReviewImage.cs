using System;
using System.Collections.Generic;

namespace GameStore.Data
{
    public partial class ReviewImage
    {
        public int ReviewImgId { get; set; }
        public string? Path { get; set; }
        public string? Extention { get; set; }
        public int ReviewId { get; set; }

        public virtual Review Review { get; set; } = null!;
    }
}
