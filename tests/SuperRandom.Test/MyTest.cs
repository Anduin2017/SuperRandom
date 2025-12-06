

namespace Anduin.SuperRandom.Test
{

    [TestClass]
    public class MyTest
    {
        private readonly Randomizer _tester = new Randomizer();

        [TestMethod]
        public void Primes()
        {
            var primes = _tester.PrimeNumbers().Take(5).ToArray();

            Assert.AreEqual(2, primes[0]);
            Assert.AreEqual(3, primes[1]);
            Assert.AreEqual(5, primes[2]);
            Assert.AreEqual(7, primes[3]);
            Assert.AreEqual(11, primes[4]);
        }

        [TestMethod]
        public void Break11Fail()
        {
            var success = _tester.TryBreakNumber(11, out int _, out int __);
            Assert.IsFalse(success);
        }

        [TestMethod]
        public void Break121Success()
        {
            var success = _tester.TryBreakNumber(121, out int p, out int q);
            Assert.IsTrue(success);
            Assert.AreEqual(11, p);
            Assert.AreEqual(11, q);
        }

        [TestMethod]
        public void Break2131Success()
        {
            var success = _tester.TryBreakNumber(2131, out int _, out int __);
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
