using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using Interfaces;

namespace LogicLayer
{
    public class GebruikerContainer
    {
        private IGebruikerDAL gebruikerDAL;
        public GebruikerContainer(IGebruikerDAL igebruikerDAL)
        {
            gebruikerDAL = igebruikerDAL;
        }

        public Gebruiker Login(string Naam, string Wachtwoord)
        {
           Gebruiker gebruiker = new Gebruiker(gebruikerDAL.LogIn(Naam, Wachtwoord));
           return gebruiker;
        }

        public int Registreer(Gebruiker gebruiker, string wachtwoord)
        {
            return gebruikerDAL.Registreer(gebruiker.NaarGebruikerDTO(), wachtwoord);
        }
    }
}
