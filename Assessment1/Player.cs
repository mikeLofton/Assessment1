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
        private int _keys;
        private string _job;
        private int _gold;

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

            set { _keys = value; }
        }

        public int Gold
        {
            get { return _gold; }

            set { _gold = value; }
        }

        public Player(Item[] items) : base()
        {
            _currentEquipment.Name = "Nothing";
            _equipment = items;
            _currentEquipmentIndex = -1;         
        }

        public Player(string name, float health, float attackPower, float defensePower, int gold, Item[] items, string job, int keys) : base(name, health, attackPower, defensePower)
        {
            _equipment = items;
            _currentEquipment.Name = "Nothing";
            _job = job;
            _gold = gold;
            _keys = keys;
            
        }

        /// <summary>
        /// Equip the item at the selected index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Allows player to buy item
        /// </summary>
        /// <param name="item"></param>
        public void Buy(Item item)
        {
            _gold -= item.Cost;

            Item[] playerConsumables = new Item[_equipment.Length + 1];

            for (int i = 0; i < _equipment.Length; i++)
                playerConsumables[i] = _equipment[i];

            playerConsumables[_equipment.Length] = item;

            _equipment = playerConsumables;
        }

        /// <summary>
        /// Gets the names of player's starting items
        /// </summary>
        /// <returns></returns>
        public string[] GetItemNames()
        {
            string[] itemNames = new string[_equipment.Length];

            for (int i = 0; i < _equipment.Length; i++)
            {
                itemNames[i] = _equipment[i].Name;
            }

            return itemNames;
        }

        /// <summary>
        /// Saves player values
        /// </summary>
        /// <param name="writer"></param>
        public override void Save(StreamWriter writer)
        {
            writer.WriteLine(_job);
            writer.WriteLine(_gold);
            writer.WriteLine(_keys);
            base.Save(writer);
            writer.WriteLine(_equipment.Length);
            writer.WriteLine(_currentEquipmentIndex);
        }

        /// <summary>
        /// Loads player values
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public override bool Load(StreamReader reader)
        {
            //If the base loading function fails...
            if (!base.Load(reader))
                //...return false
                return false;

            //If the current line can't be converted into an int...
            if (!int.TryParse(reader.ReadLine(), out _currentEquipmentIndex))
                //...return false
                return false;

            if (!int.TryParse(reader.ReadLine(), out _gold))
                return false;

            if (!int.TryParse(reader.ReadLine(), out _keys))
                return false;

            //Return whether or not the item was equipped successfully
            return TryEquip(_currentEquipmentIndex);

            
        }
    }
}
