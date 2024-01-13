using lokalniIzboriVVSGrupa3Tim2;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace TuningTestProvjeraJmbg
{
    [TestClass]
    public class TuningTestJmbg
    {
        [TestMethod]
        public void TestnaMetoda()
        {   
            int x = 0;

            List<Glasac> glasaci = new List<Glasac>();

            int j = -1;
            for(int i = 65; i <= 90; i++)
            {
                glasaci.Add(new Glasac("Damir" + (char)i, "Damic", new DateTime(1999, 8, 8), "sklj 123", "999E999", "0808999170065", Pol.muski));
                j++;
                glasaci[j].ProvjeraJmbg("0808999170065");
                glasaci.Add(new Glasac("Damir", "Damic" + (char)i, new DateTime(1999, 8, 8), "sklj 123", "999E999", "0808999170065", Pol.muski));
                j++;
                glasaci[j].ProvjeraJmbg("0808999170065");
                glasaci.Add(new Glasac("Samir" + (char)i, "Damic", new DateTime(1999, 8, 8), "sklj 123", "999E999", "0808999170065", Pol.muski));
                j++;
                glasaci[j].ProvjeraJmbg("0808999170065");
                glasaci.Add(new Glasac("Samir", "Damic" + (char)i, new DateTime(1999, 8, 8), "sklj 123", "999E999", "0808999170065", Pol.muski));
                j++;
                glasaci[j].ProvjeraJmbg("0808999170065");
                glasaci.Add(new Glasac("Semir" + (char)i, "Damic", new DateTime(1999, 8, 8), "sklj 123", "999E999", "0808999170065", Pol.muski));
                j++;
                glasaci[j].ProvjeraJmbg("0808999170065");
                glasaci.Add(new Glasac("Semir", "Damic" + (char)i, new DateTime(1999, 8, 8), "sklj 123", "999E999", "0808999170065", Pol.muski));
                j++;
                glasaci[j].ProvjeraJmbg("0808999170065");
                glasaci.Add(new Glasac("Demir" + (char)i, "Damic", new DateTime(1999, 8, 8), "sklj 123", "999E999", "0808999170065", Pol.muski));
                j++;
                glasaci[j].ProvjeraJmbg("0808999170065");
                glasaci.Add(new Glasac("Demir", "Damic" + (char)i, new DateTime(1999, 8, 8), "sklj 123", "999E999", "0808999170065", Pol.muski));
                j++;
                glasaci[j].ProvjeraJmbg("0808999170065");
                glasaci.Add(new Glasac("Ivan" + (char)i, "Damic", new DateTime(1999, 8, 8), "sklj 123", "999E999", "0808999170065", Pol.muski));
                j++;
                glasaci[j].ProvjeraJmbg("0808999170065");
                glasaci.Add(new Glasac("Ivan", "Damic" + (char)i, new DateTime(1999, 8, 8), "sklj 123", "999E999", "0808999170065", Pol.muski));
                j++;
                glasaci[j].ProvjeraJmbg("0808999170065");
                glasaci.Add(new Glasac("Mujo" + (char)i, "Damic", new DateTime(1999, 8, 8), "sklj 123", "999E999", "0808999170065", Pol.muski));
                j++;
                glasaci[j].ProvjeraJmbg("0808999170065");
                glasaci.Add(new Glasac("Mujo", "Damic" + (char)i, new DateTime(1999, 8, 8), "sklj 123", "999E999", "0808999170065", Pol.muski));
                j++;
                glasaci[j].ProvjeraJmbg("0808999170065");
                glasaci.Add(new Glasac("Suljo" + (char)i, "Damic", new DateTime(1999, 8, 8), "sklj 123", "999E999", "0808999170065", Pol.muski));
                j++;
                glasaci[j].ProvjeraJmbg("0808999170065");
                glasaci.Add(new Glasac("Suljo", "Damic" + (char)i, new DateTime(1999, 8, 8), "sklj 123", "999E999", "0808999170065", Pol.muski));
                j++;
                glasaci[j].ProvjeraJmbg("0808999170065");
                glasaci.Add(new Glasac("Neko" + (char)i, "Damic", new DateTime(1999, 8, 8), "sklj 123", "999E999", "0808999170065", Pol.muski));
                j++;
                glasaci[j].ProvjeraJmbg("0808999170065");
                glasaci.Add(new Glasac("Neko", "Damic" + (char)i, new DateTime(1999, 8, 8), "sklj 123", "999E999", "0808999170065", Pol.muski));
                j++;
                glasaci[j].ProvjeraJmbg("0808999170065");
            }
            
            
            int y = 0;

            Assert.IsTrue(true);
        }
    }
}
