using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using Interfaces;

namespace TestProjectWebshop
{
    public class RecensieStub : IRecensieDAL
    {
        public List<RecensieDTO> RecensieDTOs = new List<RecensieDTO>();
     


        public RecensieStub()
        {
            RecensieDTO recensieDTO = new RecensieDTO { recensie = "test recensie nummer 1", GebruikerID = 1, GadgetID = 1 };
            RecensieDTO recensieDTO1 = new RecensieDTO { recensie = "test recensie nummer 2", GebruikerID = 1, GadgetID = 2 };
            RecensieDTO recensieDTO2 = new RecensieDTO { recensie = "test recensie nummer 3", GebruikerID = 1, GadgetID = 1 };
            RecensieDTO recensieDTO3 = new RecensieDTO { recensie = "test recensie nummer 4", GebruikerID = 1, GadgetID = 1 };
            RecensieDTO recensieDTO4 = new RecensieDTO { recensie = "test recensie nummer 5", GebruikerID = 1, GadgetID = 2 };
            RecensieDTOs.Add(recensieDTO1);
            RecensieDTOs.Add(recensieDTO2);
            RecensieDTOs.Add(recensieDTO3);
            RecensieDTOs.Add(recensieDTO4);
            RecensieDTOs.Add(recensieDTO);
        }

        public List<RecensieDTO> LaadRecensies(int id)
        {
            List<RecensieDTO> list = new List<RecensieDTO>();
            foreach (RecensieDTO recensieDTO in RecensieDTOs)
            {
                if (recensieDTO.GadgetID == id)
                {
                    list.Add(recensieDTO);
                }
            }
            return list;
        }

        public int ZetRecensie(RecensieDTO recensieDTO)
        {
            int StartRecensieCount = RecensieDTOs.Count;
            RecensieDTOs.Add(recensieDTO);
            return RecensieDTOs.Count - StartRecensieCount;
        }
    }
}
