using IronXL;
using System;
using System.IO;
using System.Linq;

namespace TruequeApp
{
    internal class Database
    {
        private string directory = Directory.GetCurrentDirectory();

        #region Constructors
        public Database()
        {
            Exists();
        }
        #endregion

        #region Methods
        public Object CheckAllItemsValue()
        {
            WorkBook workbook = WorkBook.Load(directory + @"\Data\DB_Articulos.xlsx");
            WorkSheet worksheet = workbook.GetWorkSheet("Articulos");

            worksheet["F1"].Formula = "Sum(D:D)";
            workbook.EvaluateAll();
            var sumValue = worksheet["F1"].Value;

            return sumValue;
        }

        public void ReadAll()
        {
            try
            {
                WorkBook workbook = WorkBook.Load(directory + @"\Data\DB_Articulos.xlsx");
                WorkSheet worksheet = workbook.GetWorkSheet("Articulos");

                for (int i = 0; i < worksheet.Rows.Count(); i++)
                {
                    for (int j = 0; j < worksheet.Columns.Count(); j++)
                    {
                        string value = worksheet.Rows[i].Columns[j].Value.ToString();
                        Console.Write("|{0,-20}|", value);
                    }
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                IOException ioE = new IOException("Unable to read. The file is being used by another process.", ex.InnerException);
                throw ioE;
            }
        }

        // añade un nuevo item debajo del último item en el documento
        public void Write(string product, string descr, decimal value)
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

        public void DeleteRow(int i)
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

        // verifica si existe el documento en el path especificado.
        private void Exists()
        {
            if (!File.Exists(directory + @"\Data\DB_Articulos.xlsx"))
            {
                Console.WriteLine("No existe archivo en directorio especificado. Se ha generado un nuevo archivo.");
                Create();
            }
        }

        // crea el documento en el path especificado.
        private void Create()
        {
            if (!Directory.Exists(directory + @"\Data"))
                Directory.CreateDirectory(directory + @"\Data");

            var workbook = WorkBook.Create(ExcelFileFormat.XLSX);
            var sheet = workbook.CreateWorkSheet("Articulos");
            sheet["A1"].Value = "ID";
            sheet["B1"].Value = "Producto";
            sheet["C1"].Value = "Descripcion";
            sheet["D1"].Value = "Valor_Aprox";
            sheet["E1"].Value = "Fecha_Publicacion";
            workbook.SaveAs(directory + @"\Data\DB_Articulos.xlsx");
        }
        #endregion
    }
}