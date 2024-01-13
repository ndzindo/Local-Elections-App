using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace lokalniIzboriVVSGrupa3Tim2
{
    public class LokalniIzbori
    {
        private List<Glasac> glasaci;
        private List<Kandidat> kandidati;
        private List<Glas> glasovi;
        private List<Stranka> stranke;

        public LokalniIzbori()
        {
            glasaci = new List<Glasac>();
            kandidati = new List<Kandidat>();
            glasovi = new List<Glas>();
            stranke = new List<Stranka>();
        }

        public LokalniIzbori(List<Glasac> glasaci, List<Kandidat> kandidati, List<Glas> glasovi, List<Stranka> stranke)
        {
            this.glasaci = glasaci;
            this.kandidati = kandidati;
            this.glasovi = glasovi;
            this.stranke = stranke;
        }

        public void CsvMaker()
        {
            CsvMaker csvMaker = new CsvMaker();
        }

        public List<Glasac> Glasaci { get => glasaci; set => glasaci = value; }
        public List<Kandidat> Kandidati { get => kandidati; set => kandidati = value; }
        public List<Glas> Glasovi { get => glasovi; set => glasovi = value; }
        public List<Stranka> Stranke { get => stranke; set => stranke = value; }

        private bool UkupniIzborniPragKandidata() // da li su izbori legalni
        {
            int ukupanBrojMogucihGlasova = Glasaci.Count();
            int ukupanBrojGlasova = 0;
            foreach (Glasac g in glasaci)
            {
                if (g.GlasaoZaGradonacelnika || g.GlasaoZaVijecnika || g.GlasaoZaNacelnika)
                    ukupanBrojGlasova++;
            }

            if (ukupanBrojGlasova >= ukupanBrojMogucihGlasova * 0.2)
                return true;
            else
                return false;
        }


        private int brojGlasovaZaNacelnika()
        {

            int brojGlasova = 0;
            foreach (Glasac g in glasaci)
            {
                if (g.GlasaoZaNacelnika)
                    brojGlasova++;
            }
            return brojGlasova;

        }

        private int brojGlasovaZaGradonacelnika()
        {

            int brojGlasova = 0;
            foreach (Glasac g in glasaci)
            {
                if (g.GlasaoZaGradonacelnika)
                    brojGlasova++;
            }
            return brojGlasova;
        }

        private int brojGlasovaZaVijecnika()
        {
            int brojGlasova = 0;
            foreach (Glasac g in glasaci)
            {
                if (g.GlasaoZaVijecnika)
                    brojGlasova++;
            }
            return brojGlasova;
        }

        public bool DaLiJeKandidatOsvojioMandat(Kandidat k)
        {
            // gleda se broj glasaca koji je glasao za odredjenu poziciju i onda se broj glasova kandidata uporedjuje sa tim brojem
            // dijelimo ovdje zavisni i nezavisni
            // zavisni mora osvojiti 20 % stranke glasova
            // nezavisni 2 %
            if (k.StrankaKandidata == null)
            {
                if (k.PozicijaKandidata.NazivPozicije.Equals(NazivPozicije.nacelnik) && k.BrojGlasova >= 0.02 * brojGlasovaZaNacelnika())
                    return true;
                else if (k.PozicijaKandidata.NazivPozicije.Equals(NazivPozicije.gradonacelnik) && k.BrojGlasova >= 0.02 * brojGlasovaZaGradonacelnika())
                    return true;
                else if (k.PozicijaKandidata.NazivPozicije.Equals(NazivPozicije.vijecnik) && k.BrojGlasova >= 0.02 * brojGlasovaZaVijecnika())
                    return true;
            }
            else // ako je u stranci
            {
                // nije bitno koja je pozicija
                if (k.BrojGlasova >= k.StrankaKandidata.BrojGlasova * 0.2)
                    return true;
            }

            return false;
        }

        public bool DaLiJeStrankaOsvojilaMandat(Stranka s)
        {
            // stranka da ima mandat mora osvojiti 2%
            // stranci se dodijeli glas samo prilikom glasanja za vijecnika/vijecnike!
            int brojGlasacaVijecnika = 0;

            foreach (Glasac g in glasaci)
            {
                if (g.GlasaoZaVijecnika)
                    brojGlasacaVijecnika++;
            }

            if (s.BrojGlasova >= 0.02 * brojGlasacaVijecnika)
                return true;
            else
                return false;
        }

        public void ResetGlasanjaZaGradonacelnika(Glasac glasac)
        {
            for (int i = 0; i < glasovi.Count; i++)
            {
                bool nasaoKandidata = DajKandidata(i);

                if (nasaoKandidata)
                    break;
            }
        }

        private bool DajKandidata(int i)
        {
            
            if (glasovi[i].Glasac.GlasaoZaNacelnika)
            {
                for (int j = 0; j < kandidati.Count; j++)
                {
                    if (NadjiKandidata(i, j))
                    {
                        kandidati[j].BrojGlasova--;
                        return true;
                     
                    }
                }
            }

            return false;
        }

        private bool NadjiKandidata(int i, int j)
        {
            return kandidati[j].PozicijaKandidata.NazivPozicije == NazivPozicije.gradonacelnik &&
                                        kandidati[j].Equals(glasovi[i].Kandidat);
        }

        private bool NadjiNacelnika(int i, int j)
        {
            return kandidati[j].PozicijaKandidata.NazivPozicije == NazivPozicije.nacelnik &&
                                        kandidati[j].Equals(glasovi[i].Kandidat);
        }
        private bool DajNacelnika(int i)
        {

            if (glasovi[i].Glasac.GlasaoZaNacelnika)
            {
                for (int j = 0; j < kandidati.Count; j++)
                {
                    if (NadjiNacelnika(i, j))
                    {
                        kandidati[j].BrojGlasova--;
                        return true;

                    }
                }
            }

            return false;
        }

        /*public void ResetGlasanjaZaNacelnika()
{
   bool nasaoKandidata = false;
   for (int i = 0; i < glasovi.Count; i++)
   {

       if (glasovi[i].Glasac.GlasaoZaNacelnika)
       {
           for (int j = 0; j < kandidati.Count; j++)
           {
               nasaoKandidata = false;
               if (kandidati[j].PozicijaKandidata.NazivPozicije == NazivPozicije.nacelnik && kandidati[j].Equals(glasovi[i].Kandidat))
               {
                   kandidati[j].BrojGlasova = kandidati[j].BrojGlasova - 1;
                   nasaoKandidata = true;
                   break;     
                   }
               }
           }

       if (nasaoKandidata)
           break;
   }
}*/

        private bool DaLiJeNadjenKandidat(int i)
        {

            for (int j = 0; j < kandidati.Count; j++)
            {
                if (kandidati[j].PozicijaKandidata.NazivPozicije == NazivPozicije.nacelnik && kandidati[j].Equals(glasovi[i].Kandidat))
                {
                    kandidati[j].BrojGlasova = kandidati[j].BrojGlasova - 1;
                    return true;
                }
            }
            return false;
        }

        public void ResetGlasanjaZaNacelnika(Glasac glasac) 
        {
            for (int i = 0; i < glasovi.Count; i++)
            {
                if (DajNacelnika(i))
                    break;
            }
           
        }
        public void ResetGlasanjaZaVijecnika(Glasac glasac)
        {
            bool nasaoKandidata = false;
            for (int i = 0; i < glasovi.Count; i++)
            {

                if (glasovi[i].Glasac.GlasaoZaVijecnika)
                {
                    for (int j = 0; j < kandidati.Count; j++)
                    {
                        nasaoKandidata = false;
                        if (kandidati[j].PozicijaKandidata.NazivPozicije == NazivPozicije.vijecnik && kandidati[j].Equals(glasovi[i].Kandidat))
                        {

                            kandidati[j].BrojGlasova = kandidati[j].BrojGlasova - 1;
                            kandidati[j].StrankaKandidata.BrojGlasova = kandidati[j].StrankaKandidata.BrojGlasova - 1;
                            nasaoKandidata = true;
                            break;

                        }
                    }
                }

                if (nasaoKandidata)
                    break;
            }
        }



        // FUNKCIONALNOST 3 je radio Nedim Džindo.
        // Potrebno je omogućiti prikaz informacija o rezultatima za sve političke stranke.
        // Informacije o rezultatima za neku stranku uključuju informaciju o ukupnom broju i postotku osvojenih glasova,
        // o broju osvojenih mandata i imenima i prezimenima kandidata koji su osvojili mandate    
        // (uključujući i informacije o broju i postotku osvojenih glasova kandidata).

        public int UkupanBrojGlasovaStranaka()
        {
            int brojGlasova = 0;
            foreach (Stranka s in stranke)
            {
                brojGlasova += s.BrojGlasova;
            }
            return brojGlasova;
        }

        public int UkupanBrojGlasovaZaKandidate()
        {
            int brojGlasova = 0;
            foreach (Kandidat k in kandidati)
            {
                brojGlasova += k.BrojGlasova;
            }
            return brojGlasova;
        }

        public List<Kandidat> VratiDelegate()
        {
            List<Kandidat> vijecnici = new List<Kandidat>();
            foreach (Kandidat k in kandidati)
            {
                if (k.PozicijaKandidata.NazivPozicije.Equals(NazivPozicije.vijecnik))
                    vijecnici.Add(k);
            }

            vijecnici.Sort((v1, v2) => v1.BrojGlasova.CompareTo(v2.BrojGlasova));
            vijecnici.Reverse();

            List<Kandidat> delegati = new List<Kandidat>();
            for (int i = 0; i < 10; i++)
            {
                delegati.Add(vijecnici[i]);
            }

            return delegati;
        }

        public Kandidat PobjednikGradonacelnik()
        {
            List<Kandidat> kandidatiZaGradonacelnika = new List<Kandidat>();

            foreach (Kandidat k in kandidati)
            {
                if (k.PozicijaKandidata.NazivPozicije.Equals(NazivPozicije.gradonacelnik))
                    kandidatiZaGradonacelnika.Add(k);
            }


            kandidatiZaGradonacelnika.Sort((n1, n2) => n1.BrojGlasova.CompareTo(n2.BrojGlasova));
            kandidatiZaGradonacelnika.Reverse();

            return kandidatiZaGradonacelnika[0];
        }

        public Kandidat PobjednikNacelnik()
        {
            List<Kandidat> kandidatiZaNacelnika = new List<Kandidat>();
            
            foreach (Kandidat k in kandidati)
            {
                if (k.PozicijaKandidata.NazivPozicije.Equals(NazivPozicije.nacelnik))
                    kandidatiZaNacelnika.Add(k);
            }


            kandidatiZaNacelnika.Sort((n1, n2) => n1.BrojGlasova.CompareTo(n2.BrojGlasova));
            kandidatiZaNacelnika.Reverse();

            return kandidatiZaNacelnika[0];
        }

        public Dictionary<string, int> BrojManadataPoStranici(List<Kandidat> delegati)
        {
            Dictionary<string, int> brojMandataPoStranci = new Dictionary<string, int>();
            
            for (int i = 0; i < delegati.Count; i++)
            {
                int brojMandatara = 1;
                for (int j = i + 1; j < delegati.Count; j++)
                {
                    if (delegati[i].StrankaKandidata.NazivStranke == delegati[j].StrankaKandidata.NazivStranke)
                        brojMandatara++;
                }
                if (!brojMandataPoStranci.ContainsKey(delegati[i].StrankaKandidata.NazivStranke))
                    brojMandataPoStranci.Add(delegati[i].StrankaKandidata.NazivStranke, brojMandatara);
            }

            return brojMandataPoStranci;
        }


        public void IspisiInformacijeZaStranke()
        {
            foreach (Stranka stranka in stranke)
            {
                // Informacije o rezultatima za neku stranku uključuju informaciju o ukupnom broju i postotku osvojenih glasova: 
                double postotakStranka = Math.Round(((UkupanBrojGlasovaStranaka() - stranka.BrojGlasova) / (double)UkupanBrojGlasovaStranaka()) * 100, 2);
                Console.WriteLine(stranka.NazivStranke + " je osvojila " + stranka.BrojGlasova + " glasova, što je " + postotakStranka + " % od ukupnog broja glasova");
                if (DaLiJeStrankaOsvojilaMandat(stranka))
                    Console.WriteLine(" te je i osvojila mandat u skupštini jer je prekoračila prag od 2% glasova!\n");
                else
                    Console.WriteLine(" te nije osvojila mandat u skupštini jer nije prekoračila prag od 2% glasova!\n");

            }
            // o broju osvojenih mandata i imenima i prezimenima kandidata koji su osvojili mandate (uključujući i informacije o broju i postotku osvojenih glasova kandidata).

            // REZONOVANJE:
            // potrebno je znati koliko članova se nalazi u nekoj skupštini, mi ćemo u našem programskom rješenju uzeti broj 10 kao broj delegata u skupštini
            // Ovo znači da su kandidati koji su osvojili kandidate nalaze u top 10
            // Sortirat ćemo sve vijećnike po broju glasova i uzeti njih prvih deset

            Console.WriteLine("\n");

            Console.WriteLine("Gradonačelnik je: " + PobjednikGradonacelnik().Ime + " " + PobjednikGradonacelnik().Prezime + " iz stranke: " + PobjednikGradonacelnik().StrankaKandidata.NazivStranke + " sa osvojenim " + PobjednikGradonacelnik().BrojGlasova + " glasa\n");

            Console.WriteLine("\n");

            Console.WriteLine("Načelnik je: " + PobjednikNacelnik().Ime + " " + PobjednikNacelnik().Prezime + " iz stranke: " + PobjednikNacelnik().StrankaKandidata.NazivStranke + " sa osvojenim " + PobjednikNacelnik().BrojGlasova + " glasa\n");

            Console.WriteLine("\n");

            Console.WriteLine("Stranke su osvojile sljedeći broj mandata: \n");
            
            List<Kandidat> delegati = VratiDelegate();
            Dictionary<string, int> brojMandataPoStranci = BrojManadataPoStranici(delegati);

            foreach(var brojMandatara in brojMandataPoStranci)
            {
                Console.WriteLine("Stranka: " + brojMandatara.Key + " ima " + brojMandatara.Value + " glasova.\n");
            }

            Console.WriteLine("\n");

            Console.WriteLine("Kandidati tj. vijećnici koji su ušli u skupštinu (top 10) odnosno koji su osvojili mandate su: \n");
            foreach (Kandidat delegat in delegati)
            {
                double postotakKandidat = Math.Round(((UkupanBrojGlasovaZaKandidate() - delegat.BrojGlasova) / (double)UkupanBrojGlasovaZaKandidate()) * 100, 2);
                Console.WriteLine(delegat.Ime + " " + delegat.Prezime + ", stranka: " + delegat.StrankaKandidata.NazivStranke + " osvojio " + delegat.BrojGlasova +
                        " odnosno " + postotakKandidat + "% od ukupnog broja glasova\n");
            }

            Console.WriteLine("\n");
        }

        public void IspisiInformacijeZaStranku(Stranka s)
        {
            Stranka stranka = null;
            foreach(Stranka strankaPrivremena in stranke)
            {
                if(s.NazivStranke == strankaPrivremena.NazivStranke)
                {
                    stranka = strankaPrivremena;
                    break;
                }
            }
            double postotakStranka = Math.Round(((UkupanBrojGlasovaStranaka() - stranka.BrojGlasova) / (double)UkupanBrojGlasovaStranaka()) * 100);
            Console.WriteLine(stranka.NazivStranke + " je osvojila " + stranka.BrojGlasova + " glasova, što je " + postotakStranka + "% od ukupnog broja glasova");
            if (DaLiJeStrankaOsvojilaMandat(stranka))
                Console.WriteLine(" te je i osvojila mandat u skupštini jer je prekoračila prag od 2% glasova!\n");
            else
                Console.WriteLine(" te nije osvojila mandat u skupštini jer nije prekoračila prag od 2% glasova!\n");

            Console.WriteLine("\n");

            Kandidat gradonacelnik = PobjednikGradonacelnik();

            if(gradonacelnik.StrankaKandidata.NazivStranke == stranka.NazivStranke)
            {
                Console.WriteLine("Gradonačelnik: " + gradonacelnik.Ime + " " + gradonacelnik.Prezime + " jeste iz stranke " + stranka.NazivStranke + "\n");
            }
            else
            {
                Console.WriteLine("Gradonačelnik: " + gradonacelnik.Ime + " " + gradonacelnik.Prezime + " nije iz stranke " + stranka.NazivStranke + " već iz stranke: " + gradonacelnik.StrankaKandidata.NazivStranke + "\n");
            }

            Kandidat nacelnik = PobjednikNacelnik();

            Console.WriteLine("\n");

            if (nacelnik.StrankaKandidata.NazivStranke == stranka.NazivStranke)
            {
                Console.WriteLine("Gradonačelnik: " + nacelnik.Ime + " " + nacelnik.Prezime + " jeste iz stranke " + stranka.NazivStranke + "\n");
            }
            else
            {
                Console.WriteLine("Gradonačelnik: " + nacelnik.Ime + " " + nacelnik.Prezime + " nije iz stranke " + stranka.NazivStranke + " već iz stranke: " + nacelnik.StrankaKandidata.NazivStranke + "\n");
            }

            Console.WriteLine("\n");

            Console.WriteLine("Vijećnici koji su ušli u skupštinu odnosno koji su osvojili mandat, a pripadnici su stranke " + stranka.NazivStranke + " su: \n");
            List<Kandidat> delegati = VratiDelegate();
            int brojDelegataStranke = 0;
            foreach(Kandidat k in delegati)
            {
                if(k.StrankaKandidata.Equals(stranka))
                {
                    brojDelegataStranke++;
                    Console.WriteLine(k.Ime + " " + k.Prezime + " koji je osvojio " + k.BrojGlasova + " glasova odnosno " + Math.Round(((UkupanBrojGlasovaZaKandidate() - k.BrojGlasova) / (double)UkupanBrojGlasovaZaKandidate()) * 100, 2) + "% glasova \n");

                }
            }

            Console.WriteLine("Stranka" + stranka.NazivStranke + " ima " + brojDelegataStranke + " mandatara u skupštini.\n");

            Console.WriteLine("\n");
        }

        public Kandidat NadjiVijecnika(string ime, string prezime, string licnaKarta)
        {
            foreach(Kandidat k in kandidati)
            {
                if(k.PozicijaKandidata.NazivPozicije == NazivPozicije.vijecnik && k.Ime == ime && k.Prezime == prezime && k.BrojLicneKarte == licnaKarta)
                {
                    return k;
                }
            }
            return null;
        }

        // METODA KOJA ĆE UBACITI PODATKE ZA GLASANJE JER NEMAMO BAZU PODATAKA:
        public void KreirajIzbore()
        {
            // nevazni podaci, samo za testing:
            Biografija b = new Biografija("sklj", "sklj", new DateTime(1970, 12, 12), "sklj", "sklj", "Kandidat je bio član stranke sda od 1.1.1994 do 1.1.1997, član stranke sdp od 1.1.1997 do 2.3.1999");
            Stranka sda = new Stranka("SDA", "sklj");
            Stranka sds = new Stranka("SDS", "sklj");
            Stranka hdz = new Stranka("HDZ", "sklj");
            Stranka snsd = new Stranka("SNSD", "sklj");
            Stranka osmorka = new Stranka("OSMORKA", "sklj");

            stranke.Add(sda);
            stranke.Add(sds);
            stranke.Add(hdz);
            stranke.Add(snsd);
            stranke.Add(osmorka);

            Pozicija gradonacelnik = new Pozicija(NazivPozicije.gradonacelnik, "sklj", 4);
            Pozicija nacelnik = new Pozicija(NazivPozicije.nacelnik, "sklj", 4);
            Pozicija vijecnik = new Pozicija(NazivPozicije.vijecnik, "sklj", 4);

            // Glasaci:
            int p = 0;
            for(int i = 0; i <= 25; i++)
                glasaci.Add(new Glasac("Damir" + (char)(p + 65), "Damic", new DateTime(1999, 12, 12), "sklj 123", "999E999", "1212999170065", Pol.muski));
            p = 0;
            for(int i = 26; i <= 51; i++)
            {
                glasaci.Add(new Glasac("Damir", "Damic" + (char)(p + 65), new DateTime(1999, 12, 12), "sklj 123", "999E999", "1212999170065", Pol.muski));
                p++;
            }
            p = 0;
            for(int i = 52; i <= 77; i++)
            {
                glasaci.Add(new Glasac("Mera" + (char)(p + 65), "Meric", new DateTime(1999, 12, 12), "sklj 123", "999E999", "1212999170065", Pol.muski));
                p++;
            }
            p = 0;
            for(int i = 78; i <= 103; i++)
            {
                glasaci.Add(new Glasac("Mera", "Meric" + (char)(p + 65), new DateTime(1999, 12, 12), "sklj 123", "999E999", "1212999170065", Pol.muski));
                p++;
            }



            // Kandidati:
            Kandidat gr1 = new Kandidat("Merim", "Kulovac", new DateTime(1999, 8, 28), "sklj123", "123E123", "2808999170065", Pol.muski, b, osmorka, gradonacelnik, 10);
            Kandidat gr2 = new Kandidat("Ismar", "Visca", new DateTime(1999, 8, 28), "sklj123", "123E123", "2808999170065", Pol.muski, b, sda, gradonacelnik, 11);
            Kandidat gr3 = new Kandidat("Nedim", "Dzindo", new DateTime(1999, 8, 28), "sklj123", "123E123", "2808999170065", Pol.muski, b, hdz, gradonacelnik, 12);
            Kandidat n1 = new Kandidat("Ibrahim", "Efendic", new DateTime(1999, 8, 28), "sklj123", "123E123", "2808999170065", Pol.muski, b, osmorka, nacelnik, 13);
            Kandidat n2 = new Kandidat("Emir", "Ramadanovic", new DateTime(1999, 8, 28), "sklj123", "123E123", "2808999170065", Pol.muski, b, sda, nacelnik, 14);
            Kandidat n3 = new Kandidat("Mujo", "Mujic", new DateTime(1999, 8, 28), "sklj123", "123E123", "2808999170065", Pol.muski, b, hdz, nacelnik, 15);
            Kandidat v1 = new Kandidat("Huso", "Husic", new DateTime(1999, 8, 28), "sklj123", "123E123", "2808999170065", Pol.muski, b, osmorka, vijecnik, 16);
            Kandidat v2 = new Kandidat("Suljo", "Suljic", new DateTime(1999, 8, 28), "sklj123", "123E123", "2808999170065", Pol.muski, b, osmorka, vijecnik, 17);
            Kandidat v3 = new Kandidat("Mera", "Meric", new DateTime(1999, 8, 28), "sklj123", "123E123", "2808999170065", Pol.muski, b, osmorka, vijecnik, 18);
            Kandidat v4 = new Kandidat("Ibro", "Ibric", new DateTime(1999, 8, 28), "sklj123", "123E123", "2808999170065", Pol.muski, b, sda, vijecnik, 19);
            Kandidat v5 = new Kandidat("Isko", "Iskic", new DateTime(1999, 8, 28), "sklj123", "123E123", "2808999170065", Pol.muski, b, sda, vijecnik, 21);
            Kandidat v6 = new Kandidat("Nedim", "Nedic", new DateTime(1999, 8, 28), "sklj123", "123E123", "2808999170065", Pol.muski, b, sda, vijecnik, 22);
            Kandidat v7 = new Kandidat("Bakir", "Bakir", new DateTime(1999, 8, 28), "sklj123", "123E123", "2808999170065", Pol.muski, b, hdz, vijecnik, 23);
            Kandidat v8 = new Kandidat("Bajro", "Bajric", new DateTime(1999, 8, 28), "sklj123", "123E123", "2808999170065", Pol.muski, b, hdz, vijecnik, 24);
            Kandidat v9 = new Kandidat("VVSVVS", "VVSVVS", new DateTime(1999, 8, 28), "sklj123", "123E123", "2808999170065", Pol.muski, b, hdz, vijecnik, 25);
            Kandidat v10 = new Kandidat("WTWTWT", "WTWTWT", new DateTime(1999, 8, 28), "sklj123", "123E123", "2808999170065", Pol.muski, b, snsd, vijecnik, 26);
            Kandidat v11 = new Kandidat("OOIOOI", "OOIOOI", new DateTime(1999, 8, 28), "sklj123", "123E123", "2808999170065", Pol.muski, b, snsd, vijecnik, 27);
            Kandidat v12 = new Kandidat("RGRGRG", "RGRGRG", new DateTime(1999, 8, 28), "sklj123", "123E123", "2808999170065", Pol.muski, b, sds, vijecnik, 28);
            Kandidat v13 = new Kandidat("OISOISOIS", "OISOISOIS", new DateTime(1999, 8, 28), "sklj123", "123E123", "2808999170065", Pol.muski, b, sds, vijecnik, 29);
            Kandidat v14 = new Kandidat("PWSPWSPWS", "PWSPWSPWS", new DateTime(1999, 8, 28), "sklj123", "123E123", "2808999170065", Pol.muski, b, sda, vijecnik, 30);
            Kandidat v15 = new Kandidat("PJPPJP", "PJPPJP", new DateTime(1999, 8, 28), "sklj123", "123E123", "2808999170065", Pol.muski, b, sda, vijecnik, 31);
            kandidati.Add(gr1);
            kandidati.Add(gr2);
            kandidati.Add(gr3);
            kandidati.Add(n1);
            kandidati.Add(n2);
            kandidati.Add(n3);
            kandidati.Add(v1);
            kandidati.Add(v2);
            kandidati.Add(v3);
            kandidati.Add(v4);
            kandidati.Add(v5);
            kandidati.Add(v6);
            kandidati.Add(v7);
            kandidati.Add(v8);
            kandidati.Add(v9);
            kandidati.Add(v10);
            kandidati.Add(v11);
            kandidati.Add(v12);
            kandidati.Add(v13);
            kandidati.Add(v14);
            kandidati.Add(v15);

            // Glasovi:

            // Glasanje za gradonacelnika:
            for (int i = 0; i < 23; i++)
            {
                glasovi.Add(new Glas(glasaci[i], gr1, DateTime.Now));
                glasaci[i].GlasaoZaGradonacelnika = true;
            }

            for (int i = 23; i < 55; i++)
            {
                glasovi.Add(new Glas(glasaci[i], gr2, DateTime.Now));
                glasaci[i].GlasaoZaGradonacelnika = true;
            }
            for (int i = 55; i < 65; i++)
            {
                glasovi.Add(new Glas(glasaci[i], gr3, DateTime.Now));
                glasaci[i].GlasaoZaGradonacelnika = true;
            }


            // Glasanje za nacelnika:
            for (int i = 0; i < 23; i++)
            {
                glasovi.Add(new Glas(glasaci[i], n3, DateTime.Now));
                glasaci[i].GlasaoZaGradonacelnika = true;
            }
            for (int i = 23; i < 55; i++)
            {
                glasovi.Add(new Glas(glasaci[i], n2, DateTime.Now));
                glasaci[i].GlasaoZaNacelnika = true;
            }
            for (int i = 55; i < 65; i++)
            {
                glasovi.Add(new Glas(glasaci[i], n1, DateTime.Now));
                glasaci[i].GlasaoZaNacelnika = true;
            }


            // Glasanje za vijecnike:
            for(int i = 0; i < 11; i++)
            {
                glasovi.Add(new Glas(glasaci[i], v1, DateTime.Now));
                glasaci[i].GlasaoZaVijecnika = true;
            }
            for (int i = 11; i < 23; i++)
            {
                glasovi.Add(new Glas(glasaci[i], v2, DateTime.Now));
                glasaci[i].GlasaoZaVijecnika = true;
            }
            for (int i = 23; i < 29; i++)
            {
                glasovi.Add(new Glas(glasaci[i], v3, DateTime.Now));
                glasaci[i].GlasaoZaVijecnika = true;
            }
            for (int i = 29; i < 44; i++)
            {
                glasovi.Add(new Glas(glasaci[i], v4, DateTime.Now));
                glasaci[i].GlasaoZaVijecnika = true;
            }
            for (int i = 44; i < 50; i++)
            {
                glasovi.Add(new Glas(glasaci[i], v5, DateTime.Now));
                glasaci[i].GlasaoZaVijecnika = true;
            }
            for (int i = 50; i < 61; i++)
            {
                glasovi.Add(new Glas(glasaci[i], v6, DateTime.Now));
                glasaci[i].GlasaoZaVijecnika = true;
            }
            for (int i = 61; i < 71; i++)
            {
                glasovi.Add(new Glas(glasaci[i], v7, DateTime.Now));
                glasaci[i].GlasaoZaVijecnika = true;
            }
            for (int i = 71; i < 73; i++)
            {
                glasovi.Add(new Glas(glasaci[i], v8, DateTime.Now));
                glasaci[i].GlasaoZaVijecnika = true;
            }
            for (int i = 73; i < 88; i++)
            {
                glasovi.Add(new Glas(glasaci[i], v10, DateTime.Now));
                glasaci[i].GlasaoZaVijecnika = true;
            }
            for (int i = 88; i < 95; i++)
            {
                glasovi.Add(new Glas(glasaci[i], v11, DateTime.Now));
                glasaci[i].GlasaoZaVijecnika = true;
            }
            for (int i = 95; i < 103; i++)
            {
                glasovi.Add(new Glas(glasaci[i], v12, DateTime.Now));
                glasaci[i].GlasaoZaVijecnika = true;
            }
        }
    }
}
