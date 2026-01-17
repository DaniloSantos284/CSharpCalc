using Calculadora.Core.Interfaces;

namespace Calculadora.Core.Services
{
  public class CalculadoraService : ICalculadoraService
  {
    public double Somar(double a, double b)
    {
      return a + b;
    }

    public double Subtrair(double a, double b)
    {
      return a - b;
    }

    public double Multiplicar(double a, double b)
    {
      return a * b;
    }

    public double Dividir(double a, double b)
    {
      if (b == 0)
      {
        throw new ArgumentException("Não é possível dividir por zero.");
      }

      return a / b;
    }
  }
}