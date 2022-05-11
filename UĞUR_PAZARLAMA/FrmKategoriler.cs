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
    public partial class FrmKategoriler : DevExpress.XtraEditors.XtraForm
    {
        public FrmKategoriler()
        {
            InitializeComponent();
        }
        UgurPazarlamaEntities db = new UgurPazarlamaEntities();
        void Kategori_Listesi()
        {
            var kategoriler = (from x in db.TBL_KATEGORILER
                               select new
                               {
                                   x.ID,
                                   x.AD
                               }
            ).ToList();
            gridControl1.DataSource = kategoriler;
        }
        void Temizle()
        {
            TxtAd.Text = "";
            TxtId.Text = "";
        }
        private void FrmKategoriler_Load(object sender, EventArgs e)
        {
            Kategori_Listesi();
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            TxtId.Text = gridView1.GetFocusedRowCellValue("ID").ToString();
            TxtAd.Text = gridView1.GetFocusedRowCellValue("AD").ToString();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            if (TxtId.Text != "")
            {


                DialogResult sonuc = MessageBox.Show(TxtAd.Text + " Kategorisini Silmek İstiyor Musunuz ?", "UĞUR PAZARLAMA", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (sonuc == DialogResult.Yes)
                {
                    int id =Convert.ToInt32( TxtId.Text);
                    var silkat = db.TBL_KATEGORILER.Find(id);
                    db.TBL_KATEGORILER.Remove(silkat);
                    db.SaveChanges();
                    Kategori_Listesi();
                    MessageBox.Show("Kategori Başarıyla Silindi", "UĞUR PAZARLAMA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Temizle();

                }
            }
            else
                MessageBox.Show("Lütfen bir kategori seçin !!", "UĞUR PAZARLAMA", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void BtnGüncelle_Click(object sender, EventArgs e)
        {
            if (TxtId.Text != "")
            {

                int id = Convert.ToInt32(TxtId.Text);
                var gunkat = db.TBL_KATEGORILER.Find(id);
                gunkat.AD = TxtAd.Text;
                db.SaveChanges();
                MessageBox.Show("Kategori Bilgisi Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Kategori_Listesi();
                Temizle();
            }
            else
                MessageBox.Show("Lütfen bir kategori seçin !!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void pictureEdit3_EditValueChanged(object sender, EventArgs e)
        {
           
        }

        private void pictureEdit3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}