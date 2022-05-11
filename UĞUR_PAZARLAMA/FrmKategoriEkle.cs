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
    public partial class FrmKategoriEkle : DevExpress.XtraEditors.XtraForm
    {
        public FrmKategoriEkle()
        {
            InitializeComponent();
        }
        UgurPazarlamaEntities db = new UgurPazarlamaEntities();

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            if (TxtKategoriAdi.Text!="")
            {
                TBL_KATEGORILER t = new TBL_KATEGORILER();
                t.AD = TxtKategoriAdi.Text;
                db.TBL_KATEGORILER.Add(t);
                db.SaveChanges();
                MessageBox.Show("Kategori Başarılı Bir Şekilde Eklendi", "UĞUR PAZARLAMA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Ad Alanı Boş Geçilemez", "UĞUR PAZARLAMA", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        

        private void pictureEdit3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}