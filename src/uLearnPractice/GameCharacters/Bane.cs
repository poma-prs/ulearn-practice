using System;

namespace GameCharacters
{
    public interface IBane<T>
    {
        DateTime Start { get; }
        IAbility<T> Ability { get; } 
    }

    public class Bane<T> : IBane<T>
    {
        public Bane(IAbility<T> ability, DateTime? start = null)
        {
            if (Ability == null)
                throw new ArgumentException("Ability is null");
            Ability = ability;
            Start = start ?? DateTime.Now;
        }

        public DateTime Start { get; }
        public IAbility<T> Ability { get; }
    }
}