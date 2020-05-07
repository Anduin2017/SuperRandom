using System;
using System.Collections.Generic;
using System.Linq;

namespace TestRan
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("How many unique random numbers do you want?");
                if (!ulong.TryParse(Console.ReadLine(), out ulong counts))
                {
                    break;
                }
                var array = new ulong[counts];
                foreach (var rand in GetRandomNumbers(counts))
                {
                    Console.WriteLine("Got random number: " + rand);
                    array[rand] += 1;
                }
                foreach (var bit in array.Skip(2))
                {
                    if (bit != 1)
                    {
                        Console.WriteLine("Bit not set! Wrong code!");
                    }
                }
            }
        }

        public static bool IsPrime(ulong input)
        {
            var testSize = Math.Sqrt(input);
            for (ulong i = 2; i <= testSize; i++)
            {
                if (input % i == 0)
                    return false;
            }
            return true;
        }

        public static IEnumerable<ulong> PrimeNumbers()
        {
            yield return 2;
            for (ulong i = 3; true; i += 2)
            {
                if (IsPrime(i))
                {
                    yield return i;
                }
            }
        }

        public static IEnumerable<ulong> GetNaturalNumbers()
        {
            for (ulong i = 0; true; i++)
            {
                yield return i;
            }
        }

        public static bool IsValidE(ulong d, ulong e, ulong p, ulong q)
        {
            return (d * e) % ((p - 1) * (q - 1)) == 1;
        }

        public static bool HaveValidE(ulong d, ulong p, ulong q, out ulong e)
        {
            foreach (var naturalNumber in GetNaturalNumbers())
            {
#warning Not be best practice!
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

        public static bool TryBreakNumber(ulong x, out ulong left, out ulong right)
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

        public static (ulong d, ulong e) GetDAndE(ulong p, ulong q)
        {
            foreach (ulong d in PrimeNumbers())
            {
                if (HaveValidE(d, p, q, out ulong e))
                {
                    return (d, e);
                }
            }
            throw new InvalidOperationException("WTF!");
        }

        public static IEnumerable<ulong> GetRandomNumbers(ulong max)
        {
            ulong n, d;
            for (n = max + 2; !TryGetRSAParameters(n, out ulong p, out ulong q, out d, out ulong e); n++)
            {
            }
            return GetRandomNumbersRaw(n, d)
                .Select(t => t - 2)
                .Where(t => t < max);
        }

        public static bool TryGetRSAParameters(ulong n, out ulong p, out ulong q, out ulong d, out ulong e)
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

        public static IEnumerable<ulong> GetRandomNumbersRaw(ulong n, ulong d)
        {
            for (ulong i = 2; i < n; i++)
            {
                yield return (ulong)(Math.Pow(i, d) % n);
            }
        }
    }
}
