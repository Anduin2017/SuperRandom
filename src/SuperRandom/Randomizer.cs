using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Anduin.SuperRandom
{
    public class Randomizer
    {
        public bool IsPrime(int input)
        {
            var testSize = Math.Sqrt(input);
            for (int i = 2; i <= testSize; i++)
            {
                if (input % i == 0)
                    return false;
            }
            return true;
        }

        public IEnumerable<int> PrimeNumbers()
        {
            yield return 2;
            for (int i = 3; true; i += 2)
            {
                if (IsPrime(i))
                {
                    yield return i;
                }
            }
        }

        public IEnumerable<int> GetNaturalNumbers()
        {
            for (int i = 0; true; i++)
            {
                yield return i;
            }
        }

        public bool IsValidE(int d, int e, int p, int q)
        {
            return (d * e) % ((p - 1) * (q - 1)) == 1;
        }

        public bool HaveValidE(int d, int p, int q, out int e)
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

        public bool TryBreakNumber(int x, out int left, out int right)
        {
            if (IsPrime(x))
            {
                left = right = 0;
                return false;
            }
            var testMax = Math.Sqrt(x);
            foreach (var leftPrime in PrimeNumbers())
            {
                if (leftPrime > testMax) break;
                var rightPrime = x / leftPrime;
                if (leftPrime * rightPrime == x && IsPrime(rightPrime))
                {
                    left = leftPrime;
                    right = rightPrime;
                    return true;
                }
                else continue;
            }
            left = right = 0;
            return false;
        }

        public (int d, int e) GetDAndE(int p, int q)
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

        public IEnumerable<int> GetRandomNumbers(int max)
        {
            int n, d;
            for (n = max + 2; !TryGetRSAParameters(n, out int p, out int q, out d, out int e); n++)
            {
            }
            return GetRandomNumbersRaw(n, d)
                .Select(t => t - 2)
                .Where(t => t < max);
        }

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

        public static IEnumerable<int> GetRandomNumbersRaw(int n, int d)
        {
            for (int i = 2; i < n; i++)
            {
                var mod = BigInteger.ModPow(BigInteger.Pow(i, d), 1, n);
                yield return (int)mod;
            }
        }
    }
}
