using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using Interfaces;

namespace TestProjectWebshop
{
    public class GebruikerStub : IGebruikerDAL
    {
        public List<GebruikerDTO> gebruikerDTOs = new List<GebruikerDTO>();

        public GebruikerStub()
        {
            GebruikerDTO gebruikerDTO = new GebruikerDTO 
            {   Emailadress = "Antwansittard@gmail.com", 
                GebruikerID = 1, 
                IsVerkoper = true, 
                Naam = "Antwan Bakker", 
                Postcode = "4036ty" 
            };

            GebruikerDTO gebruikerDTO1 = new GebruikerDTO
            {
                Emailadress = "Boris@gmail.com",
                GebruikerID = 2,
                IsVerkoper = false,
                Naam = "Boris",
                Postcode = "1090ad"
            };

            GebruikerDTO gebruikerDTO2 = new GebruikerDTO
            {
                Emailadress = "Tanno@gmail.com",
                GebruikerID = 3,
                IsVerkoper = false,
                Naam = "Tanno",
                Postcode = "4754up"
            };

            gebruikerDTOs.Add(gebruikerDTO);
            gebruikerDTOs.Add(gebruikerDTO1);
            gebruikerDTOs.Add(gebruikerDTO2);
        }

        public GebruikerDTO KrijgGebruiker(int ID)
        {
            foreach (GebruikerDTO gebruikerDTO in gebruikerDTOs)
            {
                if(gebruikerDTO.GebruikerID == ID) 
                { 
                    return gebruikerDTO; 
                }
            }
            return new GebruikerDTO();
        }

        public GebruikerDTO LogIn(string LogName, string Wachtwoord)
        {
            foreach (GebruikerDTO gebruikerDTO in gebruikerDTOs)
            {
                if (gebruikerDTO.Naam == LogName)
                {
                    return gebruikerDTO;
                }
            }
            return new GebruikerDTO();
        }

        public int Registreer(GebruikerDTO gebruikerDTO, string Wachtwoord)
        {
            if (!string.IsNullOrEmpty(Wachtwoord))
            {
                int startGebruikerCount = gebruikerDTOs.Count;
                gebruikerDTOs.Add(gebruikerDTO);
                return gebruikerDTOs.Count - startGebruikerCount;
            }
            return 0;
        }
    }
}
