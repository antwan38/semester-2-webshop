using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Interfaces;

namespace DataAccessLayer
{
    public class WinkelwagenDAL : Database, IWinkelwagenDAL
    {
        public List<GadgetDTO> LaadWinkelGadget(int GebruikerID)
        {
            OpenConn();
            List<GadgetDTO> GadgetDTOs = new List<GadgetDTO>();
            List<GadgetDTO> GadgetDTOsViaID = new List<GadgetDTO>();

            List<int> GadgetsID = new List<int>();
            List<int> GadgetsAantal = new List<int>();
            SqlDataReader reader;
            string sql = "select * from WinkelwagenRegeling"; 
            SqlCommand cmd = new SqlCommand(sql, conn);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
               if(GebruikerID == (int)reader["WinkelwagenID"])
                {
                    GadgetsID.Add((int)reader["GadgetID"]);
                    GadgetsAantal.Add((int)reader["Aantal"]);
                }
            }

            SluitConn();
            GadgetDTOsViaID = VoegToeGadgetDTO(GadgetsID);
            for (int i = 0; i < GadgetsID.Count; i++)
            {
                GadgetDTOs.Add(new GadgetDTO
                {
                    Beschrijving = GadgetDTOsViaID[i].Beschrijving,
                    GadgetNummer = GadgetDTOsViaID[i].GadgetNummer,
                    Aantal = GadgetsAantal[i],
                    VerkoperID = GadgetDTOsViaID[i].VerkoperID,
                    Categorie = GadgetDTOsViaID[i].Categorie,
                    Naam = GadgetDTOsViaID[i].Naam,
                    Prijs = GadgetDTOsViaID[i].Prijs,
                    
                });
            }
            return GadgetDTOs;
        }

        

        private List<GadgetDTO> VoegToeGadgetDTO(List<int> GadgetIDs)
        {
            GadgetDAL gadgetDAL = new GadgetDAL();
            List<GadgetDTO> gadgetDTOs = new List<GadgetDTO>();
            foreach(int id in GadgetIDs)
            {
                foreach(GadgetDTO gadgetDTO in gadgetDAL.LaadGadgets())
                {
                    if(id == gadgetDTO.GadgetNummer) 
                    { gadgetDTOs.Add(gadgetDTO); }
                    
                }
            }
            return gadgetDTOs;

        }

        public int DeleteGadgetuitWinkelwagen(int GadgetID, int GebruikerID)
        {
            OpenConn();
            string sql = $"DELETE FROM WinkelwagenRegeling WHERE WinkelwagenID = @WinkelID and GadgetID = @GadgetID;";
            SqlCommand sqlCommand = new SqlCommand(sql, conn);
            sqlCommand.Parameters.Add(new SqlParameter("@WinkelID", GebruikerID));
            sqlCommand.Parameters.Add(new SqlParameter("@GadgetID", GadgetID));
            int nrRowsDeleted = sqlCommand.ExecuteNonQuery();
            SluitConn();
            return nrRowsDeleted;
        }


        public int ZetGadgetInWinkelwagen(int GadgetID, int GebruikerID)
        {
            OpenConn();
            string sql = $"insert into WinkelwagenRegeling (WinkelwagenID, GadgetID, Aantal) values (@WinkelID, @GadgetID, 1);";
            SqlCommand sqlCommand = new SqlCommand(sql, conn);
            sqlCommand.Parameters.Add(new SqlParameter("@WinkelID", GebruikerID));
            sqlCommand.Parameters.Add(new SqlParameter("@GadgetID", GadgetID));
            int nrRowsSaved = sqlCommand.ExecuteNonQuery();
            SluitConn();
            return nrRowsSaved;
        }

        
    }
}
