using Interfaces;
using LogicLayer;
using webShopASP.Models;

namespace webShopASP.Converter
{
    public static class ViewModelConverter
    {
        public static GebruikerViewModel ToViewModel(Gebruiker gebruiker)
        {
           return new GebruikerViewModel
            {
                Naam = gebruiker.Naam,
                Emailadress = gebruiker.Emailadress,
                GebruikerID = gebruiker.GebruikerID,
                IsVerkoper = gebruiker.IsVerkoper,
                Postcode = gebruiker.Postcode,
            };

        }

        public static GadgetViewModel ToViewModel(Gadget gadget, IGebruikerDAL igebruiker)
        {
            Gebruiker gebruiker = new Gebruiker();
            gebruiker = gebruiker.KrijgGebruiker(gadget.VerkoperID, igebruiker);
            return new GadgetViewModel
            {
                Aantal = gadget.Aantal,
                Beschrijving = gadget.Beschrijving,
                Categorie = gadget.Categorie,
                GadgetNummer = gadget.GadgetNummer,
                Prijs = Convert.ToString(gadget.Prijs),
                Naam = gadget.Naam,
                Verkoper = new GebruikerViewModel
                {
                    GebruikerID = gebruiker.GebruikerID,
                    Emailadress = gebruiker.Emailadress,
                    IsVerkoper = gebruiker.IsVerkoper,
                    Naam = gebruiker.Naam,
                    Postcode = gebruiker.Postcode,
                },
            };

        }
        public static RecensieViewModel ToViewModel(Recensie recensie, IGebruikerDAL igebruiker)
        {
            Gebruiker gebruiker = new Gebruiker();
            gebruiker = gebruiker.KrijgGebruiker(recensie.Gebruiker, igebruiker);
            return new RecensieViewModel { bericht = recensie.Bericht, Gebruiker = gebruiker.Naam };
        }
        public static Gebruiker FromViewModel(GebruikerViewModel gebruikerViewModel)
        {
            return new Gebruiker(gebruikerViewModel.Naam, gebruikerViewModel.Emailadress, gebruikerViewModel.Postcode, gebruikerViewModel.GebruikerID, gebruikerViewModel.IsVerkoper);
        }
    }
}
