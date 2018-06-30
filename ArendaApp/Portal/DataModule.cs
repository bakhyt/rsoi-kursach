using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Portal
{
    // Модуль данных
    public class DataModule
    {
        public static List<User> users;
        private static SqlConnection conn;

        private static String _ErrMsg;
        public static String ErrMsg()
        {
            return _ErrMsg;
        }

        public static bool Connect(String server, String dbname, String user, String pass)
        {
            try
            {
                String str = "Data Source=" + server + ";Initial Catalog=" + dbname + ";uid="+user+";pwd="+pass;
                conn = new SqlConnection(str);
                conn.Open();
                return true;
            }
            catch (Exception E)
            {
                _ErrMsg = E.Message;
                return false;
            }
        }

        public static int getIntFromSQL(String sqlstr)
        {
            SqlCommand sql = new SqlCommand(sqlstr, conn);
            sql.CommandType = CommandType.Text;
            return Int32.Parse(sql.ExecuteScalar().ToString());
        }

        private delegate void QueryDelegate(SqlDataReader r);

        private static void ExecQuery(string sql, QueryDelegate qd)
        {
            SqlCommand query = conn.CreateCommand();
            query.CommandText = sql;
            SqlDataReader reader = query.ExecuteReader();
            while (reader.Read())
                qd(reader);
            reader.Close();
        }

        public static void Load()
        {
            users = new List<User>();

            ExecQuery("SELECT * FROM USERS", (r) =>
            {
                User U = new User();
                U.ID = r.GetInt32(0);
                U.FIO = r.GetString(1);
                U.Email = r.GetString(2);
                U.Login = r.GetString(3);
                U.Pass = r.GetString(4);
                U.guid = Guid.Parse(r.GetString(5));
                users.Add(U);
            });

        }
        
        public static bool isAuthOK(string login, string pass, ref string userid)
        {
            if ((login == "admin") && (pass == "12345"))
            {
                userid = "1024";
                return true;
            }
            else
            {
                User u = users.Find((user) => user.isAuthOk(login, pass));
                if (u != null)
                {
                    userid = u.ID.ToString("D");
                    return true;
                }
                else
                    return false;
            }
        }
        public static bool addUser(string login, string pass, string email, ref string msg) 
        {
            User u = users.Find((user) => user.Login==login);
            if (u != null)
            {
                msg = "Такой логин уже занят";
                return false;
            }

            u = new User();
            u.Login = login;
            u.Pass = pass;
            u.Email = email;
            u.FIO = "Новый пользователь";
            u.guid = Guid.NewGuid();
            if (users.Count() == 0)
                u.ID = 1;
            else
                u.ID = users.Max((user) => user.ID) + 1;
            users.Add(u);

            SqlCommand sql = new SqlCommand("INSERT INTO USERS (FIO,EMAIL,LOGIN,PASS,UUID) VALUES ('" +
               u.FIO + "','" + u.Email + "','" + u.Login + "','" + u.Pass + "','"+u.guid.ToString()+"');", conn);
            sql.CommandType = CommandType.Text;
            SqlDataReader data = sql.ExecuteReader();
            data.Close();
            u.ID = getIntFromSQL("SELECT ID FROM USERS ORDER BY ID DESC");
            
            msg = "Успешная регистрация";
            return true;

        }
        public static string getUserName(string userid)
        {
            if (userid == "1024") return "Администратор";

            User u = users.Find((user) => user.ID.ToString("D")==userid);
            if (u != null)
                return u.Login;
            else
                return "Гость";
        }
       
    }
}
