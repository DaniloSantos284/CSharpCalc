using Calculadora.Core.Interfaces;
using Calculadora.Core.Exceptions;

namespace Calculadora.Core.Services
{
  public class CalculadoraValidator : ICalculadoraValidator
  {
    public void ValidarDivisor(double divisor)
    {
      if (divisor == 0)
      {
        throw new DivisaoPorZeroException();
      }
    }

    public void ValidarNumero(double numero)
    {
      // Valida se não é NaN ou infinito
      if (double.IsNaN(numero) || double.IsInfinity(numero))
      {
        throw new ArgumentException($"Número inválido: {numero}");
      }

    }

    public void ValidarOperacao(double a, double b, string operacao)
    {
      ValidarNumero(a);
      ValidarNumero(b);

      if (operacao == "/")
      {
        ValidarDivisor(b);
      }

      // Valida limites razoáveis para evitar overflow
      if (operacao == "*" && (Math.Abs(a) > 1e100 || Math.Abs(b) > 1e100))
      {
        throw new OverflowException($"Multiplicação pode causar overflow: {a} * {b}");
      }
    }
  }
}