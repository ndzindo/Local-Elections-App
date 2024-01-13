using CsvHelper;
using lokalniIzboriVVSGrupa3Tim2;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace StrankaTest
{
    [TestClass]
    public class StrankaTest
    {
        [TestMethod]
        public void TestPrikazaInformacijaORukovodstvu()
        {
            Biografija b1 = new Biografija("kandidat1", "proba1", new DateTime(1999, 1, 1), "dasdasdas", "dasdasda", "Kandidat je bio član stranke hdz od 1.1.1994 do 1.1.1997");
            Stranka s1 = new Stranka("SDA", "DADASDASDASDSA");
            Pozicija p1 = new Pozicija(NazivPozicije.nacelnik, "dasdasdsa", 33);
            Kandidat k = new Kandidat("Isko", "Iskić", new DateTime(2000, 9, 9), "adresa 23", "999T999", "0909000170065", Pol.muski, b1, s1, p1, 33);
            s1.Rukovodstvo.Add(k);
            StringAssert.StartsWith(s1.PrikazRezultataRukovodstva(), "Naziv stranke: SDA");
            StringAssert.Contains(s1.PrikazRezultataRukovodstva(), "Identifikacioni broj");
        }


        static IEnumerable<object[]> Stranke
        {
            get
            {
                return new[]
                {
                    new object[] { "SDA", "sdaaaaaaaa" },
                    new object[] { "SDP", "sdpppppppp" },
                    new object[] { "PDP", "pdpppppppp" },
                    new object[] { "NIP", "nipppppppp" },
                    new object[] { "HDZ", "hdzzzzzzzz" }
                };
            }
        }

        [TestMethod]
        [DynamicData("Stranke")]
        public void TestSettera(string naziv, string opis)
        {
            Stranka s = new Stranka(naziv, opis);
            s.NazivStranke = "PROBA";
            s.OpisStranke = "Probaaa";
            s.BrojGlasova = 200;
            s.RedniBrojMjesta = 30;
            Assert.AreEqual("PROBA", s.NazivStranke);
            Assert.AreEqual("Probaaa", s.OpisStranke);
            Assert.AreEqual(200, s.BrojGlasova);
            Assert.AreEqual(30, s.RedniBrojMjesta);
        }

        public static IEnumerable<object[]> UcitajPodatkeCSV()
        {
            using (var reader = new StreamReader("stranke.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var rows = csv.GetRecords<dynamic>();
                foreach (var row in rows)
                {
                    var values = ((IDictionary<String, Object>)row).Values;
                    var elements = values.Select(elem => elem.ToString()).ToList();
                    yield return new object[] { elements[0], elements[1] };
                }
            }
        }

        static IEnumerable<object[]> StrankeCsv
        {
            get
            {
                return UcitajPodatkeCSV();
            }
        }


        [TestMethod]
        [DynamicData("StrankeCsv")]
        public void TestKonstruktoraStrankeCsv(string naziv, string opis)
        {
            Stranka s = new Stranka(naziv, opis);
        }
    }
}
