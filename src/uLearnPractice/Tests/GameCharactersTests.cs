using System;
using System.Threading;
using GameCharacters;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class GameCharactersTests
    {
        private Character WhiteWizard { get; set; }
        private Character BlackWizard { get; set; }

        [SetUp]
        public void SetUp()
        {
            WhiteWizard = new Character(
                new Characteristic<int>(10),
                new Characteristic<int>(15),
                new RecoveryCharacteristic<int>(50, (x, span) => (int)span.TotalSeconds * 2 + x));

            BlackWizard = new Character(
                new Characteristic<int>(5),
                new Characteristic<int>(50),
                new RecoveryCharacteristic<int>(50, (x, span) => (int)span.TotalSeconds * 2 + x));
        }

        [Test]
        public void PutOn_Test()
        {
            BlackWizard.CobraRoll.PutOn(WhiteWizard.Speed);
            Assert.AreEqual(5, WhiteWizard.Speed.GetValue());

            WhiteWizard.TigerBite.PutOn(BlackWizard.Health);
            Assert.AreEqual(49, BlackWizard.Health.GetValue());

            BlackWizard.Dick.PutOn(WhiteWizard.Health);
            Assert.AreEqual(225, WhiteWizard.Health.GetValue());
        }

        [Test]
        public void CheckCooldown_Test()
        {
            Assert.IsTrue(WhiteWizard.CobraRoll.PutOn(BlackWizard.Health));
            Thread.Sleep(TimeSpan.FromMilliseconds(100));
            Assert.IsFalse(WhiteWizard.CobraRoll.PutOn(BlackWizard.Health));
            Thread.Sleep(TimeSpan.FromMilliseconds(500));
            Assert.IsTrue(WhiteWizard.CobraRoll.PutOn(BlackWizard.Health));
        }

        [Test]
        public void CheckBaneDuration_Test()
        {
            BlackWizard.CobraRoll.PutOn(WhiteWizard.Speed);
            Assert.AreEqual(5, WhiteWizard.Speed.GetValue());
            Thread.Sleep(TimeSpan.FromMilliseconds(500));
            Assert.AreEqual(10, WhiteWizard.Speed.GetValue());
        }

        [Test]
        public void CheckMana_Test()
        {
            BlackWizard.CobraRoll.PutOn(WhiteWizard.Speed);
            Assert.AreEqual(50 - BlackWizard.CobraRoll.ManaCost, BlackWizard.Mana.GetValue());

            WhiteWizard.TigerBite.PutOn(BlackWizard.Health);
            Assert.AreEqual(50 - WhiteWizard.TigerBite.ManaCost, WhiteWizard.Mana.GetValue());

            BlackWizard.Dick.PutOn(WhiteWizard.Health);
            Assert.AreEqual(
                50 - BlackWizard.CobraRoll.ManaCost - BlackWizard.Dick.ManaCost, 
                BlackWizard.Mana.GetValue());
        }
    }
}