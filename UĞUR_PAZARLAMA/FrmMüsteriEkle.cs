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
    public partial class FrmMüsteriEkle : DevExpress.XtraEditors.XtraForm
    {
        public FrmMüsteriEkle()
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
        private void FrmMüsteriEkle_Load(object sender, EventArgs e)
        {
            Il_Getir();
        }

        private void CmbIl_EditValueChanged(object sender, EventArgs e)
        {
            var y =byte.Parse( CmbIl.EditValue.ToString());
           
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

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            if (TxtAd.Text != "" && TxtSoyad.Text != "" && TxtMail.Text != "" && TxtAdres.Text != "" && TxtTelefon.Text != "" && CmbIl.Text != "" && CmbIlce.Text != "")
            {
                TBL_MUSTERILER m = new TBL_MUSTERILER();
                m.AD = TxtAd.Text;
                m.SOYAD = TxtSoyad.Text;
                m.TELEFON = TxtTelefon.Text;
                m.MAIL = TxtMail.Text;
                m.IL = CmbIl.Text;
                m.ILCE = CmbIlce.Text;
                m.ADRES = TxtAdres.Text;
                db.TBL_MUSTERILER.Add(m);
                db.SaveChanges();
                MessageBox.Show("Müşteri Başarılı Bir Şekilde Eklendi", "UĞUR PAZARLAMA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Bilgileri Kontrol Ediniz", "UĞUR PAZARLAMA", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void pictureEdit3_EditValueChanged(object sender, EventArgs e)
        {
          
        }

        private void pictureEdit3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}