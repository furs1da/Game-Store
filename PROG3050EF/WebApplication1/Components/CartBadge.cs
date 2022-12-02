using Microsoft.AspNetCore.Mvc;
using GameStore.Data;

namespace GameStore.Components
{
    public class CartBadge : ViewComponent
    {
        private ICart cart { get; set; }
        public CartBadge(ICart c) => cart = c;

        public IViewComponentResult Invoke()
        {
            return View(cart.Count);
        }
    }
}
