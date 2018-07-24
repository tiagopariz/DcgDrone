using System;
using DcgDrone.Domain.ValueObjects;

namespace DcgDrone.Domain.Entities
{
    public class Drone
    {
        public Drone(string input)
        {
            Coordenates = new Coordenates(input);
        }

        public Coordenates Coordenates { get; }
    }
}
