using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Assessment1
{
    class Player : Entity
    {
        private Item[] _equipment;
        private Item _currentEquipment;
        private int _currentEquipmentIndex;
        private ShopItem[] _consumableItems;
        private string _job;
    }
}
