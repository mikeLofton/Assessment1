using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Assessment1
{
    class Entity
    {
        private string _name;
        private float _health;
        private float _attackPower;
        private float _defensePower;
        

        public string Name
        {
            get { return _name; }
        }

        public virtual float Health
        {
            get { return _health; }

            set { _health = value; }
        }

        public virtual float AttackPower
        {
            get { return _attackPower; }
        }

        public virtual float DefensePower
        {
            get { return _defensePower; }
        }     

        public Entity()
        {
            _name = "Default";
            _health = 0;
            _attackPower = 0;
            _defensePower = 0;           
        }

        public Entity(string name, float health, float attackPower, float defensePower)
        {
            _name = name;
            _health = health;
            _attackPower = attackPower;
            _defensePower = defensePower;          
        }

        /// <summary>
        /// Calculates the damage of an attack
        /// </summary>
        /// <param name="damageAmount"></param>
        /// <returns></returns>
        public float TakeDamage(float damageAmount)
        {
            float damageTaken = damageAmount - DefensePower;

            if (damageTaken < 0)
            {
                damageTaken = 0;
            }

            _health -= damageTaken;

            return damageTaken;
        }

        /// <summary>
        /// Has an attacker damage a defender
        /// </summary>
        /// <param name="defender"></param>
        /// <returns></returns>
        public float Attack(Entity defender)
        {
            return defender.TakeDamage(AttackPower);
        }

        /// <summary>
        /// Saves entity name, health, attack, and defense
        /// </summary>
        public virtual void Save(StreamWriter writer)
        {
            writer.WriteLine(_name);
            writer.WriteLine(_health);
            writer.WriteLine(_attackPower);
            writer.WriteLine(_defensePower);
        }

        /// <summary>
        /// Loads saved values
        /// </summary>
        public virtual bool Load(StreamReader reader)
        {
            _name = reader.ReadLine();

            if (!float.TryParse(reader.ReadLine(), out _health))
                return false;

            if (!float.TryParse(reader.ReadLine(), out _attackPower))
                return false;

            if (!float.TryParse(reader.ReadLine(), out _defensePower))
                return false;

            return true;
        }
    }
}
