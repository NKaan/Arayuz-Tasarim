using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KaanOrtak
{
    public class Genel_Ortak
    {

        public static string Program_Yolu = Directory.GetParent(Application.ExecutablePath).ToString();
        public static string Bilgisayar_Kullanici_Adi = noktolama_isaretlerini_sil(SystemInformation.UserName);
        public static string Programin_Adi = Application.ProductName;

        public static int[] obje_sonu(Control obje)
        {

            int[] son = { 0, 1 };
            son[0] = obje.Location.X + obje.Size.Width;
            son[1] = obje.Location.Y + obje.Size.Height;
            return son;
        }

        public static void Bekle(double beklemesuresi)
        {

            for (int i = 0; i < beklemesuresi * 100; i++)
            {
                Thread.Sleep(10);
                Application.DoEvents();
            }

        }

        public static string noktolama_isaretlerini_sil(string silinecek_yazi)
        {

            silinecek_yazi = silinecek_yazi
                .Replace(",", "")
                .Replace(".", "")
                .Replace("!", "")
                .Replace("'", "")
                .Replace("^^", "")
                .Replace("+", "")
                .Replace("%", "")
                .Replace("&", "")
                .Replace("£", "")
                .Replace("#", "")
                .Replace("$", "")
                .Replace("½", "")
                .Replace("/", "")
                .Replace("(", "")
                .Replace(")", "")
                .Replace("=", "")
                .Replace("?", "")
                .Replace("_", "")
                .Replace("{", "")
                .Replace("[", "")
                .Replace("]", "")
                .Replace("}", "")
                .Replace("\\", "")
                .Replace("-", "")
                .Replace("|", "")
                .Replace(";", "")
                .Replace("é", "")
                .Replace(":", "")
                .Replace("\"", "");

            return silinecek_yazi;
        }

    }
}
