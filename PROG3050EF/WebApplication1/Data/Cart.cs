using System.Collections.Generic;
using System.Linq;
using GameStore.Models.DTOs;
using GameStore.Models.Repositories;
using Microsoft.AspNetCore.Http;
using GameStore.Models.ExtensionModels;
using GameStore.Models.Query;

namespace GameStore.Data
{
    public class Cart : ICart
    {
        private const string CartKey = "mycart";
        private const string CountKey = "mycount";

        private List<CartItem> items { get; set; }
        private List<CartItemDTO> storedItems { get; set; }

        private ISession session { get; set; }
        private IRequestCookieCollection requestCookies { get; set; }
        private IResponseCookies responseCookies { get; set; }

        public Cart(IHttpContextAccessor ctx)
        {
            session = ctx.HttpContext.Session;
            requestCookies = ctx.HttpContext.Request.Cookies;
            responseCookies = ctx.HttpContext.Response.Cookies;
            items = new List<CartItem>();
        }

        public void Load(IRepository<Game> data, IRepository<Merchandise> dataMerch)
        {
            items = session.GetObject<List<CartItem>>(CartKey);
            if (items == null)
            {
                items = new List<CartItem>();
                storedItems = requestCookies.GetObject<List<CartItemDTO>>(CartKey);
            }
            if (storedItems?.Count > items?.Count)
            {
                foreach (CartItemDTO storedItem in storedItems)
                {
                    if (storedItem.GameId != null)
                    {
                        var game = data.Get(new QueryOptions<Game>
                        {
                            Includes = "GameCategories.Category, GameFeatureGames.GameFeature, PlatformGames.Platform",
                            Where = b => b.GameId == storedItem.GameId
                        });

                        if (game != null)
                        {
                            var dto = new GameDTO();
                            dto.Load(game);

                            CartItem item = new CartItem
                            {
                                Game = dto,
                                Quantity = storedItem.Quantity
                            };
                            items.Add(item);
                        }
                    }
                    else
                    {
                        var merch = dataMerch.Get(new QueryOptions<Merchandise>
                        {
                            Where = b => b.MerchId == storedItem.MerchandiseId
                        });

                        if (merch != null)
                        {
                            var dto = new MerchandiseDTO();
                            dto.Load(merch);

                            CartItem item = new CartItem
                            {
                                Merchandise = dto,
                                Quantity = storedItem.Quantity
                            };
                            items.Add(item);
                        }
                    }
                    
                }
                Save();
            }
        }

        public double SubTotal => items.Sum(c => c.SubTotal());

        public int? Count => session.GetInt32(CountKey) ?? requestCookies.GetInt32(CountKey);

        public IEnumerable<CartItem> List => items;

        // type 0 - for game, type 1 - for merchandise
        public CartItem GetById(int id, int type)
        {
            if(type == 0)
            {
                return items.FirstOrDefault(ci => ci.Game != null && ci.Game.GameId == id);
            }
            else
            {
                return items.FirstOrDefault(ci => ci.Merchandise != null && ci.Merchandise.MerchId == id);
            }
        }

        public void AddGame(CartItem item)
        {
            var exists = GetById(item.Game.GameId, 0);
            if (exists == null)
                items.Add(item);
            else
                exists.Quantity += item.Quantity;
        }

        public void AddMerchandise(CartItem item)
        {
            var exists = GetById(item.Merchandise.MerchId, 1);
            if (exists == null)
                items.Add(item);
            else
                exists.Quantity += item.Quantity;
        }


        public void EditGame(CartItem item)
        {
            var exists = GetById(item.Game.GameId, 0);
            if (exists != null)
            {
                exists.Quantity = item.Quantity;
            }
        }

        public void EditMerchandise(CartItem item)
        {
            var exists = GetById(item.Merchandise.MerchId, 1);
            if (exists != null)
            {
                exists.Quantity = item.Quantity;
            }
        }

        public void Remove(CartItem item) => items.Remove(item);
        public void Clear() => items.Clear();

        public void Save()
        {
            if (items.Count == 0)
            {
                session.Remove(CartKey);
                session.Remove(CountKey);
                responseCookies.Delete(CartKey);
                responseCookies.Delete(CountKey);
            }
            else
            {
                session.SetObject<List<CartItem>>(CartKey, items);
                session.SetInt32(CountKey, items.Count);

                responseCookies.SetObject<List<CartItemDTO>>(CartKey, items.ToDTO());
                responseCookies.SetInt32(CountKey, items.Count);
            }
        }
    }
}
