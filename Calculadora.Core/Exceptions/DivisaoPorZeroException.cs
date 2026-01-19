namespace Calculadora.Core.Exceptions
{
  public class DivisaoPorZeroException : Exception
  {
    public DivisaoPorZeroException()
        : base("Não é possível dividir por zero.")
    {
    }

    public DivisaoPorZeroException(string message)
        : base(message)
    {
    }

    public DivisaoPorZeroException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
  }
}