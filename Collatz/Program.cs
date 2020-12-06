using Domain.Interfaces;
using Engine.Coefficients;
using Engines.Factor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Collatz
{
    class Program
    {
        static void Main(string[] args)
        {
            SimpleCoefficients();
        }

        static void SimpleCoefficients()
        {
            ICoefficientsEngine ce = new SimpleCoefficient();
            int cg = 0;
            List<(int, int, int)> ps = new List<(int, int, int)>() { (0, 1, 0) };
            while (true)
            {
                ps = ps.Union(ps.Select(c => ce.NextEven(c)).Union(ps.Where(c => c.Item1 == cg).Select(c => ce.NextOdd(c)))).ToList();
                var tmp = ps.OrderBy(c => c.Item2).ThenBy(c => c.Item3).ThenBy(c => c.Item1).ToList();
                cg++;
            }
        }

        static void Memory()
        {
            IFactorEngine se = new SimpleSieveFactorEngine();
            List<(int, int)[]> data = new List<(int, int)[]>();

            using (StreamWriter sw = new StreamWriter(@"d:\factors.dat"))
            {
                for (int i = 0; i < 1000000000; i++)
                {
                    (int, int)[] factors = se.Factor(i);
                    data.Add(factors);

                    sw.WriteLine($"{i}:{string.Join(":", factors.Select(f => $"{f.Item1},{f.Item2}"))}");
                }
            }
        }

        static void SimpleDump()
        {
            IFactorEngine se = new SimpleSieveFactorEngine();

            using (StreamWriter sw = new StreamWriter(Console.OpenStandardOutput()))
            {
                for (int i = 0; i < 1000000000; i++)
                {
                    (int, int)[] factors = se.Factor(i);

                    sw.WriteLine($"{i}:{string.Join(":", factors.Select(f => $"{f.Item1},{f.Item2}"))}");
                }
            }
        }

        static void ProgressiveDump()
        {
            List<int> primes = new List<int>();
            IFactorEngine se = new SimpleSieveFactorEngine();

            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);

            using (StreamWriter sw = new StreamWriter("out.dat"))
            {
                for (int i = 2; i < 1000000000; i++)
                {
                    (int, int)[] factors = se.Factor(i);

                    IEnumerable<int> newPrimes = factors.Where(f => !primes.Any(p => p == f.Item1)).Select(x => x.Item1);

                    if (newPrimes.Count() != 0)
                    {
                        primes.AddRange(newPrimes);
                        primes.Sort();

                        sw.WriteLine("\t\t===");
                        sw.WriteLine("\t\t" + string.Join("\t", primes));
                        sw.WriteLine("\t\t===");

                        Console.WriteLine("\t\t===");
                        Console.WriteLine("\t\t" + string.Join("\t", primes));
                        Console.WriteLine("\t\t===");
                    }

                    string factorData = string.Join("\t", primes.Select(p => factors.FirstOrDefault(f => f.Item1 == p).Item2));
                    sw.WriteLine($"{i}\t:\t{factorData}");

                    Console.WriteLine($"{i}\t:\t{factorData}");

                    sw.Flush();
                }
            }
        }
    }
}
