using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public struct BestellingDTO
    {
        public int GebruikerNummer;
        public int BestellingNummer;
        public List<GadgetDTO> GadgetDTOs;
    }
}
