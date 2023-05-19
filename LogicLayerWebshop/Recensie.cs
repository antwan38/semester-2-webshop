using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using Interfaces;

namespace LogicLayer
{

    public class Recensie
    {

        public string Bericht { get; private set; }
        public int Gebruiker { get; private set; }
        public int GadgetID { get; private set; }
        private IRecensieDAL irecensieDAL;
       

        public Recensie( IRecensieDAL recensieDAL, string bericht, int gebruiker, int gadgetID)
        {
            Bericht = bericht;
            Gebruiker = gebruiker;
            irecensieDAL = recensieDAL;
            GadgetID = gadgetID;



        }
        public Recensie(RecensieDTO recensieDTO)
        {
            Bericht = recensieDTO.recensie;
            GadgetID = recensieDTO.GadgetID;
            Gebruiker = recensieDTO.GebruikerID;
        }
        

        public int VoegToe()
        {
            return irecensieDAL.ZetRecensie(NaarRecencieDTO());
     
        }

        private RecensieDTO NaarRecencieDTO()
        {
            return new RecensieDTO { GebruikerID = Gebruiker, recensie = Bericht, GadgetID = this.GadgetID};
        }
    }
}
