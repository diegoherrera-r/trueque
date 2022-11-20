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
            Database db = new Database();
            db.ReadAll();
            Console.Write("Suma total del precio referencial de los productos: " + db.CheckAllItemsValue());
            Console.ReadKey();
        }
    }
}
