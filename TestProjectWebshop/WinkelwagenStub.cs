using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using Interfaces;

namespace TestProjectWebshop
{
    public class WinkelwagenStub : IWinkelwagenDAL
    {
        public List<int> Gebruiker1 = new List<int>();
        public List<int> Gebruiker2 = new List<int>();


        public WinkelwagenStub()
        {
            
            Gebruiker1.Add(1);
            Gebruiker1.Add(2);
            Gebruiker2.Add(3);

        }

        

        public List<GadgetDTO> LaadWinkelGadget(int GebruikerID)
        {
            List<GadgetDTO> gadgets = new List<GadgetDTO>();
            if(GebruikerID == 1)
            {
                foreach (int id in Gebruiker1)
                {
                    GadgetStub gadgetStub = new GadgetStub();
                    gadgets.Add(gadgetStub.ReturnGadgetByID(id));
                }
                return gadgets;
            }
            else if (GebruikerID == 2)
            {
                foreach (int id in Gebruiker2)
                {
                    GadgetStub gadgetStub = new GadgetStub();
                    gadgets.Add(gadgetStub.ReturnGadgetByID(id));
                }
                return gadgets;
            }
            else
            {
                return null; 
            }
        }

        public int DeleteGadgetuitWinkelwagen(int GadgetID, int GebruikerID)
        {
            int gadgetsStartAmount;
            if (GebruikerID == 1)
            {
                gadgetsStartAmount = Gebruiker1.Count;
                foreach (int id in Gebruiker1)
                {
                    if (GadgetID == id)
                    {
                        Gebruiker1.Remove(GadgetID);
                        return gadgetsStartAmount - Gebruiker1.Count;
                        
                    }
                }
            }
            else if (GebruikerID == 2)
            {
                gadgetsStartAmount = Gebruiker2.Count;
                foreach (int id in Gebruiker2)
                {
                    if (GadgetID == id)
                    {
                        Gebruiker1.Remove(GadgetID);
                        return gadgetsStartAmount - Gebruiker2.Count;
                    }
                }
            }
            return 404;

        }

        public int ReturnLastAddedItem(int GebruikerID)
        {
            if (GebruikerID == 1)
            {
                
                return Gebruiker1[Gebruiker1.Count - 1];
            }
            else if (GebruikerID == 2)
            {
               
                return Gebruiker2[Gebruiker2.Count - 1];
            }
            return 404;
        }


        public int ZetGadgetInWinkelwagen(int GadgetID, int GebruikerID)
        {
            int GadgetStartAmount;
            if (GebruikerID == 1)
            {
                GadgetStartAmount = Gebruiker1.Count;
                Gebruiker1.Add(GadgetID);
                return  Gebruiker1.Count - GadgetStartAmount;
            }
            else if (GebruikerID == 2)
            {
                GadgetStartAmount= Gebruiker2.Count;
                Gebruiker2.Add(GadgetID);
                return Gebruiker2.Count - GadgetStartAmount;
            }

            return 404;
        }
    }
}
