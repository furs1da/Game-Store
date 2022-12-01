using GameStore.Models.Repositories;
using System.Collections.Generic;

namespace GameStore.Data
{
    public interface ICart
    {
        double SubTotal { get; }
        int? Count { get; }
        IEnumerable<CartItem> List { get; }

        void Load(IRepository<Game> data, IRepository<Merchandise> dataMerch);
        CartItem GetById(int id, int type);

        void AddGame(CartItem item);
        void AddMerchandise(CartItem item);
        void EditGame(CartItem item);
        void EditMerchandise(CartItem item);
        void Remove(CartItem item);
        void Clear();
        void Save();
    }
}
