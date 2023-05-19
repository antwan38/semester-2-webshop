using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Interfaces;

namespace DataAccessLayer
{
    public class BestellingDAL : Database, IBestellingDAL
    {
        
        public List<BestellingDTO> LaadBestellingen(int id)
        {
            OpenConn();
            int bestelID = -1;
            bool check = false;
            List<BestellingDTO> bestellingDTOs = new List<BestellingDTO>();
            
            List<GadgetDTO> gadgets = new List<GadgetDTO>();
            SqlDataReader reader;
            string sql = "SELECT * FROM Bestelling b INNER JOIN BestellingRegeling br on b.BestellingID = br.BestellingID INNER JOIN Gadget g on br.GadgetID = g.GadgetID";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add(new SqlParameter("@ID", id));
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                if (id == (int)reader["GebruikerID"])
                {
                    if (bestelID == (int)reader["BestellingID"])
                    {
                        gadgets.Add(new GadgetDTO
                        {
                            Beschrijving = (string)reader["Beschrijving"],
                            Categorie = (string)reader["Categorie"],
                            Aantal = (int)reader["AantalBesteld"],
                            GadgetNummer = (int)reader["GadgetID"],
                            Naam = (string)reader["Naam"],


                        });


                    }
                    else
                    {
                        if (check == true)
                        {
                            bestellingDTOs.Add(new BestellingDTO
                            {
                                BestellingNummer = bestelID,
                                GebruikerNummer = (int)reader["GebruikerID"],
                                GadgetDTOs = ReturnNewList(gadgets),
                            });
                        }
                        check = true;
                        gadgets.Clear();

                        bestelID = (int)reader["BestellingID"];

                        gadgets.Add(new GadgetDTO
                        {
                            Beschrijving = (string)reader["Beschrijving"],
                            Categorie = (string)reader["Categorie"],
                            Aantal = (int)reader["AantalBesteld"],
                            GadgetNummer = (int)reader["GadgetID"],
                            Naam = (string)reader["Naam"],


                        });

                       

                    }
                }


            }
            if (gadgets.Count != 0)
            {
                bestellingDTOs.Add(new BestellingDTO
                {
                    BestellingNummer = bestelID,
                    GebruikerNummer = id,
                    GadgetDTOs = ReturnNewList(gadgets),
                });
            }
            SluitConn();
            return bestellingDTOs;
           
        }

       
        private List<GadgetDTO> ReturnNewList(List<GadgetDTO> gadgetDTOs)
        {
            List<GadgetDTO> newList = new List<GadgetDTO>();
            foreach (GadgetDTO gadget in gadgetDTOs)
            {
                newList.Add(gadget);
            }
            return newList;
        }


        public bool ZetBestellingFase1(BestellingDTO bestellingDTO)
        {
            OpenConn();
            int IDbestelling = 0;
            string sql = $"insert into Bestelling(GebruikerID) values(@GebruikerNummer) select SCOPE_IDENTITY() Delete from WinkelwagenRegeling where WinkelwagenID = @GebruikerNummer";
            SqlCommand sqlCommand = new SqlCommand(sql, conn);
            sqlCommand.Parameters.Add(new SqlParameter("@GebruikerNummer", bestellingDTO.GebruikerNummer));
            SqlDataReader reader;
            reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                IDbestelling = Convert.ToInt32(reader.GetValue(0));
            }
            SluitConn();
            int isRowsSaved = ZetBestellingFase2(IDbestelling, bestellingDTO);
            if (isRowsSaved >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private int ZetBestellingFase2(int bestellingID, BestellingDTO bestellingDTO) 
        {
            int nrRowsSaved = 0;
            OpenConn();
            foreach (GadgetDTO gadget in bestellingDTO.GadgetDTOs)
            {
                string sql = $"insert into BestellingRegeling(BestellingID, GadgetID, AantalBesteld) values(@bestellingID, @GadgetNummer, @Aantal);";
                SqlCommand sqlCommand = new SqlCommand(sql, conn);
                sqlCommand.Parameters.Add(new SqlParameter("@bestellingID", bestellingID));
                sqlCommand.Parameters.Add(new SqlParameter("@GadgetNummer", gadget.GadgetNummer));
                sqlCommand.Parameters.Add(new SqlParameter("@Aantal", gadget.Aantal));
                nrRowsSaved = sqlCommand.ExecuteNonQuery();
            }
            
            SluitConn();
            return nrRowsSaved;
        }
    }
}
