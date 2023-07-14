using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Anduin.SuperRandom
{
    public class Randomizer
    {
        /// <summary>
        /// Returns if a number is prime.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>True if input is a prime, and False if not.</returns>
        public bool IsPrime(int input)
        {
            for (int i = 2; i * i <= input; i++)
            {
                if (input % i == 0)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Returns all prime numbers by sequence. Exmaple: 2, 3, 5, 7, 11, 13...
        /// </summary>
        /// <returns>All prime numbers</returns>
        public IEnumerable<int> PrimeNumbers()
        {
            yield return 2;

            for (int i = 3;; i += 2)
            {
                var startTime = DateTime.UtcNow;
                if (IsPrime(i))
                {
                    yield return i;
                    var endTime = DateTime.UtcNow;
                    Console.WriteLine($"Yield returned a number: {i}. Costs time: [{endTime - startTime}]");
                }
            }
            // ReSharper disable once IteratorNeverReturns
        }

        /// <summary>
        /// Returns all natural numbers by sequence. Example: 0, 1, 2, 3...
        /// </summary>
        /// <returns>All natural numbers by sequence</returns>
        private IEnumerable<int> GetNaturalNumbers()
        {
            for (int i = 0;; i++)
            {
                yield return i;
            }
            // ReSharper disable once IteratorNeverReturns
        }

        /// <summary>
        /// Try to break input N to two primes. And the primes products N.
        /// </summary>
        /// <param name="n"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>True if success, and False if failed.</returns>
        public bool TryBreakNumber(int n, out int left, out int right)
        {
            right = left = 0;
            var factors = new Queue<int>(3);
            for (int i = 2; i * i <= n; ++i)
            {
                if (n % i == 0)
                {
                    while (n % i == 0)
                    {
                        factors.Enqueue(i);
                        n /= i;
                        if (factors.Count >= 3)
                        {
                            return false;
                        }
                    }
                }
            }
            if (n != 1) factors.Enqueue(n);
            if (factors.Count == 2)
            {
                left = factors.Dequeue();
                right = factors.Dequeue();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns if input E is valid for input P, Q and D.
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        /// <param name="p"></param>
        /// <param name="q"></param>
        /// <returns>True if valid, and False if failed.</returns>
        private bool IsValidE(int d, int e, int p, int q)
        {
            return (d * e) % ((p - 1) * (q - 1)) == 1;
        }

        /// <summary>
        /// Returns if input D, P and Q can successfully output an E.
        /// </summary>
        /// <param name="d"></param>
        /// <param name="p"></param>
        /// <param name="q"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        private bool HaveValidE(int d, int p, int q, out int e)
        {
            foreach (var naturalNumber in GetNaturalNumbers())
            {
                if (naturalNumber > d * (p - 1) * (q - 1))
                {
                    break;
                }
                if (IsValidE(d, naturalNumber, p, q))
                {
                    e = naturalNumber;
                    return true;
                }
            }
            e = 0;
            return false;
        }

        /// <summary>
        /// Get D and E from input P and Q.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="q"></param>
        /// <returns></returns>
        private (int d, int e) GetDAndE(int p, int q)
        {
            foreach (int d in PrimeNumbers())
            {
                if (HaveValidE(d, p, q, out int e))
                {
                    return (d, e);
                }
            }
            throw new InvalidOperationException("WTF!");
        }

        /// <summary>
        /// Try to get all parameters.
        /// </summary>
        /// <param name="n"></param>
        /// <param name="p"></param>
        /// <param name="q"></param>
        /// <param name="d"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public bool TryGetRSAParameters(int n, out int p, out int q, out int d, out int e)
        {
            p = 0;
            q = 0;
            d = 0;
            e = 0;
            if (IsPrime(n))
            {
                return false;
            }
            if (!TryBreakNumber(n, out p, out q))
            {
                return false;
            }
            if (p == q)
            {
                return false;
            }
            (d, e) = GetDAndE(p, q);
            return true;
        }

        /// <summary>
        /// Generates random sequence from input N and D.
        /// </summary>
        /// <param name="n"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        private static IEnumerable<int> GetRandomNumbersRaw(int n, int d)
        {
            for (int i = 2; i < n; i++)
            {
                var mod = BigInteger.ModPow(BigInteger.Pow(i, d), 1, n);
                yield return (int)mod;
            }
        }

        /// <summary>
        /// Get `max` numbers less than input max, but more or equal than 0.
        /// For example, if input `max` is 5, will return 5 numbers, distributed from 0, 1, 2, 3, 4
        /// </summary>
        /// <param name="max">Max value. And counts in result.</param>
        /// <returns>A sequence that is random by your input.</returns>
        public IEnumerable<int> GetRandomNumbers(int max)
        {
            int n, d;
            for (n = max + 2; !TryGetRSAParameters(n, out int _, out int __, out d, out int ___); n++)
            {
            }
            return GetRandomNumbersRaw(n, d)
                .Select(t => t - 2)
                .Where(t => t < max);
        }
    }
}
