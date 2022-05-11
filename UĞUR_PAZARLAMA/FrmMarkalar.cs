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
    public partial class FrmMarkalar : DevExpress.XtraEditors.XtraForm
    {
        public FrmMarkalar()
        {
            InitializeComponent();
        }
        UgurPazarlamaEntities db = new UgurPazarlamaEntities();
        void Kategori_Getir()
        {
            var kategori = (from x in db.TBL_KATEGORILER
                            select new
                            {
                                x.ID,
                                x.AD
                            }).ToList();
            CmbKategori.Properties.DataSource = kategori;
            CmbKategori.Properties.DisplayMember = "AD";
            CmbKategori.Properties.ValueMember = "ID";
        }
        void Temizle()
        {
            TxtId.Text = "";
            TxtMarkaAdi.Text = "";
            CmbKategori.Text="";
        }
        void Marka_Listesi()
        {
            var markalar = (from x in db.TBL_MARKALAR
                            select new
                            {
                                x.ID,
                                x.AD,
                               KATEGORI= x.TBL_KATEGORILER.AD,

                            }).ToList();
            gridControl1.DataSource = markalar;
        }
        private void pictureEdit3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmMarkalar_Load(object sender, EventArgs e)
        {
            Kategori_Getir();
            Marka_Listesi();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            if (TxtId.Text != "")
            {


                DialogResult sonuc = MessageBox.Show(TxtMarkaAdi.Text + " Markasını Silmek İstiyor Musunuz ?", "UĞUR PAZARLAMA", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (sonuc == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(TxtId.Text);
                    var silmar = db.TBL_MARKALAR.Find(id);
                    db.TBL_MARKALAR.Remove(silmar);
                    db.SaveChanges();
                    MessageBox.Show("Marka Başarıyla Silindi", "UĞUR PAZARLAMA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Marka_Listesi();
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
                var gunmar = db.TBL_MARKALAR.Find(id);
                gunmar.AD = TxtMarkaAdi.Text;
                gunmar.KATEGORI =byte.Parse( CmbKategori.EditValue.ToString());
                db.SaveChanges();
                MessageBox.Show("Marka Bilgisi Güncellendi", "UĞUR PAZARLAMA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Marka_Listesi();
                Temizle();
            }
            else
                MessageBox.Show("Lütfen bir Marka seçin !!", "UĞUR PAZARLAMA", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            TxtId.Text = gridView1.GetFocusedRowCellValue("ID").ToString();
            TxtMarkaAdi.Text = gridView1.GetFocusedRowCellValue("AD").ToString();
            CmbKategori.Text = gridView1.GetFocusedRowCellValue("KATEGORI").ToString();
        }
    }
}