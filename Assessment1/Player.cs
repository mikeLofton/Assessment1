using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Assessment1
{
    class Player : Entity
    {
        //Player Variables
        private Item[] _equipment;
        private Item _currentEquipment;
        private int _currentEquipmentIndex;       
        private int _keys;
        private string _job;
        private int _gold;

        /// <summary>
        /// Gets player defense power. If current equipment is defense type will add the current equip statboost to defense power.
        /// </summary>
        public override float DefensePower
        {
            get
            {
                if (_currentEquipment.Type == ItemType.DEFENSE)
                    return base.DefensePower + CurrentEquip.StatBoost;

                return base.DefensePower;
            }
        }

        /// <summary>
        /// Gets player attack power. If current equipment is attack type will add the current equip statboost to attack power.
        /// </summary>
        public override float AttackPower
        {
            get
            {
                if (_currentEquipment.Type == ItemType.ATTACK)
                    return base.AttackPower + CurrentEquip.StatBoost;

                return base.AttackPower;
            }
        }

        /// <summary>
        /// Gets player health. If current equipment is health type will add the current equip statboost to health.
        /// </summary>
        public override float Health
        {
            get
            {
                if (_currentEquipment.Type == ItemType.HEALTH)
                    return base.Health + CurrentEquip.StatBoost;

                return base.Health;
            }
        }

        /// <summary>
        /// Gets player's current equipment.
        /// </summary>
        public Item CurrentEquip
        {
            get { return _currentEquipment; }
        }

        /// <summary>
        /// Gets player's job. Sets job to value.
        /// </summary>
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

        /// <summary>
        /// Gets player's keys. Sets keys to value.
        /// </summary>
        public int Keys
        {
            get { return _keys; }

            set { _keys = value; }
        }

        /// <summary>
        /// Gets player's gold. Sets gold to value.
        /// </summary>
        public int Gold
        {
            get { return _gold; }

            set { _gold = value; }
        }

        /// <summary>
        /// Player constructor
        /// </summary>
        /// <param name="items">Array of player items</param>
        public Player(Item[] items) : base()
        {
            _currentEquipment.Name = "Nothing";
            _equipment = items;
            _currentEquipmentIndex = -1;         
        }

        /// <summary>
        /// Player constructor
        /// </summary>
        /// <param name="name">The player's name</param>
        /// <param name="health">The player's health</param>
        /// <param name="attackPower">The player's attack power</param>
        /// <param name="defensePower">The player's defense power</param>
        /// <param name="gold">The player's gold</param>
        /// <param name="items">The player's array of items</param>
        /// <param name="job">The player's job</param>
        /// <param name="keys">The player's keys</param>
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
        /// <param name="index">The index of the equipment array</param>
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

            if (item.Type == ItemType.HEALTH)
            {
                base.Health += item.StatBoost;
            }
            else if (item.Type == ItemType.ATTACK)
            {
                base.AttackPower += item.StatBoost;
            }
            else if (item.Type == ItemType.DEFENSE)
            {
                base.DefensePower += item.StatBoost;
            }         
        }

        /// <summary>
        /// Gets the names of player's starting items
        /// </summary>
        /// <returns>Item names</returns>
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
            base.Save(writer);            
            writer.WriteLine(_gold);
            writer.WriteLine(_keys);           
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

            if (!int.TryParse(reader.ReadLine(), out _gold))
                return false;

            if (!int.TryParse(reader.ReadLine(), out _keys))
                return false;        

            //If the current line can't be converted into an int...
            if (!int.TryParse(reader.ReadLine(), out _currentEquipmentIndex))
                //...return false
                return false;

            //Return whether or not the item was equipped successfully
            return TryEquip(_currentEquipmentIndex);

            
        }
    }
}
