using System;
using System.Collections.Generic;


namespace Interfaces
{
    public interface IWinkelwagenDAL
    {
        
        public List<GadgetDTO> LaadWinkelGadget(int GebruikerID);
        public int DeleteGadgetuitWinkelwagen(int GadgetID, int GebruikerID);
        public int ZetGadgetInWinkelwagen(int GadgetID, int GebruikerID);
        
        
    }
}
