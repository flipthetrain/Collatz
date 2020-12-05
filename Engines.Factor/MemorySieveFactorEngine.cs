using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Engines.Factor
{
    public class MemorySieveFactorEngine : IFactorEngine
    {
        private List<int> primes = null;
        private List<(int, int)[]> factorMemory;

        public MemorySieveFactorEngine(ref List<(int,int)[]> FactorMemory)
        {
            primes = new List<int>() { 2, 3 };
            factorMemory = FactorMemory;
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
            int maxFactor = (int)Math.Round(Math.Sqrt(n));

            for (int i = 0; i < primes.Count; i++)
            {
                int testPrime = primes[i];
                if (n % testPrime == 0)
                {
                    int memoryValue = n / testPrime;
                    IEnumerable<Tuple<int, int>> factors = factorMemory[memoryValue].Select(f => new Tuple<int, int>(f.Item1, f.Item2));
                }
            }

            while (true)
            {
                int numPrimes = primes.Count();
                AddNextPrimeToSieve(maxFactor);
                if (primes.Count() == numPrimes) return new (int, int)[1] { (n, 1) };
                int testPrime = primes[numPrimes+1];
                if (n % testPrime == 0) return new (int, int)[1] { (testPrime, 1) };
            }
        }
    }
}
