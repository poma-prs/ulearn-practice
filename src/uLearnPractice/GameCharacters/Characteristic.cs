using System;
using System.Linq;
using GameCharacters.Extensions;

namespace GameCharacters
{
    public interface ICharacteristic<T>
    {
        T CleanValue { get; }
        IBane<T> InnerBane { get; }
        T GetValue();
    }
    
    public class Characteristic<T> : ICharacteristic<T>
    {
        public Characteristic(T cleanValue)
        {
            CleanValue = cleanValue;
        }

        public T CleanValue { get; protected set; }
        public IBane<T> InnerBane { get; protected set; }

        public T GetValue()
        {
            return InnerBane.GetAbilities()
                .Aggregate(
                    CleanValue,
                    (value, ability) =>
                    {
                        return ability.Duration == null || InnerBane.Start.Add(ability.Duration.Value) >= DateTime.Now 
                            ? ability.UpdateFunc(value) 
                            : value;
                    });
        }

        internal void Put(IAbility<T> ability)
        {
            InnerBane = InnerBane == null ? new Bane<T>(ability) : InnerBane.Merge(ability);
        }
    }

    public interface IRecoveryCharacteristic<T> : ICharacteristic<T>
    {
        Func<T, TimeSpan, T> RecoveryFunc { get; }
    }

    public class RecoveryCharacteristic<T> : Characteristic<T>, IRecoveryCharacteristic<T>
    {
        public RecoveryCharacteristic(T cleanValue, Func<T, TimeSpan, T> recoveryFunc) : base(cleanValue)
        {
            RecoveryFunc = recoveryFunc;
        }

        private DateTime LastRecovery { get; set; } = DateTime.Now;
        public Func<T, TimeSpan, T> RecoveryFunc { get; }

        public new T GetValue()
        {
            CleanValue = RecoveryFunc(CleanValue, DateTime.Now.Subtract(LastRecovery));
            LastRecovery = DateTime.Now;
            return base.GetValue();
        }
    }
}