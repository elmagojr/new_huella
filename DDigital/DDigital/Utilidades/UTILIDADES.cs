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

namespace DDigital.Utilidades
{
    internal class UTILIDADES
    {
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
    }

    
}
