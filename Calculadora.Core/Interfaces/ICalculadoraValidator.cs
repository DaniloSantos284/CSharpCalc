namespace Calculadora.Core.Interfaces
{
  public interface ICalculadoraValidator
  {
    void ValidarDivisor(double divisor);
    void ValidarNumero(double numero);
    void ValidarOperacao(double a, double b, string operacao);
  }
}