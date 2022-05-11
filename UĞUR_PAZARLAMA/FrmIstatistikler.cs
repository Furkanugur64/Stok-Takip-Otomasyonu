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
    public partial class FrmIstatistikler : DevExpress.XtraEditors.XtraForm
    {
        public FrmIstatistikler()
        {
            InitializeComponent();
        }
        UgurPazarlamaEntities db = new UgurPazarlamaEntities();
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmIstatistikler_Load(object sender, EventArgs e)
        {
            var toplamurun = db.TBL_URUN.Count().ToString();
            var toplampersonel = db.TBL_PERSONELLER.Count().ToString();
            var toplammüsteri = db.TBL_MUSTERILER.Count().ToString();
            var toplamkategori = db.TBL_KATEGORILER.Count().ToString();
            var toplammarka = db.TBL_MARKALAR.Count().ToString();
            var toplamsatis = db.TBL_SATIS.Count().ToString();
            var enpahalıürün = db.TBL_URUN.OrderByDescending(x => x.SATISFIYAT).Select(x => x.AD).FirstOrDefault();
            var enucuzürün = db.TBL_URUN.OrderBy(x => x.SATISFIYAT).Select(x => x.AD).FirstOrDefault();
            var toplamstok = db.TBL_URUN.Sum(x => x.STOK).GetValueOrDefault();
            var kritikurun = db.TBL_URUN.Where(x => x.STOK <= 15).Count().ToString();
            var kacil = db.TBL_MUSTERILER.OrderBy(x => x.IL).Distinct().Count().ToString();
            var topalim = db.TBL_DEPO.Count().ToString();
            var urunalimtoptutar = db.TBL_DEPO.Sum(x => x.TOPLAM).GetValueOrDefault();
            var kasa = db.TBL_SATIS.Sum(x => x.TOPLAM).GetValueOrDefault();
            var ensonurun = db.TBL_DEPO.OrderByDescending(x => x.ID).Select(c => c.AD).FirstOrDefault();
            var ensonsaturun = db.TBL_SATIS.OrderByDescending(x => x.ID).Select(z => z.TBL_URUN.AD).FirstOrDefault();

            LblToplamUrun.Text = toplamurun;
            LblToplamPersonel.Text = toplampersonel;
            LblToplamMusteri.Text = toplammüsteri;
            LblToplamKategori.Text = toplamkategori;
            LblToplamMarka.Text = toplammarka;
            LblToplamSatis.Text = toplamsatis;
            LblEnPahalıUrun.Text = enpahalıürün;
            LblEnUcuzUrun.Text = enucuzürün;
            LblToplamStok.Text = toplamstok.ToString();
            LblKritikSeviye.Text = kritikurun;
            LblKaçFarkliil.Text = kacil;
            LblToplamAlim.Text = topalim;
            LblToplamAlimTutar.Text = urunalimtoptutar.ToString();
            LblKasa.Text = kasa.ToString();
            LblEnsonurun.Text = ensonurun;
            LblEnsonSaturun.Text = ensonsaturun.ToString();
            

        }
    }
}