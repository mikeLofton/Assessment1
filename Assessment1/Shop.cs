using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Assessment1
{
    class Shop
    {
        private int _gold;
        private ShopItem[] _inventory;

        public Shop (ShopItem[] items)
        {
            _inventory = items;
        }

        public bool Sell(Player player, int itemIndex)
        {
            if (player.Gold >= _inventory[itemIndex].Cost)
            {
                _gold += _inventory[itemIndex].Cost;
                player.Buy()
            }
            return false;
        }
    }
}
