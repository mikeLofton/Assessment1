using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Assessment1
{
    class Entity
    {
        //Entity Variables
        private string _name;
        private float _health;
        private float _attackPower;
        private float _defensePower;
        
        /// <summary>
        /// Gets the entity name
        /// </summary>
        public string Name
        {
            get { return _name; }
        }

        /// <summary>
        /// Gets the entity's heath. Sets it to value.
        /// </summary>
        public virtual float Health
        {
            get { return _health; }

            set { _health = value; }
        }

        /// <summary>
        /// Gets the entity attack power. Sets to value
        /// </summary>
        public virtual float AttackPower
        {
            get { return _attackPower; }

            set { _attackPower = value; }
        }

        /// <summary>
        /// Gets the entity defense power. Sets to value
        /// </summary>
        public virtual float DefensePower
        {
            get { return _defensePower; }

            set { _defensePower = value; }
        }     

        /// <summary>
        /// Entity constructor
        /// </summary>
        public Entity()
        {
            _name = "Default";
            _health = 0;
            _attackPower = 0;
            _defensePower = 0;           
        }

        /// <summary>
        /// Entity constructor
        /// </summary>
        /// <param name="name">The entity's name</param>
        /// <param name="health">The entity's health</param>
        /// <param name="attackPower">The entity's attack</param>
        /// <param name="defensePower">The entity's defense</param>
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
        /// <param name="damageAmount">The attacking character's attack power</param>
        /// <returns>Returns damage amount minus attacked character's defense power</returns>
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
        /// <param name="defender">The character being attacked</param>
        /// <returns>Returns the defender taking damage</returns>
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
