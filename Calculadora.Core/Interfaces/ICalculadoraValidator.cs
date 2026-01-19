namespace Calculadora.Core.Interfaces
{
  public interface ICalculadoraValidator
  {
    bool ValidarDivisor(double divisor);
    bool ValidarNumero(double numero);
    bool ValidarOperacao(double a, double b, string operacao);
  }
}