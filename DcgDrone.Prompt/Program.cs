using System;
using DcgDrone.Domain.Entities;

namespace DcgDrone.Prompt
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Ponto inicial: (0, 0)\n");
            Console.WriteLine("USE\n");
            Console.WriteLine("   N: para mover para o Norte");
            Console.WriteLine("   L: para mover para o Leste");
            Console.WriteLine("   S: para mover para o Sul");
            Console.WriteLine("   O: para mover para o Oeste");
            Console.WriteLine("\nMOVENDO\n");
            Console.WriteLine("   Repita as letras ou use um número informando quanto o drone deve se mover");
            Console.WriteLine("\nDESFAZENDO MOVIMENTOS\n");
            Console.WriteLine("   Use o X para desfazer passos das coordenadas");
            Console.WriteLine("\nCOMANDOS INVÁLIDOS\n");
            Console.WriteLine("   Comandos inválidos serão considerados como (999, 999)");
            Console.WriteLine("\nDIGITE AS COORDENADAS\n");
            Console.WriteLine("   Ou Samples para ver exemplos");

            Console.Write("\nComando: ");
            var input = Console.ReadLine();

            while (input == "Samples")
            {
                Samples();
                Console.Write("\nComando: ");
                input = Console.ReadLine();
            }

            Console.WriteLine(Evaluate(input));

            Console.ReadKey();
        }

        public static string Evaluate(string input)
        {
            var drone = new Drone(input);
            return drone.Coordenates.ToString();
        }

        private static void Samples()
        {
           Console.WriteLine("\nEXEMPLOS\n");
           Console.WriteLine("   (5, 5) = NNNNNLLLLL");
           Console.WriteLine("   (5, 5) = NLNLNLNLNL");     
           Console.WriteLine("   (4, 4) = NNNNNXLLLLLX");
           Console.WriteLine("   (-5, -5) = SSSSSOOOOO");
           Console.WriteLine("   (-5, -5) = S5O5");
           Console.WriteLine("   (999, 999) = NNX2");
           Console.WriteLine("   (1, 123) = N123LSX");
           Console.WriteLine("   (1, 1) = NLS3X");
           Console.WriteLine("   (1, 2) = NNNXLLLXX");
           Console.WriteLine("   (21, 21) = N40L30S20O10NLSOXX");
           Console.WriteLine("   (21, 21) = NLSOXXN40L30S20O10");
           Console.WriteLine("   (0, 1) = N2147483647XN");
           Console.WriteLine("");
        }
    }
}