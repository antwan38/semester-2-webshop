using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using Interfaces;

namespace TestProjectWebshop
{
    public class GadgetStub : IGadgetDAL
    {
        public List<GadgetDTO> gadgetDTOs = new List<GadgetDTO>(); 

        public GadgetStub()
        {

            GadgetDTO gadget = new GadgetDTO
            {
                Aantal = 5,
                Beschrijving = "Hallo dit is een testGadget",
                Categorie = "test",
                GadgetNummer = 1,
                Naam = "TestGadget",
                Prijs = (decimal)4.7,
                VerkoperID = 2,
            };

            GadgetDTO gadget1 = new GadgetDTO
            {
                Aantal = 10,
                Beschrijving = "Hallo dit is de tweede testgadget",
                Categorie = "test",
                GadgetNummer = 2,
                Naam = "TestGadget2",
                Prijs = (decimal)45.7,
                VerkoperID = 2,
            };

            GadgetDTO gadget2 = new GadgetDTO
            {
                Aantal = 15,
                Beschrijving = "Hallo dit is de derde testgadget",
                Categorie = "test",
                GadgetNummer = 3,
                Naam = "TestGadget3",
                Prijs = (decimal)200.30,
                VerkoperID = 2,
            };
            gadgetDTOs.Add(gadget1);
            gadgetDTOs.Add(gadget2);
            gadgetDTOs.Add(gadget);

        }
        public List<GadgetDTO> LaadGadgets()
        {
            return gadgetDTOs;
        }

        public int Update(GadgetDTO gadgetDTO)
        {
            foreach(GadgetDTO gadget in gadgetDTOs)
            {
                if (gadget.GadgetNummer == gadgetDTO.GadgetNummer)
                {
                    gadgetDTOs.Remove(gadget);
                    gadgetDTOs.Insert(gadgetDTO.GadgetNummer - 1, gadgetDTO);
                    return 1;
                }
            }
            return 0;
        }

        public int Verwijder(int id)
        {
            int startCount = gadgetDTOs.Count;
            foreach (GadgetDTO gadget in gadgetDTOs)
            {
                if (gadget.GadgetNummer == id)
                {
                    gadgetDTOs.Remove(gadget);
                    return startCount - gadgetDTOs.Count;
                }
            }
             
            return startCount - gadgetDTOs.Count;
        }

        public int ZetGadget(GadgetDTO gadgetDTO)
        {
            int startCount = gadgetDTOs.Count;
            gadgetDTOs.Add(gadgetDTO);
            int endCount = gadgetDTOs.Count;
            return endCount - startCount;
        }

        public GadgetDTO ReturnLastGadget()
        {
            return gadgetDTOs[gadgetDTOs.Count - 1];
        }

        public GadgetDTO ReturnGadgetByID(int ID)
        {
            foreach (GadgetDTO gadgetDTO in gadgetDTOs)
            {
                if(gadgetDTO.GadgetNummer == ID)
                {
                    return gadgetDTO;
                }
            }
            return new GadgetDTO();
        }
    }
}
