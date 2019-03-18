using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KaanOrtak
{
    public partial class Arayuz_Tasarim_Form : Form
    {
        public Arayuz_Tasarim_Form()
        {
            InitializeComponent();
        }

        int Move;
        int Mouse_X;
        int Mouse_Y;
        public bool kaydet_aktif = false;

        static Arayuz.Ayar_Kaydet Ayarlar = null;

        private void Arayuz_Tasarim_Form_Load(object sender, EventArgs e)
        {
            try { 
                Ayarlar = new Arayuz.Ayar_Kaydet(Genel_Ortak.Program_Yolu + @"\Ayarlar" + @"\" + Genel_Ortak.Programin_Adi + @"\" + Arayuz_Ortak.acik_arayuz.Name + @"\" + Arayuz_Ortak.acik_arayuz.Name + ".ini");

                Arayuz_Ortak.Kaydet_aktif = false;
                Arayuz_Ortak.ayar_oncesi_on_renk = Arayuz_Ortak.on_renk;
                Arayuz_Ortak.ayar_oncesi_arka_renk = Arayuz_Ortak.arka_plan_rengi;
                Arayuz_Ortak.ayar_oncesi_font = Arayuz_Ortak.font;
                Arayuz_Ortak.ayar_oncesi_control = Arayuz_Ortak.arayuz_secili_obje;

                obje_ozellikleri_cek();
                if(Arayuz_Ortak.Son_Ayarlanan_Renk != null)
                    panel3.BackColor = Arayuz_Ortak.Son_Ayarlanan_Renk;


                if(Arayuz_Ortak.arayuz_secili_obje.GetType() == typeof(Panel) ||
                    Arayuz_Ortak.arayuz_secili_obje.GetType().BaseType == typeof(Form) ||
                    Arayuz_Ortak.arayuz_secili_obje.GetType().BaseType == typeof(UserControl) ||
                    Arayuz_Ortak.arayuz_secili_obje.GetType() == typeof(PictureBox))
                {
                    radioButton2.Checked = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }

        }
   
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            trackBar4.Visible = true;
            label12.Visible = true;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            trackBar4.Visible = false;
            label12.Visible = false;
        }

        private void trackBar1_Scroll_1(object sender, EventArgs e)
        {

            try
            {
                obje_renk_degistir(Color.FromArgb(trackBar1.Value, trackBar2.Value, trackBar3.Value));                   
                label2.Text = trackBar1.Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }

        private void trackBar2_Scroll_1(object sender, EventArgs e)
        {
            try
            {

                obje_renk_degistir(Color.FromArgb(trackBar1.Value, trackBar2.Value, trackBar3.Value));
                label3.Text = trackBar2.Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }

        }

        private void trackBar3_Scroll_1(object sender, EventArgs e)
        {
            try {

                obje_renk_degistir(Color.FromArgb(trackBar1.Value, trackBar2.Value, trackBar3.Value));
                label5.Text = trackBar3.Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
}

        private void trackBar4_Scroll_1(object sender, EventArgs e)
        {
            try { 

                obje_renk_degistir(Color.FromArgb(trackBar4.Value,trackBar1.Value, trackBar2.Value, trackBar3.Value));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
}

        public void obje_ozellikleri_cek()
        {
            try { 

                if (radioButton1.Checked)
                {
                    trackBar1.Value = Arayuz_Ortak.on_renk.R;
                    label2.Text = trackBar1.Value.ToString();
                    trackBar2.Value = Arayuz_Ortak.on_renk.G;
                    label3.Text = trackBar2.Value.ToString();
                    trackBar3.Value = Arayuz_Ortak.on_renk.B;
                    label5.Text = trackBar3.Value.ToString();
                    panel1.BackColor = Arayuz_Ortak.on_renk;
                }
                else if (radioButton2.Checked)
                {
                    trackBar1.Value = Arayuz_Ortak.arka_plan_rengi.R;
                    label2.Text = trackBar1.Value.ToString();
                    trackBar2.Value = Arayuz_Ortak.arka_plan_rengi.G;
                    label3.Text = trackBar2.Value.ToString();
                    trackBar3.Value = Arayuz_Ortak.arka_plan_rengi.B;
                    label5.Text = trackBar3.Value.ToString();
                    panel1.BackColor = panel1.BackColor = Arayuz_Ortak.arka_plan_rengi;

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }

}

        private void obje_renk_degistir(Color renk)
        {
            try { 

                if (radioButton1.Checked)
                {
                    Arayuz_Ortak.on_renk = renk;
                }
                else if(radioButton2.Checked)
                {
                    Arayuz_Ortak.arka_plan_rengi = renk;
                }
            

                panel1.BackColor = renk;
                label11.Text = "RGB : " + trackBar1.Value + "," + trackBar2.Value + "," + trackBar3.Value;
                textBox1.Text = trackBar1.Value.ToString();
                textBox2.Text = trackBar2.Value.ToString();
                textBox3.Text = trackBar3.Value.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
}

        private void radioButton1_CheckedChanged_1(object sender, EventArgs e)
        {
            trackBar4.Visible = false;
            label12.Visible = false;
            obje_ozellikleri_cek();
        }

        private void radioButton2_CheckedChanged_1(object sender, EventArgs e)
        {
            trackBar4.Visible = true;
            label12.Visible = true;;
            obje_ozellikleri_cek();
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            Move = 1;
            Mouse_X = e.X;
            Mouse_Y = e.Y;
        }

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (Move == 1)
            {
                this.SetDesktopLocation(MousePosition.X - Mouse_X, MousePosition.Y - Mouse_Y);
            }
        }

        private void panel2_MouseUp(object sender, MouseEventArgs e)
        {

            Move = 0;
        }

        private void label13_Click(object sender, EventArgs e)
        {

            Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try { 

                ColorDialog colorDialog = new ColorDialog();
                colorDialog.ShowDialog();
                obje_renk_degistir(colorDialog.Color);
                obje_ozellikleri_cek();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try { 

                FontDialog Font = new FontDialog();
                Font.ShowColor = true;
                Font.MaxSize = 15;
                Font.MinSize = 7;
                var dialog = Font.ShowDialog();
                if(dialog == DialogResult.OK)
                {
                    Arayuz_Ortak.font = Font.Font;
                    obje_ozellikleri_cek();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try { 
                MessageBox.Show("Ayarlarınız Kaydedildi");          
                Ayarlar.Yaz(Arayuz_Ortak.arayuz_secili_obje.Name, Arayuz_Ortak._ForeColor, Arayuz_Ortak.on_renk.ToArgb().ToString());
                Ayarlar.Yaz(Arayuz_Ortak.arayuz_secili_obje.Name, Arayuz_Ortak._Back_Color, Arayuz_Ortak.arka_plan_rengi.ToArgb().ToString());
                TypeConverter tc = TypeDescriptor.GetConverter(typeof(Font));
                string fontString = tc.ConvertToString(Arayuz_Ortak.font);
                Ayarlar.Yaz(Arayuz_Ortak.arayuz_secili_obje.Name, Arayuz_Ortak._Font, fontString);
                Ayarlar.Yaz(Arayuz_Ortak.arayuz_secili_obje.Name, Arayuz_Ortak._Text, Arayuz_Ortak.arayuz_secili_obje.Text.ToString());
                Arayuz_Ortak.Son_Ayarlanan_Renk = panel1.BackColor;
                Arayuz_Ortak.Kaydet_aktif = true;

                if (radioButton1.Checked)
                {
                    panel3.BackColor = Arayuz_Ortak.on_renk;
                }else if (radioButton2.Checked)
                {
                    panel3.BackColor = Arayuz_Ortak.arka_plan_rengi;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Arayuz_Ortak.Default_Ayara_Don();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            obje_renk_degistir(Arayuz_Ortak.Son_Ayarlanan_Renk);
            obje_ozellikleri_cek();
        }

        private void Arayuz_Tasarim_Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            try { 

                if (!Arayuz_Ortak.Kaydet_aktif)
                {
                    Arayuz_Ortak.on_renk = Arayuz_Ortak.ayar_oncesi_on_renk;
                    Arayuz_Ortak.arka_plan_rengi = Arayuz_Ortak.ayar_oncesi_arka_renk;
                    Arayuz_Ortak.font = Arayuz_Ortak.ayar_oncesi_font;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }

      
        private void label14_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show((Control)sender, new Point(((Control)sender).Location.X +20, ((Control)sender).Location.Y +20));
        }

        private void tümAyarlarıSıfırlaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult secenek = MessageBox.Show("Tüm Programın Arayüz Ayarları Silinecek Eminmisiniz ?\nAyarlarınız Programı Kapatıp Açtıktan Sonra Silinecektir.", "Bilgilendirme Penceresi", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (secenek == DialogResult.Yes)
                {
                   if(Directory.Exists(Genel_Ortak.Program_Yolu + @"\Ayarlar\" + Genel_Ortak.Programin_Adi))
                        Directory.Delete(Genel_Ortak.Program_Yolu + @"\Ayarlar\" + Genel_Ortak.Programin_Adi, true);

                    MessageBox.Show("Ayarlarınız Silindi Programı Kapatıp Açınız.");
                }
                else if (secenek == DialogResult.No)
                {
                    //Hayır seçeneğine tıklandığında çalıştırılacak kodlar
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

     

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                trackBar1.Value = Int32.Parse(textBox1.Text);
                trackBar2.Value = Int32.Parse(textBox2.Text);
                trackBar3.Value = Int32.Parse(textBox3.Text);
                obje_renk_degistir(Color.FromArgb(Int32.Parse(textBox1.Text), Int32.Parse(textBox2.Text), Int32.Parse(textBox3.Text)));
                
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                trackBar1.Value = Int32.Parse(textBox1.Text);
                trackBar2.Value = Int32.Parse(textBox2.Text);
                trackBar3.Value = Int32.Parse(textBox3.Text);
                obje_renk_degistir(Color.FromArgb(Int32.Parse(textBox1.Text), Int32.Parse(textBox2.Text), Int32.Parse(textBox3.Text)));
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                trackBar1.Value = Int32.Parse(textBox1.Text);
                trackBar2.Value = Int32.Parse(textBox2.Text);
                trackBar3.Value = Int32.Parse(textBox3.Text);
                obje_renk_degistir(Color.FromArgb(Int32.Parse(textBox1.Text), Int32.Parse(textBox2.Text), Int32.Parse(textBox3.Text)));
            }
        }
    }
}
