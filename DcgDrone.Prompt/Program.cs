using System;
using DcgDrone.Domain.Entities;

namespace DcgDrone.Prompt
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(Evaluate(null));
        }

        public static string Evaluate(string input)
        {
            var drone = new Drone(input);
            return drone.Coordenates.ToString();
        }
    }
}