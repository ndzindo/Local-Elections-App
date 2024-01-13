using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using CsvHelper;
using System.IO;

namespace lokalniIzboriVVSGrupa3Tim2
{
    public class CsvMaker
    {
        public CsvMaker()
        {
            napraviGlasaceCSV();
            napraviKandidateCSV();
        }

        private void napraviKandidateCSV()
        {
            var csvPutanja = Path.Combine(Environment.CurrentDirectory, $"kandidatiLegalniSto.csv");
            using (var streamWriter = new StreamWriter(csvPutanja))
            {
                using (var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
                {
                    LokalniIzbori i = new LokalniIzbori();
                    i.KreirajIzbore();
                    var kandidati = i.Kandidati;
                    csvWriter.Context.RegisterClassMap<KandidatMap>();
                    csvWriter.WriteRecords(kandidati);
                }
            }
        }

        private void napraviGlasaceCSV()
        {
            var csvPutanja = Path.Combine(Environment.CurrentDirectory, $"glasaciLegalniSto.csv");
            using (var streamWriter = new StreamWriter(csvPutanja))
            {
                using (var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
                {
                    LokalniIzbori i = new LokalniIzbori();
                    i.KreirajIzbore();
                    var glasaci = i.Glasaci;
                    csvWriter.Context.RegisterClassMap<GlasacMap>();
                    csvWriter.WriteRecords(glasaci);
                }
            }
        }
    }
}