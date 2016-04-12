using System.Collections.Generic;
using System.Linq;

namespace GameCharacters.Extensions
{
    public static class AbilityExtensions
    {
        public static IEnumerable<IAbility<T>> GetAbilities<T>(this IAbility<T> ability)
        {
            while (ability != null)
            {
                yield return ability;
                ability = ability.InnerAbility;
            }
        }

        public static IAbility<T> Merge<T>(this IAbility<T> ability, IAbility<T> other)
        {
            return ability.GetAbilities().Aggregate(other, (a, b) => new Ability<T>(b.Duration, b.UpdateFunc, a));
        }

        public static IBane<T> ToBane<T>(this IAbility<T> ability)
        {
            return new Bane<T>(ability);
        }
    }
}