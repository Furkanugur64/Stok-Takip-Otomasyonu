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
    public partial class FrmBilgiGüncelle : DevExpress.XtraEditors.XtraForm
    {
        public FrmBilgiGüncelle()
        {
            InitializeComponent();
        }
        UgurPazarlamaEntities db = new UgurPazarlamaEntities();
        public string tcno;
        void Bilgi_Getir()
        {
            var per = db.TBL_PERSONELLER.Where(x => x.TC == tcno).FirstOrDefault();
            TxtId.Text = per.ID.ToString() ;
            TxtAd.Text = per.AD;
            TxtSoyad.Text = per.SOYAD;
            TxtTc.Text = per.TC;
            TxtMail.Text = per.MAIL;
            TxtTelefon.Text = per.TELEFON;
            TxtFotograf.Text = per.FOTOGRAF;
            pictureEdit1.Image = Image.FromFile(per.FOTOGRAF);

        }
        private void BtnGüncelle_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(TxtId.Text);
            var per = db.TBL_PERSONELLER.Find(id);
            per.AD = TxtAd.Text;
            per.SOYAD = TxtSoyad.Text;
            per.TC = TxtTc.Text;
            per.MAIL = TxtMail.Text;
            per.TELEFON = TxtTelefon.Text;
            db.SaveChanges();
            MessageBox.Show("Bilginiz Başarıyla Güncellendi", "UĞUR PAZARLAMA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Hide();
        }

        private void FrmBilgiGüncelle_Load(object sender, EventArgs e)
        {
            Bilgi_Getir();
        }

        private void pictureEdit3_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void pictureEdit2_Click(object sender, EventArgs e)
        {
            xtraOpenFileDialog1.Title = "Bir Resim Seçiniz";
            xtraOpenFileDialog1.InitialDirectory = @"D:\Visual Studio 2019\Projeler\Stok_Takip_Otomasyonu\Stok_Takip_Otomasyonu\bin\Debug\Resim";
            xtraOpenFileDialog1.DefaultExt = ".jpg";
            DialogResult sonuc = xtraOpenFileDialog1.ShowDialog();

            if (sonuc == DialogResult.OK)
            {
                pictureEdit1.Image = Image.FromFile(xtraOpenFileDialog1.FileName);
                TxtFotograf.Text = xtraOpenFileDialog1.FileName;
            }

        }
    }
}