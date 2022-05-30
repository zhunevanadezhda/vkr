using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;

namespace Diplom
{
    class BD
    {
        SqlConnection conn = new SqlConnection(@"Data Source=NADYA-PC\SQLEXPRESS;Initial Catalog=bd_diplom;Integrated Security=True;MultipleActiveResultSets=True");
        private static readonly byte[] key = Encoding.ASCII.GetBytes("SFDGKLGFHJ234LKSFDSFDGKLGFHJ234L");
        private static readonly byte[] iv = Encoding.ASCII.GetBytes("sdfhjksdhfklk234");

        private static string getString(object o)
        {
            if (o == DBNull.Value) return null;
            return (string)o;
        }
        private static int getInt(object o)
        {
            if (o == DBNull.Value) return 0;
            return (int)o;
        }
        private static DateTime getDateTime(object o)
        {
            if (o == DBNull.Value) return DateTime.MinValue;
            return (DateTime)o;
        }
        private static object setString(string s)
        {
            return string.IsNullOrEmpty(s) ? DBNull.Value : (object)s;
        }
        private static object setDateTime(DateTime s)
        {
            return s == DateTime.MinValue ? DBNull.Value : (object)s;
        }
        public bool openConnection()
        {
            if (conn.State == System.Data.ConnectionState.Closed)
            {
                conn.Open();
                return true;
            }
            else return false;
        }
        public void closeConnection()
        {
            if (conn.State == System.Data.ConnectionState.Open)
                conn.Close();
        }
        public SqlConnection GetConnection()
        {
            return conn;
        }

        //User
        private static string CalcHash2(string password)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(password);
                        }
                        string answer = "";
                        answer = Convert.ToBase64String(msEncrypt.ToArray());
                        return answer;
                    }
                }
            }
        }
        private static string CalcHash(string password)
        {
            byte[] salt;
            byte[] buffer2;
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }
        public static bool CheckHashedPassword2(string hashedPassword, string password)
        {
            string answer;
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(password);
                        }
                        answer = "";
                        answer = Convert.ToBase64String(msEncrypt.ToArray());
                    }
                }
            }
            return Equals(hashedPassword, answer);
        }
        public static List<string> CheckHashedPassword3(string hashedPassword, string password)
        {
            byte[] buffer4;
            byte[] src = Convert.FromBase64String(hashedPassword);
            if ((src.Length != 0x31) || (src[0] != 0))
            {
                //return false;
                List<string> list2 = new List<string>();
                list2.Add("false2" + hashedPassword);
                list2.Add("false2" + CalcHash(password));
                return list2;
            }
            byte[] dst = new byte[0x10];
            Buffer.BlockCopy(src, 1, dst, 0, 0x10);
            byte[] buffer3 = new byte[0x20];
            Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, dst, 0x3e8))
            {
                buffer4 = bytes.GetBytes(0x20);
            }
            string s1 = Convert.ToBase64String(buffer4);
            string s2 = Convert.ToBase64String(buffer3);
            List<string> list = new List<string>();
            list.Add(s2);
            list.Add(s1);
            return list;
            //return Equals(buffer3, buffer4);
        }
        public static bool CheckHashedPassword(string hashedPassword, string password)
        {
            byte[] buffer4;
            byte[] src = Convert.FromBase64String(hashedPassword);
            if ((src.Length != 0x31) || (src[0] != 0))
            {
                return false;
            }
            byte[] dst = new byte[0x10];
            Buffer.BlockCopy(src, 1, dst, 0, 0x10);
            byte[] buffer3 = new byte[0x20];
            Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, dst, 0x3e8))
            {
                buffer4 = bytes.GetBytes(0x20);
            }
            string s1 = Convert.ToBase64String(buffer4);
            string s2 = Convert.ToBase64String(buffer3);
            return Equals(s1, s2);

        }
        public bool CheckUser(string email, string password)
        {
            conn.Open();
            SqlCommand command = new SqlCommand("GetPassword @UserEmail", conn);
            command.Parameters.AddWithValue("@UserEmail", email);
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            string result = (string)reader[0];
            conn.Close();
            if (result == "FALSE") return false;
            else if (CheckHashedPassword(result, password)) return true;
            else return false;
        }
        public bool CheckUser(int id,string password)
        {
            conn.Open();
            SqlCommand command = new SqlCommand("select password from Users where id="+id, conn);
           // command.Parameters.AddWithValue("@UserEmail", email);
            SqlDataReader reader = command.ExecuteReader();
            string result="";
            if (reader.Read())
                result = (string)reader[0];
            conn.Close();
            if (CheckHashedPassword(result, password)) return true;
            else return false;
        }
        public bool isUser(string email)
        {
            conn.Open();
            SqlCommand command = new SqlCommand("CheckUserAvailability @UserEmail", conn);
            command.Parameters.AddWithValue("@UserEmail", email);
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            string result = (string)reader[0];
            conn.Close();
            if (result == "TRUE") return true;
            else return false;
        }
        /*  public User GetUserByEmail(string email)
          {
              conn.Open();
              SqlCommand command = new SqlCommand("GetUser @UserEmail", conn);
              command.Parameters.AddWithValue("@UserEmail", email);
              SqlDataReader reader = command.ExecuteReader();
              reader.Read();
              User user = new User((int)reader["id"], getString(reader["name"]), getString(reader["surname"]), getString(reader["patronym"]),getString(reader["telephone"]),  getString(reader["pasportNumber"]), getDateTime(reader["pasportDate"]),
                  getString(reader["pasportWhere"]), (string)reader["email"], (string)reader["password"],
                  getString(reader["numberSROO"]), getString(reader["nameSROO"]), getString(reader["insurance"]), (int)reader["experience"],
                  getString(reader["education"]), getString(reader["membership"]));
              if (reader["id_c"] != DBNull.Value)
              {
                  int id = (int)reader["id_c"];
                  command = new SqlCommand("select * from Companies where id=" + id, conn);
                  reader = command.ExecuteReader();
                  reader.Read();
                  Company company = new Company((int)reader["id"], (string)reader["name"], getString(reader["adres"]), (string)reader["OGRN"], (DateTime)reader["dateOGRN"], (string)reader["form"],
                      getString(reader["email"]), getString(reader["telephone"]), getString(reader["insurance"]));
                  user.Company = company;
              }
              conn.Close();
              return user;
          }*/
        public User GetUserById(int id_u)
        {
            conn.Open();
            SqlCommand command = new SqlCommand("select * from Users where id=" + id_u, conn);
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            User user = new User((int)reader["id"], getString(reader["name"]), getString(reader["surname"]), getString(reader["patronym"]), getString(reader["telephone"]), getString(reader["pasportNumber"]), getDateTime(reader["pasportDate"]),
                getString(reader["pasportWhere"]), (string)reader["email"], (string)reader["password"],
                getString(reader["numberSROO"]), getString(reader["nameSROO"]), getString(reader["insurance"]), (int)reader["experience"],
                getString(reader["education"]), getString(reader["membership"]));
            command = new SqlCommand("select id_c,id_u,id from Companies, UCom where id=" + id_u + " and id=id_c", conn);
            reader = command.ExecuteReader();
            if (!reader.Read())
            {
                int id = (int)reader["id_c"];
                command = new SqlCommand("select * from Companies where id=" + id, conn);
                reader = command.ExecuteReader();
                reader.Read();
                Company company = new Company((int)reader["id"], (string)reader["name"], getString(reader["adres"]), (string)reader["OGRN"], (DateTime)reader["dateOGRN"], (string)reader["form"],
                    getString(reader["email"]), getString(reader["telephone"]), getString(reader["insurance"]));
                user.Company = company;
            }
            conn.Close();
            return user;
        }
        public int GetUserId(string email)
        {
            conn.Open();
            SqlCommand command = new SqlCommand("GetUserId @UserEmail", conn);
            command.Parameters.AddWithValue("@UserEmail", email);
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            int id = (int)reader["id"];
            conn.Close();
            return id;
        }
        /* public List<User> GetUsers()
         {
             conn.Open();
             List<User> users = new List<User>();
             SqlCommand command = new SqlCommand("select * from Users order by surname,name,patronym", conn);
             SqlDataReader reader = command.ExecuteReader();
             while (reader.Read())
             {
                 User user = new User((int)reader["id"], getString(reader["name"]), getString(reader["surname"]), getString(reader["patronym"]), (string)reader["email"]);
                 if (reader["id_c"] != DBNull.Value)
                 {
                     int id = (int)reader["id_c"];
                     command = new SqlCommand("select * from Companies where id=" + id, conn);
                     reader = command.ExecuteReader();
                     reader.Read();
                     Company company = new Company((int)reader["id"], (string)reader["name"], getString(reader["adres"]), (string)reader["OGRN"], (DateTime)reader["dateOGRN"], (string)reader["form"],
                         getString(reader["email"]), getString(reader["telephone"]), getString(reader["insurance"]));
                     user.Company = company;
                 }
                 users.Add(user);
             }
             conn.Close();
             return users;
         }*/
        /*  public List<User> GetUsersByCompany(int id_c)
          {
              conn.Open();
              List<User> users = new List<User>();
              SqlCommand command = new SqlCommand("select * from Users where id_c=@id_c order by surname,name,patronym", conn);
              command.Parameters.AddWithValue("@id_c", id_c);
              SqlDataReader reader = command.ExecuteReader();
              while (reader.Read())
              {
                  User user = new User((int)reader["id"], getString(reader["name"]), getString(reader["surname"]), getString(reader["patronym"]), (string)reader["email"]);

                  users.Add(user);
              }
              conn.Close();
              return users;
          }*/
        public bool AddUser(User newUser)
        {
            conn.Open();
            SqlCommand command;
            if (newUser.PasportNumber != null)
            {
                command = new SqlCommand("insert into Users (name,surname,patronym,email,password,telephone,numberSROO,nameSROO,insurance,experience,education,membership,pasportNumber,pasportDate,pasportWhere)" +
                "values(@name, @surname, @patronym, @email, @password, @telephone, @numberSROO, @nameSROO, @insurance, @experience, @education, @membership, @pasportNumber, @pasportDate," +
                " @pasportWhere);", conn);
                command.Parameters.AddWithValue("@pasportNumber", newUser.PasportNumber);
                command.Parameters.AddWithValue("@pasportDate", newUser.PasportDate);
                command.Parameters.AddWithValue("@pasportWhere", newUser.PasportWhere);
            }
            else
                command = new SqlCommand("insert into Users (name,surname,patronym,email,password,telephone,numberSROO,nameSROO,insurance,experience,education,membership)" +
                "values(@name, @surname, @patronym, @email, @password, @telephone, @numberSROO, @nameSROO, @insurance, @experience, @education, @membership);", conn);
            command.Parameters.AddWithValue("@name", setString(newUser.Name));
            command.Parameters.AddWithValue("@surname", setString(newUser.Surname));
            command.Parameters.AddWithValue("@patronym", setString(newUser.Patronym));
            command.Parameters.AddWithValue("@email", newUser.Email);
            command.Parameters.AddWithValue("@password", CalcHash(newUser.Password));
            command.Parameters.AddWithValue("@telephone", setString(newUser.Telephone));
            command.Parameters.AddWithValue("@numberSROO", setString(newUser.NumberSROO));
            command.Parameters.AddWithValue("@nameSROO", setString(newUser.NameSROO));
            command.Parameters.AddWithValue("@insurance", setString(newUser.Insurance));
            command.Parameters.AddWithValue("@experience", newUser.Experience);
            command.Parameters.AddWithValue("@education", setString(newUser.Education));
            command.Parameters.AddWithValue("@membership", setString(newUser.Membership));
            if (command.ExecuteNonQuery() > 0)
            {
                if (newUser.Company != null)
                {
                    command = new SqlCommand("select max(id) from Users", conn);
                    int id = (Int32)command.ExecuteScalar();
                    command = new SqlCommand("insert into ucom(id_u,id_c) values(" + id + "," + newUser.Company.Id + ")", conn);
                    if (command.ExecuteNonQuery() > 0)
                    {
                        conn.Close();
                        return true;
                    }
                    else
                    {
                        conn.Close();
                        return false;
                    }
                }
                else
                {
                    conn.Close();
                    return true;
                }
            }
            else
            {
                conn.Close();
                return false;
            }
        }
        public bool UpdateUser(User newUser)
        {
            conn.Open();
            SqlCommand command;
            command = new SqlCommand("update users set name=@name,surname=@surname,patronym=@patronym,telephone=@telephone,numberSROO=@numberSROO," +
                "nameSROO=@nameSROO, insurance = @insurance, experience = @experience, education = @education, membership = @membership, pasportNumber = @pasportNumber, " +
                "pasportDate = @pasportDate, pasportWhere = @pasportWhere where id = @id; ", conn);
            if (newUser.Company != null)
                command.Parameters.AddWithValue("@id_c", newUser.Company.Id);
            else
                command.Parameters.AddWithValue("@id_c", DBNull.Value);
            command.Parameters.AddWithValue("@name", newUser.Name);
            command.Parameters.AddWithValue("@surname", newUser.Surname);
            command.Parameters.AddWithValue("@patronym", setString(newUser.Patronym));
            command.Parameters.AddWithValue("@telephone", setString(newUser.Telephone));
            command.Parameters.AddWithValue("@numberSROO", setString(newUser.NumberSROO));
            command.Parameters.AddWithValue("@nameSROO", setString(newUser.NameSROO));
            command.Parameters.AddWithValue("@insurance", setString(newUser.Insurance));
            command.Parameters.AddWithValue("@experience", newUser.Experience);
            command.Parameters.AddWithValue("@education", setString(newUser.Education));
            command.Parameters.AddWithValue("@membership", setString(newUser.Membership));
            command.Parameters.AddWithValue("@id", newUser.Id);
            command.Parameters.AddWithValue("@pasportNumber", setString(newUser.PasportNumber));
            command.Parameters.AddWithValue("@pasportDate", setDateTime(newUser.PasportDate));
            command.Parameters.AddWithValue("@pasportWhere", setString(newUser.PasportWhere));
            if (command.ExecuteNonQuery() > 0)
            {
                if (newUser.Company != null)
                {
                    command = new SqlCommand("update ucom set id_c=" + newUser.Company.Id + " where id_u=" + newUser.Id);
                    command.ExecuteNonQuery();
                }
                conn.Close(); return true;
            }
            else { conn.Close(); return false; }
        }
        public bool UpdatePassword(int id,string password)
        {
            conn.Open();
            SqlCommand command;
            command = new SqlCommand("update users set password=@password where id = @id; ", conn);
            command.Parameters.AddWithValue("@password", CalcHash(password));
            command.Parameters.AddWithValue("@id", id);
            if (command.ExecuteNonQuery() > 0)
            {
                conn.Close(); return true;
            }
            else { conn.Close(); return false; }
        }

        //Company
        public int AddCompany(Company newCompany)
        {
            conn.Open();
            SqlCommand command = new SqlCommand("insert into Companies(name,adres,OGRN,dateOGRN,form,email,telephone,insurance) values(@name,@adres,@OGRN,@dateOGRN,@form,@email,@telephone,@insurance);", conn);
            command.Parameters.AddWithValue("@name", setString(newCompany.Name));
            command.Parameters.AddWithValue("@adres", setString(newCompany.Adres));
            command.Parameters.AddWithValue("@OGRN", setString(newCompany.OGRN));
            command.Parameters.AddWithValue("@dateOGRN", setDateTime(newCompany.DateOGRN));
            command.Parameters.AddWithValue("@form", setString(newCompany.Form));
            command.Parameters.AddWithValue("@email", setString(newCompany.Email));
            command.Parameters.AddWithValue("@telephone", setString(newCompany.Telephone));
            command.Parameters.AddWithValue("@insurance", setString(newCompany.Insurance));
            if (command.ExecuteNonQuery() > 0)
            {
                command = new SqlCommand("select max(id) from Companies;", conn);
                //SqlDataReader reader = command.ExecuteReader();
                int id = (int)command.ExecuteScalar();
                conn.Close();
                return id;
            }
            else
            {
                conn.Close();
                return 0;
            }
        }
        public bool UpdateCompany(Company newCompany)
        {
            conn.Open();
            SqlCommand command = new SqlCommand("update Companies set name=@name,adres=@adres,OGRN=@OGRN,dateOGRN=@dateOGRN,form=@form,email=@email,telephone=@telephone,insurance=@insurance where id=@id;", conn);
            command.Parameters.AddWithValue("@name", setString(newCompany.Name));
            command.Parameters.AddWithValue("@adres", setString(newCompany.Adres));
            command.Parameters.AddWithValue("@OGRN", setString(newCompany.OGRN));
            command.Parameters.AddWithValue("@dateOGRN", setDateTime(newCompany.DateOGRN));
            command.Parameters.AddWithValue("@form", setString(newCompany.Form));
            command.Parameters.AddWithValue("@email", setString(newCompany.Email));
            command.Parameters.AddWithValue("@telephone", setString(newCompany.Telephone));
            command.Parameters.AddWithValue("@insurance", setString(newCompany.Insurance));
            command.Parameters.AddWithValue("@id", newCompany.Id);
            if (command.ExecuteNonQuery() > 0) { conn.Close(); return true; }
            else { conn.Close(); return false; }
        }
        public Company GetCompanyById(int id)
        {
            conn.Open();
            SqlCommand command = new SqlCommand("select * from Companies where id=" + id, conn);
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            Company company = new Company(id, getString(reader["name"]), getString(reader["adres"]), getString(reader["OGRN"]), getDateTime(reader["dateOGRN"]), getString(reader["form"]),
                getString(reader["email"]), getString(reader["telephone"]), getString(reader["insurance"]));
            conn.Close();
            return company;
        }
        public Company GetCompanyByOGRN(string OGRN)
        {
            conn.Open();
            SqlCommand command = new SqlCommand("select * from Companies where OGRN='" + OGRN + "'", conn);
            if (command.ExecuteNonQuery() > 0)
            {
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                Company company = new Company((int)reader["id"], getString(reader["name"]), getString(reader["adres"]), OGRN, getDateTime(reader["dateOGRN"]), getString(reader["form"]),
                getString(reader["email"]), getString(reader["telephone"]), getString(reader["insurance"]));
                conn.Close();
                return company;
            }
            else
            {
                conn.Close();
                return null;
            }
        }
        public List<Company> GetCompanies()
        {
            conn.Open();
            SqlCommand command = new SqlCommand("select * from Companies", conn);
            SqlDataReader reader = command.ExecuteReader();
            List<Company> companies = new List<Company>();
            while (reader.Read())
            {
                Company company = new Company((int)reader["id"], getString(reader["name"]), getString(reader["adres"]), getString(reader["OGRN"]), getDateTime(reader["dateOGRN"]), getString(reader["form"]),
                getString(reader["email"]), getString(reader["telephone"]), getString(reader["insurance"]));
                companies.Add(company);
            }
            conn.Close();
            return companies;
        }
        public bool DeleteCompany(int id)
        {
            conn.Open();
            SqlCommand command = new SqlCommand("delete from Companies where id=" + id, conn);
            if (command.ExecuteNonQuery() > 0)
            {
                conn.Close();
                return true;
            }
            else
            {
                conn.Close();
                return false;
            }
        }

        //Report
        public List<Report> GetReports(int UserId)//достать адрес из объекта
        {
            string sql;
            /*switch (s)
            {
                case "мои":
                    sql = "select * from Reports, URep where id_u=" + UserId + " and id=id_r and isCreator=1";
                    break;
                case "других":
                    sql = "select * from Reports, URep where id_u=" + UserId + " and id=id_r and isCreator=0";
                    break;
                default:
                    sql = "select * from Reports, URep where id_u=" + UserId + " and id=id_r";
                    break;
            }*/
            conn.Open();
            sql = "select * from Reports, URep where id_u=" + UserId + " and id=id_r";
            SqlCommand command = new SqlCommand(sql, conn);
            SqlDataReader reader = command.ExecuteReader();
            List<Report> reports = new List<Report>();
            while (reader.Read())
            {
                Report report = new Report((int)reader["id"], (DateTime)reader["dateInspection"], (DateTime)reader["dateValutaion"], (DateTime)reader["dateReport"], getString(reader["basedOn"]),
                    getString(reader["goal"]), getString(reader["limitation"]), (bool)reader["isReady"], getString(reader["typeObject"]));
                if (!(bool)reader["costumer"])//физ лицо
                {
                    SqlCommand command2 = new SqlCommand("select name,surname,patronym,telephone from Humans, HuRep where id_r=" + report.Id + " and id_h=id and isCostumer=1", conn);
                    SqlDataReader reader2 = command2.ExecuteReader();
                    if (reader2.Read())
                    {
                        Human costumer = new Human(getString(reader2["name"]), getString(reader2["surname"]), getString(reader2["patronym"]), getString(reader2["telephone"]));
                        report.Costumer = new Costumer(costumer);
                    }
                }
                else
                {
                    SqlCommand command2 = new SqlCommand("select name,form,telephone from Companies, ComRep where id_r=" + report.Id + " and id_c=id and isCostumer=1;", conn);
                    SqlDataReader reader2 = command2.ExecuteReader();
                    if (reader2.Read())
                    {
                        Company costumer = new Company(getString(reader2["name"]), getString(reader2["form"]), getString(reader2["telephone"]));
                        report.Costumer = new Costumer(costumer);
                    }
                }
                reports.Add(report);
            }
            conn.Close();
            return reports;
        }
        public Report GetReport(int ReportId)
        {
            conn.Open();
            SqlCommand command = new SqlCommand("select * from Reports where id=" + ReportId, conn);
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            Report report = new Report((int)reader["id"], (DateTime)reader["dateInspection"], (DateTime)reader["dateValutaion"], (DateTime)reader["dateReport"], getString(reader["basedOn"]),
                    getString(reader["goal"]), getString(reader["limitation"]), (bool)reader["isReady"], getString(reader["typeObject"]));
            if (!(bool)reader["costumer"])//физ лицо
            {
                Human costumer = GetHumanCostumer(ReportId);
                report.Costumer = new Costumer(costumer);
            }
            else//юр лицо
            {
                Company costumer = GetCompanyCostumer(ReportId);
                report.Costumer = new Costumer(costumer);
            }
            conn.Close();
            return report;
        }
        public int AddReport(Report report, int UserID)
        {
            conn.Open();
            SqlCommand command = new SqlCommand("insert into Reports(dateInspection,dateValutaion,dateReport,basedOn,goal,limitation,isReady,costumer,typeObject)" +
                " values(@dateInspection,@dateValutaion,@dateReport,@basedOn,@goal,@limitation,@isReady,@costumer,@typeObject);", conn);
            command.Parameters.AddWithValue("@dateInspection", report.DateInspection);
            command.Parameters.AddWithValue("@dateValutaion", report.DateValutaion);
            command.Parameters.AddWithValue("@dateReport", report.DateReport);
            command.Parameters.AddWithValue("@basedOn", setString(report.BasedOn));
            command.Parameters.AddWithValue("@goal", setString(report.Goal));
            command.Parameters.AddWithValue("@limitation", setString(report.Limitation));
            command.Parameters.AddWithValue("@isReady", report.IsReady);
            command.Parameters.AddWithValue("@costumer", report.Costumer.Type);
            command.Parameters.AddWithValue("@typeObject", setString(report.TypeObject));
            if (command.ExecuteNonQuery() > 0)
            {
                command = new SqlCommand("select max(id) from Reports;", conn);
                //SqlDataReader reader = command.ExecuteReader();
                int id = (int)command.ExecuteScalar();
                command = new SqlCommand("insert into URep (id_u,id_r) values (@id_u,@id_r);", conn);
                command.Parameters.AddWithValue("@id_u", UserID);
                command.Parameters.AddWithValue("@id_r", id);
                command.ExecuteNonQuery();
                conn.Close();
                return id;
            }
            else
            {
                conn.Close();
                return 0;
            }
        }
        public bool UpdateReport(Report report)
        {
            conn.Open();
            SqlCommand command = new SqlCommand("update Reports set dateInspection=@dateInspection,dateValutaion=@dateValutaion,dateReport=@dateReport,basedOn=@basedOn,goal=@goal,limitation=@limitation,isReady=@isReady,costumer=@costumer where id=@id;", conn);
            command.Parameters.AddWithValue("@dateInspection", report.DateInspection);
            command.Parameters.AddWithValue("@dateValutaion", report.DateValutaion);
            command.Parameters.AddWithValue("@dateReport", report.DateReport);
            command.Parameters.AddWithValue("@basedOn", setString(report.BasedOn));
            command.Parameters.AddWithValue("@goal", setString(report.Goal));
            command.Parameters.AddWithValue("@limitation", setString(report.Limitation));
            command.Parameters.AddWithValue("@isReady", report.IsReady);
            command.Parameters.AddWithValue("@costumer", report.Costumer.Type);
            command.Parameters.AddWithValue("@id", report.Id);
            if (command.ExecuteNonQuery() > 0) { conn.Close(); return true; }
            else { conn.Close(); return false; }
        }
        public bool SetIsReady(int ReportId, bool isReady)
        {
            conn.Open();
            SqlCommand command = new SqlCommand("update Reports set isReady=@isReady where id=@id;", conn);
            command.Parameters.AddWithValue("@isReady", isReady);
            command.Parameters.AddWithValue("@id", ReportId);
            if (command.ExecuteNonQuery() > 0) { conn.Close(); return true; }
            else { conn.Close(); return false; }
        }
        public bool DeleteReport(int ReportId)
        {
            conn.Open();
            /*SqlCommand command = new SqlCommand("select id from Humans, HuRep where id_r=" + ReportId + " and id_h=id", conn);
            List<int> ids = new List<int>();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ids.Add((int)reader["id"]);
            }
            foreach (int i in ids)
                DeleteHuman(i);
            ids.Clear();
            command = new SqlCommand("select id from Companies, ComRep where id_r=" + ReportId + " and id_c=id", conn);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                ids.Add((int)reader["id"]);
            }
            foreach (int i in ids)
                DeleteCompany(i);
            ids.Clear();
            command = new SqlCommand("select id from RealEstates, EstRep where id_r=" + ReportId + " and id_e=id;", conn);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                ids.Add((int)reader["id"]);
            }
            foreach (int i in ids)
                DeleteRealEstate(i);*/
            SqlCommand command = new SqlCommand("delete from Reports where id=" + ReportId, conn);
            if (command.ExecuteNonQuery() > 0)
            {
                conn.Close();
                return true;
            }
            else
            {
                conn.Close();
                return false;
            }
        }

        //HumanCostumer and HumanOwners
        public Human GetHumanCostumer(int ReportId)
        {
            bool b = openConnection();
            SqlCommand command = new SqlCommand("select * from Humans, HuRep where id_r=" + ReportId + " and id_h=id and isCostumer=1", conn);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                Human costumer = new Human((int)reader["id"], getString(reader["name"]), getString(reader["surname"]), getString(reader["patronym"]), getString(reader["telephone"]), getString(reader["pasportNumber"]),
                    getDateTime(reader["pasportDate"]), getString(reader["pasportWhere"]), (bool)reader["isOwner"]);
                if (b) conn.Close();
                return costumer;
            }
            else
            {
                if (b) conn.Close();
                return null;
            }
        }
        public List<Human> GetHumanOwners(int ReportId)
        {
            conn.Open();
            SqlCommand command = new SqlCommand("select id,name,surname,patronym from Humans, HuRep where id_r=" + ReportId + " and id_h=id and isOwner=1 and isCostumer=0", conn);
            SqlDataReader reader = command.ExecuteReader();
            List<Human> owners = new List<Human>();
            while (reader.Read())
            {
                //Human owner = new Human((int)reader["id"], (string)reader["name"], (string)reader["surname"], (string)reader["patronym"], (string)reader["pasportNumber"],
                //  (DateTime)reader["pasportDate"], (string)reader["pasportWhere"]);
                Human owner = new Human((int)reader["id"], getString(reader["name"]), getString(reader["surname"]), getString(reader["patronym"]));
                owners.Add(owner);
            }
            conn.Close();
            return owners;
        }
        public Human GetHuman(int HumanId)
        {
            conn.Open();
            SqlCommand command = new SqlCommand("select * from Humans where id=" + HumanId, conn);
            SqlDataReader reader = command.ExecuteReader();
            Human owner = null;
            while (reader.Read())
            {
                owner = new Human((int)reader["id"], getString(reader["name"]), getString(reader["surname"]), getString(reader["patronym"]), getString(reader["telephone"]), getString(reader["pasportNumber"]),
                    getDateTime(reader["pasportDate"]), getString(reader["pasportWhere"]));
                // Human owner = new Human((int)reader["id"], (string)reader["name"], (string)reader["surname"], (string)reader["patronym"]);
                //owners.Add(owner);
            }
            conn.Close();
            return owner;
        }
        public int AddHuman(Human human)
        {
            bool b = openConnection();
            SqlCommand command = new SqlCommand("insert into Humans (name,surname,patronym,telephone,pasportNumber,pasportDate,pasportWhere) " +
                "values(@name, @surname, @patronym, @telephone, @pasportNumber, @pasportDate, @pasportWhere)", conn);
            command.Parameters.AddWithValue("@name", setString(human.Name));
            command.Parameters.AddWithValue("@surname", setString(human.Surname));
            command.Parameters.AddWithValue("@patronym", setString(human.Patronym));
            command.Parameters.AddWithValue("@telephone", setString(human.Telephone));
            command.Parameters.AddWithValue("@pasportNumber", setString(human.PasportNumber));
            command.Parameters.AddWithValue("@pasportDate", setDateTime(human.PasportDate));
            command.Parameters.AddWithValue("@pasportWhere", setString(human.PasportWhere));
            int id;
            if (command.ExecuteNonQuery() > 0)
            {
                command = new SqlCommand("select max(id) from Humans;", conn);
                //SqlDataReader reader = command.ExecuteReader();
                id = (int)command.ExecuteScalar();
            }
            else
            {
                id = 0;
            }
            if (b) conn.Close();
            return id;
        }
        public bool AddHuRep(int ReportId, int HumanId, bool isOwner, bool isCostumer)
        {
            bool b = openConnection();
            SqlCommand command = new SqlCommand("insert into HuRep (id_h,id_r,isOwner,isCostumer) values(@id_h, @id_r, @isOwner, @isCostumer)", conn);
            command.Parameters.AddWithValue("@id_h", HumanId);
            command.Parameters.AddWithValue("@id_r", ReportId);
            command.Parameters.AddWithValue("@isOwner", isOwner);
            command.Parameters.AddWithValue("@isCostumer", isCostumer);
            bool k;
            if (command.ExecuteNonQuery() > 0) k = true;
            else k = false;
            if (b) conn.Close();
            return k;
        }
        public bool UpdateHuman(Human human)
        {
            bool b = openConnection();
            SqlCommand command = new SqlCommand("update Humans set name=@name,surname=@surname,patronym=@patronym,telephone=@telephone,pasportNumber=@pasportNumber,pasportDate=@pasportDate,pasportWhere=@pasportWhere where id=@id;", conn);
            command.Parameters.AddWithValue("@name", setString(human.Name));
            command.Parameters.AddWithValue("@surname", setString(human.Surname));
            command.Parameters.AddWithValue("@patronym", setString(human.Patronym));
            command.Parameters.AddWithValue("@telephone", setString(human.Telephone));
            command.Parameters.AddWithValue("@pasportNumber", setString(human.PasportNumber));
            command.Parameters.AddWithValue("@pasportDate", setDateTime(human.PasportDate));
            command.Parameters.AddWithValue("@pasportWhere", setString(human.PasportWhere));
            command.Parameters.AddWithValue("@id", human.Id);
            bool k;
            if (command.ExecuteNonQuery() > 0) k = true;
            else k = false;
            if (b) conn.Close();
            return k;
        }
        public bool UpdateHuRep(int ReportId, int HumanId, bool isOwner, bool isCostumer)
        {
            bool b = openConnection();
            SqlCommand command = new SqlCommand("update HuRep set isOwner=@isOwner,isCostumer=@isCostumer where id_h=@id_h and id_r=@id_r;", conn);
            command.Parameters.AddWithValue("@isOwner", isOwner);
            command.Parameters.AddWithValue("@isCostumer", isCostumer);
            command.Parameters.AddWithValue("@id_h", HumanId);
            command.Parameters.AddWithValue("@id_r", ReportId);
            bool k;
            if (command.ExecuteNonQuery() > 0) k = true;
            else k = false;
            if (b) conn.Close();
            return k;
        }
        public bool DeleteHuman(int id)
        {
            conn.Open();
            SqlCommand command = new SqlCommand("delete from Humans where id=" + id, conn);
            if (command.ExecuteNonQuery() > 0)
            {
                conn.Close();
                return true;
            }
            else
            {
                conn.Close();
                return false;
            }
        }

        //CompanyCostumer and CompanyOwners
        public Company GetCompanyCostumer(int ReportId)
        {
            bool b = openConnection();
            SqlCommand command = new SqlCommand("select * from Companies, ComRep where id_r=" + ReportId + " and id_c=id and isCostumer=1;", conn);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                Company costumer = new Company((int)reader["id"], getString(reader["name"]), getString(reader["adres"]), getString(reader["OGRN"]), getDateTime(reader["dateOGRN"]), getString(reader["form"]),
                getString(reader["email"]), getString(reader["telephone"]), (bool)reader["isOwner"]);
                if (b) conn.Close();
                return costumer;
            }
            else
            {
                if (b) conn.Close();
                return null;
            }
        }
        public List<Company> GetCompaniesOwners(int ReportId)
        {
            conn.Open();
            SqlCommand command = new SqlCommand("select id,name,form from Companies, ComRep where id_r=" + ReportId + " and id_c=id and isOwner=1 and isCostumer=0", conn);
            SqlDataReader reader = command.ExecuteReader();
            List<Company> owners = new List<Company>();
            while (reader.Read())
            {
                //Human owner = new Human((int)reader["id"], (string)reader["name"], (string)reader["surname"], (string)reader["patronym"], (string)reader["pasportNumber"],
                //  (DateTime)reader["pasportDate"], (string)reader["pasportWhere"]);
                //Human owner = new Human((int)reader["id"], (string)reader["name"], (string)reader["surname"], (string)reader["patronym"]);
                Company owner = new Company((int)reader["id"], getString(reader["name"]), getString(reader["form"]));
                owners.Add(owner);
            }
            conn.Close();
            return owners;
        }
        public bool AddComRep(int ReportId, int CompanyId, bool isOwner, bool isCostumer)
        {
            bool b = openConnection();
            SqlCommand command = new SqlCommand("insert into ComRep (id_c,id_r,isOwner,isCostumer) values(@id_c, @id_r, @isOwner, @isCostumer)", conn);
            command.Parameters.AddWithValue("@id_c", CompanyId);
            command.Parameters.AddWithValue("@id_r", ReportId);
            command.Parameters.AddWithValue("@isOwner", Convert.ToInt32(isOwner));
            command.Parameters.AddWithValue("@isCostumer", Convert.ToInt32(isCostumer));
            bool k;
            if (command.ExecuteNonQuery() > 0) k = true;
            else k = false;
            if (b) conn.Close();
            return k;
        }
        public bool UpdateComRep(int ReportId, int CompanyId, bool isOwner, bool isCostumer)
        {
            bool b = openConnection();
            SqlCommand command = new SqlCommand("update ComRep set isOwner=@isOwner,isCostumer=@isCostumer where id_c=@id_c and id_r=@id_r;", conn);
            command.Parameters.AddWithValue("@isOwner", Convert.ToInt32(isOwner));
            command.Parameters.AddWithValue("@isCostumer", Convert.ToInt32(isCostumer));
            command.Parameters.AddWithValue("@id_c", CompanyId);
            command.Parameters.AddWithValue("@id_r", ReportId);
            bool k;
            if (command.ExecuteNonQuery() > 0) k = true;
            else k = false;
            if (b) conn.Close();
            return k;
        }

        //RealEstate
        public string GetAdres(int ReportId)
        {
            conn.Open();
            SqlCommand command = new SqlCommand("select adres from RealEstates, EstRep where id_r=" + ReportId + " and id_e=id and isAnalogue=0;", conn);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                string s = getString(reader["adres"]);
                conn.Close();
                return s;
            }
            else
            {
                conn.Close();
                return "";
            }
        }
        public int AddRealEstate(int ReportId, RealEstate realEstate)
        {
            conn.Open();
            SqlCommand command;
            int id; command = new SqlCommand("insert into RealEstates (adres,region,kadastrNumber,rights) values(@adres,@region,@kadastrNumber,@rights);", conn);
            command.Parameters.AddWithValue("@kadastrNumber", setString(realEstate.KadastrNumber));
            command.Parameters.AddWithValue("@rights", setString(realEstate.Rights));
            command.Parameters.AddWithValue("@adres", setString(realEstate.Adres));
            command.Parameters.AddWithValue("@region", setString(realEstate.Region));
            //SqlDataReader reader = command.ExecuteReader();
            if (command.ExecuteNonQuery() > 0)
            {
                command = new SqlCommand("select max(id) from RealEstates;", conn);
                //SqlDataReader reader = command.ExecuteReader();
                id = (int)command.ExecuteScalar();
                AddEstRep(ReportId, id, false);
            }
            else
            {
                id = 0;
            }
            conn.Close();
            return id;
        }
        public RealEstate GetRealEstate(int ReportId)
        {
            conn.Open();
            SqlCommand command = new SqlCommand("select * from RealEstates, EstRep where id_r=" + ReportId + " and id_e=id and isAnalogue=0;", conn);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                RealEstate realEstate = new RealEstate((int)reader["id"], getString(reader["adres"]), getString(reader["region"]), getInt(reader["price"]), getString(reader["kadastrNumber"]), getString(reader["rights"]), getString(reader["link"]), getString(reader["telephone"]));
                /*if ((bool)reader["isAnalogue"] == true)
                {
                    realEstate.Link = (string)reader["link"];
                    realEstate.Telephone = (string)reader["telephone"];
                }*/
                realEstate.Elements = GetElements(realEstate.Id, false);
                conn.Close();
                return realEstate;
            }
            else
            {
                conn.Close();
                return null;
            }
        }
        public List<RealEstate> GetRealEstates(int ReportId)
        {
            conn.Open();
            SqlCommand command = new SqlCommand("select * from RealEstates, EstRep where id_r=" + ReportId + " and id_e=id order by isAnalogue;", conn);
            SqlDataReader reader = command.ExecuteReader();
            List<RealEstate> realEstates = new List<RealEstate>();
            while (reader.Read())
            {
                RealEstate realEstate = new RealEstate((int)reader["id"], getString(reader["adres"]), getString(reader["region"]), getInt(reader["price"]), getString(reader["kadastrNumber"]), getString(reader["rights"]), getString(reader["link"]), getString(reader["telephone"]));
                /*if ((bool)reader["isAnalogue"]==true)
                {
                    realEstate.Link = (string)reader["link"];
                    realEstate.Telephone = (string)reader["telephone"];
                }*/
                realEstate.Elements = GetElements(realEstate.Id, true);
                realEstates.Add(realEstate);
            }
            conn.Close();
            return realEstates;
        }
        public int AddRealEstate(int ReportId, RealEstate realEstate, bool isAnalogue, bool isReportForm, bool isValuation)
        {
            conn.Open();
            SqlCommand command;
            int id;
            if (isReportForm)
            {
                command = new SqlCommand("insert into RealEstates (adres,region,kadastrNumber,rights,link,telephone) values(@adres,@region,@kadastrNumber,@rights,@link,@telephone);", conn);
                command.Parameters.AddWithValue("@kadastrNumber", setString(realEstate.KadastrNumber));
                command.Parameters.AddWithValue("@rights", setString(realEstate.Rights));
            }
            else
            {
                command = new SqlCommand("insert into RealEstates (adres,region,price,link,telephone) values(@adres,@region,@price,@link,@telephone);", conn);
            }
            command.Parameters.AddWithValue("@adres", setString(realEstate.Adres));
            command.Parameters.AddWithValue("@region", setString(realEstate.Region));
            command.Parameters.AddWithValue("@price", realEstate.Price);
            command.Parameters.AddWithValue("@link", setString(realEstate.Link));
            command.Parameters.AddWithValue("@telephone", setString(realEstate.Telephone));
            //SqlDataReader reader = command.ExecuteReader();
            if (command.ExecuteNonQuery() > 0)
            {
                command = new SqlCommand("select max(id) from RealEstates;", conn);
                //SqlDataReader reader = command.ExecuteReader();
                id = (int)command.ExecuteScalar();
                if (realEstate.Elements != null || realEstate.Elements.Count > 0)
                    AddElements(id, realEstate.Elements, isValuation);
                AddEstRep(ReportId, id, isAnalogue);
            }
            else
            {
                id = 0;
            }
            conn.Close();
            return id;
        }
        public int AddRealEstate(int ReportId)
        {
            conn.Open();
            int id;
            SqlCommand command = new SqlCommand("insert into RealEstates(adres) values(null)", conn);
            if (command.ExecuteNonQuery() > 0)
            {
                command = new SqlCommand("select max(id) from RealEstates;", conn);
                id = (int)command.ExecuteScalar();
                AddEstRep(ReportId, id, false);
            }
            else
            {
                id = 0;
            }
            conn.Close();
            return id;
        }
        public bool UpdateRealEstate(RealEstate realEstate, bool isReportForm)
        {
            bool b = openConnection();
            SqlCommand command;
            if (isReportForm)
            {
                command = new SqlCommand("update RealEstates set adres=@adres,region=@region,kadastrNumber=@kadastrNumber,rights=@rights where id=@id;", conn);
                command.Parameters.AddWithValue("@kadastrNumber", setString(realEstate.KadastrNumber));
                command.Parameters.AddWithValue("@rights", setString(realEstate.Rights));
            }
            else
            {
                command = new SqlCommand("update RealEstates set adres=@adres,region=@region,link=@link,telephone=@telephone,price=@price where id=@id;", conn);
                command.Parameters.AddWithValue("@link", setString(realEstate.Link));
                command.Parameters.AddWithValue("@telephone", setString(realEstate.Telephone));
                command.Parameters.AddWithValue("@price", realEstate.Price);
            }
            command.Parameters.AddWithValue("@adres", setString(realEstate.Adres));
            command.Parameters.AddWithValue("@region", setString(realEstate.Region));
            command.Parameters.AddWithValue("@id", realEstate.Id);
            //SqlDataReader reader = command.ExecuteReader();
            if (command.ExecuteNonQuery() > 0)
            {
                if (!isReportForm && realEstate.Elements.Count > 0)
                    UpdateElements(realEstate.Id, realEstate.Elements, true);
                conn.Close();
                return true;
            }
            else
            {
                conn.Close();
                return false;
            }
        }
        public bool AddEstRep(int ReportId, int EstateId, bool isAnalogue)
        {
            bool b = openConnection();
            SqlCommand command = new SqlCommand("insert into EstRep (id_e,id_r,isAnalogue) values(@id_e, @id_r, @isAnalogue)", conn);
            command.Parameters.AddWithValue("@id_e", EstateId);
            command.Parameters.AddWithValue("@id_r", ReportId);
            command.Parameters.AddWithValue("@isAnalogue", Convert.ToInt32(isAnalogue));
            bool k;
            if (command.ExecuteNonQuery() > 0) k = true;
            else k = false;
            if (b) conn.Close();
            return k;
        }
        public bool DeleteRealEstate(int EstateId)
        {
            conn.Open();
            SqlCommand command = new SqlCommand("delete from RealEstates where id=" + EstateId, conn);
            if (command.ExecuteNonQuery() > 0)
            {
                conn.Close();
                return true;
            }
            else
            {
                conn.Close();
                return false;
            }
        }

        //Element
        public List<Element> GetElements(int RealEstateId, bool isValuation)
        {
            bool b = openConnection();
            SqlCommand command;
            if (isValuation)
                command = new SqlCommand("select * from Elements where id_e=" + RealEstateId + " and isValuation=1 order by number;", conn);
            else
                command = new SqlCommand("select * from Elements where id_e=" + RealEstateId + " order by number;", conn);
            SqlDataReader reader = command.ExecuteReader();
            List<Element> elements = new List<Element>();
            while (reader.Read())
            {
                Element element = new Element((string)reader["name"], getString(reader["unit"]), getString(reader["value"]), (int)reader["number"], getInt(reader["correct"]));
                elements.Add(element);
            }
            if (b) conn.Close();
            if (elements.Count > 0)
                return elements;
            else return null;
        }
        public void AddElements(int EstateId, List<Element> elements, bool isValuation)
        {
            bool b = openConnection();
            for (int i = 0; i < elements.Count; i++)
            {
                SqlCommand command = new SqlCommand("insert into Elements (id_e,name,unit,value,number,correct,isValuation) values(@id_e, @name, @unit,@value,@number,@correct,@isValuation)", conn);
                command.Parameters.AddWithValue("@id_e", EstateId);
                command.Parameters.AddWithValue("@name", elements[i].Name);
                command.Parameters.AddWithValue("@unit", setString(elements[i].Unit));
                command.Parameters.AddWithValue("@value", setString(elements[i].Value));
                command.Parameters.AddWithValue("@number", elements[i].Number);
                command.Parameters.AddWithValue("@correct", elements[i].Correct);
                command.Parameters.AddWithValue("@isValuation", isValuation);
                command.ExecuteNonQuery();
            }
            if (b) conn.Close();
        }
        public bool AddElement(int EstateId, Element element)
        {
            bool b = openConnection();
            SqlCommand command = new SqlCommand("insert into Elements (id_e,name,unit,value,number,correct,isValuation) values(@id_e, @name, @unit,@value,@number,@correct,@isValuation)", conn);
            command.Parameters.AddWithValue("@id_e", EstateId);
            command.Parameters.AddWithValue("@name", element.Name);
            command.Parameters.AddWithValue("@unit", setString(element.Unit));
            command.Parameters.AddWithValue("@value", setString(element.Value));
            command.Parameters.AddWithValue("@number", element.Number);
            command.Parameters.AddWithValue("@correct", element.Correct);
            command.Parameters.AddWithValue("@isValuation", 1);
            if (command.ExecuteNonQuery() > 0)
            {
                if (b) conn.Close();
                return true;
            }
            else
            {
                if (b) conn.Close();
                return false;
            }
        }
        public void UpdateElements(int EstateId, List<Element> elements, bool isValuation)
        {
            bool b = openConnection();
            if (IsElementsExist(EstateId))
            {
                SqlCommand command;
                foreach (Element e in elements)
                {
                    if (IsElementExist(EstateId, e.Name))
                    {
                        command = new SqlCommand("update Elements set unit=@unit,value=@value,number=@number,correct=@correct where id_e=@id_e and name=@name", conn);
                        command.Parameters.AddWithValue("@id_e", EstateId);
                        command.Parameters.AddWithValue("@name", e.Name);
                        command.Parameters.AddWithValue("@unit", setString(e.Unit));
                        command.Parameters.AddWithValue("@value", setString(e.Value));
                        command.Parameters.AddWithValue("@number", e.Number);
                        command.Parameters.AddWithValue("@correct", e.Correct);
                        command.ExecuteNonQuery();
                    }
                    else AddElement(EstateId, e);
                }
            }
            else AddElements(EstateId, elements, isValuation);
            //  DeleteElements(EstateId);
            //  AddElements(EstateId, elements);
            if (b) conn.Close();
        }
        public void UpdateElement(int EstateId, Element element, string oldname)
        {
            conn.Open();
            SqlCommand command = new SqlCommand("update Elements set name=@name, unit=@unit,value=@value,number=@number,correct=@correct where id_e=@id_e and name=@oldname", conn);
            if (IsElementExist(EstateId, element.Name))
            {
                command.Parameters.AddWithValue("@id_e", EstateId);
                command.Parameters.AddWithValue("@name", element.Name);
                command.Parameters.AddWithValue("@unit", setString(element.Unit));
                command.Parameters.AddWithValue("@value", setString(element.Value));
                command.Parameters.AddWithValue("@number", element.Number);
                command.Parameters.AddWithValue("@correct", element.Correct);
                command.Parameters.AddWithValue("@oldname", oldname);
                command.ExecuteNonQuery();
            }
            else AddElement(EstateId, element);

            //  DeleteElements(EstateId);
            //  AddElements(EstateId, elements);
            conn.Close();
        }
        public void RenameElement(int EstateId, string newName, string oldName)
        {
            if (IsElementExist(EstateId, oldName))
            {
                conn.Open();
                SqlCommand command = new SqlCommand("update Elements set name=@newname where id_e=@id_e and name=@oldname", conn);
                command.Parameters.AddWithValue("@id_e", EstateId);
                command.Parameters.AddWithValue("@newname", newName);
                command.Parameters.AddWithValue("@oldname", oldName);
                command.ExecuteNonQuery();
                conn.Close();
            }
        }
        public bool DeleteElements(int EstateId)
        {
            bool b = openConnection();
            SqlCommand command = new SqlCommand("delete from Elements where id_e=" + EstateId, conn);
            if (command.ExecuteNonQuery() > 0)
            {
                if (b) conn.Close();
                return true;
            }
            else
            {
                if (b) conn.Close();
                return false;
            }
        }
        public bool IsElementsExist(int EstateId)
        {
            bool b = openConnection();
            SqlCommand command = new SqlCommand("select * from Elements where id_e=" + EstateId + " order by number;", conn);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                if (b) conn.Close();
                return true;
            }
            else
            {
                if (b) conn.Close();
                return false;
            }
        }
        public bool IsElementExist(int EstateId, string name)
        {
            bool b = openConnection();
            SqlCommand command = new SqlCommand("select * from Elements where id_e=@id_e and name=@name order by number;", conn);
            command.Parameters.AddWithValue("@id_e", EstateId);
            command.Parameters.AddWithValue("@name", name);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                if (b) conn.Close();
                return true;
            }
            else
            {
                if (b) conn.Close();
                return false;
            }
        }
        public void DeleteElement(int ReportId,int EstateId, string name, bool isValuationForm)
        {
            bool b = openConnection();
            SqlCommand command;
            /* if (isValuationForm)
                 command = new SqlCommand("select id from RealEstates, EstRep where id_r=" + ReportId + " and id_e=id and isAnalogue=1;", conn);
             else command = new SqlCommand("select id from RealEstates, EstRep where id_r=" + ReportId + " and id_e=id order by isAnalogue;", conn);
             SqlDataReader reader = command.ExecuteReader();
             List<int> realEstates = new List<int>();
             while (reader.Read())
             {
                 realEstates.Add((int)reader["id"]);
             }
             foreach (int i in realEstates)
             {
                 command = new SqlCommand("delete from Elements where id_e=@id_e and name=@name", conn);
                 command.Parameters.AddWithValue("@id_e", i);
                 command.Parameters.AddWithValue("@name", name);
             }*/
            if (isValuationForm)
            {
                command = new SqlCommand("delete from Elements where id_e=@id_e and name=@name", conn);
                command.Parameters.AddWithValue("@id_e", EstateId);
                command.Parameters.AddWithValue("@name", name);
                command.ExecuteNonQuery();
            }
            else
            {
                command = new SqlCommand("select id from RealEstates, EstRep where id_r=" + ReportId + " and id_e=id order by isAnalogue;", conn);
                SqlDataReader reader = command.ExecuteReader();
                List<int> realEstates = new List<int>();
                while (reader.Read())
                {
                    realEstates.Add((int)reader["id"]);
                }
                foreach (int i in realEstates)
                {
                    command = new SqlCommand("delete from Elements where id_e=@id_e and name=@name", conn);
                    command.Parameters.AddWithValue("@id_e", i);
                    command.Parameters.AddWithValue("@name", name);
                    command.ExecuteNonQuery();
                }
            }
            if (b) conn.Close();
        }
        public void SetIsValuation(int ReportId, int EstateId, string name, bool isValuation)
        {
            conn.Open();
            SqlCommand command = new SqlCommand("update Elements set isValuation=@isValuation where id_e=@id_e and name=@name", conn);
            command.Parameters.AddWithValue("@id_e", EstateId);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@isValuation", isValuation);
            //DeleteElement(ReportId, name, true);
            command.ExecuteNonQuery();
            conn.Close();
        }

        //Parameter
        public List<Parameter> GetParameters(int RealEstatesId, string podhod, string metod)
        {
            conn.Open();
            SqlCommand command = new SqlCommand("select * from Parameter where id_e=@id_e and podhod=@podhod and metod=@metod;", conn);
            command.Parameters.AddWithValue("@id_e", RealEstatesId);
            command.Parameters.AddWithValue("@podhod", podhod);
            command.Parameters.AddWithValue("@metod", metod);
            SqlDataReader reader = command.ExecuteReader();
            List<Parameter> parameters = new List<Parameter>();
            while (reader.Read())
            {
                Parameter parameter = new Parameter((string)reader["name"], Convert.ToDouble(reader["value"]));
                parameters.Add(parameter);
            }
            conn.Close();
            if (parameters.Count > 0)
                return parameters;
            else return null;
        }
        public bool metodIsExist(int RealEstatesId, string podhod, string metod)
        {
            bool b = openConnection();
            SqlCommand command = new SqlCommand("select * from Parameter where id_e=@id_e and podhod=@podhod and metod=@metod;", conn);
            command.Parameters.AddWithValue("@id_e", RealEstatesId);
            command.Parameters.AddWithValue("@podhod", podhod);
            command.Parameters.AddWithValue("@metod", metod);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                if (b) conn.Close();
                return true;
            }
            else
            {
                if (b) conn.Close();
                return false;
            }
        }
        public List<string> GetPodhodsMetods(int ReportId, int EstateId)
        {
            conn.Open();
            SqlCommand command = new SqlCommand("select * from RealEstates, EstRep where id_r=" + ReportId + " and id_e=id and isAnalogue=1;", conn);
            SqlDataReader reader = command.ExecuteReader();
            List<string> metods = new List<string>();
            if (reader.Read())
                metods.Add("сравнительный|метод сравнения продаж");
            command = new SqlCommand("select * from Parameter where id_e=" + EstateId, conn);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                metods.Add((string)reader["podhod"] + "|" + (string)reader["metod"]);
            }
            conn.Close();
            return metods;
        }
        public void AddParameter(int EstateId,Parameter parameter)
        {
            bool b = openConnection();
            SqlCommand command = new SqlCommand("insert into Parameter (id_e,podhod,metod,name,value) values(@id_e, @podhod,@metod,@name, @value)", conn);
            command.Parameters.AddWithValue("@id_e", EstateId);
            command.Parameters.AddWithValue("@podhod", parameter.Podhod);
            command.Parameters.AddWithValue("@metod", parameter.Metod);
            command.Parameters.AddWithValue("@name", parameter.Name);
            command.Parameters.AddWithValue("@value", parameter.Value);
            command.ExecuteNonQuery();
            if (b) conn.Close();
        }
        public void UpdateParameters(int EstateId, List<Parameter> parameters)
        {
            bool b = openConnection();
            SqlCommand command;
            foreach (Parameter p in parameters)
            { command = new SqlCommand("update Parameter set value=@value where id_e=@id_e and podhod=@podhod and metod=@metod and name=@name", conn);
                command.Parameters.AddWithValue("@id_e", EstateId);
                command.Parameters.AddWithValue("@podhod", p.Podhod);
                command.Parameters.AddWithValue("@metod", p.Metod);
                command.Parameters.AddWithValue("@name", p.Name);
                command.Parameters.AddWithValue("@value", p.Value);
                command.ExecuteNonQuery(); }
            if (b) conn.Close();
        }

        //Document
        public List<Document> GetDocuments(int ReportId)
        {
            conn.Open();
            SqlCommand command = new SqlCommand("select * from Documents where id_r=" + ReportId, conn);
            //    command.Parameters.AddWithValue("@id_r", RealEstatesId);
            //   command.Parameters.AddWithValue("@podhod", podhod);
            //  command.Parameters.AddWithValue("@metod", metod);
            SqlDataReader reader = command.ExecuteReader();
            List<Document> documents = new List<Document>();
            while (reader.Read())
            {
                Document document = new Document((int)reader["id"], (string)reader["name"]);

                SqlCommand command2 = new SqlCommand("select * from Pics where id_d=" + document.Id, conn);
                SqlDataReader reader2 = command2.ExecuteReader();
                List<string> pics = new List<string>();
                while (reader2.Read())
                {
                    pics.Add(getString(reader2["path"]));
                }
                if (pics.Count > 0)
                    document.ListPic = pics;
                documents.Add(document);
            }
            conn.Close();
            if (documents.Count > 0)
                return documents;
            else return null;
        }
        public int AddDocument(int ReportId, Document document)
        {
            bool b = openConnection();
            SqlCommand command = new SqlCommand("insert into Documents(id_r,name) values(@id_r,@name)", conn);
            command.Parameters.AddWithValue("@id_r", ReportId);
            command.Parameters.AddWithValue("@name", document.Name);
            int id = 0;
            if (command.ExecuteNonQuery() > 0)
            {
                command = new SqlCommand("select max(id) from Documents;", conn);
                id = (int)command.ExecuteScalar();
                foreach (string pic in document.ListPic)
                {
                    command = new SqlCommand("insert into Pics(id_d,path) values(@id_d,@path)", conn);
                    command.Parameters.AddWithValue("@id_d", id);
                    command.Parameters.AddWithValue("@path", pic);
                    command.ExecuteNonQuery();
                }
            }
            if (b) conn.Close();
            return id;
        }
        public void UpdateDocuments(int ReportId, List<Document> documents)
        {
            DeleteDocuments(ReportId);
            foreach (Document d in documents)
                AddDocument(ReportId, d);
        }
        public bool DeleteDocument(int DocumentId)
        {
            bool b = openConnection();
            SqlCommand command = new SqlCommand("delete from Documents where id=" + DocumentId, conn);
            if (command.ExecuteNonQuery() > 0)
            {
                if (b) conn.Close();
                return true;
            }
            else
            {
                if (b) conn.Close();
                return false;
            }
        }
        public bool DeleteDocuments(int ReportId)
        {
            bool b = openConnection();
            SqlCommand command = new SqlCommand("delete from Documents where id_r=" + ReportId, conn);
            if (command.ExecuteNonQuery() > 0)
            {
                if (b) conn.Close();
                return true;
            }
            else
            {
                if (b) conn.Close();
                return false;
            }
        }

        //Sample
        public List<Sample> GetSamples(int UserId)
        {
            conn.Open();
            SqlCommand command = new SqlCommand("select * from Samples,USam where id_u=" + UserId+"and id_s=id", conn);
            SqlDataReader reader = command.ExecuteReader();
            List<Sample> samples = new List<Sample>();
            while (reader.Read())
            {
                Sample sample = new Sample((int)reader["id"], (string)reader["name"], (string)reader["path"]);
                samples.Add(sample);
            }
            conn.Close();
            if (samples.Count > 0)
                return samples;
            else return null;
        }
        public int AddSample(int UserId,Sample sample)
        {
            conn.Open();
            SqlCommand command = new SqlCommand("insert into Samples(name,path) values(@name,@path)", conn);
            command.Parameters.AddWithValue("@name", sample.Name);
            command.Parameters.AddWithValue("@path", sample.Path);
            int id = 0;
            if (command.ExecuteNonQuery() > 0)
            {
                command = new SqlCommand("select max(id) from Samples;", conn);
                id = (int)command.ExecuteScalar();
                command = new SqlCommand("insert into USam(id_u,id_s) values("+UserId+","+id+");", conn);
                command.ExecuteNonQuery();
            }
            conn.Close();
            return id;
        }
        public bool DeleteSample(int SampleId)
        {
            conn.Open();
            SqlCommand command = new SqlCommand("delete from Samples where id=" + SampleId, conn);
            if (command.ExecuteNonQuery() > 0)
            {
                conn.Close();
                return true;
            }
            else
            {
                conn.Close();
                return false;
            }
        }
        public bool UpdateSample(Sample sample)
        {
            conn.Open();
            SqlCommand command = new SqlCommand("update Samples set name=@name, path=@path where id=@id", conn);
            command.Parameters.AddWithValue("@name", sample.Name);
            command.Parameters.AddWithValue("@path", sample.Path);
            command.Parameters.AddWithValue("@id", sample.Id);
            if (command.ExecuteNonQuery()>0)
            {
                conn.Close();
                return true;
            }
            else{
                conn.Close();
                return false;
            }
        }
        //value-стоимость
    }
}