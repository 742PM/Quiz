using System;
using System.Collections.Generic;
using System.Linq;
using DataBase.Entities;
using Domain.Entities;
using Domain.Entities.TaskGenerators;
using MongoDB.Driver;

namespace DataBase
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var filler = new DatabaseFiller();
            filler.Fill();
        }
    }
}