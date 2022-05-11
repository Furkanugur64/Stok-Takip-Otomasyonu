using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UĞUR_PAZARLAMA.Models;

namespace UĞUR_PAZARLAMA
{
    public partial class FrmGirisYap : DevExpress.XtraEditors.XtraForm
    {
        public FrmGirisYap()
        {
            InitializeComponent();
        }
        UgurPazarlamaEntities db = new UgurPazarlamaEntities();
        private void FrmGirisYap_Load(object sender, EventArgs e)
        {
            
        }

        private void PInsta_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.instagram.com/ugurfurkan64/");
        }

        private void PLinkedin_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.linkedin.com/in/furkan-ugur64/");
        }

        private void PFace_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.facebook.com/furkan.ugur.1023/");
        }

        private void BtnCikisYap_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtnGirisYap_Click(object sender, EventArgs e)
        {
            if (TxtKullaniciadi.Text == "11111111111" || TxtKullaniciadi.Text == "11111111112")
            {
                var bilgi = db.TBL_ADMIN.FirstOrDefault(c => c.KullanıcıAdı == TxtKullaniciadi.Text && c.Sifre == TxtSifre.Text);
                if (bilgi!=null)
                {
                    FrmYöneticiGiris fr = new FrmYöneticiGiris();
                    fr.tc = TxtKullaniciadi.Text;
                    fr.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Kullanıcı Adı Ve/Veya Şifre Yanlış");
                }

            }
            else
            {
                var bilgi = db.TBL_ADMIN.FirstOrDefault(c => c.KullanıcıAdı == TxtKullaniciadi.Text && c.Sifre == TxtSifre.Text);
                if (bilgi!=null)
                {
                    FrmPersonelGiris fr = new FrmPersonelGiris();
                    fr.tc = TxtKullaniciadi.Text;
                    fr.Show();
                    this.Hide();
                }

                else
                {
                    MessageBox.Show("Kullanıcı Adı Ve/Veya Şifre Yanlış");

                }
            }
        }
    }
}