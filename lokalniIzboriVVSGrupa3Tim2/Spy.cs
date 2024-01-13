using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lokalniIzboriVVSGrupa3Tim2
{
    public class Spy : IProvjera
    {
        public bool Glasao { get; set; }
        public bool DaLiJeVecGlasao(string IDBroj)
        {
            if(Glasao == true)
            {
                return true;
            }
            else 
            { 
                return false; 
            }
        }
    }
}
