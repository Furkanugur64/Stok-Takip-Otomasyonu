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
    public partial class FrmSatisListesi : DevExpress.XtraEditors.XtraForm
    {
        public FrmSatisListesi()
        {
            InitializeComponent();
        }
        UgurPazarlamaEntities db = new UgurPazarlamaEntities();
        private void pictureEdit3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        void Satis_Listesi()
        {
            var satislar = (from x in db.TBL_SATIS
                            select new
                            {
                                x.ID,
                                x.TBL_URUN.AD,
                                MUSTERI=   x.TBL_MUSTERILER.AD + " "+x.TBL_MUSTERILER.SOYAD,
                                PERSONEL=x.TBL_PERSONELLER.AD+" "+x.TBL_PERSONELLER.SOYAD,
                                x.TARIH,
                                x.ADET,
                                x.FIYAT,
                                x.TOPLAM
                            }).ToList();
            gridControl1.DataSource = satislar;
        }
        private void FrmSatisListesi_Load(object sender, EventArgs e)
        {
            Satis_Listesi();
        }

        private void TxtPersonel_EditValueChanged(object sender, EventArgs e)
        {
            var satislar = (from x in db.TBL_SATIS.Where(x => x.TBL_PERSONELLER.AD.Contains(TxtPersonel.Text))
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

        private void TxtMüsteri_EditValueChanged(object sender, EventArgs e)
        {
            var satislar = (from x in db.TBL_SATIS.Where(x => x.TBL_MUSTERILER.AD.Contains(TxtMüsteri.Text))
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

        
    }
}