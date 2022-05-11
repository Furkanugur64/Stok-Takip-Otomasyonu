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
    public partial class FrmUrunEkle : DevExpress.XtraEditors.XtraForm
    {
        public FrmUrunEkle()
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
        private void FrmUrunEkle_Load(object sender, EventArgs e)
        {
            Kategori_Getir();
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
        private void CmbKategori_EditValueChanged(object sender, EventArgs e)
        {
            var y = int.Parse(CmbKategori.EditValue.ToString());

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

        private void pictureEdit3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            if (TxtUrunAdi.Text != "" && TxtFiyat.Text != "" && TxtOzellik.Text != "" && TxtStok.Text != "" && CmbKategori.Text != "" && CmbMarka.Text != "")
            {
                alıshesapla();
                string foto = @"D:\Visual Studio 2019\Projeler\Stok_Takip_Otomasyonu\Stok_Takip_Otomasyonu\bin\Debug\Ürünler\Yok.png";
                TBL_URUN urn = new TBL_URUN();
                urn.AD = TxtUrunAdi.Text;
                urn.SATISFIYAT = decimal.Parse(TxtFiyat.Text);
               urn.ALISFIYAT = alıs;
                urn.MARKA = byte.Parse(CmbMarka.EditValue.ToString());
                urn.KATEGORI = byte.Parse(CmbKategori.EditValue.ToString());
                urn.STOK = byte.Parse(TxtStok.Text);
                urn.FOTOGRAF = foto;
                urn.OZELLIKLER = TxtOzellik.Text;
                db.TBL_URUN.Add(urn);
                db.SaveChanges();
                
                MessageBox.Show("Ürün Başarılı Bir Şekilde Eklendi", "UĞUR PAZARLAMA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
                MessageBox.Show("Boş Alan Bırakmayınız !!", "- UĞUR PAZARLAMA -", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}