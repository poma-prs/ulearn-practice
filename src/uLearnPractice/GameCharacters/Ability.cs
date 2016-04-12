using System;

namespace GameCharacters
{
    public interface IAbility<T>
    {
        TimeSpan Duration { get; }
        Func<T, T> UpdateFunc { get; }
        IAbility<T> InnerAbility { get; }
    }

    public class Ability<T> : IAbility<T>
    {
        public Ability(TimeSpan duration, Func<T, T> updateFunc, IAbility<T> innerAbility = null)
        {
            Duration = duration;
            UpdateFunc = updateFunc;
            InnerAbility = innerAbility;
        }

        public TimeSpan Duration { get; }
        public Func<T, T> UpdateFunc { get; }
        public IAbility<T> InnerAbility { get; }
    }
}