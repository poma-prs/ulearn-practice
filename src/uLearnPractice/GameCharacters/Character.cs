using System;

namespace GameCharacters
{
    public class Character
    {
        public Character(Characteristic<int> speed, Characteristic<int> health, RecoveryCharacteristic<int> mana)
        {
            Speed = speed;
            Health = health;
            Mana = mana;

            CobraRoll = new CharacterAbility<int>(this, TimeSpan.FromMilliseconds(500), x => x/2, TimeSpan.FromMilliseconds(500), 10);
            TigerBite = new CharacterAbility<int>(this, TimeSpan.FromSeconds(10), x => x - 1, TimeSpan.FromSeconds(10), 5);
            Dick = new CharacterAbility<int>(this, TimeSpan.FromDays(1), x => x * x, TimeSpan.FromDays(1), 100);
        }

        public Characteristic<int> Speed { get; }
        public Characteristic<int> Health { get; }
        public RecoveryCharacteristic<int> Mana { get; }

        public CharacterAbility<int> CobraRoll { get; }
        public CharacterAbility<int> TigerBite { get; }
        public CharacterAbility<int> Dick { get; }
    }
}