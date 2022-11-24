using IronXL;
using System;
using System.IO;
using System.Linq;

namespace TruequeApp
{
    internal class Item
    {
        private string name;
        private string product;
        private string descr;
        private decimal value;
        private string pDesired;
        private string pDesired2;
        private string pDesired3;
        private string directory = Directory.GetCurrentDirectory();

        #region Constructors
        public Item() { }

        public Item(string name, string product, string descr, decimal value, string pDesired, string pDesired2, string pDesired3)
        {
            this.name = name;
            this.product = product;
            this.descr = descr;
            this.value = value;
            this.pDesired = pDesired;
            this.pDesired2 = pDesired2;
            this.pDesired3 = pDesired3;
        }
        #endregion

        #region Methods
        private void Find(int rowIndex)
        {
            try
            {
                WorkBook wb = WorkBook.Load(directory + @"\Data\DB_Articulos.xlsx");
                WorkSheet ws = wb.WorkSheets.First();
                var x = 1;
                for (int i = 0; i < ws.Columns.Count(); i++)
                {
                    string value = ws.Rows[rowIndex].Columns[i].Value.ToString();
                    Console.Write("{0,-20}|", value);
                }
                Console.WriteLine();
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // verifica si existe (al menos) un objeto en el documento que califica el criterio
        public bool Exists()
        {
            WorkBook wb = WorkBook.Load(directory + @"\Data\DB_Articulos.xlsx");
            WorkSheet ws = wb.WorkSheets.First();
            var range = ws["C:C"];
            var check = 0;

            foreach (var item in range)
            {
                if (item.ToString() == pDesired || item.ToString() == pDesired2 || item.ToString() == pDesired3)
                {
                    var rowIndex = item.RowIndex;
                    Find(rowIndex);
                    check++;
                }
            }

            if (check != 0)
                return true;
            else
                return false;
        }

        // añade un nuevo item debajo del último item en el documento
        public void Add()
        {
            try
            {
                WorkBook wb = WorkBook.Load(directory + @"\Data\DB_Articulos.xlsx");
                WorkSheet ws = wb.GetWorkSheet("Articulos");

                int i = ws.Rows.Count() + 1;
                ws["A" + i].Value = Utility.IDGenerator();
                ws["B" + i].Value = name.ToString().ToLower();
                ws["C" + i].Value = product.ToString().ToLower();
                ws["D" + i].Value = descr.ToString().ToLower();
                ws["E" + i].Value = value;
                ws["F" + i].Value = pDesired.ToString().ToLower();
                ws["G" + i].Value = pDesired2.ToString().ToLower();
                ws["H" + i].Value = pDesired3.ToString().ToLower();
                ws["I" + i].Value = DateTime.Now.ToString("dd/MM/yyyy");
                ws.SaveAs(directory + @"\Data\DB_Articulos.xlsx");
            }
            catch (Exception ex)
            {
                IOException ioE = new IOException("Unable to write. The file is being used by another process.", ex.InnerException);
                throw ioE;
            }
        }

        public void Delete(string id)
        {
            WorkBook wb = WorkBook.Load(directory + @"\Data\DB_Articulos.xlsx");
            WorkSheet ws = wb.GetWorkSheet("Articulos");

            var range = ws["A:A"];
            
            foreach (var item in range)
            {
                if (item.ToString() == id)
                {
                    var i = item.RowIndex;
                    Log.WriteLog(name, product, value);
                    ws.Rows[i].RemoveRow();
                    ws.SaveAs(directory + @"\Data\DB_Articulos.xlsx");
                }
            }
        }
        #endregion
    }
}
