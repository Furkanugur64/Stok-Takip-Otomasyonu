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
    public partial class FrmYöneticiGiris : DevExpress.XtraEditors.XtraForm
    {
        public FrmYöneticiGiris()
        {
            InitializeComponent();
        }
        public string tc;
        UgurPazarlamaEntities db = new UgurPazarlamaEntities();
        void Kasa_hesapla()
        {
            var kasa = db.TBL_SATIS.Sum(x => x.TOPLAM).GetValueOrDefault();
            LblKasa.Text = kasa + " TL";
        }
        private void BtnUrunListesi_Click(object sender, EventArgs e)
        {
            FrmPersoneller u = new FrmPersoneller();
            u.ShowDialog();
        }
        void yönetici_bilgi()
        {
            var adm = db.TBL_PERSONELLER.Where(x=>x.TC==tc).FirstOrDefault();
            LblAdSoyad.Text = adm.AD + " " + adm.SOYAD;
            LblStatu.Text = adm.TBL_STATULER.STATUAD;
            pictureEdit1.Image = Image.FromFile(adm.FOTOGRAF);
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
                                       x.TBL_STATULER.STATUAD,
                                       x.MAIL,
                                       x.TELEFON,
                                       x.FOTOGRAF
                                   }).ToList();
                gridControl1.DataSource = personeller;
            
        }
        void Stok_Sayisi()
        {
            var stok = db.TBL_URUN.Sum(x => x.STOK).GetValueOrDefault();
            LblToplamStok.Text = stok + " Adet";
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
            FrmMarkalar m = new FrmMarkalar();
            m.ShowDialog();
        }

        private void BtnAlımListesi_Click(object sender, EventArgs e)
        {
            FrmHarcamalar h = new FrmHarcamalar();
            h.ShowDialog();
        }

        private void BtnSatisYap_Click(object sender, EventArgs e)
        {
            FrmSatis s = new FrmSatis();
            s.ShowDialog();
            Stok_Sayisi();
        }

        private void BtnMarkaEkle_Click(object sender, EventArgs e)
        {
            FrmMarkaEkle m = new FrmMarkaEkle();
            m.ShowDialog();

        }

        private void BtnMüsteriEkle_Click(object sender, EventArgs e)
        {
            FrmMüsteriEkle m = new FrmMüsteriEkle();
            m.ShowDialog();
        }

        private void BtnUrunEkle_Click(object sender, EventArgs e)
        {
            FrmUrunEkle urn = new FrmUrunEkle();
            urn.ShowDialog();
            Stok_Sayisi();
        }

        private void BtnKategoriEkle_Click(object sender, EventArgs e)
        {
            FrmKategoriEkle kat = new FrmKategoriEkle();
            kat.ShowDialog();
        }

        private void pictureEdit3_EditValueChanged(object sender, EventArgs e)
        {
            FrmGirisYap gr = new FrmGirisYap();
            gr.Show();
            this.Hide();
              
        }

        private void pictureEdit3_Click(object sender, EventArgs e)
        {
            FrmGirisYap gr = new FrmGirisYap();
            gr.Show();
            this.Close();
        }

        private void FrmYöneticiGiris_Load(object sender, EventArgs e)
        {
            Kasa_hesapla();
            yönetici_bilgi();
            Personel_Listesi();
            Stok_Sayisi();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            FrmBilgiGüncelle bilgiGüncelle = new FrmBilgiGüncelle();
            bilgiGüncelle.tcno = tc;
            bilgiGüncelle.ShowDialog();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            FrmUrun fr = new FrmUrun();
            fr.ShowDialog();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            FrmDepo fr = new FrmDepo();
            fr.ShowDialog();
            Stok_Sayisi();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            LblTarih.Text = DateTime.Now.ToLongDateString();
            LblSaat.Text = DateTime.Now.ToLongTimeString();
        }
    }
}