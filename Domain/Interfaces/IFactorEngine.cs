using System;

namespace Domain.Interfaces
{
    public interface IFactorEngine
    {
        (int, int)[] Factor(int n);
    }
}
