namespace Calculadora.Core.Interfaces
{
  public interface ILogger
  {
    void LogInfo(string mensagem);
    void LogErro(string mensagem);
    void LogOperacaoSucesso(string operacao, double a, double b, double resultado);
    void LogOperacaoErro(string operacao, double a, double b, string erro);
  }
}