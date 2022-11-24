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
            new Database();
            while (true)
            {
                Console.Clear();
                MenuList("1", "Intercambiar");
                MenuList("2", "Mostrar productos");
                MenuList("3", "Valorizacion de productos");
                MenuList("4", "Salir");
                Console.Write("\nIr a: ");

                string option = Console.ReadLine();

                if (option == "1")
                {
                    Console.Clear();
                    Console.Write("Nombre: ");
                    var name = Console.ReadLine();
                    Console.Write("\nProducto a intercambiar: ");
                    var product = Console.ReadLine();
                    Console.Write("\nDescripcion breve: ");
                    var descr = Console.ReadLine();
                    Console.Write("\nValor referencial: $");
                    var value = decimal.Parse(Console.ReadLine());

                    Console.WriteLine("\nAhora ingrese los productos por los que desea intercambiar el producto registrado".Pastel("#8BC6FC"));

                    Console.Write("\nProducto Deseado N°1: ");
                    var pDesired = Console.ReadLine();
                    Console.Write("\nProducto Deseado N°2: ");
                    var pDesired2 = Console.ReadLine();
                    Console.Write("\nProducto Deseado N°3: ");
                    var pDesired3 = Console.ReadLine();
                    Console.WriteLine();
                    Item item = new Item(name, product, descr, value, pDesired, pDesired2, pDesired3);
                    if (item.Exists())
                    {
                        Console.Write("\n¿Desea realizar un trueque con alguno de los productos? (y/n): ");
                        var response = Console.ReadLine().ToLower().Trim();
                        if (response == "y")
                        {
                            Console.Write("\nIngrese ID del producto seleccionado: ");
                            var id = Console.ReadLine().Trim();
                            item.Delete(id);
                            Console.WriteLine("\nIntercambio exitoso.".Pastel("#009A17"));
                            Utility.KeyToContinue();
                        }
                        else
                        {
                            Console.Write("\n¿Desea almacenar el producto en inventario para un futuro intercambio? (y/n): ");
                            var response2 = Console.ReadLine().ToLower().Trim();
                            if (response2 == "y")
                            {
                                item.Add();
                                Console.WriteLine("\nProducto almacenado correctamente.".Pastel("#009A17"));
                                Thread.Sleep(2000);
                            }
                            else
                            {
                                Console.WriteLine("\nVolviendo al menu...".Pastel("#ECE81A"));
                                Thread.Sleep(2000);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("No existen productos actualmente que califiquen para el intercambio.".Pastel("#ECE81A"));
                        Console.Write("\n¿Desea almacenar el producto en inventario para un futuro intercambio? (y/n): ");
                        var response = Console.ReadLine().ToLower().Trim();
                        if(response == "y")
                        {
                            item.Add();
                            Console.WriteLine("\nProducto almacenado correctamente.".Pastel("#009A17"));
                            Thread.Sleep(2000);
                        }
                        else
                        {
                            Console.WriteLine("\nVolviendo al menu...".Pastel("#ECE81A"));
                            Thread.Sleep(2000);
                        }
                    }
                }
                else if (option == "2")
                {
                    Console.Clear();
                    Database.ReadAll();
                    Utility.KeyToContinue();
                }
                else if (option == "3")
                {
                    Console.Clear();
                    Console.WriteLine("Valor referencial total en inventario: $" + Database.CheckAllItemsValue());
                    Utility.KeyToContinue();
                }
                else if (option == "4")
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
