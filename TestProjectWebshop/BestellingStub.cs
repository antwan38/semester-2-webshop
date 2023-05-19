using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using Interfaces;

namespace TestProjectWebshop
{
    public class BestellingStub : IBestellingDAL
    {
        public List<BestellingDTO> bestellingDTOs = new List<BestellingDTO>();

        public BestellingStub()
        {
            BestellingDTO bestelling = new BestellingDTO
            {
                BestellingNummer = 1,
                GebruikerNummer = 2,
                GadgetDTOs = new List<GadgetDTO> { new GadgetDTO { Aantal = 10,
                Beschrijving = "Hallo dit is de tweede testgadget",
                Categorie = "test",
                GadgetNummer = 2,
                Naam = "TestGadget2",
                Prijs = (decimal)45.7,
                VerkoperID = 2,
                } },
            };

            BestellingDTO bestelling2 = new BestellingDTO
            {
                BestellingNummer = 2,
                GebruikerNummer = 2,
                GadgetDTOs = new List<GadgetDTO> { new GadgetDTO { Aantal = 5,
                Beschrijving = "Hallo dit is een testGadget",
                Categorie = "test",
                GadgetNummer = 1,
                Naam = "TestGadget",
                Prijs = (decimal)4.7,
                VerkoperID = 2
                } },
            };

            BestellingDTO bestelling3 = new BestellingDTO
            {
                BestellingNummer = 3,
                GebruikerNummer = 2,
                GadgetDTOs = new List<GadgetDTO> { new GadgetDTO { Aantal = 15,
                Beschrijving = "Hallo dit is de derde testgadget",
                Categorie = "test",
                GadgetNummer = 3,
                Naam = "TestGadget3",
                Prijs = (decimal)200.30,
                VerkoperID = 2,
                } },
            };

            BestellingDTO bestelling4 = new BestellingDTO
            {
                BestellingNummer = 4,
                GebruikerNummer = 1,
                GadgetDTOs = new List<GadgetDTO> { new GadgetDTO{ Aantal = 10,
                Beschrijving = "Hallo dit is de tweede testgadget",
                Categorie = "test",
                GadgetNummer = 2,
                Naam = "TestGadget2",
                Prijs = (decimal)45.7,
                VerkoperID = 2,
                } },
            };
            bestellingDTOs.Add(bestelling);
            bestellingDTOs.Add(bestelling2); 
            bestellingDTOs.Add(bestelling3);
            bestellingDTOs.Add(bestelling4);

        }

        public List<BestellingDTO> LaadBestellingen(int id)
        {
            List<BestellingDTO> list = new List<BestellingDTO>();
            foreach (BestellingDTO bestelling in bestellingDTOs)
            {
                if (bestelling.GebruikerNummer == id)
                {
                    list.Add(bestelling);
                }
            }
            return list;
        }

        public bool ZetBestellingFase1(BestellingDTO bestellingDTO)
        {
            int StartBestellingCount = bestellingDTOs.Count;
            bestellingDTOs.Add(bestellingDTO);
            if (bestellingDTOs.Count - StartBestellingCount >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

        public BestellingDTO ReturnLastBestellingDTO()
        {
            return bestellingDTOs[bestellingDTOs.Count - 1];
        }
    }
}
