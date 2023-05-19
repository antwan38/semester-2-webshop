using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace LogicLayer
{
    public class DALFactory
    {
        public GadgetDAL GetGadgetDAL()
        {
            return new GadgetDAL();
        }

        public RecensieDAL GetRecensieDAL() 
        { 
            return new RecensieDAL(); 
        }
        public BestellingDAL GetBestellingDAL()
        {
            return new BestellingDAL();
        }

        public GebruikerDAL GetGebruikerDAL()
        {
            return new GebruikerDAL();
        }

        public WinkelwagenDAL GetWinkelwagenDAL()
        {
            return new WinkelwagenDAL();
        }
    }
}
