using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Interfaces;
namespace DataAccessLayer
{
    public class GebruikerDAL : Database, IGebruikerDAL
    {
        public GebruikerDTO KrijgGebruiker(int ID)
        {
            OpenConn();
            string sql = $"select* from Gebruiker where GebruikerID = @ID;";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add(new SqlParameter("@ID", ID));
            SqlDataReader dr = cmd.ExecuteReader();
            GebruikerDTO gebruikerDTO = new GebruikerDTO();
            while (dr.Read())
            {
               gebruikerDTO = new GebruikerDTO 
                {
                    Naam = (string)dr["Naam"],
                    Emailadress = (string)dr["Emailadres"],
                    Postcode = (string)dr["Postcode"],
                    GebruikerID = ID,
                    IsVerkoper = (bool)dr["IsVerkoper"]
                };
            }
            SluitConn();
            return gebruikerDTO;
        }

        

        public GebruikerDTO LogIn(string LogName, string Wachtwoord)
        {
            OpenConn();
            string sql = $"select* from Gebruiker where Naam = @Naam and Wachtwoord = @Wachtwoord;";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add(new SqlParameter("@Naam", LogName));
            cmd.Parameters.Add(new SqlParameter("@Wachtwoord", Wachtwoord));
            SqlDataReader dr = cmd.ExecuteReader();
            GebruikerDTO gebruikerDTO = new GebruikerDTO();
            while (dr.Read())
            {
                gebruikerDTO = new GebruikerDTO
                {
                    Naam = (string)dr["Naam"],
                    Emailadress = (string)dr["Emailadres"],
                    Postcode = (string)dr["Postcode"],
                    GebruikerID = (int)dr["GebruikerID"],
                    IsVerkoper = (bool)dr["IsVerkoper"]
                };
            }
            SluitConn();
            return gebruikerDTO;
        }

       

        public int Registreer(GebruikerDTO gebruikerDTO, string Wachtwoord)
        {
            if (CheckEmailIsBestaand(gebruikerDTO.Emailadress) == true)
            {
                return 2;
            }
            OpenConn();
            string sql = $"insert into Gebruiker (Naam, Emailadres, Postcode, Wachtwoord, IsVerkoper) values (@Naam, @Email, @Postcode, @Wachtwoord, @IsVerkoper);";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add(new SqlParameter("@Naam", gebruikerDTO.Naam));
            cmd.Parameters.Add(new SqlParameter("@Wachtwoord", Wachtwoord));
            cmd.Parameters.Add(new SqlParameter("@Email", gebruikerDTO.Emailadress));
            cmd.Parameters.Add(new SqlParameter("@Postcode", gebruikerDTO.Postcode));
            cmd.Parameters.Add(new SqlParameter("@IsVerkoper", gebruikerDTO.IsVerkoper));
            int nrRowsSaved = cmd.ExecuteNonQuery();
            
            
            SluitConn();
            return nrRowsSaved;
        }

        private bool CheckEmailIsBestaand(string email)
        {
            OpenConn();
            string sql = $"select* from Gebruiker where Emailadres = @Email;";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add(new SqlParameter("@Email", email));
            SqlDataReader dr = cmd.ExecuteReader();
            GebruikerDTO gebruikerDTO = new GebruikerDTO();
            while (dr.Read())
            {
                gebruikerDTO = new GebruikerDTO
                {
                    Naam = (string)dr["Naam"],
                    Emailadress = (string)dr["Emailadres"],
                    Postcode = (string)dr["Postcode"],
                    GebruikerID = (int)dr["GebruikerID"],
                    IsVerkoper = (bool)dr["IsVerkoper"]
                };
            }
            SluitConn();
            if (gebruikerDTO.Emailadress != null || gebruikerDTO.Emailadress == "")
            {
                return true;
            }
            return false;
        }
    }
}
