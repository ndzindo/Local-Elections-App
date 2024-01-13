using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace lokalniIzboriVVSGrupa3Tim2
{
    public class Biografija
    {
        private string imeKandidata;
        private string prezimeKandidata;
        private DateTime datumRodjenjaKandidata;
        private string adresaKandidata;
        private string strucnaSpremaKandidata;
        private string opisKandidata;
        public static Regex OpisRegex = new Regex("^\\s?(Kandidat je bio )? ?član stranke (\\w*) od (([0]?[1-9]|[1|2][0-9]|[3][0|1])[\\.]([0]?[1-9]|[1][0-2])[\\.]([0-9]{4}|[0-9]{2})) do (([0]?[1-9]|[1|2][0-9]|[3][0|1])[\\.]([0]?[1-9]|[1][0-2])[\\.]([0-9]{4}|[0-9]{2}))(,\\s|$)$");

        public Biografija(string imeKandidata, string prezimeKandidata, DateTime datumRodjenjaKandidata, string adresaKandidata, string strucnaSpremaKandidata, string opisKandidata)
        {
            this.imeKandidata = imeKandidata;
            this.prezimeKandidata = prezimeKandidata;
            this.datumRodjenjaKandidata = datumRodjenjaKandidata;
            this.adresaKandidata = adresaKandidata;
            this.strucnaSpremaKandidata = strucnaSpremaKandidata;
            
            ProvjeraOpisa(opisKandidata);
            this.opisKandidata = opisKandidata;
        }

        private void ProvjeraOpisa(string opis)
        {
            string[] stranke = opis.Split(',');
            foreach(string stranka in stranke)
            {
                if (stranka != "")
                {
                    if (!OpisRegex.IsMatch(stranka))
                        throw new ArgumentException("Opis mora biti u formatu \"Kandidat je bio član stranke X1 od Y1 do Z1, član stranke X2 od Y2 do Z2...\"");
                    Match m = Biografija.OpisRegex.Match(stranka); //Grupa 2 -> Ime stranke, Grupa 3 -> Pocetni datum, Grupa 7 -> Krajnji datum 
                    DateTime pocetak = DateTime.Parse(m.Groups[3].Value, new CultureInfo("pl"));
                    DateTime kraj = DateTime.Parse(m.Groups[7].Value, new CultureInfo("pl"));
                    if (pocetak > DateTime.Now || kraj > DateTime.Now)
                        throw new ArgumentException("Datumi ne smiju biti u buducnosti");
                    if (pocetak > kraj)
                        throw new ArgumentException("Pocetni datum clanstva ne smije biti kasniji od datuma kraja clanstva u stranci");
                }

            }
        }

        public string ImeKandidata { get => imeKandidata; set => imeKandidata = value; }
        public string PrezimeKandidata { get => prezimeKandidata; set => prezimeKandidata = value; }
        public DateTime DatumRodjenjaKandidata { get => datumRodjenjaKandidata; set => datumRodjenjaKandidata = value; }
        public string AdresaKandidata { get => adresaKandidata; set => adresaKandidata = value; }
        public string StrucnaSpremaKandidata { get => strucnaSpremaKandidata; set => strucnaSpremaKandidata = value; }
        public string OpisKandidata { get => opisKandidata; set { ProvjeraOpisa(value); opisKandidata = value; } }
    }
}
