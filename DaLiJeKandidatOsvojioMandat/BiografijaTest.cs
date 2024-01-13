using CsvHelper;
using lokalniIzboriVVSGrupa3Tim2;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;

namespace BiografijaTest
{
    [TestClass]         // Testove radio Emir Ramadanović
    public class BiografijaTest
    {
        [TestMethod]
        public void TestKonstruktora()
        {
            Biografija b = new Biografija("Kando", "Kandidatic", new DateTime(1995, 2, 19), "Trg Kandidata 5", "Nizi nivo", "Kandidat je bio član stranke hdz od 1.1.1994 do 1.1.1997");
            Assert.AreEqual("Kando", b.ImeKandidata);
            Assert.AreEqual("Kandidatic", b.PrezimeKandidata);
            Assert.AreEqual(new DateTime(1995, 2, 19), b.DatumRodjenjaKandidata);
            Assert.AreEqual("Trg Kandidata 5", b.AdresaKandidata);
            Assert.AreEqual("Nizi nivo", b.StrucnaSpremaKandidata);
            Assert.AreEqual("Kandidat je bio član stranke hdz od 1.1.1994 do 1.1.1997", b.OpisKandidata);       
        }

        static IEnumerable<object[]> Biografije
        {
            get
            {
                return new[]
                {
                    new object[] { "Kando1", "Kandidatic1", new DateTime(1995, 2, 19), "Trg Kandidata 5", "Nizi nivo", "Kandidat je bio član stranke sda od 18.3.1998 do 1.8.1997" }, //pada zbog pocetnog datuma koji je veci od krajnjeg
                    new object[] { "Kando2", "Kandidatic2", new DateTime(1994, 1, 5), "Trg Kandidata 5", "Visi nivo", "Kandidat je bio član stranke hdz od 16.7.1994 do 19.11.2030" }, // pada zbog datuma u buducnosti
                    new object[] { "Kando4", "Kandidatic4", new DateTime(1992, 7, 29), "Trg Kandidata 5", "Nizi nivo", "Kandidat je bio član stranke od 11.1.1993 do 15.10.2000" }, //pada jer nema imena stranke
                    new object[] { "Kando5", "Kandidatic5", new DateTime(1992, 7, 29), "Trg Kandidata 5", "Nizi nivo", "Kandidat je kandidat" } //pada zbog formata
                };
            }
        }
        static IEnumerable<object[]> BiografijeLegalne
        {
            get
            {
                return new[]
                {
                    new object[] { "Kando1", "Kandidatic1", new DateTime(1995, 2, 19), "Trg Kandidata 5", "Nizi nivo", "Kandidat je bio član stranke sda od 18.3.1991 do 1.8.1999" },
                    new object[] { "Kando2", "Kandidatic2", new DateTime(1994, 1, 5), "Trg Kandidata 5", "Visi nivo", "Kandidat je bio član stranke hdz od 16.7.1994 do 19.11.1997" },
                    new object[] { "Kando3", "Kandidatic3", new DateTime(1992, 7, 29), "Trg Kandidata 5", "Nizi nivo", "Kandidat je bio član stranke hdz od 11.1.1993 do 15.10.2000" },
                    new object[] { "Kando4", "Kandidatic4", new DateTime(1992, 7, 29), "Trg Kandidata 5", "Nizi nivo", "" } //test ne pada, jer opisa uopste i nema (kandidati koji nemaju nikakve historije i prvi put se kandiduju nemaju opisa)
                };
            }
        }

        [TestMethod]
        [DynamicData("Biografije")]
        [ExpectedException(typeof(ArgumentException))]
        public void TestKonstruktoraBiografijeIzuzetak(string ime, string prezime, DateTime rodjenje, string adresa, string nivoss, string opis)
        {
            Biografija b = new Biografija(ime, prezime, rodjenje, adresa, nivoss, opis); // Test baca argument exception zbog datuma i formata opisa
        }

        [TestMethod]
        [DynamicData("BiografijeLegalne")]
        public void TestKonstruktoraBiografije(string ime, string prezime, DateTime rodjenje, string adresa, string nivoss, string opis)
        {
            Biografija b = new Biografija(ime, prezime, rodjenje, adresa, nivoss, opis);
        }

        public static IEnumerable<object[]> UcitajPodatkeCSV()
        {
            using (var reader = new StreamReader("biografije.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var rows = csv.GetRecords<dynamic>();
                foreach (var row in rows)
                {
                    var values = ((IDictionary<String, Object>)row).Values;
                    var elements = values.Select(elem => elem.ToString()).ToList();
                    yield return new object[] { elements[0], elements[1], elements[2], elements[3], elements[4], elements[5] };
                }
            }
        }
        
        static IEnumerable<object[]> BiografijeCSV
        {
            get
            {
                return UcitajPodatkeCSV();
            }
        }
        
        [TestMethod]
        [DynamicData("BiografijeCSV")]
        [ExpectedException(typeof(ArgumentException))]
        public void TestKonstruktoraBiografijeIzuzetakCSV(string ime, string prezime, DateTime rodjenje, string adresa, string nivoss, string opis)
        {
            Biografija b = new Biografija(ime, prezime, rodjenje, adresa, nivoss, opis); // Test baca argument exception zbog datuma i formata opisa
        }

    }
}