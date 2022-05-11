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
    public partial class FrmDepo : DevExpress.XtraEditors.XtraForm
    {
        public FrmDepo()
        {
            InitializeComponent();
        }
        UgurPazarlamaEntities db = new UgurPazarlamaEntities();
        void Urun_Listesi()
        {
            var urunler = (from x in db.TBL_URUN
                           select new
                           {
                               x.AD,
                           }).ToList();
            CmbUrun.Properties.DataSource = urunler;
            CmbUrun.Properties.DisplayMember = "AD";
        }
       
        

        private void FrmDepo_Load(object sender, EventArgs e)
        {
            TxtTarih.Text = DateTime.Now.ToShortDateString();
            
            Urun_Listesi();
        }

        private void CmbUrun_EditValueChanged(object sender, EventArgs e)
        {
            TxtAdet.Text = "0";
            
            LblTutar.Text = "0 TL";
            var urun = db.TBL_URUN.Where(x => x.AD == CmbUrun.Text).FirstOrDefault();
            TxtBarkodNo.Text = urun.BARKODNO.ToString();
            TxtAlisFiyat.Text = urun.ALISFIYAT.ToString();

        }
        decimal toplam;
        private void TxtAdet_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void BtnIptal_Click(object sender, EventArgs e)
        {
            TxtBarkodNo.Text = "";
            TxtTarih.Text = "";
            TxtAdet.Text = "0";
            TxtAlisFiyat.Text = "";
            CmbUrun.Text = "";
        }
        void depoya_ekle()
        {            
            TBL_DEPO dep = new TBL_DEPO();
            dep.BARKODNO =short.Parse( TxtBarkodNo.Text);
            dep.AD = CmbUrun.Text;
            dep.TARIH = TxtTarih.Text;
            dep.ADET =byte.Parse( TxtAdet.Text);
            dep.ALISFIYAT = decimal.Parse(TxtAlisFiyat.Text);
            dep.TOPLAM = decimal.Parse(toplam.ToString());
            db.TBL_DEPO.Add(dep);
            db.SaveChanges();
            MessageBox.Show("Ürün Stoğu Eklendi", "UĞUR PAZARLAMA", MessageBoxButtons.OK, MessageBoxIcon.Information);
           
        }
        private void BtnUrunAlimiYap_Click(object sender, EventArgs e)
        {
            if (CmbUrun.Text != "" && TxtTarih.Text != "  .  ." && TxtAdet.Text != "0")
            {
                depoya_ekle();

            }
            else
            {
                MessageBox.Show("Lütfen Değerleri Kontrol Ediniz !!", "UĞUR PAZARLAMA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureEdit3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (TxtAlisFiyat.Text!="" && TxtAdet.Text!="")
            {
                decimal adet = decimal.Parse(TxtAlisFiyat.Text);
                decimal alisfiyat = decimal.Parse(TxtAdet.Text);
                toplam = adet * alisfiyat;
                
                LblTutar.Text = toplam+" TL";
                
            }
            else
            {
                MessageBox.Show("Ürün Seçin ve adet bilgisi girin !!", "UĞUR PAZARLAMA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}