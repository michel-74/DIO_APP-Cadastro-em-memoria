using System;
using System.Linq;
using System.IO;

namespace CatalogoPessoal
{
    class Program
    {
        static SerieRepositorio repositorio = new SerieRepositorio();
        static FilmeRepositorio repositorioFilme = new FilmeRepositorio();
        static LivroRepositorio repositorioLivro = new LivroRepositorio();
        static void Main(string[] args)
        {
            // Configuração de cores para funcionalidade do Terminal
            // Homenagem à LocalizaLabs - "Coração Verde"
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();

            // Menu Principal.
            string opcaoPrincipal = ObterOpcaoPrincipal();
            
            while (opcaoPrincipal.ToUpper() != "X")
            {
                switch (opcaoPrincipal)
                {
                    case "1":
                        ObterOpcaoModoSerie();
                        break;
                    case "2":
                        ObterOpcaoModoFilme();
                        break;
                    case "3":
                        ObterOpcaoModoLivro();
                        break;
                    case "C":
                        Console.Clear();
                        break;

                    default:
                        {
                            if (opcaoPrincipal.ToUpper() !="1" && opcaoPrincipal.ToUpper() !="2" && opcaoPrincipal.ToUpper() !="3" && opcaoPrincipal.ToUpper() !="C")
                            {
                                System.Console.WriteLine("Opção inválida, favor entrar com outra opção.");
                                Console.ReadLine();
                            }
                            break;
                        }

                }

                opcaoPrincipal = ObterOpcaoPrincipal();
            }

            System.Console.WriteLine("Obrigado por utilizar nossos serviços.");
            Console.ReadLine();
            Console.ResetColor();
            Console.Clear();            
        }   // Final do Programa Principal.
        // Início Rotina Menu Inicial.
        private static string ObterOpcaoPrincipal()
        {
            System.Console.WriteLine();
            System.Console.WriteLine("Aplicativo de Cadastro de Multimídia Pessoal!!!");
            System.Console.WriteLine("Informe a opção desejada");
            System.Console.WriteLine("1- Módulo de Cadastro de Séries");
            System.Console.WriteLine("2- Módulo de Cadastro de Filmes");
            System.Console.WriteLine("3- Módulo de Cadastro de Livros");
            System.Console.WriteLine("C- Limpar Tela");
            System.Console.WriteLine("X- Sair do Aplicativo");

            string opcaoPrincipal = Console.ReadLine().ToUpper();
            System.Console.WriteLine();
            return opcaoPrincipal;
        }   // Final da Rotina Menu Inicial.

        // Início Rotina do Modo de Série.
        private static string ObterOpcaoModoSerie()
        {
            System.Console.WriteLine();
            System.Console.WriteLine("Catálogo de Séries Pessoais!!!");
            System.Console.WriteLine("Informe a opção desejada");
            System.Console.WriteLine("1- Listar séries");
            System.Console.WriteLine("2- Inserir nova série");
            System.Console.WriteLine("3- Atualizar série");
            System.Console.WriteLine("4- Excluir série");
            System.Console.WriteLine("5- Visualizar série");
            System.Console.WriteLine("C- Limpar Tela");
            System.Console.WriteLine("X- Voltar ao início");

            string opcaoSerie = Console.ReadLine().ToUpper();
            System.Console.WriteLine();
            while (opcaoSerie.ToUpper() != "X")
            {
                switch (opcaoSerie)
                {
                    case "1":
                        ListarSeries();
                        break;
                    case "2":
                        InserirSerie();
                        break;
                    case "3":
                        AtualizarSerie();
                        break;
                    case "4":
                        ExcluirSerie();
                        break;
                    case "5":
                        VisualizarSerie();
                        break;
                    case "C":
                        Console.Clear();
                        break;

                    default:
                        {
                            if (opcaoSerie.ToUpper() !="1" && opcaoSerie.ToUpper() !="2" && opcaoSerie.ToUpper() !="3" && opcaoSerie.ToUpper() !="4" && opcaoSerie.ToUpper() !="5" && opcaoSerie.ToUpper() !="C")
                            {
                                System.Console.WriteLine("Opção inválida, favor entrar com outra opção.");
                                Console.ReadLine();
                            }
                            break;
                        }
                }

                opcaoSerie = ObterOpcaoModoSerie();
            }
            Console.Clear();
            return opcaoSerie;
        }

        private static void ListarSeries()
        {
            System.Console.WriteLine("Listar séries");

            var lista = repositorio.Lista();

            if (lista.Count == 0)
            {
                System.Console.WriteLine("Nenhuma série cadastrada.");
                return;
            }
            foreach (var serie in lista)
            {
                var Excluido = serie.retornaExcluido();

                System.Console.WriteLine("#ID: {0}  -  {1}  {2}",serie.retornaId(), serie.retornaTitulo(), (Excluido ? "-  *Excluído*" : ""));
            }
        }

        private static void InserirSerie()
        {
            System.Console.WriteLine("Inserir nova série");

            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                System.Console.WriteLine("{0} - {1}", i, Enum.GetName(typeof(Genero), i));
            }
            Console.WriteLine("Digite o gênero entre as opções acima: ");
            int entradaGenero = int.Parse(Console.ReadLine());

            Console.WriteLine("Digite o Título da Série: ");
            string entradaTitulo = Console.ReadLine();

            Console.WriteLine("Digite o Ano de início da Série: ");
            int entradaAno = int.Parse(Console.ReadLine());

            Console.WriteLine("Digite a Descrição da Série: ");
            string entradaDescricao = Console.ReadLine();

            Serie novaSerie = new Serie(id: repositorio.ProximoId(),
                                        genero: (Genero)entradaGenero,
                                        titulo: entradaTitulo,
                                        ano: entradaAno,
                                        descricao: entradaDescricao);

            repositorio.Insere(novaSerie);
        }

        private static void AtualizarSerie()
        {
            System.Console.WriteLine("Digite o Id da série a ser atualizada: ");
            int indiceSerie = int.Parse(Console.ReadLine());

            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                System.Console.WriteLine("{0} - {1}", i, Enum.GetName(typeof(Genero), i));
            }
            Console.WriteLine("Digite o gênero entre as opções acima: ");
            int entradaGenero = int.Parse(Console.ReadLine());

            Console.WriteLine("Digite o Título da Série: ");
            string entradaTitulo = Console.ReadLine();

            Console.WriteLine("Digite o Ano de início da Série: ");
            int entradaAno = int.Parse(Console.ReadLine());

            Console.WriteLine("Digite a Descrição da Série: ");
            string entradaDescricao = Console.ReadLine();

            Serie atualizaSerie = new Serie(id: indiceSerie,
                                        genero: (Genero)entradaGenero,
                                        titulo: entradaTitulo,
                                        ano: entradaAno,
                                        descricao: entradaDescricao);

            repositorio.Atualiza(indiceSerie, atualizaSerie);
        }

        private static void ExcluirSerie()
        {
            System.Console.Write("Digite o Id da série a ser excluída: ");
            int indiceSerie = int.Parse(Console.ReadLine());

            Console.Clear();
            System.Console.WriteLine("**************************************************");
            System.Console.WriteLine("**                                              **");
            System.Console.WriteLine("**   !!! ALERTA DE EXCLUSÃO DE REGISTRO  !!!    **");
            System.Console.WriteLine("**                                              **");
            System.Console.WriteLine("** Deseja realmente excluir o registro abaixo:  **");
            System.Console.WriteLine("**                                              **");
            System.Console.WriteLine("**************************************************");

            var serie = repositorio.RetornaPorId(indiceSerie);

            Console.WriteLine(serie);

            System.Console.WriteLine("");
            System.Console.WriteLine("Digite 1 - Para Excluir o registro");
            System.Console.WriteLine("Digite 2 = Para Abortar a Exclusão");
            int confirma = int.Parse(Console.ReadLine());

            if (confirma == 1)
            {
                repositorio.Exclui(indiceSerie);
                System.Console.WriteLine("Série Excluída com sucesso!!!");
            }           
        }

        private static void VisualizarSerie()
        {
            System.Console.Write("Digite o Id da série a ser visualizada: ");
            int indiceSerie = int.Parse(Console.ReadLine());

            var serie = repositorio.RetornaPorId(indiceSerie);

            Console.WriteLine(serie);
            
        } // Final Rotina do Modo de Série.


        // Início Rotina do Modo de Filme.
        private static string ObterOpcaoModoFilme()
        {
            System.Console.WriteLine();
            System.Console.WriteLine("Catálogo de Filmes Pessoais!!!");
            System.Console.WriteLine("Informe a opção desejada");
            System.Console.WriteLine("1- Listar filmes");
            System.Console.WriteLine("2- Inserir novo filme");
            System.Console.WriteLine("3- Atualizar filme");
            System.Console.WriteLine("4- Excluir filme");
            System.Console.WriteLine("5- Visualizar filme");
            System.Console.WriteLine("C- Limpar Tela");
            System.Console.WriteLine("X- Voltar ao início");

            string opcaoFilme = Console.ReadLine().ToUpper();
            System.Console.WriteLine();
            while (opcaoFilme.ToUpper() != "X")
            {
                switch (opcaoFilme)
                {
                    case "1":
                        ListarFilmes();
                        break;
                    case "2":
                        InserirFilme();
                        break;
                    case "3":
                        AtualizarFilme();
                        break;
                    case "4":
                        ExcluirFilme();
                        break;
                    case "5":
                        VisualizarFilme();
                        break;
                    case "C":
                        Console.Clear();
                        break;

                    default:
                        {
                            if (opcaoFilme.ToUpper() !="1" && opcaoFilme.ToUpper() !="2" && opcaoFilme.ToUpper() !="3" && opcaoFilme.ToUpper() !="4" && opcaoFilme.ToUpper() !="5" && opcaoFilme.ToUpper() !="C")
                            {
                                System.Console.WriteLine("Opção inválida, favor entrar com outra opção.");
                                Console.ReadLine();
                            }
                            break;
                        }
                }

                opcaoFilme = ObterOpcaoModoFilme();
            }
            Console.Clear();
            return opcaoFilme;
        }

        private static void ListarFilmes()
        {
            System.Console.WriteLine("Listar filmes");

            var lista = repositorioFilme.Lista();

            if (lista.Count == 0)
            {
                System.Console.WriteLine("Nenhum filme cadastrado.");
                return;
            }
            foreach (var filme in lista)
            {
                var Excluido = filme.retornaExcluido();

                System.Console.WriteLine("#ID: {0}  -  {1}  {2}",filme.retornaId(), filme.retornaTitulo(), (Excluido ? "-  *Excluído*" : ""));
            }
        }

        private static void InserirFilme()
        {
            System.Console.WriteLine("Inserir novo filme");

            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                System.Console.WriteLine("{0} - {1}", i, Enum.GetName(typeof(Genero), i));
            }
            Console.WriteLine("Digite o gênero entre as opções acima: ");
            int entradaGenero = int.Parse(Console.ReadLine());

            Console.WriteLine("Digite o Título do Filme: ");
            string entradaTitulo = Console.ReadLine();

            Console.WriteLine("Digite o Ano de início do Filme: ");
            int entradaAno = int.Parse(Console.ReadLine());

            Console.WriteLine("Digite a Descrição do Filme: ");
            string entradaDescricao = Console.ReadLine();

            Filme novoFilme = new Filme(id: repositorioFilme.ProximoId(),
                                        genero: (Genero)entradaGenero,
                                        titulo: entradaTitulo,
                                        ano: entradaAno,
                                        descricao: entradaDescricao);

            repositorioFilme.Insere(novoFilme);
        }

        private static void AtualizarFilme()
        {
            System.Console.WriteLine("Digite o Id do filme a ser atualizado: ");
            int indiceFilme = int.Parse(Console.ReadLine());

            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                System.Console.WriteLine("{0} - {1}", i, Enum.GetName(typeof(Genero), i));
            }
            Console.WriteLine("Digite o gênero entre as opções acima: ");
            int entradaGenero = int.Parse(Console.ReadLine());

            Console.WriteLine("Digite o Título do Filme: ");
            string entradaTitulo = Console.ReadLine();

            Console.WriteLine("Digite o Ano de início do Filme: ");
            int entradaAno = int.Parse(Console.ReadLine());

            Console.WriteLine("Digite a Descrição do Filme: ");
            string entradaDescricao = Console.ReadLine();

            Filme atualizaFilme = new Filme(id: indiceFilme,
                                        genero: (Genero)entradaGenero,
                                        titulo: entradaTitulo,
                                        ano: entradaAno,
                                        descricao: entradaDescricao);

            repositorioFilme.Atualiza(indiceFilme, atualizaFilme);
        }

        private static void ExcluirFilme()
        {
            System.Console.Write("Digite o Id do filme a ser excluído: ");
            int indiceFilme = int.Parse(Console.ReadLine());

            Console.Clear();
            System.Console.WriteLine("**************************************************");
            System.Console.WriteLine("**                                              **");
            System.Console.WriteLine("**   !!! ALERTA DE EXCLUSÃO DE REGISTRO  !!!    **");
            System.Console.WriteLine("**                                              **");
            System.Console.WriteLine("** Deseja realmente excluir o registro abaixo:  **");
            System.Console.WriteLine("**                                              **");
            System.Console.WriteLine("**************************************************");

            var filme = repositorioFilme.RetornaPorId(indiceFilme);

            Console.WriteLine(filme);

            System.Console.WriteLine("");
            System.Console.WriteLine("Digite 1 - Para Excluir o registro");
            System.Console.WriteLine("Digite 2 = Para Abortar a Exclusão");
            int confirma = int.Parse(Console.ReadLine());

            if (confirma == 1)
            {
                repositorio.Exclui(indiceFilme);
                System.Console.WriteLine("Filme Excluído com sucesso!!!");
            }           
        }

        private static void VisualizarFilme()
        {
            System.Console.Write("Digite o Id do Filme a ser visualizado: ");
            int indiceFilme = int.Parse(Console.ReadLine());

            var filme = repositorioFilme.RetornaPorId(indiceFilme);

            Console.WriteLine(filme);
            
        } // Final Rotina do Modo de Filme.

        // Início Rotina do Modo de Livro.
        private static string ObterOpcaoModoLivro()
        {
            System.Console.WriteLine();
            System.Console.WriteLine("Catálogo de Livros Pessoais!!!");
            System.Console.WriteLine("Informe a opção desejada");
            System.Console.WriteLine("1- Listar livros");
            System.Console.WriteLine("2- Inserir novo livro");
            System.Console.WriteLine("3- Atualizar livro");
            System.Console.WriteLine("4- Excluir livro");
            System.Console.WriteLine("5- Visualizar livro");
            System.Console.WriteLine("C- Limpar Tela");
            System.Console.WriteLine("X- Voltar ao início");

            string opcaoLivro = Console.ReadLine().ToUpper();
            System.Console.WriteLine();
            while (opcaoLivro.ToUpper() != "X")
            {
                switch (opcaoLivro)
                {
                    case "1":
                        ListarLivros();
                        break;
                    case "2":
                        InserirLivro();
                        break;
                    case "3":
                        AtualizarLivro();
                        break;
                    case "4":
                        ExcluirLivro();
                        break;
                    case "5":
                        VisualizarLivro();
                        break;
                    case "C":
                        Console.Clear();
                        break;

                    default:
                        {
                            if (opcaoLivro.ToUpper() !="1" && opcaoLivro.ToUpper() !="2" && opcaoLivro.ToUpper() !="3" && opcaoLivro.ToUpper() !="4" && opcaoLivro.ToUpper() !="5" && opcaoLivro.ToUpper() !="C")
                            {
                                System.Console.WriteLine("Opção inválida, favor entrar com outra opção.");
                                Console.ReadLine();
                            }
                            break;
                        }
                }

                opcaoLivro = ObterOpcaoModoLivro();
            }
            Console.Clear();
            return opcaoLivro;
        }

        private static void ListarLivros()
        {
            System.Console.WriteLine("Listar livros");

            var lista = repositorioLivro.Lista();

            if (lista.Count == 0)
            {
                System.Console.WriteLine("Nenhum livro cadastrado.");
                return;
            }
            foreach (var livro in lista)
            {
                var Excluido = livro.retornaExcluido();

                System.Console.WriteLine("#ID: {0}  -  {1}  {2}",livro.retornaId(), livro.retornaTitulo(), (Excluido ? "-  *Excluído*" : ""));
            }
        }

        private static void InserirLivro()
        {
            System.Console.WriteLine("Inserir novo livro");

            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                System.Console.WriteLine("{0} - {1}", i, Enum.GetName(typeof(Genero), i));
            }
            Console.WriteLine("Digite o gênero entre as opções acima: ");
            int entradaGenero = int.Parse(Console.ReadLine());

            Console.WriteLine("Digite o Título do Livro: ");
            string entradaTitulo = Console.ReadLine();

            Console.WriteLine("Digite o Ano de início do Livro: ");
            int entradaAno = int.Parse(Console.ReadLine());

            Console.WriteLine("Digite a Descrição do Livro: ");
            string entradaDescricao = Console.ReadLine();

            Livro novoLivro = new Livro(id: repositorioLivro.ProximoId(),
                                        genero: (Genero)entradaGenero,
                                        titulo: entradaTitulo,
                                        ano: entradaAno,
                                        descricao: entradaDescricao);

            repositorioLivro.Insere(novoLivro);
        }

        private static void AtualizarLivro()
        {
            System.Console.WriteLine("Digite o Id do filme a ser atualizado: ");
            int indiceLivro = int.Parse(Console.ReadLine());

            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                System.Console.WriteLine("{0} - {1}", i, Enum.GetName(typeof(Genero), i));
            }
            Console.WriteLine("Digite o gênero entre as opções acima: ");
            int entradaGenero = int.Parse(Console.ReadLine());

            Console.WriteLine("Digite o Título do Livro: ");
            string entradaTitulo = Console.ReadLine();

            Console.WriteLine("Digite o Ano de início do Livro: ");
            int entradaAno = int.Parse(Console.ReadLine());

            Console.WriteLine("Digite a Descrição do Livro: ");
            string entradaDescricao = Console.ReadLine();

            Livro atualizaLivro = new Livro(id: indiceLivro,
                                        genero: (Genero)entradaGenero,
                                        titulo: entradaTitulo,
                                        ano: entradaAno,
                                        descricao: entradaDescricao);

            repositorioLivro.Atualiza(indiceLivro, atualizaLivro);
        }

        private static void ExcluirLivro()
        {
            System.Console.Write("Digite o Id do livro a ser excluído: ");
            int indiceLivro = int.Parse(Console.ReadLine());

            Console.Clear();
            System.Console.WriteLine("**************************************************");
            System.Console.WriteLine("**                                              **");
            System.Console.WriteLine("**   !!! ALERTA DE EXCLUSÃO DE REGISTRO  !!!    **");
            System.Console.WriteLine("**                                              **");
            System.Console.WriteLine("** Deseja realmente excluir o registro abaixo:  **");
            System.Console.WriteLine("**                                              **");
            System.Console.WriteLine("**************************************************");

            var livro = repositorioLivro.RetornaPorId(indiceLivro);

            Console.WriteLine(livro);

            System.Console.WriteLine("");
            System.Console.WriteLine("Digite 1 - Para Excluir o registro");
            System.Console.WriteLine("Digite 2 = Para Abortar a Exclusão");
            int confirma = int.Parse(Console.ReadLine());

            if (confirma == 1)
            {
                repositorioLivro.Exclui(indiceLivro);
                System.Console.WriteLine("Livro Excluído com sucesso!!!");
            }           
        }

        private static void VisualizarLivro()
        {
            System.Console.Write("Digite o Id do Livro a ser visualizado: ");
            int indiceLivro = int.Parse(Console.ReadLine());

            var livro = repositorioLivro.RetornaPorId(indiceLivro);

            Console.WriteLine(livro);
            
        } // Final Rotina do Modo de Livro.
    }
}
