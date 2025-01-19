using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Controlador
{
    public class BBDD
    {
        private const string DatabaseFilename = "MiBDD.db3";

        public string DatabasePath =>
            Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);

        public BBDD()
        {
            CrearTabla();
            InsertarAdmin();
        }

        private void CrearTabla()
        {
            try
            {
                string query = "CREATE TABLE IF NOT EXISTS Users(Name TEXT PRIMARY KEY, Password TEXT NOT NULL, Rol TEXT CHECK (Rol IN ('user', 'admin')))";
                using (SqliteConnection connection = new SqliteConnection("Data Source=" + DatabasePath))
                {
                    connection.Open();
                    SqliteCommand cmd = connection.CreateCommand();
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (SqliteException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private int InsertarAdmin()
        {

            int result = 0;
            string query = "INSERT OR IGNORE INTO Users (Name, Password, Rol) VALUES (@name, @pass, @r)";
            try
            {
                using (SqliteConnection connection = new SqliteConnection("Data Source=" + DatabasePath))
                {
                    connection.Open();
                    SqliteCommand cmd = connection.CreateCommand();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@name", "admin");
                    cmd.Parameters.AddWithValue("@pass", "admin");
                    cmd.Parameters.AddWithValue("@r", "admin");
                    result = cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (SqliteException e)
            {
                Console.WriteLine(e.Message);
            }
            return result;
        }

        public int InsertarUsuario(string n, string p)
        {
            int result = 0;
            string query = "INSERT OR IGNORE INTO Users (Name, Password, Rol) VALUES (@name, @pass, @r)";
            try
            {
                using (SqliteConnection connection = new SqliteConnection("Data Source=" + DatabasePath))
                {
                    connection.Open();
                    SqliteCommand cmd = connection.CreateCommand();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@name", n);
                    cmd.Parameters.AddWithValue("@pass", p);
                    cmd.Parameters.AddWithValue("@r", "user");
                    result = cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (SqliteException e)
            {
                Console.WriteLine(e.Message);
            }
            return result;
        }

        public DataTable ConsultarUser(string n)
        {
            DataTable tabla = new DataTable();
            try
            {
                using (SqliteConnection connection = new SqliteConnection("Data Source=" + DatabasePath))
                {
                    connection.Open();
                    SqliteCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT Name, Password, Rol FROM Users WHERE Name = @name";
                    cmd.Parameters.AddWithValue("@name", n);
                    tabla.Load(cmd.ExecuteReader());
                }
            }
            catch (SqliteException e)
            {
                Console.WriteLine(e.Message);
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return tabla;
        }
    }
}