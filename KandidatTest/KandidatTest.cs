using CsvHelper;
using lokalniIzboriVVSGrupa3Tim2;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace KandidatTest
{
    [TestClass]
    public class KandidatTest
    {
        [TestMethod]
        public void TestKonstruktora()
        {
            Biografija b1 = new Biografija("kandidat1", "proba1", new DateTime(1999, 1, 1), "dasdasdas", "dasdasda", "Kandidat je bio član stranke hdz od 1.1.1994 do 1.1.1997");
            Stranka s1 = new Stranka("SDA", "DADASDASDASDSA");
            Pozicija p1 = new Pozicija(NazivPozicije.nacelnik, "dasdasdsa", 33);
            Kandidat k = new Kandidat("Isko", "Iskić", new DateTime(2000, 9, 9), "adresa 23", "999T999", "0909000170065", Pol.muski, b1, s1, p1, 33);
            Assert.AreEqual("Isko", k.Ime);
            Assert.AreEqual("Iskić", k.Prezime);
            Assert.AreEqual(new DateTime(2000, 9, 9), k.DatumRodjenja);
            Assert.AreEqual("adresa 23", k.Adresa);
            Assert.AreEqual("999T999", k.BrojLicneKarte);
            Assert.AreEqual("0909000170065", k.Jmbg);
            Assert.AreEqual(Pol.muski, k.Pol);
        }

        [TestMethod]
        public void TestSetterPozicije()
        {
            Biografija b1 = new Biografija("kandidat1", "proba1", new DateTime(1999, 1, 1), "dasdasdas", "dasdasda", "Kandidat je bio član stranke hdz od 1.1.1994 do 1.1.1997");
            Stranka s1 = new Stranka("SDA", "DADASDASDASDSA");
            Pozicija p1 = new Pozicija(NazivPozicije.nacelnik, "dasdasdsa", 33);
            Kandidat k = new Kandidat("Isko", "Iskić", new DateTime(2000, 9, 9), "adresa 23", "999T999", "0909000170065", Pol.muski, b1, s1, p1, 33);
            Pozicija p2 = new Pozicija(NazivPozicije.gradonacelnik, "dasdasdsa", 33);
            k.PozicijaKandidata = p2;
            Assert.AreEqual(p2, k.PozicijaKandidata);
        }

        [TestMethod]
        public void TestSetterRednogBrojaOsvojenogMjesta()
        {
            Biografija b1 = new Biografija("kandidat1", "proba1", new DateTime(1999, 1, 1), "dasdasdas", "dasdasda", "Kandidat je bio član stranke hdz od 1.1.1994 do 1.1.1997");
            Stranka s1 = new Stranka("SDA", "DADASDASDASDSA");
            Pozicija p1 = new Pozicija(NazivPozicije.nacelnik, "dasdasdsa", 33);
            Kandidat k = new Kandidat("Isko", "Iskić", new DateTime(2000, 9, 9), "adresa 23", "999T999", "0909000170065", Pol.muski, b1, s1, p1, 33);
            Pozicija p2 = new Pozicija(NazivPozicije.gradonacelnik, "dasdasdsa", 33);
            k.RedniBrojOsvojenogMjesta = 2;
            Assert.AreEqual(2, k.RedniBrojOsvojenogMjesta);
        }

        [TestMethod]
        public void TestSetterBrojaGlasova()
        {
            Biografija b1 = new Biografija("kandidat1", "proba1", new DateTime(1999, 1, 1), "dasdasdas", "dasdasda", "Kandidat je bio član stranke hdz od 1.1.1994 do 1.1.1997");
            Stranka s1 = new Stranka("SDA", "DADASDASDASDSA");
            Pozicija p1 = new Pozicija(NazivPozicije.nacelnik, "dasdasdsa", 33);
            Kandidat k = new Kandidat("Isko", "Iskić", new DateTime(2000, 9, 9), "adresa 23", "999T999", "0909000170065", Pol.muski, b1, s1, p1, 33);
            Pozicija p2 = new Pozicija(NazivPozicije.gradonacelnik, "dasdasdsa", 33);
            k.BrojGlasova = 100;
            
            Assert.AreEqual(100, k.BrojGlasova);
        }

        [TestMethod]
        public void TestSetterBrojaNaListi()
        {
            Biografija b1 = new Biografija("kandidat1", "proba1", new DateTime(1999, 1, 1), "dasdasdas", "dasdasda", "Kandidat je bio član stranke hdz od 1.1.1994 do 1.1.1997");
            Stranka s1 = new Stranka("SDA", "DADASDASDASDSA");
            Pozicija p1 = new Pozicija(NazivPozicije.nacelnik, "dasdasdsa", 33);
            Kandidat k = new Kandidat("Isko", "Iskić", new DateTime(2000, 9, 9), "adresa 23", "999T999", "0909000170065", Pol.muski, b1, s1, p1, 33);
            Pozicija p2 = new Pozicija(NazivPozicije.gradonacelnik, "dasdasdsa", 33);
            k.BrojNaListi = 5;
            Assert.AreEqual(5, k.BrojNaListi);
        }
    }
}
