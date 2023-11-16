using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Windows;
using ÜbungCrud;

namespace DBConnection
{
    class SqliteDataAccess
    {
        
        public static Mitarbeiter loadMAbyID(int personalnr)
        {
            using (var con = new SQLiteConnection(loadConnectionString()))
            {
                con.Open();
                Mitarbeiter ma = new Mitarbeiter();

                using (var cmd = new SQLiteCommand(con))
                {
                    cmd.CommandText = "SELECT * FROM Mitarbeiter WHERE PersNr = @personalnr";
                    cmd.Parameters.AddWithValue("@personalnr", personalnr);
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        if(rdr.Read())
                        {
                            ma.persNr = rdr.GetInt32(0);
                            ma.name = rdr.GetString(1);
                            ma.gehalt= rdr.GetInt32(2);
                        }     
                        
                    }
                }
                return ma;
            }            
        }
        public static List<string> loadMA()
        {
            using (var con = new SQLiteConnection(loadConnectionString()))
            {
                con.Open();
                List<string> plist = new List<string>();
                string stm = "SELECT * FROM Mitarbeiter where PersNr";

                using (var cmd = new SQLiteCommand(stm, con))
                {
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Mitarbeiter ma = new Mitarbeiter(rdr.GetInt32(0), rdr.GetString(1), rdr.GetInt32(2));
                            plist.Add(ma.persNr + " - " + ma.name + " - " + ma.gehalt);
                        }
                        return plist;
                    }
                }
            }
        }
        public static void AddMA(Mitarbeiter person)
        {
            using (var con = new SQLiteConnection(loadConnectionString()))
            {
                con.Open();

                using (var cmd = new SQLiteCommand(con))
                {
                    cmd.CommandText = "INSERT INTO Mitarbeiter(name, gehalt) VALUES(@name,@gehalt)";
                    cmd.Parameters.AddWithValue("@name", person.name);
                    cmd.Parameters.AddWithValue("@gehalt", person.gehalt);
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();                 

                }
            }
        }

        public static void UpdateMA(Mitarbeiter person)
        {
            using (var con = new SQLiteConnection(loadConnectionString()))
            {
                con.Open();

                using (var cmd = new SQLiteCommand(con))
                {
                    cmd.CommandText = "UPDATE Mitarbeiter SET Name=@name, Gehalt=@gehalt WHERE PersNr=@persnr;";
                    cmd.Parameters.AddWithValue("@name", person.name);
                    cmd.Parameters.AddWithValue("@gehalt", person.gehalt);
                    cmd.Parameters.AddWithValue("@persnr", person.persNr);
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void removeMA(Mitarbeiter person)
        {
            using (var con = new SQLiteConnection(loadConnectionString()))
            {
                con.Open();

                using (var cmd = new SQLiteCommand(con))
                {
                    cmd.CommandText = "DELETE FROM Mitarbeiter WHERE PersNr=@persnr;";
                    cmd.Parameters.AddWithValue("@persnr", person.persNr);
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private static string loadConnectionString()
        {
            return "DataSource=MitarbeiterDB.db;Version=3;";
        }
    }
}
