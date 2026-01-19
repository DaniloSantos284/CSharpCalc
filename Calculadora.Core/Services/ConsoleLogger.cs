using Calculadora.core.Interfaces;

namespace Calculadora.Core.Services
{
  public class ConsoleLogger : ILogger
  {
    public void LogInfo(string mensagem)
    {
      Console.WriteLine($"[INFO] {DateTime.Now:HH:mm:ss}: {mensagem}");
    }

    public void LogErro(string mensagem)
    {
      Console.WriteLine($"[ERRO] {DateTime.Now:HH:mm:ss}: {mensagem}");
    }

    public void LogOperacao(string operacao, double a, double b, double resultado)
    {
      Console.WriteLine($"[OPER] {DateTime.Now:HH:mm:ss}: {a} {operacao} {b} = {resultado}");
    }
    
    public void LogOperacao(string operacao, double a, double b, string erro)
    {
      Console.WriteLine($"[OPER] {DateTime.Now:HH:mm:ss}: {a} {operacao} {b} -> ERRO: {erro}");
    }
  }
}