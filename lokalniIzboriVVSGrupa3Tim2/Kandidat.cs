using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lokalniIzboriVVSGrupa3Tim2
{
    public class KandidatMap : ClassMap<Kandidat>
    {
        public KandidatMap()
        {
            Map(k => k.Ime).Name("Ime");
            Map(k => k.Prezime).Name("Prezime");
            Map(k => k.DatumRodjenja).Name("Rodjenje");
            Map(k => k.Adresa).Name("Adresa");
            Map(k => k.BrojLicneKarte).Name("LicnaKarta");
            Map(k => k.Jmbg).Name("Jmbg");
            Map(k => k.Pol).Name("Pol");

            // Map(k => k.BiografijaKandidata).Name("Biografija");
            Map(k => k.BiografijaKandidata.StrucnaSpremaKandidata).Name("StrucnaSprema");
            Map(k => k.BiografijaKandidata.OpisKandidata).Name("OpisKandidata");

            // Map(k => k.StrankaKandidata).Name("Stranka");
            Map(k => k.StrankaKandidata.NazivStranke).Name("NazivStranke");
            Map(k => k.StrankaKandidata.OpisStranke).Name("OpisStranke");
            Map(k => k.StrankaKandidata.BrojGlasova).Name("BrojGlasovaStranke");
            Map(k => k.StrankaKandidata.RedniBrojMjesta).Name("RedniBrojMjestaStranke");

            // Map(k => k.PozicijaKandidata).Name("Pozicija");
            Map(k => k.PozicijaKandidata.NazivPozicije).Name("NazivPozicijeKandidata");
            Map(k => k.PozicijaKandidata.OpisPozicije).Name("OpisPozicija");
            Map(k => k.PozicijaKandidata.DuzinaTrajanjaMandata).Name("Trajanje");


            Map(k => k.BrojNaListi).Name("RedniBrojListe");
        }
    }


    public class Kandidat : Glasac
    {
        private Biografija biografijaKandidata;
        private Stranka strankaKandidata;
        private Pozicija pozicijaKandidata;
        private int brojGlasova;
        private int redniBrojOsvojenogMjesta;
        private int brojNaListi;

        public Kandidat(String ime, String prezime, DateTime datumRodjenja, String adresa, String brojLicneKarte, string jmbg, Pol pol, Biografija biografijaKandidata, Stranka strankaKandidata, Pozicija pozicijaKandidata, int brojNaListi) : base(ime,prezime,datumRodjenja,adresa,brojLicneKarte,jmbg,pol)
        {
            this.biografijaKandidata = biografijaKandidata;
            this.strankaKandidata = strankaKandidata;
            this.pozicijaKandidata = pozicijaKandidata;
            this.brojNaListi = brojNaListi;
            brojGlasova = 0;
            redniBrojOsvojenogMjesta = 0;
        }

        public Biografija BiografijaKandidata { get => biografijaKandidata; set => biografijaKandidata = value; }
        public Stranka StrankaKandidata { get => strankaKandidata; set => strankaKandidata = value; }
        public Pozicija PozicijaKandidata { get => pozicijaKandidata; set => pozicijaKandidata = value; }
        public int RedniBrojOsvojenogMjesta { get => redniBrojOsvojenogMjesta; set => redniBrojOsvojenogMjesta = value; }
        public int BrojGlasova { get => brojGlasova; set => brojGlasova = value; }
        public int BrojNaListi { get => brojNaListi; set => brojNaListi = value; }

       /*public override bool Equals(Object obj)
       {
            //Kandidat gr1 = new Kandidat(b, osmorka, gradonacelnik, 10);

            Kandidat k = (Kandidat)obj;
            if (k == null)
                return false;
            if (this.BiografijaKandidata.ImeKandidata == k.BiografijaKandidata.ImeKandidata && this.BiografijaKandidata.ImeKandidata == k.BiografijaKandidata.PrezimeKandidata && this.RedniBrojOsvojenogMjesta == k.RedniBrojOsvojenogMjesta &&
                this.PozicijaKandidata.NazivPozicije == k.PozicijaKandidata.NazivPozicije && this.PozicijaKandidata.OpisPozicije == k.PozicijaKandidata.OpisPozicije && this.PozicijaKandidata.DuzinaTrajanjaMandata == k.PozicijaKandidata.DuzinaTrajanjaMandata)
                return true;
             return false;
        }*/
    }
}
