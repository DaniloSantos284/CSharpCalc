using Calculadora.Core.Interfaces;
using Calculadora.Core.Exceptions;

namespace Calculadora.Core.Services
{
  public class CalculadoraValidator : ICalculadoraValidator
  {
    public bool ValidarDivisor(double divisor)
    {
      if (divisor == 0)
      {
        throw new DivisaoPorZeroException();
      }
      return true;
    }

    public bool ValidarNumero(double numero)
    {
      // Valida se não é NaN ou infinito
      if (double.IsNaN(numero) || double.IsInfinity(numero))
      {
        throw new ArgumentException($"Número inválido: {numero}");
      }
      return true;
    }

    public bool ValidarOperacao(double a, double b, string operacao)
    {
      ValidarNumero(a);
      ValidarNumero(b);

      if (operacao == "/")
      {
        ValidarDivisor(b);
      }
      return true;
    }
  }
}