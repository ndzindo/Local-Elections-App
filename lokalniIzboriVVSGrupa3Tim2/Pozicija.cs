using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lokalniIzboriVVSGrupa3Tim2
{
    public enum NazivPozicije { vijecnik, nacelnik, gradonacelnik };

    public class Pozicija
    {
        private NazivPozicije nazivPozicije;
        private string opisPozicije;
        private int duzinaTrajanjaMandata;

        public Pozicija(NazivPozicije nazivPozicije, string opisPozicije, int duzinaTrajanjaMandata)
        {
            this.nazivPozicije = nazivPozicije;
            this.opisPozicije = opisPozicije;
            this.duzinaTrajanjaMandata = duzinaTrajanjaMandata;
        }

        public NazivPozicije NazivPozicije { get => nazivPozicije; set => nazivPozicije = value; }
        public string OpisPozicije { get => opisPozicije; set => opisPozicije = value; }
        public int DuzinaTrajanjaMandata { get => duzinaTrajanjaMandata; set => duzinaTrajanjaMandata = value; }
    }
}
