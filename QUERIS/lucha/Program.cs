using System;
using System.IO;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        // Define el directorio en el que se buscarán los archivos .sql
        string directorio = @"C:\Users\roken\Desktop\new_huella\QUERIS\"; // Cambia esto por la ruta del directorio que desees

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
                    Console.WriteLine("Nombres de archivos .sql encontrados:");

                    // Recorre cada archivo encontrado y ejecuta el comando dbisql
                    foreach (string archivo in archivosSql)
                    {
                        Console.WriteLine($"Ejecutando archivo: {Path.GetFileName(archivo)}");

                        // Configura el proceso para ejecutar dbisql
                        Process process = new();
                        process.StartInfo.FileName = @"C:\Program Files\SQL Anywhere 12\Bin32\dbisql.exe"; // Ruta al ejecutable de dbisql
                        process.StartInfo.Arguments = $"-c \"DSN=SISC\" \"{archivo}\""; // Parámetros del comando, incluyendo la ruta del archivo .sql
                        process.StartInfo.RedirectStandardOutput = true; // Redirige la salida si quieres capturarla
                        process.StartInfo.RedirectStandardError = true; // Redirige los errores si es necesario
                        process.StartInfo.UseShellExecute = false; // Necesario para redirigir la salida
                        process.StartInfo.CreateNoWindow = true; // Ejecuta sin crear una ventana de consola

                        // Inicia el proceso
                        process.Start();

                        // Lee la salida del comando
                        string output = process.StandardOutput.ReadToEnd();
                        string error = process.StandardError.ReadToEnd();

                        // Espera a que el proceso termine
                        process.WaitForExit();

                        // Muestra el resultado del comando en la consola
                        if (string.IsNullOrEmpty(error))
                        {
                            Console.WriteLine($"Comando ejecutado exitosamente para el archivo: {Path.GetFileName(archivo)}");
                            Console.WriteLine(output);
                        }
                        else
                        {
                            Console.WriteLine($"Error al ejecutar el archivo: {Path.GetFileName(archivo)}");
                            Console.WriteLine(error);
                        }
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
