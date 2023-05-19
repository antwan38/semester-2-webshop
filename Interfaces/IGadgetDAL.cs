using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;



namespace Interfaces
{
    public interface IGadgetDAL
    {
        public List<GadgetDTO> LaadGadgets();
        public int ZetGadget(GadgetDTO gadgetDTO);
        public int Update(GadgetDTO gadgetDTO);
        public int Verwijder(int id);
    }
}
