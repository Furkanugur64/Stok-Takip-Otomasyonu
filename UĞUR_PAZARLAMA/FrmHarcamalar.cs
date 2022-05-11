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
    public partial class FrmHarcamalar : DevExpress.XtraEditors.XtraForm
    {
        public FrmHarcamalar()
        {
            InitializeComponent();
        }
        UgurPazarlamaEntities db = new UgurPazarlamaEntities();
        void Harcama_Listesi()
        {
            var depo = (from x in db.TBL_DEPO
                        select new
                        {
                            x.ID,
                            x.BARKODNO,
                            x.AD,
                            x.TARIH,
                            x.ADET,
                            x.ALISFIYAT,
                            x.TOPLAM
                        }).ToList();
            gridControl1.DataSource = depo;
        }

        private void FrmHarcamalar_Load(object sender, EventArgs e)
        {
            Harcama_Listesi();
        }

        private void pictureEdit3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}