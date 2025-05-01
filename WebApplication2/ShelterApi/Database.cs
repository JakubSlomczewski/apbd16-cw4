using System;
using System.Collections.Generic;
using ShelterApi.Models;

namespace ShelterApi
{
    public static class Database
    {
        public static List<Animal> Animals { get; } = new()
        {
            new Animal { Id=1, Name="Reksio", Category="dog", Weight=10.5, FurColor="brown" },
            new Animal { Id=2, Name="Mruczek", Category="cat", Weight=4.2, FurColor="black" }
        };

        public static List<Visit> Visits { get; } = new()
        {
            new Visit { Id=1, AnimalId=1, Date=DateTime.Parse("2025-04-28"), Description="Szczepienie", Price=150m }
        };
    }
}