using System;
using System.Collections.Generic;
using System.Linq;

public class Maquina
{
    string[] Estados { get; set; }
    string[] Alfabeto { get; set; }
    string EstadoInicial { get; set; }
    string[] EstadoFinal { get; set; }
    Dictionary<string, Dictionary<string, string>> Transicoes { get; set; }
    public void ConfigurarMaquina()
    {
        LimparConfiguracoes();
        ConfigurarEstados();
        ConfigurarEstadoInicial();
        ConfigurarEstadoFinal();
        ConfigurarAlfabeto();
        ConfigurarTransicoes();

        ImprimeConfiguracoes();

    }

    private void LimparConfiguracoes()
    {
        Estados = null;
        Alfabeto = null;
        EstadoInicial = null;
        EstadoFinal = null;
        Transicoes = null;
    }

    private void ConfigurarEstados()
    {
        Console.WriteLine("Insira separado por vírgulas os estados desejados. - Após pressione ENTER");
        string estadosLeitura = Console.ReadLine();
        Estados = estadosLeitura.Trim().Split(",");
    }
    private void ConfigurarEstadoInicial()
    {
        string leitura;
        do
        {
            Console.WriteLine("Insira qual estado será o início - Após pressione ENTER");
            leitura = Console.ReadLine();
        } while (!Estados.Contains(leitura));
        EstadoInicial = leitura;
    }
    private void ConfigurarEstadoFinal()
    {

        string[] finais;
        string final;
        do
        {
            Console.WriteLine("Insira quais estados serão o finais, separados por vírgula - Após pressione ENTER");
            final = Console.ReadLine();
            finais = final.Split(",");
            var res = Estados.Intersect(finais);
        }
        while (finais.Length != Estados.Intersect(finais).Count());
        EstadoFinal = finais;
    }
    private void ConfigurarAlfabeto()
    {
        string alfabetoLeitura;
        Console.WriteLine("Insira qual estado será o alfabeto, separado por vírgula - Após pressione ENTER");
        alfabetoLeitura = Console.ReadLine();

        Alfabeto = alfabetoLeitura.Trim().Split(",");
    }
    private void ConfigurarTransicoes()
    {
        Console.WriteLine("Nessa etapa você deve informar para cada estado o símbolo de entrada e o estado de transição. Caso tenha terminado o mapeamento ou não desejas mapear para o estado informado estado, digite proximo.");
        Dictionary<string, Dictionary<string, string>> transicoes = new Dictionary<string, Dictionary<string, string>>();
        foreach (string estado in Estados)
        {
            Dictionary<string, string> mapaEstado = new Dictionary<string, string>();
            Console.WriteLine($"Para o estado: {estado}, digite o símbolo de entrada com o estado de transição separado por vírgula.\n Após aperte enter. Ex: Letra,estado");
            string mapeamento = "";
            do
            {
                mapeamento = Console.ReadLine();
                if (!mapeamento.Contains(","))
                {
                    Console.WriteLine("Entrada não está no formato especificado.");
                    continue;
                }

                string[] vetorLeitura = mapeamento.Split(",");
                if (!Alfabeto.Contains(vetorLeitura[0]) || !Estados.Contains(vetorLeitura[1]))
                {
                    Console.WriteLine("Entrada não está no alfabeto cadastrado ou estado informado não foi cadastrado previamente. Verfique e tente novamente.");
                    continue;
                }

                mapaEstado.Add(vetorLeitura[0], vetorLeitura[1]);

            } while (!mapeamento.Equals("proximo"));
            transicoes.Add(estado, mapaEstado);
        }
        Transicoes = transicoes;
    }

    public void ImprimeConfiguracoes()
    {
        if (!EstaConfigurada())
        {
            Console.WriteLine("Máquina não configurada.");
            return;
        }

        Console.Write("Estados: ");
        foreach (var v in Estados)
        {
            Console.Write(v + " - ");
        }
        Console.WriteLine("");
        Console.Write("Alfabeto: ");
        foreach (var v in Alfabeto)
        {
            Console.Write(v + " - ");
        }
        Console.WriteLine("");

        Console.WriteLine("Estado Inicial: " + EstadoInicial);
        Console.Write("Estados finais: ");
        foreach (var v in EstadoFinal)
        {
            Console.Write(v + " - ");
        }
        Console.WriteLine("");

        Console.WriteLine("Regras de transição: ");

        foreach (KeyValuePair<string, Dictionary<string, string>> entry in Transicoes)
        {
            Console.Write("Estado: " + entry.Key + " ");
            foreach (KeyValuePair<string, string> entryDict in entry.Value)
            {
                Console.Write(" Aceita: " + entryDict.Key + " Vai para: " + entryDict.Value ?? "");
            }
            Console.WriteLine("");
        }
    }
    public Maquina() { }
    public Maquina(string[] estados, string[] alfabeto, string estadoInicial, string[] estadoFinal, Dictionary<string, Dictionary<string, string>> transicoes)
    {
        Estados = estados;
        Alfabeto = alfabeto;
        EstadoInicial = estadoInicial;
        EstadoFinal = estadoFinal;
        Transicoes = transicoes;
    }
    public bool EstaConfigurada()
    {
        return !(Estados == null || EstadoFinal == null || EstadoInicial == null || Transicoes == null || Alfabeto == null);
    }
    public bool ValidaEntrada(string sentenca)
    {
        Console.WriteLine("Validando Entrada da sentença: " + sentenca);
        string estadoApontado = EstadoInicial;
        for (int i = 0; i < sentenca.Length; i++)
        {
            Console.WriteLine();
            Console.WriteLine("Está no estado: " + estadoApontado + " - Lendo:" + sentenca[i]);

            if (Transicoes[estadoApontado].ContainsKey(sentenca[i].ToString()))
            {
                estadoApontado = Transicoes[estadoApontado][sentenca[i].ToString()];
                Console.WriteLine("Troca para apontar para o estado: " + estadoApontado);
            }
            else
            {
                Console.WriteLine("Entrada não válida!");
                return false;
            }
        }

        if (EstadoFinal.Contains(estadoApontado))
        {
            Console.WriteLine("Entrada Válida.");
            return true;
        }
        else
        {
            Console.WriteLine("Entrada inválida.");
            return false;
        }
    }

    public void ModoValidacao()
    {
        if (!EstaConfigurada())
        {
            Console.WriteLine("Máquina não configurada.");
            return;
        }
        string sentenca = "";
        do
        {
            Console.Write("Digite a sentença que desejas validar e aperte ENTER. Para sair, digite sair.");
            sentenca = Console.ReadLine();
            if (sentenca.Equals("sair"))
                continue;
            var retorno = ValidaEntrada(sentenca);
            Console.WriteLine("Sua sentença é " + (retorno ? "Válida" : "Inválida"));
        } while (!sentenca.Equals("sair"));
    }
}