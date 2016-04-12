using System.Collections.Generic;

namespace GameCharacters.Extensions
{
    public static class BaneExtensions
    {
        public static IBane<T> Merge<T>(this IBane<T> bane, IAbility<T> ability)
        {
            return new Bane<T>(bane.Ability.Merge(ability), bane.Start);
        }

        public static IEnumerable<IAbility<T>> GetAbilities<T>(this IBane<T> bane)
        {
            return bane.Ability.GetAbilities();
        }
    }
}