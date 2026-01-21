// Calculadora.Core/Services/CalculadoraService.cs
using Calculadora.Core.Interfaces;
using Calculadora.Core.Exceptions;

namespace Calculadora.Core.Services
{
  public class CalculadoraService : ICalculadoraService
  {
    private readonly ILogger _logger;
    private readonly ICalculadoraValidator _validator;

    // Construtor que recebe as dependências
    public CalculadoraService(ILogger logger, ICalculadoraValidator validator)
    {
      _logger = logger ?? throw new ArgumentNullException(nameof(logger));
      _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }

    // Método genérico para realizar operações e evitar repetição de código
    private double ExecutarOperacao(string operacao, double a, double b, Func<double, double, double> operacaoMatematica)
    {
      try
      {
        _validator.ValidarOperacao(a, b, operacao);

        var resultado = operacaoMatematica(a, b);

        // Verificar overflow pós-operação
        if (double.IsInfinity(resultado))
        {
          throw new OverflowException($"Resultado da operação {operacao} causou overflow.");
        }

        _logger.LogOperacaoSucesso(operacao, a, b, resultado);
        _logger.LogInfo($"{GetNomeOperacao(operacao)} realizada: {a} {operacao} {b} = {resultado}");

        return resultado;
      }
      catch (Exception ex) when (ex is DivisaoPorZeroException || ex is OverflowException)
      {
        _logger.LogOperacaoErro(operacao, a, b, ex.Message);
        _logger.LogErro($"Erro na {GetNomeOperacao(operacao).ToLower()}: {ex.Message}");
        throw;
      }
      catch (Exception ex)
      {
        _logger.LogOperacaoErro(operacao, a, b, ex.Message);
        _logger.LogErro($"Erro inesperado na {GetNomeOperacao(operacao).ToLower()}: {ex.Message}");
        throw;
      }
    }

    private string GetNomeOperacao(string operacao)
    {
      return operacao switch
      {
        "+" => "Adição",
        "-" => "Subtração",
        "*" => "Multiplicação",
        "/" => "Divisão",
        _ => "Operação desconhecida"
      };
    }

    public double Somar(double a, double b)
    {
      return ExecutarOperacao("+", a, b, (x, y) => x + y);
    }

    public double Subtrair(double a, double b)
    {
      return ExecutarOperacao("-", a, b, (x, y) => x - y);
    }

    public double Multiplicar(double a, double b)
    {
      return ExecutarOperacao("*", a, b, (x, y) => x * y);
    }

    public double Dividir(double a, double b)
    {
      return ExecutarOperacao("/", a, b, (x, y) => x / y);
    }
  }
}