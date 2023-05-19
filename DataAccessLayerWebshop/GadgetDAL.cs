using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Interfaces;

namespace DataAccessLayer
{
    public class GadgetDAL : Database, IGadgetDAL
    {
       
        public List<GadgetDTO> LaadGadgets()
        {
           
            OpenConn();
            SqlCommand command;
            SqlDataReader reader;
            string sql;
            sql = "select* from Gadget";
            command = new SqlCommand(sql, conn);
            reader = command.ExecuteReader();

            List<GadgetDTO> gadgets = new List<GadgetDTO>();
           
            while (reader.Read())
            {
                
                gadgets.Add(new GadgetDTO
                {
                    Aantal = (int)reader["Aantal"],
                    Beschrijving = (string)reader["Beschrijving"],
                    Categorie = (string)reader["Categorie"],
                    Naam = (string)reader["Naam"],
                    GadgetNummer = (int)reader["GadgetID"],
                    Prijs = (decimal)reader["Prijs"],
                    VerkoperID = (int)reader["VerkoperID"],

            });
                

            }
            
            SluitConn();

            return gadgets;
        }

        public int Verwijder(int id)
        {
            
            OpenConn();
            SqlCommand command;
            int NrRowsDeleted = 0;
            string sql;
            sql = $"Delete Gadget where GadgetID = @ID;";
            command = new SqlCommand(sql, conn);
            command.Parameters.Add(new SqlParameter("@ID", id));
            NrRowsDeleted = command.ExecuteNonQuery();
            SluitConn();
            return NrRowsDeleted;


        }
        public int ZetGadget(GadgetDTO gadgetDTO)
        {
            OpenConn();
            SqlCommand command;
            int NrRowsSaved = 0;
            string sql;
            sql = $"INSERT INTO Gadget (VerkoperID, Beschrijving, Aantal, Naam, Categorie, Prijs) VALUES( @GebruikerID, @Beschrijving, @Aantal , @Naam, @Categorie, @Prijs);";
            command = new SqlCommand(sql, conn);
            command.Parameters.Add(new SqlParameter("@GebruikerID", gadgetDTO.VerkoperID));
            command.Parameters.Add(new SqlParameter("@Beschrijving", gadgetDTO.Beschrijving));
            command.Parameters.Add(new SqlParameter("@Aantal", gadgetDTO.Aantal));
            command.Parameters.Add(new SqlParameter("@Naam", gadgetDTO.Naam));
            command.Parameters.Add(new SqlParameter("@Categorie", gadgetDTO.Categorie));
            command.Parameters.Add(new SqlParameter("@Prijs", gadgetDTO.Prijs));
            NrRowsSaved = command.ExecuteNonQuery();
            SluitConn();
            return NrRowsSaved;

        }

        public int Update(GadgetDTO gadgetDTO)
        {
            OpenConn();
            SqlCommand command;
            int NrRowsUpdated = 0;
            string sql;
            sql = $"UPDATE Gadget SET VerkoperID = @GebruikerID, Beschrijving = @Beschrijving, Aantal = @Aantal, Naam = @Naam, Categorie = @Categorie, Prijs = @Prijs WHERE GadgetID = @ID; ";
            command = new SqlCommand(sql, conn);
            command.Parameters.Add(new SqlParameter("@GebruikerID", gadgetDTO.VerkoperID));
            command.Parameters.Add(new SqlParameter("@Beschrijving", gadgetDTO.Beschrijving));
            command.Parameters.Add(new SqlParameter("@Aantal", gadgetDTO.Aantal));
            command.Parameters.Add(new SqlParameter("@Naam", gadgetDTO.Naam));
            command.Parameters.Add(new SqlParameter("@Categorie", gadgetDTO.Categorie));
            command.Parameters.Add(new SqlParameter("@Prijs", gadgetDTO.Prijs));
            command.Parameters.Add(new SqlParameter("@ID", gadgetDTO.GadgetNummer));
            NrRowsUpdated = command.ExecuteNonQuery();
            SluitConn();
            return NrRowsUpdated;

        }

        
    }
}

