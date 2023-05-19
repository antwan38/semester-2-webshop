using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using Interfaces;

namespace LogicLayer
{

    public class Bestelling
    {

        public List<Gadget> Gadgets { get; private set; }
        public int GebruikerNummer { get; private set; }
        public int BestellingNummer { get; private set; }
        private IBestellingDAL ibestellingDAL;

        public Bestelling( IBestellingDAL bestellingDAL, int gebruikerNummer, int bestellingNummer, List<Gadget> gadgets)
        {
            GebruikerNummer = gebruikerNummer;
            BestellingNummer = bestellingNummer;
            Gadgets = gadgets;
            ibestellingDAL = bestellingDAL;

        }
        public Bestelling()
        {

        }
        public Bestelling(IBestellingDAL bestellingDAL)
        {
            ibestellingDAL = bestellingDAL;
        }

        public bool VoegToe(List<Gadget> gadgets, int gebruikernummer)
        {
            
            GebruikerNummer = gebruikernummer;
            Gadgets = gadgets;
            return ibestellingDAL.ZetBestellingFase1(NaarBestellingDTO());


        }

        private BestellingDTO NaarBestellingDTO()
        {
            List<GadgetDTO> gadgetDTOs = new List<GadgetDTO>();
            foreach (Gadget gadget in Gadgets)
            {
                gadgetDTOs.Add(gadget.NaarGadgetDTO());
            }

            return new BestellingDTO
            {
                GebruikerNummer = this.GebruikerNummer,
                GadgetDTOs = gadgetDTOs,

            };
        }
    }
}
