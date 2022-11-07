using System;
using System.Collections.Generic;

namespace GameStore.Data
{
    public partial class MerchandiseImage
    {
        public int MerchImgId { get; set; }
        public int MerchandiseId { get; set; }
        public string? Path { get; set; }
        public string? Extention { get; set; }

        public virtual Merchandise Merchandise { get; set; } = null!;
    }
}
