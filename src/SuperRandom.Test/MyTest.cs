using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Anduin.SuperRandom.Test
{
    [TestClass]
    public class MyTest
    {
        private Randomizer _tester;

        [TestInitialize]
        public void Init()
        {
            _tester = new Randomizer();
        }


        [TestMethod]
        public void Primes()
        {
            var primes = _tester.PrimeNumbers().Take(5).ToArray();

            Assert.AreEqual(primes[0], 2);
            Assert.AreEqual(primes[1], 3);
            Assert.AreEqual(primes[2], 5);
            Assert.AreEqual(primes[3], 7);
            Assert.AreEqual(primes[4], 11);
        }

        [TestMethod]
        public void Break11Fail()
        {
            var success = _tester.TryBreakNumber(11, out int p, out int q);
            Assert.IsFalse(success);
        }

        [TestMethod]
        public void Break121Success()
        {
            var success = _tester.TryBreakNumber(121, out int p, out int q);
            Assert.IsTrue(success);
            Assert.AreEqual(p, 11);
            Assert.AreEqual(q, 11);
        }

        [TestMethod]
        public void Break2131Success()
        {
            var success = _tester.TryBreakNumber(2131, out int p, out int q);
            Assert.IsFalse(success);
        }

        [TestMethod]
        public void BreakMillionNumbers()
        {
            var random = new Random();
            for (int i = 0; i < 1000 * 1000; i++)
            {
                var testNumber = random.Next(4, 10000);
                var success = _tester.TryBreakNumber(testNumber, out int p, out int q);
                if (success)
                {
                    Assert.IsTrue(_tester.IsPrime(p));
                    Assert.IsTrue(_tester.IsPrime(q));
                    Assert.AreEqual(testNumber, p * q);
                }
            }
        }
    }
}
