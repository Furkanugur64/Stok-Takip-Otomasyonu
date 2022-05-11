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
    public partial class FrmPersonelGiris : DevExpress.XtraEditors.XtraForm
    {
        public FrmPersonelGiris()
        {
            InitializeComponent();
        }
        public string tc;
        UgurPazarlamaEntities db = new UgurPazarlamaEntities();
        private void BtnUrunListesi_Click(object sender, EventArgs e)
        {
            FrmUrun urn = new FrmUrun();
            urn.ShowDialog();
        }
        void Satis_Listesi()
        {
            var satislar = (from x in db.TBL_SATIS
                            select new
                            {
                                x.ID,
                                x.TBL_URUN.AD,
                                MUSTERI = x.TBL_MUSTERILER.AD + " " + x.TBL_MUSTERILER.SOYAD,
                                PERSONEL = x.TBL_PERSONELLER.AD + " " + x.TBL_PERSONELLER.SOYAD,
                                x.TARIH,
                                x.ADET,
                                x.FIYAT,
                                x.TOPLAM
                            }).ToList();
            gridControl1.DataSource = satislar;
        }
        void Kasa_hesapla()
        {
            var kasa = db.TBL_SATIS.Sum(x => x.TOPLAM).GetValueOrDefault();
            LblKasa.Text = kasa + " TL";
        }
        void Personel_Bilgi()
        {
            var per = db.TBL_PERSONELLER.Where(x => x.TC == tc).FirstOrDefault();
            LblAdSoyad.Text = per.AD + " " + per.SOYAD;
            LblStatu.Text = per.TBL_STATULER.STATUAD;
            pictureEdit1.Image = Image.FromFile(per.FOTOGRAF);
        }
        private void BtnIstatistikler_Click(object sender, EventArgs e)
        {
            FrmIstatistikler ıst = new FrmIstatistikler();
            ıst.ShowDialog();
        }

        private void BtnKategoriListesi_Click(object sender, EventArgs e)
        {
            FrmKategoriler kat = new FrmKategoriler();
            kat.ShowDialog();
        }

        private void BtnMüsteriListesi_Click(object sender, EventArgs e)
        {
            FrmMüsteriler m = new FrmMüsteriler();
            m.ShowDialog();
        }

        private void BtnSatisListesi_Click(object sender, EventArgs e)
        {
            FrmSatisListesi s = new FrmSatisListesi();
            s.ShowDialog();
        }

        private void BtnMarkaListesi_Click(object sender, EventArgs e)
        {
            FrmMarkalar marka = new FrmMarkalar();
            marka.ShowDialog();
        }

        private void BtnSatisYap_Click(object sender, EventArgs e)
        {
            FrmSatis s = new FrmSatis();
            s.ShowDialog();
            Kasa_hesapla();
        }

        private void BtnMarkaEkle_Click(object sender, EventArgs e)
        {
            FrmMarkaEkle ma = new FrmMarkaEkle();
            ma.ShowDialog();
        }

        private void BtnMüsteriEkle_Click(object sender, EventArgs e)
        {
            FrmMüsteriEkle me = new FrmMüsteriEkle();
            me.ShowDialog();
        }

        private void BtnUrunEkle_Click(object sender, EventArgs e)
        {
            FrmUrunEkle ur = new FrmUrunEkle();
            ur.ShowDialog();
        }

        private void BtnKategoriEkle_Click(object sender, EventArgs e)
        {
            FrmKategoriEkle kat = new FrmKategoriEkle();
            kat.ShowDialog();
        }

        private void pictureEdit3_Click(object sender, EventArgs e)
        {
            FrmGirisYap gr = new FrmGirisYap();
            gr.Show();
            this.Hide();
        }

        private void FrmPersonelGiris_Load(object sender, EventArgs e)
        {
            Personel_Bilgi();
            Kasa_hesapla();
            Satis_Listesi();
        }

        private void BtnBilgilerimiGüncelle_Click(object sender, EventArgs e)
        {
            FrmBilgiGüncelle bilgiGüncelle = new FrmBilgiGüncelle();
            bilgiGüncelle.tcno = tc;
            bilgiGüncelle.ShowDialog();
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit1.Checked)
            {
                var satislar = (from x in db.TBL_SATIS.Where(x=>x.TBL_PERSONELLER.TC==tc)
                                select new
                                {
                                    x.ID,
                                    x.TBL_URUN.AD,
                                    MUSTERI = x.TBL_MUSTERILER.AD + " " + x.TBL_MUSTERILER.SOYAD,
                                    PERSONEL = x.TBL_PERSONELLER.AD + " " + x.TBL_PERSONELLER.SOYAD,
                                    x.TARIH,
                                    x.ADET,
                                    x.FIYAT,
                                    x.TOPLAM
                                }).ToList();
                gridControl1.DataSource = satislar;
            }
            else
            {
                Satis_Listesi();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            LblTarih.Text = DateTime.Now.ToLongDateString();
            LblSaat.Text = DateTime.Now.ToLongTimeString();
        }
    }
}