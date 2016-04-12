using System;

namespace GameCharacters
{
    public interface IAbility<T>
    {
        TimeSpan Cooldown { get; }
        TimeSpan? Duration { get; }
        Func<T, T> UpdateFunc { get; }
        IAbility<T> InnerAbility { get; }
        bool PutOn(Characteristic<T> characteristic);
    }

    public class Ability<T> : IAbility<T>
    {
        private DateTime LastUse { get; set; } = DateTime.MinValue;

        public Ability(TimeSpan cooldown, Func<T, T> updateFunc, TimeSpan? duration = null, IAbility<T> innerAbility = null)
        {
            if (updateFunc == null)
                throw new ArgumentException();
            Cooldown = cooldown;
            Duration = duration;
            UpdateFunc = updateFunc;
            InnerAbility = innerAbility;
        }

        public TimeSpan Cooldown { get; }
        public TimeSpan? Duration { get; }
        public Func<T, T> UpdateFunc { get; }
        public IAbility<T> InnerAbility { get; }

        public bool PutOn(Characteristic<T> characteristic)
        {
            if (LastUse.Add(Cooldown) >= DateTime.Now)
                return false;
            characteristic.Put(this);
            LastUse = DateTime.Now;
            return true;
        }
    }

    public interface ICharacterAbility<T> : IAbility<T>
    {
        Character Character { get; }
        int ManaCost { get; }
    }

    public class CharacterAbility<T> : Ability<T>, ICharacterAbility<T>
    {
        public CharacterAbility(Character character, TimeSpan cooldown, Func<T, T> updateFunc, TimeSpan duration, int manaCost, IAbility<T> innerAbility = null) 
            : base(cooldown, updateFunc, duration, innerAbility)
        {
            if (character == null)
                throw new ArgumentException();
            Character = character;
            ManaCost = manaCost;
        }
        
        public Character Character { get; }
        public int ManaCost { get; }

        public new bool PutOn(Characteristic<T> characteristic)
        {
            var ability = new Ability<int>(TimeSpan.Zero, x => x - ManaCost);
            ability.PutOn(Character.Mana);
            return base.PutOn(characteristic);
        }
    }
}