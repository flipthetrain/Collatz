namespace Domain.Interfaces
{
    public interface ICoefficientsEngine
    {
        (int,int,int) NextEven((int, int, int) current);
        (int,int,int) NextOdd((int, int, int) current);

    }
}
