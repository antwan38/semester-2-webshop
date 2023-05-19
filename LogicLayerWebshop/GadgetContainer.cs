using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using Interfaces;

namespace LogicLayer
{

    public class GadgetContainer
    {
        private IGadgetDAL GadgetDAL;

        public GadgetContainer(IGadgetDAL gadgetDAL)
        {
            GadgetDAL = gadgetDAL;
        }

        public List<Gadget> LaadGadgets()
        {
            List<Gadget> Gadgets = new List<Gadget>();
            foreach (GadgetDTO gadgetDTO in GadgetDAL.LaadGadgets())
            {
                Gadgets.Add(new Gadget(gadgetDTO));
            }
            return Gadgets;
        }

        public int Verwijder(int Gadgetid)
        {
            return GadgetDAL.Verwijder(Gadgetid);

        }


    }
}

