using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace lokalniIzboriVVSGrupa3Tim2
{
    public enum Pol { muski, zenski };


    public class GlasacMap : ClassMap<Glasac>
    {
        public GlasacMap()
        {
            Map(g => g.Ime).Name("Ime");
            Map(g => g.Prezime).Name("Prezime");
            Map(g => g.DatumRodjenja).Name("DatumRodjenja");
            Map(g => g.Adresa).Name("Adresa");
            Map(g => g.BrojLicneKarte).Name("LicnaKarta");
            Map(g => g.Jmbg).Name("Jmbg");
            Map(g => g.Pol).Name("Pol");
        }
    }


    public class Glasac
    {
        private string ime;
        private string prezime;
        private string adresa;
        private string brojLicneKarte;
        private string jik; // jik - jedinstveni identifikacioni kod (sadrzi prva dva karaktera svih podataka o korisniku)
        private string jmbg;
        private DateTime datumRodjenja;
        private Pol pol;
        private bool glasaoZaGradonacelnika = false;
        private bool glasaoZaNacelnika = false;
        private bool glasaoZaVijecnika = false;

        public Glasac(String ime, String prezime, DateTime datumRodjenja, String adresa, String brojLicneKarte, string jmbg, Pol pol)
        {
            ProvjeraImena(ime);
            this.ime = ime;

            ProvjeraPrezimena(prezime);
            this.prezime = prezime;

            ProvjeraDatumaRodjenja(datumRodjenja);
            this.datumRodjenja = datumRodjenja;

            ProvjeraAdrese(adresa);
            this.adresa = adresa;

            ProvjeraBrojaLicneKarte(brojLicneKarte);
            this.brojLicneKarte = brojLicneKarte;

            ProvjeraJmbg(jmbg);
            this.jmbg = jmbg;

            this.pol = pol;

            FormirajJikGlasaca();
        }

        // validaciju pisao: Merim Kulovac

        private int BrojCrtica(string ime)
        {
            int brojac = 0;
            for (int i = 0; i < ime.Length; i++)
            {
                if (ime[i] == '-')
                    brojac++;
            }
            return brojac;
        }

        bool ProvjeriImePrezimeDodatno(string ime)
        {
            foreach (char c in ime.ToCharArray())
            {
                if (!Char.IsLetter(c))
                {
                    if (c == '-')
                        continue;
                    else
                        return false;
                }
            }
            return true;
        }

        private void ProvjeriNullIPrazno(string ime)
        {
            if (ime == null)
                throw new ArgumentNullException("Glasač mora imati ime - ime ne smije biti NULL!");

            if (ime.Length == 0)
                throw new ArgumentException("Ime ne može biti prazna riječ!");
        }

        /*private void ProvjeraImena(string ime)
        {
            if (ime == null)
                throw new ArgumentNullException("Glasač mora imati ime - ime ne smije biti NULL!");

            if (ime.Length == 0)
                throw new ArgumentException("Ime ne može biti prazna riječ!");

            if (ime.Length < 2 || ime.Length > 40)
                throw new ArgumentOutOfRangeException("Upisano ime nije validno! Ime mora imati izmedju 2 i 40 karaktera!");

            if (!ProvjeriImePrezimeDodatno(ime) || BrojCrtica(ime) > 1)
                throw new ArgumentException("Ime smije da sadrži samo slova i crticu");
        }*/

        private void ProvjeraImena(string ime) //refaktoring
        {
            ProvjeriNullIPrazno(ime);

            if (ime.Length < 2 || ime.Length > 40)
                throw new ArgumentOutOfRangeException("Upisano ime nije validno! Ime mora imati izmedju 2 i 40 karaktera!");

            if (!ProvjeriImePrezimeDodatno(ime) || BrojCrtica(ime) > 1)
                throw new ArgumentException("Ime smije da sadrži samo slova i crticu");
        }

        private void ProvjeraPrezimena(string prezime)
        {
            if (prezime == null)
                throw new ArgumentNullException("Glasač mora imati prezime - prezime ne smije biti NULL!");

            if (prezime.Length == 0)
                throw new ArgumentException("Prezime ne može biti prazna riječ!");

            if (prezime.Length < 3 || prezime.Length > 50)
                throw new ArgumentOutOfRangeException("Upisano prezime nije validno! Prezime mora imati izmedju 3 i 50 karaktera!");

            if (!ProvjeriImePrezimeDodatno(prezime) || BrojCrtica(prezime) > 1)
                throw new ArgumentException("Prezime smije da sadrži samo slova i crticu");
        }

        private void ProvjeraAdrese(string adresa)
        {
            // mora se prvo provjeriti da li je adresa null pa onda trimovati...
            if (adresa == null)
                throw new ArgumentNullException("Glasač mora imati adresu - adresa ne smije biti NULL!");

            adresa = adresa.TrimStart(); // ovim se obezbjedjuje da ne unesemo samo praznine / razmake
            adresa = adresa.TrimEnd();

            if (adresa.Length == 0)
                throw new ArgumentException("Adresa ne može biti prazna riječ!");
        }

        private void ProvjeraDatumaRodjenja(DateTime datum)
        {
            // if (datum == null)
            //    throw new ArgumentNullException("Glasač mora imati datum rođenja - datum rođenja ne smije biti NULL!");
            // C# sam od sebe ne dozvoljava null na DateTime
            if (datum > DateTime.Now)
                throw new ArgumentOutOfRangeException("Datum rođenja ne može biti veći od današnjeg datuma!");

            if (DateTime.Now.Year - datum.Year < 18)
                throw new ArgumentOutOfRangeException("Glasač nije punoljetan!");
        }

        private void ProvjeraBrojaLicneKarte(string brojLicneKarte)
        {
            // prvo provjera null, pa onda trim...
            if (brojLicneKarte == null)
                throw new ArgumentNullException("Glasač mora imati broj lične karte - broj lične karte ne smije biti NULL!");
              
            brojLicneKarte = brojLicneKarte.ToUpper();
            brojLicneKarte = brojLicneKarte.TrimStart(); // moze se desiti da se greskom unese razmak na kraju ili pocetku...
            brojLicneKarte = brojLicneKarte.TrimEnd();

            if (brojLicneKarte.Length != 7)
                throw new ArgumentException("Broj lične karte mora imati tačno 7 karaktera!");

            PomocnaProvjeraLicneKarte(brojLicneKarte);
        }

        private void PomocnaProvjeraLicneKarte(string brojLicneKarte)
        {
            for (int i = 0; i < brojLicneKarte.Length; i++)
            {
                if (i != 3)
                {
                    if (!Char.IsDigit(brojLicneKarte[i]))
                        throw new ArgumentException("Na " + (i + 1) + ". mjestu broja lične karte mora biti broj koji je u rasponu od 0 do 9!");
                }
                else
                {
                    if (brojLicneKarte[i] == 'E' || brojLicneKarte[i] == 'J' || brojLicneKarte[i] == 'K' || brojLicneKarte[i] == 'M' || brojLicneKarte[i] == 'T')
                        continue;
                    else
                        throw new ArgumentOutOfRangeException("Na " + (i + 1) + ". mjestu broja lične karte mora biti slovo koje pripada skupu {E, J, K, M, T}!");
                }
            }
        }

        /*public void ProvjeraJmbg(string jmbg) // REFAKTOR METODA
        {
            if (jmbg == null)
                throw new ArgumentNullException("Glasač mora imati JMBG - JMBG ne smije biti NULL!");

            if (jmbg.Length != 13)
                throw new ArgumentException("JMBG mora imati tačno 13 brojeva!");

            DaLiPostojiZabranjeniZnak(jmbg);

            string dan = VratiDanDatuma();
            string mjesec = VratiMjesecDatuma();
            string godina = datumRodjenja.Year.ToString().Remove(0, 1);

            if (jmbg.Substring(0, 2) != dan || jmbg.Substring(2, 2) != mjesec || jmbg.Substring(4, 3) != godina)
                throw new ArgumentException("Prvih 7 cifara JMBG moraju biti jednake datumu rođenja!");
        }*/

        /*public void ProvjeraJmbg(string jmbg) // bez tuninga
        {
            // potrebno je samo provjeriti prvih 7 brojeva tj da li su jednaki datumu, prva cifra godine se ne gleda vec preostale 3:
            if (jmbg == null)
                throw new ArgumentNullException("Glasač mora imati JMBG - JMBG ne smije biti NULL!");

            if (jmbg.Length != 13)
                throw new ArgumentException("JMBG mora imati tačno 13 brojeva!");

            foreach (char c in jmbg.ToCharArray())
            {
                if (!Char.IsDigit(c))
                    throw new ArgumentException("JMBG mora sadržavati samo brojeve!");
            }


            string dan = "";
            if (datumRodjenja.Day < 10)
            {
                dan = "0" + datumRodjenja.Day.ToString();
            }
            else
            {
                dan = datumRodjenja.Day.ToString();
            }

            string mjesec = "";
            if (datumRodjenja.Month < 10)
            {
                mjesec = "0" + datumRodjenja.Month.ToString();
            }
            else
            {
                mjesec = datumRodjenja.Month.ToString();
            }

            string godina = datumRodjenja.Year.ToString().Remove(0, 1);

            // string top = dan + mjesec + godina;
            if (jmbg.Substring(0, 2) != dan || jmbg.Substring(2, 2) != mjesec || jmbg.Substring(4, 3) != godina) // 2808999
                throw new ArgumentException("Prvih 7 cifara JMBG moraju biti jednake datumu rođenja!");
        }*/

        /*public void ProvjeraJmbg(string jmbg) // TUNING 1
        {
                // potrebno je samo provjeriti prvih 7 brojeva tj da li su jednaki datumu, prva cifra godine se ne gleda vec preostale 3:
                if (jmbg == null)
                    throw new ArgumentNullException("Glasač mora imati JMBG - JMBG ne smije biti NULL!");

                if (jmbg.Length != 13)
                    throw new ArgumentException("JMBG mora imati tačno 13 brojeva!");
    
                DaLiPostojiZabranjeniZnak(jmbg);

                string dan = "";
                if (datumRodjenja.Day < 10)
                {
                    dan = "0" + datumRodjenja.Day.ToString();
                }
                else
                {
                    dan = datumRodjenja.Day.ToString();
                }

                string mjesec = "";
                if (datumRodjenja.Month < 10)
                {
                    mjesec = "0" + datumRodjenja.Month.ToString();
                }
                else
                {
                    mjesec = datumRodjenja.Month.ToString();
                }

                string godina = datumRodjenja.Year.ToString().Remove(0, 1);

                // string top = dan + mjesec + godina;
                if (jmbg.Substring(0, 2) != dan || jmbg.Substring(2, 2) != mjesec || jmbg.Substring(4, 3) != godina) // 2808999
                    throw new ArgumentException("Prvih 7 cifara JMBG moraju biti jednake datumu rođenja!");
        }*/

        /*public void ProvjeraJmbg(string jmbg) //TUNING 2
        {
            // potrebno je samo provjeriti prvih 7 brojeva tj da li su jednaki datumu, prva cifra godine se ne gleda vec preostale 3:
            if (jmbg == null)
                throw new ArgumentNullException("Glasač mora imati JMBG - JMBG ne smije biti NULL!");

            if (jmbg.Length != 13)
                throw new ArgumentException("JMBG mora imati tačno 13 brojeva!");

            DaLiPostojiZabranjeniZnak(jmbg);

            string dan = VratiDanDatuma();

            string mjesec = "";
            if (datumRodjenja.Month < 10)
            {
                mjesec = "0" + datumRodjenja.Month.ToString();
            }
            else
            {
                mjesec = datumRodjenja.Month.ToString();
            }

            string godina = datumRodjenja.Year.ToString().Remove(0, 1);

            // string top = dan + mjesec + godina;
            if (jmbg.Substring(0, 2) != dan || jmbg.Substring(2, 2) != mjesec || jmbg.Substring(4, 3) != godina) // 2808999
                throw new ArgumentException("Prvih 7 cifara JMBG moraju biti jednake datumu rođenja!");
        }*/
        
        /*
        public void ProvjeraJmbg(string jmbg) //TUNING 3
        {
            // potrebno je samo provjeriti prvih 7 brojeva tj da li su jednaki datumu, prva cifra godine se ne gleda vec preostale 3:
            if (jmbg == null)
                throw new ArgumentNullException("Glasač mora imati JMBG - JMBG ne smije biti NULL!");

            if (jmbg.Length != 13)
                throw new ArgumentException("JMBG mora imati tačno 13 brojeva!");

            DaLiPostojiZabranjeniZnak(jmbg);

            string dan = VratiDanDatuma();

            string mjesec = VratiMjesecDatuma();

            string godina = datumRodjenja.Year.ToString().Remove(0, 1);

            // string top = dan + mjesec + godina;
            if (jmbg.Substring(0, 2) != dan || jmbg.Substring(2, 2) != mjesec || jmbg.Substring(4, 3) != godina) // 2808999
                throw new ArgumentException("Prvih 7 cifara JMBG moraju biti jednake datumu rođenja!");
        }*/


        public void ProvjeraJmbg(string jmbg) //TUNING 4
        {
            // potrebno je samo provjeriti prvih 7 brojeva tj da li su jednaki datumu, prva cifra godine se ne gleda vec preostale 3:
            if (jmbg == null)
                throw new ArgumentNullException("Glasač mora imati JMBG - JMBG ne smije biti NULL!");

            if (jmbg.Length != 13)
                throw new ArgumentException("JMBG mora imati tačno 13 brojeva!");

            DaLiPostojiZabranjeniZnak(jmbg);


            string godina = datumRodjenja.Year.ToString().Remove(0, 1);

            // string top = dan + mjesec + godina;
            if (jmbg.Substring(0, 2) != VratiDanDatuma() || jmbg.Substring(2, 2) != VratiMjesecDatuma() || jmbg.Substring(4, 3) != godina) // 2808999
                throw new ArgumentException("Prvih 7 cifara JMBG moraju biti jednake datumu rođenja!");
        }

        private void DaLiPostojiZabranjeniZnak(string jmbg)
        {
            foreach (char c in jmbg.ToCharArray())
            {
                if (!Char.IsDigit(c))
                    throw new ArgumentException("JMBG mora sadržavati samo brojeve!");
            }
        }

        private string VratiDanDatuma()
        {
            string dan = "";
            if (datumRodjenja.Day < 10)
            {
                dan = "0" + datumRodjenja.Day.ToString();
            }
            else
            {
                dan = datumRodjenja.Day.ToString();
            }
            return dan;
        }

        private string VratiMjesecDatuma()
        {
            string mjesec = "";
            if (datumRodjenja.Month < 10)
            {
                mjesec = "0" + datumRodjenja.Month.ToString();
            }
            else
            {
                mjesec = datumRodjenja.Month.ToString();
            }
            return mjesec;
        }

        public void ProvjeriJIK(string jik)
        {
            jik.ToLower();
            
            string dan = "";
            if (datumRodjenja.Day < 10)
            {
                dan = "0" + datumRodjenja.Day.ToString();
            }
            else
            {
                dan = datumRodjenja.Day.ToString();
            }

            if (jik.Substring(0, 2) != ime.Substring(0, 2).ToLower() || jik.Substring(2, 2) != prezime.Substring(0, 2).ToLower() || jik.Substring(4, 2) != adresa.Substring(0, 2).ToLower() || jik.Substring(6, 2) != brojLicneKarte.Substring(0, 2) || jik.Substring(8, 2) != jmbg.ToString().Substring(0, 2) || jik.Substring(10, 2) != dan)
                throw new ArgumentException("JIK nije ispravan!");
        }

        // potrebno je validaciju dodati i u setterima iako nije naglašeno!
        // posto se JIK sastoji od ovih podataka, potrebno je napraviti novi JIK ?!
        public string Ime
        {
            get => ime;

            set
            {
                ProvjeraImena(value);
                ime = value;
                FormirajJikGlasaca();
            }
        }
        public string Prezime
        {
            get => prezime;
            set
            {
                ProvjeraPrezimena(value);
                prezime = value;
                FormirajJikGlasaca();
            }
        }
        public string Adresa
        {
            get => adresa;
            set
            {
                ProvjeraAdrese(value);
                adresa = value;
                FormirajJikGlasaca();
            }
        }
        public string BrojLicneKarte
        {
            get => brojLicneKarte;
            set
            {
                ProvjeraBrojaLicneKarte(value);
                brojLicneKarte = value;
                FormirajJikGlasaca();
            }
        }
        public string Jmbg
        {
            get => jmbg;
            set
            {
                ProvjeraJmbg(value);
                jmbg = value;
                FormirajJikGlasaca();
            }
        }
        public DateTime DatumRodjenja // ovdje bi trebalo mijenjati i JMBG jer se mijenja datum rodjenja, a on je sastavni dio JMBG-a
        {
            get => datumRodjenja;
            set
            {
                ProvjeraDatumaRodjenja(value);
                datumRodjenja = value;
                PromijeniJmbg(datumRodjenja);
                FormirajJikGlasaca();
            }
        }
        public string Jik
        {
            get => jik;
            set
            {
                ProvjeriJIK(value);
                jik = value.ToLower();
            }
        }
        public Pol Pol { get => pol; set => pol = value; }
        public bool GlasaoZaGradonacelnika { get => glasaoZaGradonacelnika; set => glasaoZaGradonacelnika = value; }
        public bool GlasaoZaNacelnika { get => glasaoZaNacelnika; set => glasaoZaNacelnika = value; }
        public bool GlasaoZaVijecnika { get => glasaoZaVijecnika; set => glasaoZaVijecnika = value; }



        void FormirajJikGlasaca()
        {
            // ovdje treba formirati jik na nacin da se iz svih gore informacija uzima po dva pocetna karaktera osim spola
            string dan = "";
            if (datumRodjenja.Day < 10)
            {
                dan = "0" + datumRodjenja.Day.ToString();
            }
            else
            {
                dan = datumRodjenja.Day.ToString();
            }

            jik = ime.Substring(0, 2) + prezime.Substring(0, 2) + adresa.Substring(0, 2) + brojLicneKarte.Substring(0, 2) + jmbg.Substring(0, 2) + dan.Substring(0, 2);
            jik = jik.ToLower();
        }


        private void PromijeniJmbg(DateTime datumRodjenja)
        {
            string dan = "";
            if (datumRodjenja.Day < 10)
            {
                dan = "0" + datumRodjenja.Day.ToString();
            }
            else
            {
                dan = datumRodjenja.Day.ToString();
            }

            string mjesec = "";
            if (datumRodjenja.Month < 10)
            {
                mjesec = "0" + datumRodjenja.Month.ToString();
            }
            else
            {
                mjesec = datumRodjenja.Month.ToString();
            }

            string godina = datumRodjenja.Year.ToString().Remove(0, 1);

            string noviJmbg = dan + mjesec + godina + jmbg.Substring(7, 6);

            jmbg = noviJmbg;
        }


        public bool ProvjeraSifre(string tajnaSifra, bool validanJik)
        {
            
            if (validanJik && tajnaSifra == "VVS20222023")
            {
                return true;
            }

            return false;
        }

        public bool VjerodostojnostGlasaca(IProvjera sigurnosnaProvjera)
        {
            if (sigurnosnaProvjera.DaLiJeVecGlasao(jik))
                throw new Exception("Glasač je već izvršio glasanje!");
            return true;
        }

        /*public override bool Equals(Object obj)
        {
            //Glasac ponistiGlas = new Glasac("DamirA", "Damic", new DateTime(1999, 12, 12), "sklj 123", "999E999", "1212999170065", Pol.muski);
            Glasac g = (Glasac)obj;
            if (g == null)
                return false;
            if (ime == g.Ime && prezime == g.Prezime && datumRodjenja.Day == g.DatumRodjenja.Day && datumRodjenja.Month == g.DatumRodjenja.Month
                && datumRodjenja.Year == g.DatumRodjenja.Year && adresa == g.Adresa && brojLicneKarte == g.BrojLicneKarte && jmbg == g.Jmbg && pol == g.Pol)
                return true;
            return false;
        }*/
    }
}

