using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruequeApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Item item = new Item();
            Database.ReadAll();
            Console.Write("Buscar producto: ");
            var product = Console.ReadLine();
            item.Find(product);
            Console.ReadKey();
        }
    }
}
