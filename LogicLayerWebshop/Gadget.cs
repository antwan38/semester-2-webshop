using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using Interfaces;

namespace LogicLayer
{

    public class Gadget
    {
        public int Aantal { get; private set; }
        public decimal Prijs { get; private set; }
        public string Beschrijving { get; private set; }
        public string Categorie { get; private set; }
        public string Naam { get; private set; }
        public int GadgetNummer { get; private set; }
       
        public int VerkoperID { get; private set; }
        private IGadgetDAL iGadgetDAL;

        

        public Gadget(IGadgetDAL gadgetDAL, int verkoperID, string beschrijving, int aantal, string naam, string categorie, decimal prijs)
        {
            Aantal = aantal;
            VerkoperID = verkoperID;
            Beschrijving = beschrijving;
            Categorie = categorie;
            Naam = naam;
            Prijs = prijs;
            iGadgetDAL = gadgetDAL;

        }

        public Gadget(IGadgetDAL gadgetDAL, int verkoperID, string beschrijving, int aantal, string naam, string categorie, decimal prijs, int gadgetNummer)
        {
            Aantal = aantal;
            VerkoperID = verkoperID;
            Beschrijving = beschrijving;
            Categorie = categorie;
            Naam = naam;
            Prijs = prijs;
            iGadgetDAL = gadgetDAL;
            GadgetNummer = gadgetNummer;
        }

       
        public Gadget(GadgetDTO dto)
        {
            Aantal = dto.Aantal;
            GadgetNummer = dto.GadgetNummer;
            Naam = dto.Naam;
            Beschrijving =  dto.Beschrijving;
            Categorie = dto.Categorie;
            Prijs = dto.Prijs;
            VerkoperID = dto.VerkoperID;
           
           
        }

        public Gadget VeranderAantal(int aantal)
        {
            
            return new Gadget(iGadgetDAL, VerkoperID, Beschrijving, aantal, Naam, Categorie, Prijs, GadgetNummer);
        }


        public int Voegtoe()
        {
            
            return iGadgetDAL.ZetGadget(NaarGadgetDTO());


        }

        public int Update()
        {

            return iGadgetDAL.Update(NaarGadgetDTO());


        }

        public int Update(IGadgetDAL gadgetDAL)
        {

            return gadgetDAL.Update(NaarGadgetDTO());


        }

        public GadgetDTO NaarGadgetDTO()
        {


            return new GadgetDTO
            {
                Aantal = this.Aantal,
                Beschrijving = this.Beschrijving,
                Categorie = this.Categorie,
                Naam = this.Naam,
                Prijs = this.Prijs,
                GadgetNummer = this.GadgetNummer,
                VerkoperID = VerkoperID,
            };
        }


    }
}
