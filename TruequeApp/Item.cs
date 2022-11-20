using IronXL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruequeApp
{
    internal class Item
    {
        private string product;
        private string descr;
        private decimal value;
        private string directory = Directory.GetCurrentDirectory();

        #region Constructors
        public Item(string product, string descr, decimal value)
        {
            this.product = product;
            this.descr = descr;
            this.value = value;
        }
        #endregion

        #region Methods
        public Object Find()
        {
            //TODO: Buscar y mostrar la fila de un articulo por su nombre en el documento
            return null;
        }

        public bool Exists()
        {
            //TODO: verificar si existe el objeto en el documento
            return false;
        }
        
        // añade un nuevo item debajo del último item en el documento
        public void Add()
        {
            try
            {
                WorkBook workbook = WorkBook.Load(directory + @"\Data\DB_Articulos.xlsx");
                WorkSheet worksheet = workbook.GetWorkSheet("Articulos");

                int i = worksheet.Rows.Count() + 1;
                worksheet["A" + i].Value = Utility.IDGenerator();
                worksheet["B" + i].Value = product.ToString().ToLower();
                worksheet["C" + i].Value = descr.ToString().ToLower();
                worksheet["D" + i].Value = value;
                worksheet["E" + i].Value = DateTime.Today;
                worksheet.SaveAs(directory + @"\Data\DB_Articulos.xlsx");
            }
            catch (Exception ex)
            {
                IOException ioE = new IOException("Unable to write. The file is being used by another process.", ex.InnerException);
                throw ioE;
            }
        }

        public void Delete(int i)
        {
            WorkBook workbook = WorkBook.Load(directory + @"\Data\DB_Articulos.xlsx");
            WorkSheet worksheet = workbook.GetWorkSheet("Articulos");

            if (i <= 0)
                Console.WriteLine("Error, ingresar valor mayor a 0. Ninguna fila fue eliminada.");
            else
            {
                worksheet.Rows[i].RemoveRow();
                worksheet.SaveAs(directory + @"\Data\DB_Articulos.xlsx");
            }
        }
        #endregion
    }
}
