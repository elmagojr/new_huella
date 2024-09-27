using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        // Define el directorio en el que se buscarán los archivos .sql
        string directorio = @"C:\TuDirectorio"; // Cambia esto por la ruta del directorio que desees

        try
        {
            // Verifica si el directorio existe
            if (Directory.Exists(directorio))
            {
                // Busca todos los archivos .sql en el directorio y subdirectorios
                string[] archivosSql = Directory.GetFiles(directorio, "*.sql", SearchOption.AllDirectories);

                // Verifica si se encontraron archivos .sql
                if (archivosSql.Length > 0)
                {
                    Console.WriteLine("Archivos .sql encontrados:");

                    // Recorre cada archivo encontrado y lo muestra en la consola
                    foreach (string archivo in archivosSql)
                    {
                        Console.WriteLine(archivo);
                    }
                }
                else
                {
                    Console.WriteLine("No se encontraron archivos .sql en el directorio.");
                }
            }
            else
            {
                Console.WriteLine("El directorio no existe.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ocurrió un error: {ex.Message}");
        }
    }
}
