using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KaanOrtak
{
    class Arayuz_Ortak
    {
        public static int Move = 0;
        public static int Mouse_X;
        public static int Mouse_Y;

        public static bool Kaydet_aktif = false;
        public static Color ayar_oncesi_on_renk;
        public static Color ayar_oncesi_arka_renk;
        public static Font ayar_oncesi_font;
        public static Control ayar_oncesi_control;

        public static Color K_on_renk = Color.Empty, k_arka_renk = Color.Empty;

        public static Control arayuz_secili_obje = null;
        public static Control acik_arayuz = null;

        public static int arayuz_secili_obje_cor_x = 0;
        public static int arayuz_secili_obje_cor_y = 0;
        static Arayuz.Ayar_Kaydet Ayarlar = null;

        public static string _ForeColor = "On_Plan_Renk";
        public static string _Back_Color = "Arka_Plan_Renk";
        public static string _Font = "Yazi_Tipi";
        public static string _Text = "Yazan_Yazi";
        public static Color Son_Ayarlanan_Renk;



        private static Control Formu_Ac()
        {
            if (Application.OpenForms["Arayuz_Tasarim_Form"] != null)
            {
                Application.OpenForms["Arayuz_Tasarim_Form"].Close();

                if (!Arayuz_Ortak.Kaydet_aktif)
                {
                    ayar_oncesi_control.ForeColor = ayar_oncesi_on_renk;
                    ayar_oncesi_control.BackColor = ayar_oncesi_arka_renk;
                    ayar_oncesi_control.Font = ayar_oncesi_font;
                }
         
            }
                
            Control Tasarim = new Arayuz_Tasarim_Form();
            

            return Tasarim;
        }

        public static Color arka_plan_rengi
        {
                
            get
            {
             
                return arayuz_secili_obje.BackColor;
               
            }
            set
            {
                arayuz_secili_obje.BackColor = value;
            }
           
        }

        public static Color on_renk
        {

            get
            {
                if (arayuz_secili_obje.GetType() == typeof(LinkLabel))
                {
                    return ((LinkLabel)arayuz_secili_obje).LinkColor;
                }
                else
                {
                    return arayuz_secili_obje.ForeColor;
                }
              
            }
            set
            {
                if(arayuz_secili_obje.GetType() == typeof(LinkLabel))
                {
                    ((LinkLabel)arayuz_secili_obje).LinkColor = value;
                }
                else
                {
                    arayuz_secili_obje.ForeColor = value;
                }

               
            }
        }

        public static Font font
        {
            get
            {
                return arayuz_secili_obje.Font;
            }
            set
            {
                arayuz_secili_obje.Font = value;
            }
        }

        public static void Kaan_Arayuz_Tasarim(Control control)
        {
            try { 
                acik_arayuz = control;
                //Default_Ayar_Kontrol_Et();
                Ayarlar = new Arayuz.Ayar_Kaydet(Genel_Ortak.Program_Yolu + @"\Ayarlar" + @"\" + Genel_Ortak.Programin_Adi + @"\" + acik_arayuz.Name + @"\" + acik_arayuz.Name + ".ini");
                Arayuz.Ayar_Kaydet.Ayarlar_Klasoru_Duzenle(Genel_Ortak.Programin_Adi, acik_arayuz.Name);
                Obje_Ayarlarini_Yukle(control);
                control.MouseClick += ControlOnMouseClick;
            
                if (control.HasChildren)
                    AddOnMouseClickHandlerRecursive(control.Controls);

       
                Arayuz.Ayar_Kaydet.Def_ayar_yok = false;
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }

        private static IEnumerable<Control> Obje_Listele(Control control, Type type = null)
        {
            var controls = control.Controls.Cast<Control>();

            if (type == null)
            {
                return controls.SelectMany(ctrl => Obje_Listele(ctrl, type))
                                                      .Concat(controls);
                //.Where(c => c.GetType() == type);
            }
            else
            {
                return controls.SelectMany(ctrl => Obje_Listele(ctrl, type))
                                      .Concat(controls)
                                      .Where(c => c.GetType() == type);
            }


        }

        private static void AddOnMouseClickHandlerRecursive(IEnumerable controls)
        {
            try { 
                foreach (Control control in controls)
                {
                    Obje_Ayarlarini_Yukle(control);

                    control.MouseDown += ControlOnMouseClick;

                    if (control.HasChildren)
                        AddOnMouseClickHandlerRecursive(control.Controls);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }

        private static void Obje_Ayarlarini_Yukle(Control control)
        {
            try { 
                TypeConverter tc = TypeDescriptor.GetConverter(typeof(Font));

                string arka_plan_rengi1 = Ayarlar.Oku(control.Name, _Back_Color);
                string on_plan_rengi = Ayarlar.Oku(control.Name, _Back_Color);
                string font1 = Ayarlar.Oku(control.Name, _Font);
                arayuz_secili_obje = control;

                if(arka_plan_rengi1 != "")
                {
                    arka_plan_rengi = Color.FromArgb(Convert.ToInt32(Ayarlar.Oku(control.Name, _Back_Color)));
                }
                if(on_plan_rengi != "")
                {
                    on_renk = Color.FromArgb(Convert.ToInt32(Ayarlar.Oku(control.Name, _ForeColor)));
                }
                if(font1 != "")
                {
                    font = (Font)tc.ConvertFromString(Ayarlar.Oku(control.Name, _Font));
                }

                arayuz_secili_obje = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }

        }
     
        private static void ControlOnMouseClick(object sender, MouseEventArgs args)
        {
            try { 
                if (args.Button != MouseButtons.Middle)
                    return;

                arayuz_secili_obje = (Control)sender;

                Control parent_obje = arayuz_secili_obje;

                while (parent_obje.GetType().BaseType != typeof(UserControl))
                {
                    if (parent_obje.GetType().BaseType == typeof(Form)) break;
                    parent_obje = parent_obje.Parent;
                }

                acik_arayuz = parent_obje;

                Ayarlar = new Arayuz.Ayar_Kaydet(Genel_Ortak.Program_Yolu + @"\Ayarlar" + @"\" + Genel_Ortak.Programin_Adi + @"\" + acik_arayuz.Name + @"\" + acik_arayuz.Name + ".ini");

                arayuz_secili_obje_cor_x = arayuz_secili_obje.Location.X;
                arayuz_secili_obje_cor_y = arayuz_secili_obje.Location.Y;
                //(sender as Control).Text

                var contextMenu = new ContextMenu(new[] {
                    new MenuItem("Düzenle", Arayuz_Ayar_Sekmesi_Ac),
                    new MenuItem("On Rengi Kaydet", K_on_renk_Kaydet),
                    new MenuItem("Arka Rengi Kaydet", K_arka_renk_Kaydet),
                    new MenuItem("Kaydedilen On Rengi Uygula", K_on_renk_Uygula),
                    new MenuItem("Kaydedilen Arka Rengi Uygula", K_arka_renk_Uygula),
                    new MenuItem("Default Ayara Dön", def_ayar_context)

                });
                    contextMenu.Show((Control)sender, new Point(args.X, args.Y));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }

        }

        private static void Arayuz_Ayar_Sekmesi_Ac(object sender, EventArgs eventArgs)
        {
            try { 
                int[] Arayuz_Kordinat = Genel_Ortak.obje_sonu(arayuz_secili_obje);
                //Acik_Sayfa.Controls.Add(Formu_Ac());
                Formu_Ac().Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }

        }

        private static void K_on_renk_Kaydet(object sender, EventArgs eventArgs)
        {

            K_on_renk = on_renk;

        }

        private static void K_arka_renk_Kaydet(object sender, EventArgs eventArgs)
        {

            k_arka_renk = arka_plan_rengi;

        }

        private static void K_on_renk_Uygula(object sender, EventArgs eventArgs)
        {
            try { 
                if(K_on_renk != Color.Empty)
                {
                    on_renk = K_on_renk;
                    Ayarlar.Yaz(arayuz_secili_obje.Name, _ForeColor, on_renk.ToArgb().ToString());
                    Ayarlar.Yaz(arayuz_secili_obje.Name, _Back_Color, arka_plan_rengi.ToArgb().ToString());
                    TypeConverter tc = TypeDescriptor.GetConverter(typeof(Font));
                    string fontString = tc.ConvertToString(font);
                    Ayarlar.Yaz(arayuz_secili_obje.Name, _Font, fontString);
                    Ayarlar.Yaz(arayuz_secili_obje.Name, _Text, arayuz_secili_obje.Text.ToString());
                }
                else
                {
                    MessageBox.Show("İlk Önce Renk Kaydediniz");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }

        }

        private static void K_arka_renk_Uygula(object sender, EventArgs eventArgs)
        {
            try { 
                if (k_arka_renk != Color.Empty)
                {
                    arka_plan_rengi = k_arka_renk;
                    Ayarlar.Yaz(arayuz_secili_obje.Name, _ForeColor, on_renk.ToArgb().ToString());
                    Ayarlar.Yaz(arayuz_secili_obje.Name, _Back_Color, arka_plan_rengi.ToArgb().ToString());
                    TypeConverter tc = TypeDescriptor.GetConverter(typeof(Font));
                    string fontString = tc.ConvertToString(font);
                    Ayarlar.Yaz(arayuz_secili_obje.Name, _Font, fontString);
                    Ayarlar.Yaz(arayuz_secili_obje.Name, _Text, arayuz_secili_obje.Text.ToString());
                }
                else
                {
                    MessageBox.Show("İlk Önce Renk Kaydediniz");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }


        private static void def_ayar_context(object sender, EventArgs eventArgs)
        {
            Default_Ayara_Don();
        }

        public static void Default_Ayara_Don()
        {
            try { 
                on_renk = new System.Drawing.Color();

                if(arayuz_secili_obje.GetType() == typeof(Panel) ||
                    arayuz_secili_obje.GetType().BaseType == typeof(UserControl) ||
                    arayuz_secili_obje.GetType().BaseType == typeof(Form))
                {
                    arka_plan_rengi = Color.White;
                }
                else
                {
                    arka_plan_rengi = new System.Drawing.Color();
                }
            
                Ayarlar.Yaz(Arayuz_Ortak.arayuz_secili_obje.Name, null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }


        private static void Default_Ayar_Kontrol_Et()
        {

            if (!Arayuz.Ayar_Kaydet.Ayarlar_Klasoru_Duzenle(Genel_Ortak.Programin_Adi, acik_arayuz.Name))
            {
                //Default_Ayar_Olustur(acik_arayuz);
                Arayuz.Ayar_Kaydet.Def_ayar_yok = true;

            }

        }



        private static void Default_Ayar_Olustur(Control control)
        {

            Ayarlar.Yaz(arayuz_secili_obje.Name, null, null);
        }

    }
}
