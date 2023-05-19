using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Interfaces;
using System.Activities.Expressions;
using DataAccessLayerWebshop;

namespace DataAccessLayer
{
    public class Database
    {
        private const string connectstring = "Server=mssqlstud.fhict.local;Database=dbi479677;User Id=dbi479677; Password=Antwan38; MultipleActiveResultSets=True";
        protected SqlConnection conn;

        protected Database()
        {
            conn = new SqlConnection(connectstring);
        }

        protected void OpenConn()
        {
            try { conn.Open();
                
            }
            catch (SqlException ex)
            {
                throw new TemporaryExceptionDAL("Check uw verbinding met de VPN");
            }
            
        }

        protected void SluitConn()
        {
           
            conn.Close(); 
        }


        






        

    }
}
