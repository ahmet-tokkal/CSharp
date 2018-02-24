using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjeOdevi
{
    public partial class frmDiyalog : Form
    {
        public frmDiyalog()
        {
            InitializeComponent();
        }

        private void btnGeri_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmDiyalog_Load(object sender, EventArgs e)
        {
            
        }
        public void Doldur(Ders drs,string bolum,string sinif,string donem)
        {
            lblDersAd.Text = drs.DersAd;
            lblDersKodu.Text = drs.DersKodu;
            lblDersSaat.Text = drs.DersSaat.ToString();
            lblDersKredi.Text = drs.DersKredi.ToString();
            lblDersAkts.Text = drs.DersAkts.ToString();
            lblDersBolum.Text = bolum;
            lblDersSinif.Text = sinif;
            lblDersDonem.Text = donem;
            lblDerslikAd.Text = drs.DerslikAd;

        }
    }
}
