namespace Calculadora.Core.Models
{
  public class OperacaoResultado
  {
    public double Valor { get; }
    public bool Sucesso { get; }
    public string MensagemErro { get; }
    public DateTime DataOperacao { get; }

    // Construtor para operação bem-sucedida
    public OperacaoResultado(double valor)
    {
      Valor = valor;
      Sucesso = true;
      MensagemErro = string.Empty;
      DataOperacao = DateTime.Now;
    }

    // Construtor para operação com erro
    public OperacaoResultado(string mensagemErro)
    {
      Valor = 0;
      Sucesso = false;
      MensagemErro = mensagemErro;
      DataOperacao = DateTime.Now;
    }
  }
}