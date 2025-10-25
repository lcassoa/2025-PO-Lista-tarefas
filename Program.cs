using System;
using System.Collections.Generic;

class Program
{
    // Criamos a instância de Operacoes aqui para ser usada por todos os métodos
    static Operacoes operacoes = new Operacoes();

    static void Main(string[] args)
    {
        while (true)
        {
            MostrarMenu();
            string opcao = Console.ReadLine() ?? "";

            switch (opcao)
            {
                case "1":
                    ListarTarefas();
                    break;
                case "2":
                    CriarTarefa();
                    break;
                case "3":
                    AlterarTarefa();
                    break;
                case "4":
                    ExcluirTarefa();
                    break;
                case "5":
                    Console.WriteLine("Saindo...");
                    return; // Encerra o programa
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
    }

    static void MostrarMenu()
    {
        Console.Clear();
        Console.WriteLine("--- GERENCIADOR DE TAREFAS ---");
        Console.WriteLine("1. Listar Tarefas");
        Console.WriteLine("2. Criar Nova Tarefa");
        Console.WriteLine("3. Alterar Tarefa");
        Console.WriteLine("4. Excluir Tarefa");
        Console.WriteLine("5. Sair");
        Console.Write("Escolha uma opção: ");
    }

    static void ListarTarefas()
    {
        Console.WriteLine("\n--- Listando todas as tarefas ---");
        try
        {
            IList<Tarefa> listaDeTarefas = operacoes.Listar();
            if (listaDeTarefas.Count > 0)
            {
                foreach (var t in listaDeTarefas)
                {
                    Console.WriteLine($"ID: {t.Id} | Nome: {t.Nome} | Descrição: {t.Descricao} | Status: {t.Status}");
                }
            }
            else
            {
                Console.WriteLine("Nenhuma tarefa encontrada.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ocorreu um erro ao listar: {ex.Message}");
        }
    }

    static void CriarTarefa()
    {
        Console.WriteLine("\n--- Criando uma nova tarefa ---");
        try
        {
            Console.Write("Digite o nome da tarefa: ");
            string nome = Console.ReadLine() ?? "";

            Console.Write("Digite a descrição: ");
            string descricao = Console.ReadLine() ?? "";

            var novaTarefa = new Tarefa
            {
                Nome = nome,
                Descricao = descricao,
                DataCriacao = DateTime.Now,
                Status = 1, // 1 = Pendente
                DataExecucao = null
            };
            
            int id = operacoes.Criar(novaTarefa);
            Console.WriteLine($"Tarefa '{nome}' criada com sucesso! ID: {id}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao criar tarefa: {ex.Message}");
        }
    }

    static void AlterarTarefa()
    {
        Console.WriteLine("\n--- Alterando uma tarefa ---");
        try
        {
            Console.Write("Digite o ID da tarefa que deseja alterar: ");
            int id = Convert.ToInt32(Console.ReadLine());

            // Aqui precisaríamos buscar a tarefa primeiro para mostrar os dados atuais,
            // mas como o método Buscar() não está pronto, vamos pedir todos os dados.

            Console.Write("Digite o NOVO nome da tarefa: ");
            string nome = Console.ReadLine() ?? "";

            Console.Write("Digite a NOVA descrição: ");
            string descricao = Console.ReadLine() ?? "";
            
            Console.Write("Digite o NOVO status (número): ");
            int status = Convert.ToInt32(Console.ReadLine());

            var tarefaAlterada = new Tarefa
            {
                Id = id,
                Nome = nome,
                Descricao = descricao,
                Status = status,
                // Poderíamos perguntar sobre a DataExecucao também
            };
            
            operacoes.Alterar(tarefaAlterada);
            Console.WriteLine($"Tarefa com ID {id} alterada com sucesso!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao alterar tarefa: {ex.Message}");
        }
    }

    static void ExcluirTarefa()
    {
        Console.WriteLine("\n--- Excluindo uma tarefa ---");
        try
        {
            Console.Write("Digite o ID da tarefa que deseja excluir: ");
            int id = Convert.ToInt32(Console.ReadLine());

            // Adicionar uma confirmação seria uma boa prática
            Console.Write($"Tem certeza que deseja excluir a tarefa com ID {id}? (s/n): ");
            string confirmacao = Console.ReadLine()?.ToLower() ?? "";

            if (confirmacao == "s")
            {
                operacoes.Excluir(id);
                Console.WriteLine($"Tarefa com ID {id} excluída com sucesso!");
            }
            else
            {
                Console.WriteLine("Operação cancelada.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao excluir tarefa: {ex.Message}");
        }
    }
}
