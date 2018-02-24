using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjeOdevi
{
    public class Ders
    {
        public int DersID { get; set; }
        public string DersKodu  { get; set; }
        public string DersAd { get; set; }
        public int DersSaat { get; set; }
        public int DersKredi { get; set; }
        public int DersAkts { get; set; }
        public int BolumID { get; set; }
        public int SinifID { get; set; }
        public int DonemID { get; set; }
        public string DerslikAd { get; set; }
        public Ders()
        {

        }
    }
}
