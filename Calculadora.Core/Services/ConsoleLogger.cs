using Calculadora.Core.Interfaces;

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
      Console.ForegroundColor = ConsoleColor.Red;
      Console.WriteLine($"[ERRO] {DateTime.Now:HH:mm:ss}: {mensagem}");
      Console.ResetColor();
    }

    public void LogOperacaoSucesso(string operacao, double a, double b, double resultado)
    {
      Console.ForegroundColor = ConsoleColor.Green;
      Console.WriteLine($"[OK] {DateTime.Now:HH:mm:ss}: {a} {operacao} {b} = {resultado}");
      Console.ResetColor();
    }
    
    public void LogOperacaoErro(string operacao, double a, double b, string erro)
    {
      Console.ForegroundColor = ConsoleColor.Yellow;
      Console.WriteLine($"[FALHA] {DateTime.Now:HH:mm:ss}: {a} {operacao} {b} -> ERRO: {erro}");
      Console.ResetColor();
    }
  }
}