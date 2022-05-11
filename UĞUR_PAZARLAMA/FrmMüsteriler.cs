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
    public partial class FrmMüsteriler : DevExpress.XtraEditors.XtraForm
    {
        public FrmMüsteriler()
        {
            InitializeComponent();
        }
        UgurPazarlamaEntities db = new UgurPazarlamaEntities();
        void Il_Getir()
        {
            var iller = (from x in db.TBL_ILLER
                         select new
                         {
                             x.ID,
                             x.SEHIR
                         }).ToList();
            CmbIl.Properties.DataSource = iller;
            CmbIl.Properties.DisplayMember = "SEHIR";
            CmbIl.Properties.ValueMember = "ID";
        }
        void Müsteri_Listesi()
        {
            var müsteriler = (from x in db.TBL_MUSTERILER
                              select new
                              { x.ID,
                                  x.AD,
                                  x.SOYAD,
                                  x.TELEFON,
                                  x.MAIL,
                                  x.IL,
                                  x.ILCE,
                                  x.ADRES
                              }).ToList();
            gridControl1.DataSource = müsteriler;
        }
        void Temizle()
        {
            TxtAd.Text = "";
            TxtAdres.Text = "";
            TxtId.Text = "";
            TxtMail.Text = "";
            TxtSoyad.Text = "";
            TxtTelefon.Text = "";
            CmbIl.Text = "";
            CmbIlce.Text = "";
        }

        private void FrmMüsteriler_Load(object sender, EventArgs e)
        {
            Il_Getir();
            Müsteri_Listesi();
        }

        private void CmbIl_EditValueChanged(object sender, EventArgs e)
        {
            var y = byte.Parse(CmbIl.EditValue.ToString());

            var Ilce = (from x in db.TBL_ILCELER.Where(x => x.SEHIR == y)
                        select new
                        {
                            x.ID,
                            x.ILCE
                        }).ToList();

            CmbIlce.Properties.DataSource = Ilce;
            CmbIlce.Properties.DisplayMember = "ILCE";
            CmbIlce.Properties.ValueMember = "ID";
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            if (TxtId.Text != "")
            {


                DialogResult sonuc = MessageBox.Show(TxtAd.Text +" "+TxtSoyad.Text+ " İsimli Müşteriyi Silmek İstiyor Musunuz ?", "UĞUR PAZARLAMA", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (sonuc == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(TxtId.Text);
                    var silmus = db.TBL_MUSTERILER.Find(id);
                    db.TBL_MUSTERILER.Remove(silmus);
                    db.SaveChanges();
                    MessageBox.Show("Müşteri Başarıyla Silindi", "UĞUR PAZARLAMA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Müsteri_Listesi();
                    Temizle();

                }
            }
            else
                MessageBox.Show("Lütfen bir Müşteri seçin !!", "UĞUR PAZARLAMA", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void BtnGüncelle_Click(object sender, EventArgs e)
        {
            if (TxtId.Text != "")
            {

                int id = Convert.ToInt32(TxtId.Text);
                var gunmus = db.TBL_MUSTERILER.Find(id);
                gunmus.AD = TxtAd.Text;
                gunmus.ADRES = TxtAdres.Text;
                gunmus.IL = CmbIl.Text;
                gunmus.ILCE = CmbIlce.Text;
                gunmus.MAIL = TxtMail.Text;
                gunmus.SOYAD = TxtSoyad.Text;
                gunmus.TELEFON = TxtTelefon.Text;               
                db.SaveChanges();
                MessageBox.Show("Müşteri Bilgisi Güncellendi", "UĞUR PAZARLAMA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Müsteri_Listesi();
                Temizle();
            }
            else
                MessageBox.Show("Lütfen bir Müşteri seçin !!", "UĞUR PAZARLAMA", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            TxtId.Text = gridView1.GetFocusedRowCellValue("ID").ToString();
            TxtAd.Text = gridView1.GetFocusedRowCellValue("AD").ToString();
            TxtSoyad.Text = gridView1.GetFocusedRowCellValue("SOYAD").ToString();
            TxtTelefon.Text = gridView1.GetFocusedRowCellValue("TELEFON").ToString();
            TxtMail.Text = gridView1.GetFocusedRowCellValue("MAIL").ToString();
            CmbIl.Text = gridView1.GetFocusedRowCellValue("IL").ToString();
            CmbIlce.Text = gridView1.GetFocusedRowCellValue("ILCE").ToString();
            TxtAdres.Text = gridView1.GetFocusedRowCellValue("ADRES").ToString();
        }

       

        private void pictureEdit3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            Temizle();
        }
    }
}