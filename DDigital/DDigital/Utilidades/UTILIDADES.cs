using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using DPUruNet;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace DDigital.Utilidades
{
   
    internal class UTILIDADES
    {
        public PERMISOS permisosSuperUS = new PERMISOS { AdmonRoles=true, AdmonUsuarios=true, EliminarHuellas=true, nombre_rol="SU", QuitarAccesoUsr=true, RegistrarHuellas=true, SeleccionarDedos=true, SeleccionarMano=true, Tiene_acceso="SI", VerHuellas=true, VerificarHuellas=true, VerificarMancomuna=true};
        public Bitmap CreateBitmap(byte[] bytes, int width, int height)
        {
            if (bytes == null || bytes.Length == 0)
            {
                throw new ArgumentException("El array de bytes no puede ser nulo o estar vacío.");
            }

            int pixelCount = width * height;
            if (bytes.Length < pixelCount)
            {
                throw new ArgumentException("La cantidad de bytes es insuficiente para crear el bitmap con las dimensiones especificadas.");
            }

            byte[] rgbBytes = new byte[pixelCount * 3];

            for (int i = 0; i < pixelCount; i++)
            {
                rgbBytes[(i * 3)] = bytes[i];
                rgbBytes[(i * 3) + 1] = bytes[i];
                rgbBytes[(i * 3) + 2] = bytes[i];
            }

            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            BitmapData data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

            for (int i = 0; i < height; i++)
            {
                IntPtr p = new IntPtr(data.Scan0.ToInt64() + data.Stride * i);
                Marshal.Copy(rgbBytes, i * width * 3, p, width * 3);
            }

            bmp.UnlockBits(data);

            return bmp;
        }
        public byte[] ConveritirBitmap_tojpeg(Bitmap bitmapp)
        {
            byte[] array;

            using (Bitmap Bitmap_reescalado = new Bitmap(bitmapp, new Size(80, 80)))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    Bitmap_reescalado.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    return array = ms.ToArray();
                }
            }

        }


        public string identificaDedo(string dedo)
        {
            switch (dedo)
            {
                case "DI1":
                    return "Pulgar Izquierdo";
                case "DI2":
                    return "Índice Izquierdo";
                case "DI3":
                    return "Medio Izquierdo";
                case "DI4":
                    return "Anular Izquierdo";
                case "DI5":
                    return "Meñique Izquierdo";
                case "DD1":
                    return "Pulgar Derecho";
                case "DD2":
                    return "Índice Derecho";
                case "DD3":
                    return "Medio Derecho";
                case "DD4":
                    return "Anular Derecho";
                case "DD5":
                    return "Meñique Derecho";
                default: return "N/a";
            }
        }


        public CREDENCIALES LEER_CREDENCIALES()
        {
            CREDENCIALES credencials = new CREDENCIALES();
            string path = @"C:\SISC\Addons\DDigital\ENLACE.txt";

            try
            {
                if (File.Exists(path))
                {
                    string[] credo = File.ReadAllLines(path);
                    if (credo.Length == 0)
                    {
                        credencials = null;
                    }
                    else
                    {
                        foreach (string lineaCred in credo)
                        {
                            string[] credencial = lineaCred.Split(',');
                            foreach (string item in credencial)
                            {
                                string[] partes = item.Split(':');
                                switch (partes[0].Trim())
                                {
                                    case "codigo":
                                        credencials.codigo = partes[1].Trim();
                                        break;
                                    case "nombre":
                                        credencials.nombre = partes[1].Trim();
                                        break;
                                    case "identidad":
                                        credencials.identidad = partes[1].Trim();
                                        break;
                                    case "fromAction":
                                        credencials.fromAction = partes[1].Trim();
                                        break;
                                    case "usr_logged":
                                        credencials.usr_logged = partes[1].Trim();
                                        break;
                                    case "cta":
                                        credencials.cta = partes[1].Trim();
                                        break;
                                    case "cia":
                                        credencials.cia = partes[1].Trim();
                                        break;
                                    case "fil":
                                        credencials.fil = partes[1].Trim();
                                        break;
                                    default:
                                        break;
                                }
                            }

                        }
                     //   File.WriteAllText(path, string.Empty);////quitar que no se borre
                    }

                    return credencials;
                }
                else
                {
                    MessageBox.Show(HAS_ERROS("IO0001"), "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return null;

                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string HAS_ERROS(string code)
        {
            switch (code)
            {             
                case "IO0001":
                    return "IO0001: No se puede leer las credeciales: Favor Contacte con soporte. Se Cerrará el Add-On ";
                case "IO0002":
                    return "Valor nulo para credenciales ";
                case "HD00001":
                    return "La huella que intenta registrar ya existe. ";
                case "HD00002":
                    return "No se pudo guardar la huella ";
                case "DB00000":
                    return "DB00000 Sin conexion. Verifique la conexion del servidor de base de datos: ";
                case "DB00001":
                    return "DB00001 Cosulta/Parametro: ";
                case "DB00002":
                    return "DB00002 Consulta: ";
                case "DB00003":
                    return "DB00003 Conteo / Consulta: ";
                case "DB00004":
                    return "DB00004 Cosulta/Parametro: ";

                case "LE00001":
                    return "LE00001 Letor de huellas desconectado ";
                case "LE00002":
                    return "LE00002 Se ha desconectado el lector o no se detecta ";
                case "VE00001":
                    return "VE00001 Error en la captura ";
                case "VE00002":
                    return "VE00002 Error en la verificacion ";
                default:
                    return "Unknow error ";
                 
            }
        }

        public bool ExportarTXT(DATA_INTERSEPTOR link)
        {

            string path = @"C:\SISC\Addons\DDigital\ENLACE.txt";

            try
            {
                using (StreamWriter sw = new StreamWriter(path, false))
                {
                    sw.WriteLine(link.FLAG);
                    sw.WriteLine(link.HUE_IDENTIDAD);
                    sw.WriteLine(link.HUE_CODIGO);
                    sw.WriteLine(link.TIPO_PER);
                    sw.WriteLine(link.USR_VERIFICO);
                    sw.WriteLine(link.NOMBRE_VERIFICA);
                }
                return true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(this.HAS_ERROS("VE00002") + ex.Message);
                return false;
            }
        }



    }

    
}
