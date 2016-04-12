using System;
using System.Collections.Generic;

namespace GameCharacters
{
    public class Character
    {
        public Character(Characteristic<int> speed, Characteristic<int> health)
        {
            Speed = speed;
            Health = health;
        }

        public Characteristic<int> Speed { get; }
        public Characteristic<int> Health { get; }

        public Ability<int> CobraRoll { get; } = new Ability<int>(TimeSpan.FromSeconds(10), x => x/2);
        public Ability<int> TigerBite { get; } = new Ability<int>(TimeSpan.FromSeconds(10), x => x - 1);
    }
}