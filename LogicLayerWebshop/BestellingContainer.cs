using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using Interfaces;

namespace LogicLayer
{ 
    public class BestellingContainer
    {
        private IGadgetDAL GadgetDAL;
        private IBestellingDAL iBestellingDAL;
        private int ID;

        public BestellingContainer(int id, IGadgetDAL gadgetDAL, IBestellingDAL ibestellingDAL)
        {
            GadgetDAL = gadgetDAL;
            iBestellingDAL = ibestellingDAL;
            ID = id; 
            
        }

        public List<Bestelling> LaadBestellingen()
        {
            List<Bestelling> Bestellings = new List<Bestelling>();
            foreach (BestellingDTO bestelling in iBestellingDAL.LaadBestellingen(ID))
            {
                Bestellings.Add(NaarBestelling(bestelling));
            }
            return Bestellings;
        }

        private Bestelling NaarBestelling(BestellingDTO bestelling)
        {
            List<Gadget> gadgets = new List<Gadget>();
            GadgetContainer gadgetContainer = new GadgetContainer(GadgetDAL);
            foreach (GadgetDTO gadgetDTO in bestelling.GadgetDTOs)
            {
                gadgets.Add(new Gadget(gadgetDTO));
            }
            return new Bestelling( iBestellingDAL ,bestelling.GebruikerNummer, bestelling.BestellingNummer, gadgets );
        }

    }
}
