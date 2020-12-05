using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;

namespace Engines.Factor
{
    public class LookupFactorEngine : IFactorEngine
    {
        private int[] primes = null;

        public LookupFactorEngine(string PrimeLookupFile)
        {
            primes = new int[0];
            using (StreamReader sr = new StreamReader(PrimeLookupFile))
            {
                while (!sr.EndOfStream)
                {
                    primes = LoadPrimes(primes, sr);
                }
            }
        }

        private int[] LoadPrimes(int[] primes, StreamReader sr)
        {
            int newPrimesLength = primes.Length == 0 ? 1 : primes.Length * 2;
            int[] newPrimes = new int[newPrimesLength];
            Array.Copy(primes, 0, newPrimes, 0, primes.Length);
            for (int i = primes.Length; i < newPrimesLength; i++)
            {
                string primeRec = sr.ReadLine();
                if (int.TryParse(primeRec, out int nextPrime))
                {
                    newPrimes[i] = nextPrime;
                }
                else
                {
                    throw new ArgumentException($"Bad prime record {primeRec} in line {i}.");
                }

                if (sr.EndOfStream)
                {
                    int[] tmpPrimes = new int[i+1];
                    Array.Copy(newPrimes, 0, tmpPrimes, 0, i+1);
                    newPrimes = tmpPrimes;
                    i = newPrimesLength;
                }
            }
            Array.Sort(newPrimes);
            return newPrimes;
        }

        public (int, int)[] Factor(int n)
        {
            List<(int, int)> factors = new List<(int, int)>();
            int maxPrime = (int)Math.Round(Math.Sqrt(n));

            for (int i = 0; i < primes.Length; i++)
            {
                int testPrime = primes[i];
                if (testPrime > maxPrime) break;
                int power = 0;
                while (n % testPrime == 0)
                {
                    n=n/testPrime;
                    power++;
                }
                if (power!=0) factors.Add((testPrime, power));
                if (n == 1) break;
            }
            if (n != 1) factors.Add((n, 1));
            return factors.ToArray();
        }
    }
}
