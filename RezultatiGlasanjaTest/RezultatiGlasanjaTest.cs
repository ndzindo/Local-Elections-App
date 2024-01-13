using CsvHelper;
using lokalniIzboriVVSGrupa3Tim2;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace RezultatiGlasanjaTest
{
    // NEDIM DŽINDO
    [TestClass]
    public class RezultatiGlasanjaTest
    {
        [TestMethod]
        public void TestiranjeRezultataGlasanjaZaSveStranke()
        {
            LokalniIzbori lokalniIzbori = new LokalniIzbori();
            lokalniIzbori.KreirajIzbore();
            lokalniIzbori.IspisiInformacijeZaStranke();

            // provjera ko je pobijedio na izborima:
            Kandidat pobjednikGradonacelnik = lokalniIzbori.PobjednikGradonacelnik();
            Kandidat pobjednikNacelnik = lokalniIzbori.PobjednikNacelnik();
            List<Kandidat> pobjedniciVijecnici = lokalniIzbori.VratiDelegate();

            // Prema podacima gradonacelnik je Ismar Visca, nacelnik je Emir Ramadanovic, a vijecnik prvi je WT, a zadnji je Mera
            // TESTIRANJE POBJEDNIKA IZBORA:
            Assert.AreEqual(pobjednikGradonacelnik.Ime, "Ismar");
            StringAssert.Contains(pobjednikGradonacelnik.Ime, "sm");
            Assert.AreEqual(pobjednikNacelnik.Ime, "Emir");
            StringAssert.StartsWith("Em", "Emir".Substring(0, 2));
            Assert.AreEqual(pobjedniciVijecnici[0].Ime, "WTWTWT");
            StringAssert.EndsWith("WT", pobjedniciVijecnici[0].Ime.Substring(4, 2));
            Assert.AreEqual(pobjedniciVijecnici[9].Ime, "Mera");

            // TESTIRANJE BROJA GLASOVA KANDIDATA I STRANAKA (UKUPAN BROJ)
            Assert.AreEqual(233, lokalniIzbori.UkupanBrojGlasovaZaKandidate());
            Assert.AreEqual(103, lokalniIzbori.UkupanBrojGlasovaStranaka());

            // TESTIRANJE BROJA MANDATA PO STRANKAMA U SKUPŠTINI
            Dictionary<string, int> mandati = lokalniIzbori.BrojManadataPoStranici(pobjedniciVijecnici);
            Assert.AreEqual(3, mandati["SDA"]); // CASE-SENSITIV KEY !
            Assert.AreNotEqual(10, mandati["OSMORKA"]);
        }

        [TestMethod]
        public void TestiranjeRezultataZaJednuStranku()
        {
            LokalniIzbori lokalniIzbori = new LokalniIzbori();
            lokalniIzbori.KreirajIzbore();
            Stranka sda = new Stranka("SDA", "sklj");
            lokalniIzbori.IspisiInformacijeZaStranku(sda);

            // Provjera da li su pobjednici iz stranke SDA
            Kandidat pobjednikGradonacelnik = lokalniIzbori.PobjednikGradonacelnik();
            Kandidat pobjednikNacelnik = lokalniIzbori.PobjednikNacelnik();
            List<Kandidat> pobjedniciVijecnici = lokalniIzbori.VratiDelegate();
            Assert.AreEqual("SDA", pobjednikGradonacelnik.StrankaKandidata.NazivStranke);
            Assert.AreEqual("SDA", pobjednikGradonacelnik.StrankaKandidata.NazivStranke);
            Assert.AreNotEqual("SDA", pobjedniciVijecnici[0].StrankaKandidata.NazivStranke);
            Assert.AreNotEqual("SDA", pobjedniciVijecnici[9].StrankaKandidata.NazivStranke);

            // Gubitnicka stranka za nacelnik i gradonacelnika:
            Stranka osmorka = new Stranka("OSMORKA", "sklj");
            Assert.AreNotEqual("OSMORKA", pobjednikGradonacelnik.StrankaKandidata.NazivStranke);
            Assert.AreNotEqual("OSMORKA", pobjednikGradonacelnik.StrankaKandidata.NazivStranke);
        }


        // DATA-DRIVEN UNIT TESTOVI (CSV):

        static CsvMaker csvMaker;
        [ClassInitialize]
        public static void Incijalizacijja(TestContext context)
        {
            csvMaker = new CsvMaker();
        }


        static IEnumerable<object[]> UcitajPodatkeGlasaciCSV()
        {
            using (var reader = new StreamReader("glasaciLegalniSto.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var rows = csv.GetRecords<dynamic>();
                foreach (var row in rows)
                {
                    var values = ((IDictionary<String, Object>)row).Values;
                    var elements = values.Select(elem => elem.ToString()).ToList();
                    yield return new object[] { elements[0], elements[1], elements[2], elements[3], elements[4], elements[5], elements[6] };
                }
            }
        }
        static IEnumerable<object[]> UcitajPodatkeKandidatiCSV()
        {
            using (var reader = new StreamReader("kandidatiLegalniSto.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var rows = csv.GetRecords<dynamic>();
                foreach (var row in rows)
                {
                    var values = ((IDictionary<String, Object>)row).Values;
                    var elements = values.Select(elem => elem.ToString()).ToList();
                    yield return new object[] { elements[0], elements[4], elements[9], elements[12], elements[13], elements[14], elements[15],
                            elements[1], elements[5], elements[2], elements[6],
                        elements[10], elements[11], elements[3], elements[7], elements[8], elements[16]};

                    // public void UbacivanjeKandidataIzCSV(string ime, string prezime, string rodjenje, string adresa, string licnaKarta, string jmbg, string pol,
                    // string strucnaSprema, string opisKandidata, string nazivStranke, string opisStranke, int brojGlasova, int redniBrojMjesta, string nazivPozicije,
                    // string opisPozicije, int trajanje, int redniBroj)
                }
            }
        }

        static IEnumerable<object[]> legalniGlasaciCSV
        {
            get
            {
                return UcitajPodatkeGlasaciCSV();
            }
        }

        static IEnumerable<object[]> legalniKandidatiCSV
        {
            get
            {
                return UcitajPodatkeKandidatiCSV();
            }
        }

        DateTime VratiDatum(string datum)
        {
            return DateTime.Parse(datum);
        }


        Pol VratiPol(string pol)
        {
            if (pol == "muski")
                return Pol.muski;
            else
                return Pol.zenski;
        }

        [TestMethod]
        [DynamicData("legalniGlasaciCSV")]
        public void UbacivanjeGlasacaIzCSV(string ime, string prezime, string rodjenje, string adresa, string licnaKarta, string jmbg, string pol)
        {
            Glasac g = new Glasac(ime, prezime, VratiDatum(rodjenje), adresa, licnaKarta, jmbg, VratiPol(pol));
            Assert.AreEqual(g.Ime, ime);
        }

        [TestMethod]
        [DynamicData("legalniKandidatiCSV")]
        public void UbacivanjeKandidataIzCSV(string ime, string prezime, string rodjenje, string adresa, string licnaKarta, string jmbg, string pol, string strucnaSprema, string opisKandidata, string nazivStranke, string opisStranke, string brojGlasova, string redniBrojMjesta, string nazivPozicije, string opisPozicije, string trajanje, string redniBroj)
        {
            NazivPozicije np = NazivPozicije.nacelnik; // po defaultu

            Console.Write(ime);

            if (nazivPozicije == "gradonacelnik")
                np = NazivPozicije.gradonacelnik;
            if (nazivPozicije == "nacelnik")
                np = NazivPozicije.nacelnik;
            if (nazivPozicije == "vijecnik")
                np = NazivPozicije.vijecnik;

            Kandidat k = new Kandidat(ime, prezime, VratiDatum(rodjenje), adresa, licnaKarta, jmbg, VratiPol(pol), new Biografija(ime, prezime, VratiDatum(rodjenje), adresa, strucnaSprema, opisKandidata), new Stranka(nazivStranke, opisStranke), new Pozicija(np, opisPozicije, (int)Int32.Parse(trajanje)), (int)Int32.Parse(redniBroj));
            Assert.AreEqual(k.Ime, ime);
        }

    }
}
