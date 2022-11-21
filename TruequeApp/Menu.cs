using Pastel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TruequeApp
{
    internal class Menu
    {
        public static void MainMenu()
        {
            Item item = new Item();
            while (true)
            {
                Console.Clear();
                MenuList("1", "Intercambiar");
                MenuList("2", "Mostrar productos");
                MenuList("3", "Productos historicos");
                MenuList("4", "Valorizacion de productos");
                MenuList("5", "Salir");
                Console.Write("\nIr a: ");

                string option = Console.ReadLine();

                if (option == "1")
                {
                    /*TODO: El usuario entrega los datos del producto (nombre, descripcion y valor) guardando los valores.
                     *      Luego se le pide escribir el nombre del producto que desea y se invoca al metodo Find() de la clase item para verificar 
                     *      si existe alguna(s) fila(s) con ese nombre. Si existe se le retorna al usuario la fila o filas que correspondan y se le pregunta 
                     *      por cual quiere intercambiarlo. Si el usuario opta por elegir uno se graba la transacción en el archivo log y se elimina 
                     *      si quiere guardar el producto en el documento para un futuro intercambio; si la respuesta es "no" no se guarda el objeto y se 
                     *      devuelve al menu.
                    */
                    Console.Clear();
                    Console.Write("Nombre del producto: ");
                    var product = Console.ReadLine();
                    Console.Write("\n Descripcion breve: ");
                    var descr = Console.ReadLine();
                    Console.Write("\n Valor referencial: $");
                    var value = Console.ReadLine();

                    Console.Write("\n¿Por cual producto quiere intercambiarlo?: ");
                    var desiredProduct = Console.ReadLine();
                    //...
                    
                }
                else if (option == "2")
                {
                    Console.Clear();
                    Database.ReadAll();
                    Utility.KeyToContinue();
                }
                else if (option == "3")
                {
                    /*TODO: mostrar todas las filas del documento que tengan una fecha superior a 1 mes basado en la fecha en que fueron agregados al mismo.
                     * Usar LinQ o similar.
                     */
                }
                else if (option == "4")
                {
                    Console.Clear();
                    Console.WriteLine("Valor referencial total en inventario: $" + Database.CheckAllItemsValue());
                    Utility.KeyToContinue();
                }
                else if (option == "5")
                {
                    Environment.Exit(1);
                }
                else
                {
                    Console.WriteLine("\nError, opción no se encuentra disponible. Por favor ingrese una opción valida.".Pastel("#E10600"));
                    Thread.Sleep(1000);
                }
            }
        }

        private static void MenuList(string prefix, string msg)
        {
            Console.Write("[");
            Console.Write(prefix.Pastel("#8BC6FC"));
            Console.WriteLine("] " + msg);
        }
    }
}
