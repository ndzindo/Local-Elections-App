using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lokalniIzboriVVSGrupa3Tim2
{
    public class Stranka
    {
        private string nazivStranke;
        private string opisStranke;
        private int brojGlasova;
        private int redniBrojMjesta;
        private List<Kandidat> rukovodstvo;

        public Stranka(string nazivStranke, string opisStranke)
        {
            this.nazivStranke = nazivStranke;
            this.opisStranke = opisStranke;
            brojGlasova = 0;
            redniBrojMjesta = 0;
            rukovodstvo = new List<Kandidat>();
        }

        public string NazivStranke { get => nazivStranke; set => nazivStranke = value; }
        public string OpisStranke { get => opisStranke; set => opisStranke = value; }
        public int BrojGlasova { get => brojGlasova; set => brojGlasova = value; }
        public int RedniBrojMjesta { get => redniBrojMjesta; set => redniBrojMjesta = value; }
        public List<Kandidat> Rukovodstvo { get => rukovodstvo; set => rukovodstvo = value; }

        public string PrikazRezultataRukovodstva() 
        {
            string ispis = "";
            ispis = ispis + "Naziv stranke: " + nazivStranke;
            int ukupnoGlasova = 0;
            foreach (Kandidat k in rukovodstvo)
            {
                ukupnoGlasova = ukupnoGlasova + k.BrojGlasova;
            }
            ispis = ispis + "\nUkupan broj glasova: " + ukupnoGlasova + "\nKandidati: ";
            foreach (Kandidat k in rukovodstvo)
            {
                ispis = ispis + "\nIdentifikacioni broj: " + k.Jik;
            }
            return ispis;
        }
    }
}
