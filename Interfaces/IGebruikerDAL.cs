using System;
using System.Collections.Generic;

namespace Interfaces
{
    public interface IGebruikerDAL
    {
        public GebruikerDTO KrijgGebruiker(int ID);
        public GebruikerDTO LogIn(string LogName, string Wachtwoord);
        public int Registreer(GebruikerDTO gebruikerDTO, string Wachtwoord);
        
        
    }
}
