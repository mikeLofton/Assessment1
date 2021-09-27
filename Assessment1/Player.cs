﻿using System;
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

        public Player(Item[] items) : base()
        {
            _currentEquipment.Name = "Nothing";
            _equipment = items;
            _currentEquipmentIndex = -1;           
        }

        public Player(string name, float health, float attackPower, float defensePower, int gold, Item[] items, string job) : base(name, health, attackPower, defensePower, gold)
        {
            _equipment = items;
            _currentEquipment.Name = "Nothing";
            _job = job;
        }
    }
}