﻿using IronXL;
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
        public object CheckAllItemsValue()
        {
            WorkBook wb = WorkBook.Load(directory + @"\Data\DB_Articulos.xlsx");
            WorkSheet ws = wb.GetWorkSheet("Articulos");

            ws["F1"].Formula = "Sum(D:D)";
            wb.EvaluateAll();
            var sumValue = ws["F1"].Value;

            return sumValue;
        }

        public void ReadAll()
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
            sheet["B1"].Value = "Producto";
            sheet["C1"].Value = "Descripcion";
            sheet["D1"].Value = "Valor_Aprox";
            sheet["E1"].Value = "Fecha_Publicacion";
            wb.SaveAs(directory + @"\Data\DB_Articulos.xlsx");
        }
        #endregion
    }
}