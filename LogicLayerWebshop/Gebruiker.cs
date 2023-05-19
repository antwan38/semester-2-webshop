using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using Interfaces;

namespace LogicLayer
{

    public class Gebruiker
    {
        public string Naam { get; private set; }
        public string Emailadress { get; private set; }
        public string Postcode { get; private set; }
        public int GebruikerID { get; private set; }
        public bool IsVerkoper { get; private set; }
       

        public Gebruiker()
        {
           
        }

        public Gebruiker(string naam, string emailadress, string postcode, int gebruikerID, bool isVerkoper)
        {
            Naam = naam;
            Emailadress = emailadress;
            Postcode = postcode;
            GebruikerID = gebruikerID;
            IsVerkoper = isVerkoper;
        }

        public Gebruiker(GebruikerDTO DTO)
        {
            GebruikerID = DTO.GebruikerID;
            Naam = DTO.Naam;
            Emailadress = DTO.Emailadress;
            Postcode = DTO.Postcode;
            IsVerkoper = DTO.IsVerkoper;
        }

        public Gebruiker KrijgGebruiker(int id, IGebruikerDAL igebruikerDAL)
        {
            
            return new Gebruiker(igebruikerDAL.KrijgGebruiker(id));
        }

        public GebruikerDTO NaarGebruikerDTO()
        {
            return new GebruikerDTO
            {
                Naam = this.Naam,
                Emailadress = this.Emailadress,
                Postcode = this.Postcode,
                IsVerkoper = this.IsVerkoper,
                GebruikerID = this.GebruikerID,
            };
        }
    }
}
