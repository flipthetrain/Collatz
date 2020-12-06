using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Engines.Factor
{
    public class SimpleSieveFactorEngine : IFactorEngine
    {
        private List<int> primes = null;

        public SimpleSieveFactorEngine()
        {
            primes = new List<int>();
        }

        private void AddNextPrimeToSieve(int stopValue)
        {
            int testPrime = primes.Max() + 2;
            if (testPrime > stopValue) return;

            while (primes.Any(x => testPrime % x == 0))
            {
                testPrime += 2;
                if (testPrime > stopValue) return;
            }
            primes.Add(testPrime);
            return;
        }


        public (int, int)[] Factor(int n)
        {
            List<(int, int)> factors = new List<(int, int)>();
            int power = 0;
            int maxFactor = (int)Math.Round(Math.Sqrt(n));

            // 0 and 1 are special cases
            if (n == 0 || n == 1) return factors.ToArray();

            // seed initial primes
            // 2 is special in that it's even
            // 3 is the first odd prime and all other primes after are odd
            if (primes.Count == 0)
            {
                primes.Add(2);
                primes.Add(3);
            }

            int startIndex = 0;
            while (n > 1)
            {
                for (int i = startIndex; i < primes.Count; i++)
                {
                    int testPrime = primes[i];
                    power = 0;
                    while (n % testPrime == 0)
                    {
                        n = n / testPrime;
                        power++;
                    }
                    if (power != 0) factors.Add((testPrime, power));
                    if (n == 1) return factors.ToArray();
                }

                startIndex = primes.Count();
                AddNextPrimeToSieve(maxFactor);
                if (primes.Count() == startIndex)
                {
                    factors.Add((n, 1));
                    return factors.ToArray();
                }
            }

            return factors.ToArray();
        }
    }
}
