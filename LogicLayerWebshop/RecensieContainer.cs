using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using Interfaces;

namespace LogicLayer
{ 
    public class RecensieContainer
    {
        private IRecensieDAL Recensie;
        private int ID;

        public RecensieContainer(int id, IRecensieDAL recensie)
        {
            ID = id; 
            Recensie = recensie;
        }

        public List<Recensie> LaadRecensies()
        {
            List<Recensie> recensies = new List<Recensie>();
            foreach (RecensieDTO recensie in Recensie.LaadRecensies(ID))
            {
                recensies.Add(new Recensie(recensie));
            }
            return recensies;
        }

        
        

    }
}
