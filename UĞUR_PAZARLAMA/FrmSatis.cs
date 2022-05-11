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
    public partial class FrmSatis : DevExpress.XtraEditors.XtraForm
    {
        public FrmSatis()
        {
            InitializeComponent();
        }
        UgurPazarlamaEntities db = new UgurPazarlamaEntities();
        void Urun_Listesi()
        {
            var urunler = (from x in db.TBL_URUN
                           select new
                           {
                               x.BARKODNO,
                               x.AD,
                               MARKA = x.TBL_MARKALAR.AD,
                               KATEGORI = x.TBL_KATEGORILER.AD,
                               x.STOK,
                               x.ALISFIYAT,
                               x.SATISFIYAT,
                               x.FOTOGRAF,
                               x.OZELLIKLER

                           }).ToList();
            gridControl1.DataSource = urunler;
        }
        void Kategori_Getir()
        {
            var kat = (from x in db.TBL_KATEGORILER
                       select new
                       {
                           x.ID,
                           x.AD
                       }).ToList();
            CmbKategori .Properties.DataSource = kat;
            CmbKategori.Properties.DisplayMember = "AD";
            CmbKategori.Properties.ValueMember = "ID";
        }
        void Personeller()
        {
            var per = (from x in db.TBL_PERSONELLER
                       select new
                       {
                           x.ID,
                           Ad = x.AD + " " + x.SOYAD,
                       }).ToList();
            CmbPersonel.Properties.DataSource = per;
            CmbPersonel.Properties.DisplayMember = "Ad";
            CmbPersonel.Properties.ValueMember = "ID";
         }
        void Müsteriler()
        {
            var per = (from x in db.TBL_MUSTERILER
                       select new
                       {
                           x.ID,
                           Ad = x.AD + " " + x.SOYAD,
                       }).ToList();
            CmbMusteri.Properties.DataSource = per;
            CmbMusteri.Properties.DisplayMember = "Ad";
            CmbMusteri.Properties.ValueMember = "ID";

        }
        private void FrmSatis_Load(object sender, EventArgs e)
        {
            Urun_Listesi();
            Personeller();
            Müsteriler();
            Kategori_Getir();
        }

        private void Cmbpersonel_EditValueChanged(object sender, EventArgs e)
        {
            var y =byte.Parse( CmbPersonel.EditValue.ToString());
            var per = db.TBL_PERSONELLER.Where(x => x.ID == y).FirstOrDefault();
            TxtPerTelefon.Text = per.TELEFON;
            TxtTc.Text = per.TC;
            TxtPersonel.Text = CmbPersonel.Text;
            TxtYetki.Text = per.TBL_STATULER.STATUAD;
            TxtPersonel.Text = CmbPersonel.Text;
            LblPersonel.ForeColor = Color.Blue;

        }

        private void MskAdSoyad_EditValueChanged(object sender, EventArgs e)
        {
            var y = byte.Parse(CmbMusteri.EditValue.ToString());
            var per = db.TBL_MUSTERILER.Where(x => x.ID == y).FirstOrDefault();
            TxtTelefon.Text = per.TELEFON;
            TxtMüsteri.Text = CmbMusteri.Text;
            TxtIl.Text = per.IL;
            TxtIlce.Text = per.ILCE;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (TxtMüsteri.Text == "")
            {
                LblMusteri.ForeColor = Color.Red;
            }
            if (TxtPersonel.Text == "")
            {
                LblPersonel.ForeColor = Color.Red;
            }
            if (TxtUrun.Text == "")
            {
                LblUrun.ForeColor = Color.Red;
            }
            if (TxtTarih.Text == "")
            {
                LblSAtTarih.ForeColor = Color.Red;
            }
            if (TxtAdet.Text=="")
            {
                LblAdet.ForeColor = Color.Red;
            }

            if (LblMusteri.ForeColor == Color.Blue && LblPersonel.ForeColor == Color.Blue && LblUrun.ForeColor == Color.Blue && LblSAtTarih.ForeColor == Color.Blue && LblAdet.ForeColor == Color.Blue)
            {
                TBL_SATIS s = new TBL_SATIS();
                s.URUN =short.Parse( TxtBarkodNo.Text);
                s.MUSTERI = byte.Parse(CmbMusteri.EditValue.ToString());
                s.PERSONEL = byte.Parse(CmbPersonel.EditValue.ToString());
                s.TARIH = TxtTarih.Text;
                s.ADET = short.Parse(TxtAdet.Text);
                s.FIYAT =decimal.Parse( TxtFiyat.Text);
                s.TOPLAM = decimal.Parse(LblToplamTutar.Text);
                db.TBL_SATIS.Add(s);
                db.SaveChanges();
                MessageBox.Show("UĞUR PAZARLAMA'yı tercih Ettiğiniz İçin Teşekkür Ederiz \n\n TUTAR=" + LblToplamTutar.Text);
                Temizle();
                LblToplamTutar.Text = " 0 TL";
                Urun_Listesi();
            }
            else
            {
                MessageBox.Show("Lütfen Kırmızı Alanları Kontrol Ediniz !!", " UĞUR PAZARLAMA ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            if (TxtFiyat.Text!="" && TxtAdet.Text!="")
            {
                decimal fiyat = decimal.Parse(TxtFiyat.Text);
                decimal adet = decimal.Parse(TxtAdet.Text);
                decimal toplam = fiyat * adet;
                LblToplamTutar.Text = toplam.ToString();

            }
        }
        void Temizle()
        {
            CmbKategori.Text = "";
            TxtTc.Text = "";
            TxtTelefon.Text = "";
            TxtYetki.Text = "";
            TxtPerTelefon.Text = "";
            TxtIl.Text = "";
            TxtIlce.Text = "";
            CmbMusteri.Text = "";
            TxtMüsteri.Text = "";
            TxtPersonel.Text = "";
            TxtUrun.Text = "";
            TxtBarkodNo.Text = "";
            TxtUrunAdi.Text = "";
            TxtFiyat.Text = "";
            TxtStok.Text = "";
            TxtOzellik.Text = "";
            pictureEdit1.Image = null;
            TxtTarih.Text = "";
            TxtAdet.Text = "";
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void TxtÜrünAdıAra_EditValueChanged(object sender, EventArgs e)
        {
            var urunler = (from x in db.TBL_URUN.Where(x => x.AD.Contains(TxtÜrünAdıAra.Text))
                           select new
                           {
                               x.BARKODNO,
                               x.AD,
                               MARKA = x.TBL_MARKALAR.AD,
                               KATEGORI = x.TBL_KATEGORILER.AD,
                               x.STOK,
                               x.ALISFIYAT,
                               x.SATISFIYAT,
                               x.FOTOGRAF,
                               x.OZELLIKLER

                           }).ToList();
            gridControl1.DataSource = urunler;
        }

        private void labelControl26_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (CmbMarka.Text == "")
            {
                var urunler = (from x in db.TBL_URUN.Where(x => x.TBL_KATEGORILER.AD.Contains(CmbKategori.Text))
                               select new
                               {
                                   x.BARKODNO,
                                   x.AD,
                                   MARKA = x.TBL_MARKALAR.AD,
                                   KATEGORI = x.TBL_KATEGORILER.AD,
                                   x.STOK,
                                   x.ALISFIYAT,
                                   x.SATISFIYAT,
                                   x.FOTOGRAF,
                                   x.OZELLIKLER

                               }).ToList();
                gridControl1.DataSource = urunler;

            }
            else
            {
                var urunler = (from x in db.TBL_URUN.Where(x => x.TBL_MARKALAR.AD.Contains(CmbMarka.Text))
                               select new
                               {
                                   x.BARKODNO,
                                   x.AD,
                                   MARKA = x.TBL_MARKALAR.AD,
                                   KATEGORI = x.TBL_KATEGORILER.AD,
                                   x.STOK,
                                   x.ALISFIYAT,
                                   x.SATISFIYAT,
                                   x.FOTOGRAF,
                                   x.OZELLIKLER

                               }).ToList();
                gridControl1.DataSource = urunler;
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
          TxtStok.Text = gridView1.GetFocusedRowCellValue("STOK").ToString();
            TxtFiyat.Text = gridView1.GetFocusedRowCellValue("SATISFIYAT").ToString();
           TxtUrun.Text = gridView1.GetFocusedRowCellValue("AD").ToString();
            TxtOzellik.Text = gridView1.GetFocusedRowCellValue("OZELLIKLER").ToString();
            pictureEdit1.Image = Image.FromFile(gridView1.GetFocusedRowCellValue("FOTOGRAF").ToString());
            LblUrun.ForeColor = Color.Blue;
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

        private void CmbMusteri_EditValueChanged(object sender, EventArgs e)
        {
            var y = byte.Parse(CmbMusteri.EditValue.ToString());
            var per = db.TBL_MUSTERILER.Where(x => x.ID == y).FirstOrDefault();
            TxtTelefon.Text = per.TELEFON;
            TxtIl.Text = per.IL;
            TxtIlce.Text = per.ILCE;
            TxtMüsteri.Text = CmbMusteri.Text;
            LblMusteri.ForeColor = Color.Blue;

        }

        private void TxtAdet_EditValueChanged(object sender, EventArgs e)
        {
            if (TxtAdet.Text!="")
            {
                LblAdet.ForeColor = Color.Blue;

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            LblTarih.Text = DateTime.Now.ToLongDateString();
            LblSaat.Text = DateTime.Now.ToLongTimeString();
        }
    }
}