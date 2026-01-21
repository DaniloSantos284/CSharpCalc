using System.Globalization;
using Calculadora.Core.Services;
using Calculadora.Core.Interfaces;
using Calculadora.Core.Exceptions;

namespace Calculadora.App
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.OutputEncoding = System.Text.Encoding.UTF8;
      Console.Title = "Calculadora Avançada";

      // Configuração
      ILogger logger = new ConsoleLogger();
      ICalculadoraValidator validator = new CalculadoraValidator();
      ICalculadoraService calculadora = new CalculadoraService(logger, validator);

      ExibirTituloAnimado();

      bool executando = true;
      int operacoesRealizadas = 0;

      while (executando)
      {
        Console.Clear();
        ExibirCabecalho(operacoesRealizadas);
        ExibirMenu();

        string? opcao = LerOpcaoMenu();

        switch (opcao)
        {
          case "0":
            executando = false;
            ExibirMensagemDespedida(operacoesRealizadas);
            break;

          case "1":
          case "2":
          case "3":
          case "4":
            Console.Clear();
            ExibirCabecalho(operacoesRealizadas);
            ProcessarOperacao(calculadora, opcao, ref operacoesRealizadas);
            break;

          case "5":
            Console.Clear();
            ExibirCabecalho(operacoesRealizadas);
            ExibirCreditos();
            AguardarContinuar();
            break;

          default:
            ExibirMensagemErro("Opção inválida! Escolha entre 0 e 5.");
            AguardarContinuar();
            break;
        }
      }
    }

    static void ExibirTituloAnimado()
    {
      Console.ForegroundColor = ConsoleColor.Cyan;
      string[] frames = { "Iniciando calculadora...", "Preparando interface...", "Tudo pronto!" };

      foreach (var frame in frames)
      {
        Console.Write($"\r{frame}");
        Thread.Sleep(500);
      }
      Console.WriteLine("\n");
      Console.ResetColor();
    }

    static void ExibirCabecalho(int totalOperacoes)
    {
      Console.ForegroundColor = ConsoleColor.Magenta;
      Console.WriteLine("╔══════════════════════════════════════════════════════╗");
      Console.WriteLine("║                    CALCULADORA .NET                  ║");
      Console.WriteLine("╠══════════════════════════════════════════════════════╣");
      Console.WriteLine($"║  Operações realizadas: {totalOperacoes,-4}           📅 {DateTime.Now:dd/MM/yyyy}  ║");
      Console.WriteLine("╚══════════════════════════════════════════════════════╝");
      Console.ResetColor();
      Console.WriteLine();
    }

    static void ExibirMenu()
    {
      Console.ForegroundColor = ConsoleColor.Yellow;
      Console.WriteLine("📋 MENU PRINCIPAL");
      Console.WriteLine(new string('─', 50));
      Console.ResetColor();

      Console.WriteLine("┌──────────────────────────────────────────────────┐");
      Console.WriteLine("│  1.  +   Somar                                   │");
      Console.WriteLine("│  2.  -   Subtrair                                │");
      Console.WriteLine("│  3.  X   Multiplicar                             │");
      Console.WriteLine("│  4.  /   Dividir                                 │");
      Console.WriteLine("│  5.  ℹ️   Informações                             │");
      Console.WriteLine("│  0.  🚪  Sair                                    │");
      Console.WriteLine("└──────────────────────────────────────────────────┘");
      Console.WriteLine();
    }

    static string? LerOpcaoMenu()
    {
      Console.ForegroundColor = ConsoleColor.Cyan;
      Console.Write("👉 Selecione uma opção (0-5): ");
      Console.ResetColor();

      string? opcao = Console.ReadLine();
      Console.WriteLine();
      return opcao;
    }

    static void ProcessarOperacao(ICalculadoraService calculadora, string opcao, ref int contador)
    {
      try
      {
        string simbolo = opcao switch { "1" => "+", "2" => "-", "3" => "*", "4" => "/", _ => "?" };
        string nomeOp = opcao switch { "1" => "SOMA", "2" => "SUBTRAÇÃO", "3" => "MULTIPLICAÇÃO", "4" => "DIVISÃO", _ => "OPERAÇÃO" };

        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine($"{nomeOp}");
        Console.WriteLine(new string('═', 50));
        Console.ResetColor();

        double a = LerNumeroComEstilo("Primeiro número");
        double b = LerNumeroComEstilo("Segundo número");

        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine($"Operação: {FormatarNumero(a)} {simbolo} {FormatarNumero(b)}");
        Console.WriteLine(new string('─', 50));
        Console.ResetColor();

        double resultado = opcao switch
        {
          "1" => calculadora.Somar(a, b),
          "2" => calculadora.Subtrair(a, b),
          "3" => calculadora.Multiplicar(a, b),
          "4" => calculadora.Dividir(a, b),
          _ => throw new InvalidOperationException()
        };

        ExibirResultadoComAnimacao(resultado);
        contador++;

        Console.WriteLine();
        ExibirMensagemSucesso("Operação concluída com sucesso! ✅");
      }
      catch (DivisaoPorZeroException ex)
      {
        ExibirMensagemErro($"{ex.Message}");
      }
      catch (OverflowException ex)
      {
        ExibirMensagemErro($"{ex.Message}");
      }
      catch (ArgumentException ex)
      {
        ExibirMensagemErro($"{ex.Message}");
      }
      catch (Exception ex)
      {
        ExibirMensagemErro($"Erro inesperado: {ex.Message}");
      }

      AguardarContinuar();
    }

    static double LerNumeroComEstilo(string label)
    {
      while (true)
      {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write($"{label}: ");
        Console.ResetColor();

        string? input = Console.ReadLine();

        if (double.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out double numero))
        {
          return numero;
        }

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("     Valor inválido! Digite um número (ex: 10, 3.14, -5)");
        Console.ResetColor();
      }
    }

    static string FormatarNumero(double numero)
    {
      // Formata números muito grandes ou muito pequenos
      if (Math.Abs(numero) > 1e10 || (Math.Abs(numero) < 1e-10 && numero != 0))
      {
        return numero.ToString("0.###e+0", CultureInfo.InvariantCulture);
      }

      // Formata números com muitas casas decimais
      string formatted = numero.ToString("0.################", CultureInfo.InvariantCulture);

      // Remove zeros desnecessários no final
      if (formatted.Contains('.'))
      {
        formatted = formatted.TrimEnd('0').TrimEnd('.');
      }

      return formatted.Length == 0 ? "0" : formatted;
    }

    static void ExibirResultadoComAnimacao(double resultado)
    {
      Console.ForegroundColor = ConsoleColor.Yellow;

      // Animação simples
      string[] loading = { "Calculando", "Calculando.", "Calculando..", "Calculando..." };
      foreach (var frame in loading)
      {
        Console.Write($"\r{frame}");
        Thread.Sleep(150);
      }

      Console.ResetColor();
      Console.WriteLine("\n");

      // Exibe o resultado com destaque
      Console.ForegroundColor = ConsoleColor.Green;
      Console.WriteLine("┌──────────────────────────────────────────────────┐");
      Console.WriteLine("│                    RESULTADO                    │");
      Console.WriteLine("├──────────────────────────────────────────────────┤");
      Console.WriteLine($"│     Valor: {FormatarNumero(resultado),-36} │");
      Console.WriteLine($"│     Notação: {resultado,-34:e} │");
      Console.WriteLine($"│     Tipo: {resultado.GetType().Name,-37} │");
      Console.WriteLine("└──────────────────────────────────────────────────┘");
      Console.ResetColor();
    }

    static void ExibirCreditos()
    {
      Console.ForegroundColor = ConsoleColor.Cyan;
      Console.WriteLine("INFORMAÇÕES DO PROJETO");
      Console.WriteLine(new string('═', 50));
      Console.ResetColor();

      Console.WriteLine("┌──────────────────────────────────────────────────┐");
      Console.WriteLine("│      Calculadora .NET                            │");
      Console.WriteLine("│      Desenvolvido com C#                         │");
      Console.WriteLine("│      Desenvolvida por: DaniloSantos284           │");
      Console.WriteLine("│                                                  │");
      Console.WriteLine("│     Funcionalidades:                             │");
      Console.WriteLine("│     • 4 operações básicas                        │");
      Console.WriteLine("│     • Validação robusta                          │");
      Console.WriteLine("│     • Sistema de logging                         │");
      Console.WriteLine("│     • Tratamento de erros                        │");
      Console.WriteLine("│     • Interface colorida                         │");
      Console.WriteLine("│                                                  │");
      Console.WriteLine("│     Boas práticas aplicadas:                     │");
      Console.WriteLine("│     • Injeção de dependência                     │");
      Console.WriteLine("│     • Princípios SOLID                           │");
      Console.WriteLine("│     • Tratamento de exceções                     │");
      Console.WriteLine("└──────────────────────────────────────────────────┘");
      Console.WriteLine();
    }

    static void ExibirMensagemSucesso(string mensagem)
    {
      Console.ForegroundColor = ConsoleColor.Green;
      Console.WriteLine($"{mensagem}");
      Console.ResetColor();
    }

    static void ExibirMensagemErro(string mensagem)
    {
      Console.ForegroundColor = ConsoleColor.Red;
      Console.WriteLine($"{mensagem}");
      Console.ResetColor();
    }

    static void ExibirMensagemDespedida(int totalOperacoes)
    {
      Console.Clear();
      Console.ForegroundColor = ConsoleColor.Magenta;

      string[] goodbye = {
                "╔══════════════════════════════════════════════════════╗",
                "║                    ATÉ LOGO!                      ║",
                "╠══════════════════════════════════════════════════════╣",
                $"║    Total de operações realizadas: {totalOperacoes,-3}        ║",
                $"║    Sessão: {DateTime.Now:HH:mm:ss}                           ║",
                "║                                                      ║",
                "║    Obrigado por usar a Calculadora .NET! 👋         ║",
                "║    Pressione qualquer tecla para sair...             ║",
                "╚══════════════════════════════════════════════════════╝"
            };

      foreach (var line in goodbye)
      {
        Console.WriteLine(line);
      }

      Console.ResetColor();
      Console.ReadKey();
    }

    static void AguardarContinuar()
    {
      Console.WriteLine();
      Console.ForegroundColor = ConsoleColor.DarkGray;
      Console.Write("Pressione ENTER para continuar...");
      Console.ResetColor();
      Console.ReadLine();
    }
  }
}