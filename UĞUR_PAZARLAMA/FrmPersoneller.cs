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
    public partial class FrmPersoneller : DevExpress.XtraEditors.XtraForm
    {
        public FrmPersoneller()
        {
            InitializeComponent();
        }
        UgurPazarlamaEntities db = new UgurPazarlamaEntities();
        private void pictureEdit2_EditValueChanged(object sender, EventArgs e)
        {
            
        }
        void Statü_Getir()
        {
            var statuler = (from x in db.TBL_STATULER
                            select new
                            {
                                x.ID,
                                x.STATUAD
                            }).ToList();
            CmbStatü.Properties.DataSource = statuler;
            CmbStatü.Properties.DisplayMember = "STATUAD";
            CmbStatü.Properties.ValueMember = "ID";

        }
        void Personel_Listesi()
        {
            var personeller = (from x in db.TBL_PERSONELLER
                               select new
                               {
                                   x.ID,
                                   x.AD,
                                   x.SOYAD,
                                   x.TC,
                                   STATU=x.TBL_STATULER.STATUAD,
                                   x.MAIL,
                                   x.TELEFON,
                                   x.FOTOGRAF
                               }).ToList();
            gridControl1.DataSource = personeller;
        }
        void Temizle()
        {
            TxtId.Text = "";
            TxtAd.Text = "";
            TxtSoyad.Text = "";
            TxtTc.Text = "";
            CmbStatü.Text = "";
            TxtMail.Text = "";
            TxtTelefon.Text = "";
            TxtFotograf.Text = "";
        }
        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            if (TxtAd.Text != "" && TxtSoyad.Text != "" && TxtMail.Text != "" && CmbStatü.Text != "Bir Statü Seçin" && TxtTc.Text != "" && TxtMail.Text != "")
            {
                if (pictureEdit1.Image == null)
                {
                    TxtFotograf.Text = @"D:\Visual Studio 2019\Projeler\Stok_Takip_Otomasyonu\Stok_Takip_Otomasyonu\bin\Debug\Resim\Personel.png";
                }
                TBL_PERSONELLER p = new TBL_PERSONELLER();
                p.AD = TxtAd.Text;
                p.SOYAD = TxtSoyad.Text;
                p.TC = TxtTc.Text;
                p.STATU = byte.Parse(CmbStatü.EditValue.ToString());
                p.MAIL = TxtMail.Text;
                p.TELEFON = TxtTelefon.Text;
                p.FOTOGRAF = TxtFotograf.Text;
                db.TBL_PERSONELLER.Add(p);
                db.SaveChanges();
                MessageBox.Show("Personel Başarılı Bir Şekilde Eklendi", "UĞUR PAZARLAMA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Temizle();
                Personel_Listesi();

            }
            else
            {
                MessageBox.Show("Lütfen Boş Alan Bırakmayın", "UĞUR PAZARLAMA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            if (TxtId.Text != "")
            {
                DialogResult sonuc = MessageBox.Show(TxtAd.Text + " Adlı Personeli Silmek İstiyor Musunuz ?", "UĞUR PAZARLAMA", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (sonuc == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(TxtId.Text);
                    var silper = db.TBL_PERSONELLER.Find(id);
                    db.TBL_PERSONELLER.Remove(silper);
                    db.SaveChanges();
                    MessageBox.Show("Personel Silme İşlemi Başarılı Bir Şekilde Gerçekleşti", "UĞUR PAZARLAMA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Temizle();
                    Personel_Listesi();
                }
            }
            else
            {
                MessageBox.Show("Lütfen Silmek İstediğiniz Personeli Seçin", "UĞUR PAZARLAMA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnGüncelle_Click(object sender, EventArgs e)
        {
            if (TxtId.Text != "")
            {
                int id = Convert.ToInt32(TxtId.Text);
                var gunper = db.TBL_PERSONELLER.Find(id);
                gunper.AD = TxtAd.Text;
                gunper.SOYAD = TxtSoyad.Text;
                gunper.TC = TxtTc.Text;
                gunper.STATU = byte.Parse(CmbStatü.EditValue.ToString());
                gunper.MAIL = TxtMail.Text;
                gunper.TELEFON = TxtTelefon.Text;
                gunper.FOTOGRAF = TxtFotograf.Text;
                MessageBox.Show("Personel Güncelleme İşlemi Başarılı Bir Şekilde Gerçekleşti", "UĞUR PAZARLAMA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                db.SaveChanges();
                Personel_Listesi();

            }
            else
            {
                MessageBox.Show("Lütfen Güncellemek İstediğiniz Personeli Seçin", "UĞUR PAZARLAMA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            TxtId.Text = gridView1.GetFocusedRowCellValue("ID").ToString();
            TxtAd.Text = gridView1.GetFocusedRowCellValue("AD").ToString();
            TxtSoyad.Text = gridView1.GetFocusedRowCellValue("SOYAD").ToString();
            TxtTc.Text = gridView1.GetFocusedRowCellValue("TC").ToString();
            CmbStatü.Text = gridView1.GetFocusedRowCellValue("STATU").ToString();
            TxtMail.Text = gridView1.GetFocusedRowCellValue("MAIL").ToString();
            TxtTelefon.Text = gridView1.GetFocusedRowCellValue("TELEFON").ToString();
            TxtFotograf.Text = gridView1.GetFocusedRowCellValue("FOTOGRAF").ToString();
            pictureEdit1.Image = Image.FromFile(gridView1.GetFocusedRowCellValue("FOTOGRAF").ToString());
        }

        private void FrmPersoneller_Load(object sender, EventArgs e)
        {
            Statü_Getir();
            Personel_Listesi();
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

        private void pictureEdit3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}