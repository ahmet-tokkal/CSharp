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
    public partial class frmAnaSayfa : Form
    {
        public string secilenFakulteId,bolumId,sinifId,donemId;
        public string bolumAd, sınıfAd, donemAd;
        VeriTabani vt = new VeriTabani("DESKTOP-G1TQMPU\\SQLEXPRESS", "DersProgrami");
        public DataTable dersProgramıTablosu = new DataTable();


        public List<string> bolumler = new List<string>();
        public List<string> bolumIDler = new List<string>();
        public List<Ders> dersler = new List<Ders>();

        public frmAnaSayfa()
        {
            InitializeComponent();
            btnBolum.Enabled = false;
            btnSinif.Enabled = false;
            btnDonem.Enabled = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnFakulte_Click(object sender, EventArgs e)
        {
            pnlFakulte.Visible = true;
            pnlBolum.Visible = false;
            pnlSinif.Visible = false;
            pnlDonem.Visible = false;
            pnlDersProgrami.Visible = false;
        }

        private void btnBolum_Click(object sender, EventArgs e)
        {
            pnlFakulte.Visible = false;
            pnlBolum.Visible = true;
            pnlSinif.Visible = false;
            pnlDonem.Visible = false;
            pnlDersProgrami.Visible = false;
        }

        private void btnSinif_Click(object sender, EventArgs e)
        {
            pnlFakulte.Visible = false;
            pnlBolum.Visible = false;
            pnlSinif.Visible = true;
            pnlDonem.Visible = false;
            pnlDersProgrami.Visible = false;
        }

        private void btnDonem_Click(object sender, EventArgs e)
        {
            pnlFakulte.Visible = false;
            pnlBolum.Visible = false;
            pnlSinif.Visible = false;
            pnlDonem.Visible = true;
            pnlDersProgrami.Visible = false;
        }

       

        private void btnDersProgrami_Click(object sender, EventArgs e)
        {
            pnlFakulte.Visible = false;
            pnlBolum.Visible = false;
            pnlSinif.Visible = false;
            pnlDonem.Visible = false;
            pnlDersProgrami.Visible = true;

            //Dersleri ve Ders programı şablonunu veritabanından çekip tablelara atama
            string dersSorgu = "Select * from tblDers where BolumID =" + bolumId + " and SınıfID=" + sinifId + " and DonemID=" + donemId;
            string programSorgu= "Select * from tblDersProgram";
            DataTable dersTablosu = new DataTable();

            dersTablosu = vt.TabloyaCek(dersSorgu);
            lblDersProgTanim.Text = "Yalova Üniversitesi "+bolumAd+" Bölümü "+sınıfAd+" "+donemAd+" Ders Programı";
            dersProgramıTablosu = vt.TabloyaCek(programSorgu);
            //datagridview'e tablo gönderiliyor
            dataGridView1.DataSource = dersProgramıTablosu;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dataGridView1.Rows[i].Cells[0].Style.BackColor = Color.RoyalBlue;

                dataGridView1.Rows[i].Cells[0].Style.Font = new Font("Comic Sans MS", 12, FontStyle.Bold);
                dataGridView1.Rows[i].Cells[5].Style.BackColor = ColorTranslator.FromHtml("#C63B3B");
            }
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.RoyalBlue;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Comic Sans MS", 8, FontStyle.Bold);

            for (int i=0; i<dataGridView1.Columns.Count;i++)
            {
                dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            dataGridView1.EnableHeadersVisualStyles = false;
            //Tablodaki dersler Ders sınıfına aktarılıyor
            TabloyuDerseAktar(dersTablosu);
            //Ders programı oluşturuluyor.
            TabloyuOlustur();
        }


        private void Fakulte_Click(object sender,EventArgs e)
        {
            btnBolum.Enabled = true;
            pnlBolum.Controls.Clear();
            lblBolumTanim.Visible = true;

            Button btnFakulte = (Button)sender;
            secilenFakulteId = btnFakulte.Name[btnFakulte.Name.Length - 1].ToString();
            pnlFakulte.Visible = false;
            pnlBolum.Refresh();
            //Bölümler ve bölümıdlerin çekilmesi
            bolumler = vt.BelirliVeriCek("tblBolum", "BolumAd", "FakulteID", secilenFakulteId);
            bolumIDler = vt.BelirliVeriCek("tblBolum", "BolumID", "FakulteID", secilenFakulteId);
            //bölüm butonlarının otomatik oluşması
            int btnKonumX = 70;
            int btnKonumY = 80;
            for (int i = 0; i < bolumler.Count; i++)
            {
                Button btn = new Button();
                btn.Name = bolumIDler[i];
                btn.Text = bolumler[i];
                btn.Font = new Font("Berlin Sans FB", 18);
                btn.BackColor = ColorTranslator.FromHtml("#00D900");
                btn.ForeColor = Color.White;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.Size = new Size(470, 113);
                if (btnKonumX + 490 > 1770)
                {
                    btnKonumX = 70;
                    btnKonumY += 133;
                }

                btn.Location = new Point(btnKonumX,btnKonumY);
                btnKonumX += 490;
                btn.Click += new EventHandler(Bolum_Click);
                pnlBolum.Controls.Add(btn);
            }
            pnlBolum.Visible = true;
        }
        private void Bolum_Click(object sender, EventArgs e)
        {
            lblBolumTanim.Visible = false;
            btnSinif.Enabled = true;
            Button btn = (Button)sender;
            bolumId = btn.Name;
            bolumAd = btn.Text;
            lblBolumTanim.Visible = false;
            pnlBolum.Visible = false;
            pnlSinif.Visible = true;
        }
        private void Sinif_Click(object sender, EventArgs e)
        {
            btnDonem.Enabled = true;
            Button btn = (Button)sender;
            sinifId = btn.Name[btn.Name.Length - 1].ToString();
            sınıfAd = btn.Text;
            pnlSinif.Visible = false;
            pnlDonem.Visible = true;
        }
        private void Donem_Click(object sender, EventArgs e)
        {
            lblBolumTanim.Visible = false;
            Button btn = (Button)sender;
            donemId = btn.Name[btn.Name.Length - 1].ToString();
            donemAd = btn.Text;
            pnlDonem.Visible = false;
            pnlDersProgrami.Visible = true;
            btnDersProgrami.Enabled = true;
            btnDersProgrami_Click(sender, e);
        }
        public void TabloyuOlustur()
        {
            int k = 0;
            Random rnd = new Random();
            List<string> cıkanlar = new List<string>();

            int a, b;
            //derslerin ders programına eklenmesi
            while (k < dersler.Count)
            {
                do
                {
                    b = rnd.Next(0, 5);
                    a = rnd.Next(1, 11);                   
                } while (cıkanlar.Contains(b.ToString() + a.ToString()) || a==5);
                cıkanlar.Add(b.ToString() + a.ToString());
                for (int i = 0; i < dersler[k].DersSaat && a<11; i++)
                {
                    dataGridView1.Rows[b].Cells[a].Value = dersler[k].DersKodu + Environment.NewLine + dersler[k].DersAd+Environment.NewLine+dersler[k].DerslikAd;
                    dataGridView1.Rows[b].Cells[a].Tag = dersler[k].DersID;
                    if (!cıkanlar.Contains(b.ToString() + (a + 1).ToString()) && a + 1 < 11)
                    {
                        if (a + 1 == 5)
                            a += 2;
                        else
                            a++;
                        cıkanlar.Add(b.ToString() + a.ToString());
                    }
                    else
                    {
                        do
                        {
                            b = rnd.Next(0, 5);
                            a = rnd.Next(1, 11);
                        } while (cıkanlar.Contains(b.ToString() + a.ToString()) || a == 5);
                        cıkanlar.Add(b.ToString() + a.ToString());
                    }
                }
                k++;
            }
        }
        public void TabloyuDerseAktar(DataTable dt)
        {
            dersler.Clear();
            //dersler tablosunun ders tipine çevrilmesi
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Ders ders = new Ders();
                ders.DersID = Convert.ToInt32(dt.Rows[i]["DersID"]);
                ders.DersKodu = dt.Rows[i]["DersKodu"].ToString();
                ders.DersAd = dt.Rows[i]["DersAd"].ToString();
                ders.DersSaat = Convert.ToInt32(dt.Rows[i]["DersSaat"]);
                ders.DersKredi = Convert.ToInt32(dt.Rows[i]["DersKredi"]);
                ders.DersAkts = Convert.ToInt32(dt.Rows[i]["DersAkts"]);
                ders.BolumID = Convert.ToInt32(dt.Rows[i]["BolumID"]);
                ders.SinifID = Convert.ToInt32(dt.Rows[i]["SınıfID"]);
                ders.DonemID = Convert.ToInt32(dt.Rows[i]["DonemID"]);
                ders.DerslikAd = dt.Rows[i]["DerslikAd"].ToString();
                dersler.Add(ders);

            }
        }
        public void Hucre_Click(Object sender, DataGridViewCellMouseEventArgs e )
        {
            //hücreye tıklandığında ders bilgilerinin diyalog oalrak gösterilmesi
            DataGridView dgv = (DataGridView)sender;
            frmDiyalog frmDia = new frmDiyalog();
            Ders seciliDers = new Ders();
            foreach (var item in dersler)
            {
                if(item.DersID==Convert.ToInt32(dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag))
                {
                    seciliDers = item;
                }
            }
            if(!(seciliDers.DersID==0))
            {
                frmDia.Doldur(seciliDers, bolumAd, sınıfAd, donemAd);
                frmDia.Show();
            }
            
        }

      
        
        
    }
}
