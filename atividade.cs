using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static List<string> nomes = new List<string>();
    static List<string> grupos = new List<string>();
    static List<double> cargas = new List<double>();
    static List<int> repeticoes = new List<int>();

    static void Main()
    {
        int opcao;

        do
        {
            Console.WriteLine("\n===== MENU =====");
            Console.WriteLine("1 - Adicionar exercício");
            Console.WriteLine("2 - Listar exercícios");
            Console.WriteLine("3 - Buscar exercício por nome");
            Console.WriteLine("4 - Filtrar por grupo muscular");
            Console.WriteLine("5 - Calcular carga total do treino");
            Console.WriteLine("6 - Exibir exercício mais pesado");
            Console.WriteLine("7 - Remover exercício");
            Console.WriteLine("0 - Sair");
            Console.Write("Escolha: ");

            if (!int.TryParse(Console.ReadLine(), out opcao))
            {
                Console.WriteLine("Opção inválida!");
                continue;
            }

            switch (opcao)
            {
                case 1:
                    AdicionarExercicio();
                    break;
                case 2:
                    ListarExercicios();
                    break;
                case 3:
                    BuscarExercicio();
                    break;
                case 4:
                    FiltrarGrupo();
                    break;
                case 5:
                    CalcularCargaTotal();
                    break;
                case 6:
                    ExercicioMaisPesado();
                    break;
                case 7:
                    RemoverExercicio();
                    break;
                case 0:
                    Console.WriteLine("Saindo...");
                    break;
                default:
                    Console.WriteLine("Opção inválida!");
                    break;
            }

        } while (opcao != 0);
    }

    static void AdicionarExercicio()
    {
        string nome;
        do
        {
            Console.Write("Nome do exercício: ");
            nome = Console.ReadLine();
        } while (string.IsNullOrWhiteSpace(nome));

        Console.Write("Grupo muscular: ");
        string grupo = Console.ReadLine();

        double carga;
        while (true)
        {
            Console.Write("Carga (kg): ");
            if (double.TryParse(Console.ReadLine(), out carga) && carga >= 0)
                break;
            Console.WriteLine("Valor inválido!");
        }

        int reps;
        while (true)
        {
            Console.Write("Repetições: ");
            if (int.TryParse(Console.ReadLine(), out reps) && reps >= 1)
                break;
            Console.WriteLine("Valor inválido!");
        }

        nomes.Add(nome);
        grupos.Add(grupo);
        cargas.Add(carga);
        repeticoes.Add(reps);

        Console.WriteLine("Exercício adicionado com sucesso!");
    }

    static void ListarExercicios()
    {
        if (nomes.Count == 0)
        {
            Console.WriteLine("Nenhum exercício cadastrado.");
            return;
        }

        for (int i = 0; i < nomes.Count; i++)
        {
            Console.WriteLine($"{nomes[i]} - {grupos[i]} - {cargas[i]} kg - {repeticoes[i]} reps");
        }
    }

    static void BuscarExercicio()
    {
        if (nomes.Count == 0)
        {
            Console.WriteLine("Lista vazia.");
            return;
        }

        Console.Write("Digite o nome: ");
        string busca = Console.ReadLine();

        var resultado = nomes
            .Select((nome, index) => new { nome, index })
            .FirstOrDefault(x => x.nome.Equals(busca, StringComparison.OrdinalIgnoreCase));

        if (resultado == null)
        {
            Console.WriteLine("Exercício não encontrado.");
        }
        else
        {
            int i = resultado.index;
            Console.WriteLine($"{nomes[i]} - {grupos[i]} - {cargas[i]} kg - {repeticoes[i]} reps");
        }
    }

    static void FiltrarGrupo()
    {
        if (nomes.Count == 0)
        {
            Console.WriteLine("Lista vazia.");
            return;
        }

        Console.Write("Digite o grupo muscular: ");
        string grupoBusca = Console.ReadLine();

        var filtrados = grupos
            .Select((g, i) => new { g, i })
            .Where(x => x.g.Equals(grupoBusca, StringComparison.OrdinalIgnoreCase));

        if (!filtrados.Any())
        {
            Console.WriteLine("Nenhum exercício encontrado.");
            return;
        }

        foreach (var item in filtrados)
        {
            Console.WriteLine(nomes[item.i]);
        }
    }

    static void CalcularCargaTotal()
    {
        if (cargas.Count == 0)
        {
            Console.WriteLine("Lista vazia.");
            return;
        }

        double total = cargas.Sum();
        Console.WriteLine($"Carga total: {total} kg");
    }

    static void ExercicioMaisPesado()
    {
        if (cargas.Count == 0)
        {
            Console.WriteLine("Lista vazia.");
            return;
        }

        double max = cargas.Max();
        int index = cargas.IndexOf(max);

        Console.WriteLine($"Mais pesado: {nomes[index]} - {max} kg");
    }

    static void RemoverExercicio()
    {
        if (nomes.Count == 0)
        {
            Console.WriteLine("Lista vazia.");
            return;
        }

        Console.Write("Digite o nome do exercício: ");
        string nome = Console.ReadLine();

        int index = nomes.FindIndex(n => n.Equals(nome, StringComparison.OrdinalIgnoreCase));

        if (index == -1)
        {
            Console.WriteLine("Exercício não encontrado.");
            return;
        }

        nomes.RemoveAt(index);
        grupos.RemoveAt(index);
        cargas.RemoveAt(index);
        repeticoes.RemoveAt(index);

        Console.WriteLine("Exercício removido com sucesso!");
    }
}
