using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lokalniIzboriVVSGrupa3Tim2
{
    public class Glas
    {
        private Glasac glasac;
        private Kandidat kandidat;
        private DateTime datumGlasanja;

        public Glas(Glasac glasac, Kandidat kandidat, DateTime datumGlasanja)
        {
            this.glasac = glasac;
            this.kandidat = kandidat;
            this.datumGlasanja = datumGlasanja;
            
            // povećavanje glasova ovdje, a ne u main odnosno u Program.cs!
            if(kandidat.StrankaKandidata != null && kandidat.PozicijaKandidata.NazivPozicije.Equals(NazivPozicije.vijecnik))
            {
                kandidat.BrojGlasova = kandidat.BrojGlasova + 1;
                kandidat.StrankaKandidata.BrojGlasova = kandidat.StrankaKandidata.BrojGlasova + 1;
            }
            else
            {
                kandidat.BrojGlasova = kandidat.BrojGlasova + 1;
            }

            if (kandidat.PozicijaKandidata.NazivPozicije == NazivPozicije.gradonacelnik)
                glasac.GlasaoZaGradonacelnika = true;
            if (kandidat.PozicijaKandidata.NazivPozicije == NazivPozicije.nacelnik)
                glasac.GlasaoZaNacelnika = true;
            if (kandidat.PozicijaKandidata.NazivPozicije == NazivPozicije.vijecnik)
                glasac.GlasaoZaVijecnika = true;
        }

        public Glasac Glasac { get => glasac; set => glasac = value; }
        public Kandidat Kandidat { get => kandidat; set => kandidat = value; }
        public DateTime DatumGlasanja { get => datumGlasanja; set => datumGlasanja = value; }

        /*public override bool Equals(Object obj)
        {
            Glas g = (Glas)obj;
            if(g == null) return false;
            if(g.Glasac.Equals(this.Glasac) && g.Kandidat.Equals(this.Kandidat))
                return true;
            return false;
        }*/
    }
}
