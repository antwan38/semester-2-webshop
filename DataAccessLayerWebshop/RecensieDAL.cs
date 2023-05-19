using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Interfaces;

namespace DataAccessLayer
{
    public class RecensieDAL : Database, IRecensieDAL
    {
        public List<RecensieDTO> LaadRecensies(int id)
        {
            OpenConn();
            SqlCommand command;
            SqlDataReader reader;
            string sql;
            sql = "select* from Recensie where GadgetID = @ID";
            command = new SqlCommand(sql, conn);
            command.Parameters.Add(new SqlParameter("@ID", id));
            reader = command.ExecuteReader();
            List<RecensieDTO> recensies = new List<RecensieDTO>();
            while (reader.Read())
            {
               recensies.Add(new RecensieDTO { recensie = (string)reader["Recensie"],
                   GebruikerID = (int)reader["GebruikerID"], GadgetID = (int)reader["GadgetID"] });
            }
            SluitConn();
            return recensies;
        }

        public int ZetRecensie(RecensieDTO recensieDTO)
        {
            OpenConn();
            SqlCommand command;
            string sql;
            sql = "insert into Recensie (GadgetID, Recensie, GebruikerID) values (@GadgetID, @Recensie, @GebruikerID)";
            command = new SqlCommand(sql, conn);
            command.Parameters.Add(new SqlParameter("@GadgetID", recensieDTO.GadgetID));
            command.Parameters.Add(new SqlParameter("@Recensie", recensieDTO.recensie));
            command.Parameters.Add(new SqlParameter("@GebruikerID", recensieDTO.GebruikerID));
            int nrRowsSaved = command.ExecuteNonQuery();
            SluitConn();
            return nrRowsSaved;
        }
    }
}
