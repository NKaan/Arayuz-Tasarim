using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KaanOrtak.Arayuz
{
    class Ayar_Kaydet
    {

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        public Ayar_Kaydet(string yol)
        {
            Yol = yol;
        }

        public static bool Def_ayar_yok = false;
        private string Yol = String.Empty;
        public string Default { get; set; }

        public string Oku(string baslik, string okunacak_veri)
        {
            try
            {         
                Default = Default ?? String.Empty;
                StringBuilder StrBuild = new StringBuilder(256);
                GetPrivateProfileString(baslik, okunacak_veri, Default, StrBuild, 255, Yol);
                return StrBuild.ToString();
             }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }

        public long Yaz(string baslik, string yazilacak_veri_basligi, string yazilacak_veri)
        {
            try { 

                return WritePrivateProfileString(baslik, yazilacak_veri_basligi, yazilacak_veri, Yol);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }

        public static bool Ayarlar_Klasoru_Duzenle(string program_adi = null, string form_adi = null)
        {
            try { 
                if (!Directory.Exists(Genel_Ortak.Program_Yolu + @"\Ayarlar"))
                    Directory.CreateDirectory(Genel_Ortak.Program_Yolu + @"\Ayarlar");

                if (!Directory.Exists(Genel_Ortak.Program_Yolu + @"\Ayarlar\" + program_adi))
                    Directory.CreateDirectory(Genel_Ortak.Program_Yolu + @"\Ayarlar\" + program_adi);

                if (!Directory.Exists(Genel_Ortak.Program_Yolu + @"\Ayarlar\" + program_adi + @"\" + form_adi))
                {
                    Directory.CreateDirectory(Genel_Ortak.Program_Yolu + @"\Ayarlar\" + program_adi + @"\" + form_adi);
                    FileStream fs = File.Create(Genel_Ortak.Program_Yolu + @"\Ayarlar\" + program_adi + @"\" + form_adi + @"\" + form_adi + ".ini");              
                    fs.Close();


                    return false;
                }
                else
                    return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }

        }

    }
}
