using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using Interfaces;


namespace LogicLayer
{

    public class WinkelwagenContainer
    {
        int id = 0;
        private IGadgetDAL GadgetDAL;
        private IWinkelwagenDAL WinkelwagenDAL;
       public WinkelwagenContainer(int gebruikerID, IWinkelwagenDAL winkelwagenDAL)
        {
            id = gebruikerID;
            WinkelwagenDAL = winkelwagenDAL;
            
            
        }
        public int DeleteGadgetuitWinkelwagen(int GadgetID)
        {
            
           return WinkelwagenDAL.DeleteGadgetuitWinkelwagen(GadgetID, id);
        }
        public int AddGadgetInWinkelwagen(int GadgetID)
        {
            
           return WinkelwagenDAL.ZetGadgetInWinkelwagen(GadgetID, id);
        }

        public List<Gadget> LaadWinkelGadget()
        {
            List<GadgetDTO> gadgetDTOs = WinkelwagenDAL.LaadWinkelGadget(id);
            if (gadgetDTOs != null)
            {
                List<Gadget> list = new List<Gadget>();
                foreach (GadgetDTO gadgetDTO in gadgetDTOs)
                {
                    list.Add(new Gadget(gadgetDTO));
                }
                return list;
            }
            return null;
        }
    }
}
