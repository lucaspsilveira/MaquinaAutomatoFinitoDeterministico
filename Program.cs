using System;
using System.Collections.Generic;
using System.Linq;

Console.WriteLine("Bem vindo - Máquina Autômato Finito Determinístico!");
Maquina maquina = new Maquina();
int opcao = 0;
do
{
    Console.WriteLine("==================================");
    Console.WriteLine("==            MENU              ==");
    Console.WriteLine("==================================");
    Console.WriteLine("Selecione:");
    Console.WriteLine("1 - Configurar Máquina");
    Console.WriteLine("2 - Dados da máquina atual");
    Console.WriteLine("3 - Validar Sentenças.");
    Console.WriteLine("4 - Sair.");

    if (!int.TryParse(Console.ReadLine(), out opcao))
        continue;

    switch (opcao)
    {
        case 1:
            maquina.ConfigurarMaquina();
            break;
        case 2:
            maquina.ImprimeConfiguracoes();
            break;
        case 3:
            maquina.ModoValidacao();
            break;
        case 4:
            Console.Write("Obrigado por utilizar nosso programa!");
            break;
        default:
            Console.WriteLine("Entrada inválida. Seleciona alguma opção do menu.");
            break;
    }
} while (opcao != 4);



Console.WriteLine("That's all folks.");


