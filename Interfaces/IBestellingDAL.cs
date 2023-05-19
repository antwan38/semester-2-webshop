using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace Interfaces
{
    public interface IBestellingDAL
    {

        public List<BestellingDTO> LaadBestellingen(int id);
        public bool ZetBestellingFase1(BestellingDTO bestellingDTO);
        
        
    }
}
