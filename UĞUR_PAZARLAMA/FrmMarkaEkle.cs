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
    public partial class FrmMarkaEkle : DevExpress.XtraEditors.XtraForm
    {
        public FrmMarkaEkle()
        {
            InitializeComponent();
        }
        UgurPazarlamaEntities db = new UgurPazarlamaEntities();
        void Kategori_Getir()
        {
            var kategori = (from x in db.TBL_KATEGORILER
                            select new {
                            x.ID,
                            x.AD}).ToList();
            CmbKategori.Properties.DataSource = kategori;
            CmbKategori.Properties.DisplayMember = "AD";
            CmbKategori.Properties.ValueMember = "ID";
        }
        private void FrmMarkaEkle_Load(object sender, EventArgs e)
        {
            Kategori_Getir();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            if (TxtMarkaAdi.Text != "" && CmbKategori.Text != "Bir Kategori Seçin")
            {
                TBL_MARKALAR m = new TBL_MARKALAR();
                m.AD = TxtMarkaAdi.Text;
                m.KATEGORI =byte.Parse( CmbKategori.EditValue.ToString());
                db.TBL_MARKALAR.Add(m);
                db.SaveChanges();
                MessageBox.Show("Marka Başarılı Bir Şekilde Eklendi", "UĞUR PAZARLAMA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Bilgileri Kontrol Ediniz", "UĞUR PAZARLAMA", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void pictureEdit3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}