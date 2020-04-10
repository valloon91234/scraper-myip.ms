using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopify2
{
    class Dao
    {
        const String DB_FILENAME = @"data.db";
        const String DB_PASSWORD = @"SELECT * FROM user WHERE id=@id AND deleted=0";
        const String DATETIME_FORMAT = @"yyyy-MM-dd HH:mm:ss";
        protected static SQLiteConnection connection = null;
        public static Boolean Encrypted;

        static Dao()
        {
            FileInfo fileInfo = new FileInfo(DB_FILENAME);
            if (!fileInfo.Exists) throw new FileNotFoundException("Can not find database file - " + DB_FILENAME);
            try
            {
                String connectionString = @"Data Source=" + DB_FILENAME + ";Password=" + DB_PASSWORD + ";Version=3";
                SQLiteConnection con = new SQLiteConnection(connectionString);
                con.Open();
                using (SQLiteCommand command = con.CreateCommand())
                {
                    command.CommandText = "PRAGMA encoding";
                    var result = command.ExecuteScalar();
                }
                Encrypted = true;
                connection = con;
            }
            catch
            {
                String connectionString = @"Data Source=" + DB_FILENAME + ";Version=3";
                SQLiteConnection con = new SQLiteConnection(connectionString);
                con.Open();
                using (SQLiteCommand command = con.CreateCommand())
                {
                    command.CommandText = "PRAGMA encoding";
                    var result = command.ExecuteScalar();
                }
                Encrypted = false;
                connection = con;
            }

            //using (SQLiteCommand command = connection.CreateCommand())
            //{
            //    command.CommandText = "PRAGMA journal_mode = WAL";
            //    var result = command.ExecuteNonQuery();
            //}
            //using (SQLiteCommand command = connection.CreateCommand())
            //{
            //    command.CommandText = "PRAGMA synchronous = NORMAL";
            //    var result = command.ExecuteNonQuery();
            //}
#if DEBUG
            Console.WriteLine("Connection has created.");
#endif
        }

        public static Boolean Close()
        {
            try
            {
                connection.Close();
                connection.Dispose();
                return true;
            }
            catch { }
            return false;
        }

        public static void Encrypt()
        {
            connection.ChangePassword(DB_PASSWORD);
        }

        public static void Decrypt()
        {
            connection.ChangePassword("");
        }

        public static T GetValue<T>(object obj)
        {
            Type type = typeof(T);
            if (obj == null || obj == DBNull.Value)
            {
                return default(T); // returns the default value for the type
            }
            else if (type == typeof(DateTime))
            {
                return (T)(Object)DateTime.ParseExact((String)obj, DATETIME_FORMAT, CultureInfo.CurrentCulture);
            }
            else
            {
                return (T)Convert.ChangeType(obj, typeof(T));
            }
        }

        public static String ToDateTimeString(DateTime dt)
        {
            return dt.ToString(DATETIME_FORMAT);
        }

        public static DateTime ParseDateTimeString(String s)
        {
            return DateTime.ParseExact(s, DATETIME_FORMAT, CultureInfo.CurrentCulture);
        }

        public static Boolean IsExist(String domain)
        {
            using (SQLiteCommand command = connection.CreateCommand())
            {
                command.CommandText = @"SELECT COUNT(*) count FROM tbl_domain WHERE domain=@domain";
                command.Parameters.Add("domain", System.Data.DbType.String).Value = domain;
                SQLiteDataReader dr = command.ExecuteReader();
                dr.Read();
                int count = Convert.ToInt32(dr["count"]);
                return count > 0;
            }
        }

        public static int Insert(Model m)
        {
            using (SQLiteCommand command = connection.CreateCommand())
            {
                command.CommandText = @"INSERT INTO tbl_domain(domain,ip,company,location,city,rating) VALUES(@Domain,@IP,@Company,@Location,@City,@Rating)";
                command.Parameters.Add("Domain", System.Data.DbType.String).Value = m.Domain;
                command.Parameters.Add("IP", System.Data.DbType.String).Value = m.IP;
                command.Parameters.Add("Company", System.Data.DbType.String).Value = m.Company;
                command.Parameters.Add("Location", System.Data.DbType.String).Value = m.Location;
                command.Parameters.Add("City", System.Data.DbType.String).Value = m.City;
                command.Parameters.Add("Rating", System.Data.DbType.String).Value = m.Rating;
                return command.ExecuteNonQuery();
            }
        }

        public static Model SelectByDomain(String domain)
        {
            using (SQLiteCommand command = connection.CreateCommand())
            {
                command.CommandText = @"SELECT * FROM tbl_domain WHERE domain=@domain";
                command.Parameters.Add("domain", System.Data.DbType.String).Value = domain;
                SQLiteDataReader dr = command.ExecuteReader();
                Model m = null;
                while (dr.Read())
                {
                    m = new Model();
                    m.Id = GetValue<int>(dr["id"]);
                    m.Domain = GetValue<String>(dr["domain"]);
                    m.IP = GetValue<String>(dr["ip"]);
                    m.Company = GetValue<String>(dr["company"]);
                    m.Location = GetValue<String>(dr["location"]);
                    m.City = GetValue<String>(dr["city"]);
                    m.Rating = GetValue<int>(dr["rating"]);
                    m.Email = GetValue<String>(dr["email"]);
                    m.Error = GetValue<String>(dr["error"]);
                }
                return m;
            }
        }

        public static List<Model> SelectAll(String suffix = null, Boolean onlyNoEmail = false, Boolean onlyNoError = false)
        {
            using (SQLiteCommand command = connection.CreateCommand())
            {
                if (onlyNoEmail)
                {
                    if (onlyNoError)
                        command.CommandText = @"SELECT * FROM tbl_domain WHERE email IS NULL AND error IS NULL ORDER BY id";
                    else
                        command.CommandText = @"SELECT * FROM tbl_domain WHERE email IS NULL ORDER BY id";
                }
                else
                {
                    if (suffix == null)
                        command.CommandText = @"SELECT * FROM tbl_domain ORDER BY id";
                    else
                        command.CommandText = @"SELECT * FROM tbl_domain" + suffix + " ORDER BY id";
                }
                SQLiteDataReader dr = command.ExecuteReader();
                List<Model> list = new List<Model>();
                while (dr.Read())
                {
                    Model m = new Model();
                    m.Id = GetValue<int>(dr["id"]);
                    m.Domain = GetValue<String>(dr["domain"]);
                    m.IP = GetValue<String>(dr["ip"]);
                    m.Company = GetValue<String>(dr["company"]);
                    m.Location = GetValue<String>(dr["location"]);
                    m.City = GetValue<String>(dr["city"]);
                    m.Rating = GetValue<int>(dr["rating"]);
                    m.Email = GetValue<String>(dr["email"]);
                    m.Error = GetValue<String>(dr["error"]);
                    list.Add(m);
                }
                return list;
            }
        }

        public static int UpdateEmail(Model m)
        {
            using (SQLiteCommand command = connection.CreateCommand())
            {
                command.CommandText = @"UPDATE tbl_domain SET email=@Email,error=null WHERE id=@Id";
                command.Parameters.Add("ID", System.Data.DbType.Int32).Value = m.Id;
                command.Parameters.Add("Email", System.Data.DbType.String).Value = m.Email;
                return command.ExecuteNonQuery();
            }
        }

        public static int UpdateError(Model m)
        {
            using (SQLiteCommand command = connection.CreateCommand())
            {
                command.CommandText = @"UPDATE tbl_domain SET error=@Error WHERE id=@Id";
                command.Parameters.Add("ID", System.Data.DbType.Int32).Value = m.Id;
                command.Parameters.Add("Error", System.Data.DbType.String).Value = m.Error;
                return command.ExecuteNonQuery();
            }
        }
    }
}
