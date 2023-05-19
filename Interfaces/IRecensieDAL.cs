using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace Interfaces
{
    public interface IRecensieDAL
    {

        public List<RecensieDTO> LaadRecensies(int id);
        public int ZetRecensie(RecensieDTO recensieDTO);
        
        
    }
}
