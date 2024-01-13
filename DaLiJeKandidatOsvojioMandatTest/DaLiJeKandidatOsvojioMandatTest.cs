using lokalniIzboriVVSGrupa3Tim2;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DaLiJeKandidatOsvojioMandatTest
{
    [TestClass]
    public class DaLiJeKandidatOsvojioMandatTest
    {
        [TestMethod]
        public void TestSlucaj1()
        {
            Biografija b = new Biografija("sklj", "sklj", new DateTime(1970, 12, 12), "sklj", "sklj", "Kandidat je bio član stranke sda od 1.1.1994 do 1.1.1997, član stranke sdp od 1.1.1997 do 2.3.1999");
            LokalniIzbori lokalniIzbori = new LokalniIzbori();
            Pozicija nacelnik = new Pozicija(NazivPozicije.nacelnik, "sklj", 4);
            Stranka hdz = new Stranka("HDZ", "sklj");
            Kandidat k = new Kandidat("Mujo", "Mujic", new DateTime(1999, 8, 28), "sklj123", "123E123", "2808999170065", Pol.muski, b, hdz, nacelnik, 15);
            k.BrojGlasova = 0;
            hdz.BrojGlasova = 1;
            Assert.IsFalse(lokalniIzbori.DaLiJeKandidatOsvojioMandat(k));
        }

        [TestMethod]
        public void TestSlucaj2()
        {
            Biografija b = new Biografija("sklj", "sklj", new DateTime(1970, 12, 12), "sklj", "sklj", "Kandidat je bio član stranke sda od 1.1.1994 do 1.1.1997, član stranke sdp od 1.1.1997 do 2.3.1999");
            LokalniIzbori lokalniIzbori = new LokalniIzbori();
            Pozicija nacelnik = new Pozicija(NazivPozicije.nacelnik, "sklj", 4);
            Stranka hdz = new Stranka("HDZ", "sklj");
            Kandidat k = new Kandidat("Mujo", "Mujic", new DateTime(1999, 8, 28), "sklj123", "123E123", "2808999170065", Pol.muski, b, hdz, nacelnik, 15);
            k.BrojGlasova = 1;
            hdz.BrojGlasova = 1;
            Assert.IsTrue(lokalniIzbori.DaLiJeKandidatOsvojioMandat(k));
        }

        [TestMethod]
        public void TestSlucaj3()
        {
            Biografija b = new Biografija("sklj", "sklj", new DateTime(1970, 12, 12), "sklj", "sklj", "Kandidat je bio član stranke sda od 1.1.1994 do 1.1.1997, član stranke sdp od 1.1.1997 do 2.3.1999");
            LokalniIzbori lokalniIzbori = new LokalniIzbori();
            Pozicija nacelnik = new Pozicija(NazivPozicije.nacelnik, "sklj", 4);
            Kandidat k = new Kandidat("Mujo", "Mujic", new DateTime(1999, 8, 28), "sklj123", "123E123", "2808999170065", Pol.muski, b, null, nacelnik, 15);
            k.BrojGlasova = Int32.MaxValue;
            Assert.IsTrue(lokalniIzbori.DaLiJeKandidatOsvojioMandat(k));
        }

        [TestMethod]
        public void TestSlucaj4()
        {
            Biografija b = new Biografija("sklj", "sklj", new DateTime(1970, 12, 12), "sklj", "sklj", "Kandidat je bio član stranke sda od 1.1.1994 do 1.1.1997, član stranke sdp od 1.1.1997 do 2.3.1999");
            LokalniIzbori lokalniIzbori = new LokalniIzbori();
            Pozicija nacelnik = new Pozicija(NazivPozicije.gradonacelnik, "sklj", 4);
            Kandidat k = new Kandidat("Mujo", "Mujic", new DateTime(1999, 8, 28), "sklj123", "123E123", "2808999170065", Pol.muski, b, null, nacelnik, 15);
            k.BrojGlasova = Int32.MaxValue;
            Assert.IsTrue(lokalniIzbori.DaLiJeKandidatOsvojioMandat(k));
        }

        [TestMethod]
        public void TestSlucaj5()
        {
            Biografija b = new Biografija("sklj", "sklj", new DateTime(1970, 12, 12), "sklj", "sklj", "Kandidat je bio član stranke sda od 1.1.1994 do 1.1.1997, član stranke sdp od 1.1.1997 do 2.3.1999");
            LokalniIzbori lokalniIzbori = new LokalniIzbori();
            Pozicija nacelnik = new Pozicija(NazivPozicije.vijecnik, "sklj", 4);
            Kandidat k = new Kandidat("Mujo", "Mujic", new DateTime(1999, 8, 28), "sklj123", "123E123", "2808999170065", Pol.muski, b, null, nacelnik, 15);
            k.BrojGlasova = Int32.MaxValue;
            Assert.IsTrue(lokalniIzbori.DaLiJeKandidatOsvojioMandat(k));
        }

        [TestMethod]
        public void TestSlucaj6()
        {
            Biografija b = new Biografija("sklj", "sklj", new DateTime(1970, 12, 12), "sklj", "sklj", "Kandidat je bio član stranke sda od 1.1.1994 do 1.1.1997, član stranke sdp od 1.1.1997 do 2.3.1999");
            LokalniIzbori lokalniIzbori = new LokalniIzbori();
            Pozicija nacelnik = new Pozicija(NazivPozicije.nacelnik, "sklj", 4);
            Kandidat k = new Kandidat("Mujo", "Mujic", new DateTime(1999, 8, 28), "sklj123", "123E123", "2808999170065", Pol.muski, b, null, nacelnik, 15);
            k.BrojGlasova = -1;
            Assert.IsFalse(lokalniIzbori.DaLiJeKandidatOsvojioMandat(k));
        }
    }
}
