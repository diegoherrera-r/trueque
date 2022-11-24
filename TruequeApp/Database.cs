using IronXL;
using System;
using System.IO;
using System.Linq;

namespace TruequeApp
{
    internal class Database
    {
        private static string directory = Directory.GetCurrentDirectory();

        #region Constructors
        public Database()
        {
            Exists();
        }
        #endregion

        #region Methods
        public static object CheckAllItemsValue()
        {
            WorkBook wb = WorkBook.Load(directory + @"\Data\DB_Articulos.xlsx");
            WorkSheet ws = wb.GetWorkSheet("Articulos");

            ws["Z1"].Formula = "Sum(E:E)";
            wb.EvaluateAll();
            var sumValue = ws["Z1"].Value;

            return sumValue;
        }

        public static void ReadAll()
        {
            try
            {
                WorkBook wb = WorkBook.Load(directory + @"\Data\DB_Articulos.xlsx");
                WorkSheet ws = wb.GetWorkSheet("Articulos");

                for (int i = 0; i < ws.Rows.Count(); i++)
                {
                    for (int j = 0; j < ws.Columns.Count(); j++)
                    {
                        string value = ws.Rows[i].Columns[j].Value.ToString();
                        Console.Write("{0,-20}|", value);
                    }
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                IOException ioE = new IOException("No es posible leer el archivo. El archivo está siendo usado por otro proceso.", ex.InnerException);
                throw ioE;
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

            var wb = WorkBook.Create(ExcelFileFormat.XLSX);
            var sheet = wb.CreateWorkSheet("Articulos");
            sheet["A1"].Value = "ID";
            sheet["B1"].Value = "Nombre";
            sheet["C1"].Value = "Producto";
            sheet["D1"].Value = "Descripcion";
            sheet["E1"].Value = "Valor_Aprox";
            sheet["F1"].Value = "Deseo_1";
            sheet["G1"].Value = "Deseo_2";
            sheet["H1"].Value = "Deseo_3";
            sheet["I1"].Value = "Fecha_Publicacion";
            wb.SaveAs(directory + @"\Data\DB_Articulos.xlsx");
        }
        #endregion
    }
}