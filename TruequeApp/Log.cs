using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruequeApp
{
    internal class Log
    {
        private static string directory = Directory.GetCurrentDirectory();


        #region Methods
        public static void WriteLog(string name, string product, decimal value)
        {
            var path = directory + @"\Data\log.txt";

            using (StreamWriter sw = new StreamWriter(path, true)) 
            {
                sw.WriteLine($"{name} intercambió [{product} - ${value}] - {DateTime.Now}");
            }
        }
        #endregion
    }
}
