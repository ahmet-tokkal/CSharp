using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;
namespace ProjeOdevi
{
    class VeriTabani
    {
        SqlConnection baglanti;
        public string strBaglanti;
        public VeriTabani()
        {

        }
        public VeriTabani(string serverAdi,string vtAdi)
        {
            strBaglanti = @"Data Source = "+serverAdi+"; Initial Catalog = "+vtAdi+"; Integrated Security = True";
            baglanti = new SqlConnection(strBaglanti);
        }
        public List<string> BelirliVeriCek(string tblAd, string cekilecek,string istenilen,string deger)
        {
            List<string> liste = new List<string>();
            try
            {
               
                baglanti.Open();
                string strVeriCek = "Select * from " + tblAd +" where "+istenilen+" ="+deger+"";
                SqlCommand veriCekKomut = new SqlCommand(strVeriCek, baglanti);
                SqlDataReader oku = veriCekKomut.ExecuteReader();
                while (oku.Read())
                {
                    liste.Add(oku[cekilecek].ToString());
                }
                baglanti.Close();
                return liste;
            }
            catch (SqlException hata)
            {
                MessageBox.Show(hata.Message);
                return liste;
            }
        }
        public DataTable TabloyaCek(string sorgu)
        {
            DataTable dT = new DataTable();
            try
            {
                SqlDataAdapter sDA = new SqlDataAdapter(sorgu, baglanti);
                sDA.Fill(dT);
                return dT;
            }
            catch (SqlException hata)
            {
                MessageBox.Show(hata.Message);
                return dT;
            }
        }

    }
}
