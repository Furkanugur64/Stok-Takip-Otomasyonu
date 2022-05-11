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
    public partial class FrmUrun : DevExpress.XtraEditors.XtraForm
    {
        public FrmUrun()
        {
            InitializeComponent();
        }
        UgurPazarlamaEntities db = new UgurPazarlamaEntities();
        void Kategori_Getir()
        {
            var kat = (from x in db.TBL_KATEGORILER
                       select new
                       {
                           x.ID,
                           x.AD
                       }).ToList();
            CmbKategori.Properties.DataSource = kat;
            CmbKategori.Properties.DisplayMember = "AD";
            CmbKategori.Properties.ValueMember = "ID";
        }
        void Temizle()
        {
            
                TxtUrunAdi.Text = "";
                TxtBarkodNo.Text = "";
                TxtFotoğraf.Text = "";
                TxtFiyat.Text = "";
                TxtOzellik.Text = "";
                TxtStok.Text = "";
                CmbKategori.Text = "";
                CmbMarka.Text ="";
                pictureEdit1.Image = null;
            
        }
        void Urun_Listesi()
        {
            var urunler = (from x in db.TBL_URUN
                           select new
                           {
                             x.BARKODNO,
                             x.AD,
                             MARKA=  x.TBL_MARKALAR.AD,
                             KATEGORI = x.TBL_KATEGORILER.AD,
                             x.STOK,
                             x.ALISFIYAT,
                             x.SATISFIYAT,
                             x.FOTOGRAF,
                             x.OZELLIKLER

                           }).ToList();
            gridControl1.DataSource = urunler;
        }
        decimal alıs, satıs;
        void alıshesapla()
        {

            satıs = decimal.Parse(TxtFiyat.Text);
            if (satıs > 0 && satıs < 400)
            {
                alıs = satıs - 75;
            }
            else if (satıs >= 400 && satıs < 1000)
            {
                alıs = satıs - 260;
            }
            else if (satıs >= 1000 && satıs < 2500)
            {
                alıs = satıs - 475;
            }
            else if (satıs >= 2500 && satıs < 7000)
            {
                alıs = satıs - 1250;
            }
            else
            {
                alıs = satıs - 3000;
            }
        }
        private void FrmUrun_Load(object sender, EventArgs e)
        {
            Kategori_Getir();
            Urun_Listesi();
        }

        private void CmbKategori_EditValueChanged(object sender, EventArgs e)
        {
            var y = byte.Parse(CmbKategori.EditValue.ToString());

            var Ilce = (from x in db.TBL_MARKALAR.Where(x => x.KATEGORI == y)
                        select new
                        {
                            x.ID,
                            x.AD
                        }).ToList();

            CmbMarka.Properties.DataSource = Ilce;
            CmbMarka.Properties.DisplayMember = "AD";
            CmbMarka.Properties.ValueMember = "ID";
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            if (TxtBarkodNo.Text!="")
            {
                int barkodno = int.Parse(TxtBarkodNo.Text);
                var silurun = db.TBL_URUN.Find(barkodno);
                db.TBL_URUN.Remove(silurun);
                MessageBox.Show("Ürün Başarılı Bir Şekilde Silindi", "UĞUR PAZARLAMA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                db.SaveChanges();
                Urun_Listesi();
            }
            else
            {
                MessageBox.Show("Silmek İstediğiniz Ürünü Seçiniz !!", "- UĞUR PAZARLAMA -", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void BtnGüncelle_Click(object sender, EventArgs e)
        {
            
            if (TxtBarkodNo.Text!="")
            {
                alıshesapla();
                if (TxtFotoğraf.Text == "")
                {
                    TxtFotoğraf.Text = @"D:\Visual Studio 2019\Projeler\Stok_Takip_Otomasyonu\Stok_Takip_Otomasyonu\bin\Debug\Ürünler\Yok.png";
                }
                int barkodno = int.Parse(TxtBarkodNo.Text);
                var urn = db.TBL_URUN.Find(barkodno);
                urn.AD = TxtUrunAdi.Text;
                urn.SATISFIYAT = decimal.Parse(TxtFiyat.Text);
                urn.ALISFIYAT = alıs;
                urn.MARKA = byte.Parse(CmbMarka.EditValue.ToString());
                urn.KATEGORI = byte.Parse(CmbKategori.EditValue.ToString());
                urn.STOK = byte.Parse(TxtStok.Text);
                urn.FOTOGRAF = TxtFotoğraf.Text;
                urn.OZELLIKLER = TxtOzellik.Text;
                MessageBox.Show("Ürün Başarılı Bir Şekilde Güncellendi", "UĞUR PAZARLAMA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Urun_Listesi();
                Temizle();
               
                db.SaveChanges();
            }
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void pictureEdit2_Click(object sender, EventArgs e)
        {
            xtraOpenFileDialog1.Title = "Bir Resim Seçiniz";
            xtraOpenFileDialog1.InitialDirectory = @"D:\Visual Studio 2019\Projeler\Stok_Takip_Otomasyonu\Stok_Takip_Otomasyonu\bin\Debug\Ürünler";
            xtraOpenFileDialog1.DefaultExt = ".jpg";
            DialogResult sonuc = xtraOpenFileDialog1.ShowDialog();

            if (sonuc == DialogResult.OK)
            {
                pictureEdit1.Image = Image.FromFile(xtraOpenFileDialog1.FileName);
                TxtFotoğraf.Text = xtraOpenFileDialog1.FileName;
            }
        }

        private void pictureEdit3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            TxtBarkodNo.Text = gridView1.GetFocusedRowCellValue("BARKODNO").ToString();
            TxtUrunAdi.Text = gridView1.GetFocusedRowCellValue("AD").ToString();
            CmbKategori.Text = gridView1.GetFocusedRowCellValue("KATEGORI").ToString();
            CmbMarka.Text = gridView1.GetFocusedRowCellValue("MARKA").ToString();
            TxtStok.Text = gridView1.GetFocusedRowCellValue("STOK").ToString();
            TxtFiyat.Text = gridView1.GetFocusedRowCellValue("SATISFIYAT").ToString();
            TxtFotoğraf.Text = gridView1.GetFocusedRowCellValue("FOTOGRAF").ToString();
            TxtOzellik.Text = gridView1.GetFocusedRowCellValue("OZELLIKLER").ToString();
            pictureEdit1.Image=Image.FromFile(gridView1.GetFocusedRowCellValue("FOTOGRAF").ToString());
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            if (TxtUrunAdi.Text != "" && TxtFiyat.Text != "" && TxtOzellik.Text != "" && TxtStok.Text != "" && CmbKategori.Text != "" && CmbMarka.Text != "")
            {
                if (TxtFotoğraf.Text == "")
                {
                    TxtFotoğraf.Text = @"D:\Visual Studio 2019\Projeler\Stok_Takip_Otomasyonu\Stok_Takip_Otomasyonu\bin\Debug\Ürünler\Yok.png";
                }
                alıshesapla();
                TBL_URUN urn = new TBL_URUN();
                urn.AD = TxtUrunAdi.Text;
                urn.SATISFIYAT =decimal.Parse( TxtFiyat.Text);
                urn.ALISFIYAT =alıs;
                urn.MARKA= byte.Parse(CmbMarka.EditValue.ToString());
                urn.KATEGORI= byte.Parse(CmbKategori.EditValue.ToString());
                urn.STOK = byte.Parse( TxtStok.Text);
                urn.FOTOGRAF = TxtFotoğraf.Text;
                urn.OZELLIKLER = TxtOzellik.Text;
                db.TBL_URUN.Add(urn);
                db.SaveChanges();
                Urun_Listesi();
                Temizle();
                MessageBox.Show("Ürün Başarılı Bir Şekilde Eklendi", "UĞUR PAZARLAMA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Boş Alan Bırakmayınız !!", "- UĞUR PAZARLAMA -", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}