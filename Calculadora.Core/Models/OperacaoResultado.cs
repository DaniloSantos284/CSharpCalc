namespace Calculadora.Core.Models
{
  public class OperacaoResultado
  {
    public bool Sucesso { get; }
    public double Valor { get; }
    public string MensagemErro { get; }
    public DateTime DataHora { get; }

    // Construtor para operação bem-sucedida
    public OperacaoResultado(double valor)
    {
      Sucesso = true;
      Valor = valor;
      MensagemErro = string.Empty;
      DataHora = DateTime.Now;
    }

    // Construtor para operação com erro
    public OperacaoResultado(string erro)
    {
      Sucesso = false;
      Valor = 0;
      MensagemErro = erro;
      DataHora = DateTime.Now;
    }
  }
}