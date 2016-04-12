using System.Linq;
using GameCharacters.Extensions;

namespace GameCharacters
{
    public interface ICharacteristic<T>
    {
        T DefaultValue { get; }
        IBane<T> InnerBane { get; }
        T GetValue();
        void Put(IAbility<T> ability);
    }
    
    public class Characteristic<T> : ICharacteristic<T>
    {
        public Characteristic(T defaultValue)
        {
            DefaultValue = defaultValue;
        }

        public T DefaultValue { get; }
        public IBane<T> InnerBane { get; protected set; }

        public T GetValue()
        {
            return InnerBane.GetAbilities()
                .Aggregate(DefaultValue, (value, ability) => ability.UpdateFunc(value));
        }

        public void Put(IAbility<T> ability)
        {
            InnerBane = InnerBane.Merge(ability);
        }
    }
}