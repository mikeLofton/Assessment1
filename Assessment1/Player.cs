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
        private int _keys;
        private string _job;

        public override float DefensePower
        {
            get
            {
                if (_currentEquipment.Type == ItemType.DEFENSE)
                    return base.DefensePower + CurrentEquip.StatBoost;

                return base.DefensePower;
            }
        }

        public override float AttackPower
        {
            get
            {
                if (_currentEquipment.Type == ItemType.ATTACK)
                    return base.AttackPower + CurrentEquip.StatBoost;

                return base.AttackPower;
            }
        }

        public override float Health
        {
            get
            {
                if (_currentEquipment.Type == ItemType.HEALTH)
                    return base.Health + CurrentEquip.StatBoost;

                return base.Health;
            }
        }

        public Item CurrentEquip
        {
            get { return _currentEquipment; }
        }

        public string Job
        {
            get
            {
                return _job;
            }

            set
            {
                _job = value;
            }
        }   

        public int Keys
        {
            get { return _keys; }
        }

        public Player(Item[] items) : base()
        {
            _currentEquipment.Name = "Nothing";
            _equipment = items;
            _currentEquipmentIndex = -1;           
        }

        public Player(string name, float health, float attackPower, float defensePower, int gold, Item[] items, string job, int keys) : base(name, health, attackPower, defensePower, gold)
        {
            _equipment = items;
            _currentEquipment.Name = "Nothing";
            _job = job;
            _keys = keys;
        }

        public bool TryEquip(int index)
        {
            if (index >= _equipment.Length || index < 0)
            {
                return false;
            }

            _currentEquipmentIndex = index;

            _currentEquipment = _equipment[_currentEquipmentIndex];

            return true;
        }

        public bool TryRemoveCurrentEquip()
        {
            if (CurrentEquip.Name == "Nothing")
            {
                return false;
            }

            _currentEquipmentIndex = -1;

            _currentEquipment = new Item();
            _currentEquipment.Name = "Nothing";

            return true;
        }

        public string[] GetItemNames()
        {
            string[] itemNames = new string[_equipment.Length];

            for (int i = 0; i < _equipment.Length; i++)
            {
                itemNames[i] = _equipment[i].Name;
            }

            return itemNames;
        }
    }
}
