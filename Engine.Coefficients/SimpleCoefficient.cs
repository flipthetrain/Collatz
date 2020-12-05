using Domain.Interfaces;

namespace Engine.Coefficients
{
    public class SimpleCoefficient : ICoefficientsEngine
    {
        public (int, int, int) NextEven((int, int, int) current)
        {
            int d = current.Item1+1;
            int a = current.Item2;
            int b = current.Item3;

            return (d, a, b);
        }

        public (int, int, int) NextOdd((int, int, int) current)
        {
            int d = current.Item1;
            int a = current.Item2 * 3;
            int b = current.Item3 * 3 + 1;

            return (d,a,b);
        }
    }
}
