using System;
using System.Collections.Generic;
using System.Linq;

namespace TestRan
{
    class Program
    {
        static void Main(string[] args)
        {
            const int counts = 1003;
            var array = new int[counts];
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

        public static bool IsPrime(int input)
        {
            var testSize = Math.Sqrt(input);
            for (int i = 2; i <= testSize; i++)
            {
                if (input % i == 0)
                    return false;
            }
            return true;
        }

        public static IEnumerable<int> GetPrimeNumbers()
        {
            for (int i = 2; true; i++)
            {
                if (IsPrime(i))
                {
                    yield return i;
                }
            }
        }

        public static IEnumerable<int> GetNaturalNumbers()
        {
            for (int i = 0; true; i++)
            {
                yield return i;
            }
        }

        public static bool IsValidE(int d, int e, int p, int q)
        {
            return (d * e) % ((p - 1) * (q - 1)) == 1;
        }

        public static bool CanBreak(int input, out int left, out int right)
        {
            (left, right) = BreakNumber(input);
            return (left, right) != (-1, -1);
        }

        public static (int, int) BreakNumber(int x)
        {
            if (IsPrime(x))
            {
                return (-1, -1);
            }
            var testMax = x / 2 + 1;
            foreach (var leftPrime in GetPrimeNumbers())
            {
                if (leftPrime > testMax) break;
                foreach (var rightPrime in GetPrimeNumbers())
                {
                    if (rightPrime > testMax) break;
                    else if (leftPrime * rightPrime > x) break;
                    else if (leftPrime * rightPrime == x) return (leftPrime, rightPrime);
                    else continue;
                }
            }
            return (-1, -1);
        }

        public static bool HaveValidE(int d, int p, int q, out int e)
        {
            foreach (var naturalNumber in GetNaturalNumbers())
            {
                if (naturalNumber > d * ((p - 1) * (q - 1)))
                {
                    break;
                }
                if (IsValidE(d, naturalNumber, p, q))
                {
                    e = naturalNumber;
                    return true;
                }
            }
            e = -1;
            return false;
        }

        public static (int d, int e) GetDAndE(int p, int q)
        {
            foreach (int d in GetPrimeNumbers())
            {
                if (HaveValidE(d, p, q, out int e))
                {
                    return (d, e);
                }
            }
            throw new InvalidOperationException("WTF!");
        }

        public static IEnumerable<int> GetRandomNumbers(int n)
        {
            if (IsPrime(n))
            {
                throw new InvalidOperationException("Can't input prime!");
            }
            if (!CanBreak(n, out int p, out int q))
            {
                throw new InvalidOperationException("Can't break to primes!");
            }
            if (p == q)
            {
                throw new InvalidOperationException("Can't break to two same primes!");
            }
            var (d, e) = GetDAndE(p, q);
            for (int i = 2; i < n; i++)
            {
                yield return (int)(Math.Pow(i, d) % n);
            }
        }
    }
}
