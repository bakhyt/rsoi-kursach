using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace ArendaRESTLib
{
    public class DataModule
    {
        private SqlConnection conn;

        private String _ErrMsg;
        public String ErrMsg()
        {
            return _ErrMsg;
        }

        public bool Connect(String server, String dbname, String user, String pass)
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

        public int getIntFromSQL(String sqlstr)
        {
            SqlCommand sql = new SqlCommand(sqlstr, conn);
            sql.CommandType = CommandType.Text;
            return Int32.Parse(sql.ExecuteScalar().ToString());
        }

        private delegate void QueryDelegate(SqlDataReader r);

        private void ExecQuery(string sql, QueryDelegate qd)
        {
            SqlCommand query = conn.CreateCommand();
            query.CommandText = sql;
            SqlDataReader reader = query.ExecuteReader();
            while (reader.Read())
                qd(reader);
            reader.Close();
        }

        public void ExecQuery(string sql) {
                SqlCommand query = new SqlCommand(sql, conn);
                query.CommandType = CommandType.Text;
                SqlDataReader data = query.ExecuteReader();
                data.Close();
        }

        public List<ArendaItem> getRooms()
        {
            List<ArendaItem> rooms = new List<ArendaItem>();

            ExecQuery("SELECT * FROM ROOMS", (r) =>
            {
                ArendaItem U = new ArendaItem();
                U.id = r["ID"].ToString();
                U.roomtype = r["ROOMTYPE"].ToString();
                U.address = r["ADDRESS"].ToString();
                U.city = r["CITY"].ToString();
                U.price = Int32.Parse(r["PRICE"].ToString());
                U.s = Int32.Parse(r["PRICE"].ToString());
                U.elite = r["ELITE"].ToString().Equals("1");
                if (r["UUID"].ToString().Equals(""))
                    U.guid = Guid.NewGuid();
                else
                    U.guid = Guid.Parse(r["UUID"].ToString());
                rooms.Add(U);
            });

            return rooms;
        }

        public static int INITIALSUM = 1000;
                
        public int getAccountSum(string user_id)
        {
            if (getIntFromSQL("SELECT COUNT(*) FROM ACCOUNTS WHERE USER_ID=" + user_id) == 0)
                ExecQuery("INSERT INTO ACCOUNTS (USER_ID,SUMMA) VALUES (" + user_id + "," + INITIALSUM.ToString("D") + ")");
            return getIntFromSQL("SELECT SUMMA FROM ACCOUNTS WHERE USER_ID=" + user_id);
        }

        public bool incAccountSum(string user_id, int value)
        {
            int tek = getAccountSum(user_id);
            ExecQuery("UPDATE ACCOUNTS SET SUMMA=" + (tek + value).ToString("D") + " WHERE USER_ID=" + user_id);
            return true;
        }

        public bool decAccountSum(string user_id, int value)
        {
            int tek = getAccountSum(user_id);
            if (tek < value) return false;

            ExecQuery("UPDATE ACCOUNTS SET SUMMA=" + (tek - value).ToString("D") + " WHERE USER_ID=" + user_id);
            return true;
        }

        public bool IsRoomReserved(string user_id, string roomid)
        {
            return getIntFromSQL("SELECT COUNT(*) FROM RESERVED WHERE USER_ID=" + user_id + " AND ROOM_ID=" + roomid) > 0;
        }

        public bool DoReserveRoom(string user_id, string roomid)
        {
            if (!IsRoomReserved(user_id,roomid))
                ExecQuery("INSERT INTO RESERVED (USER_ID,ROOM_ID) VALUES (" + user_id + "," + roomid + ")");
            return true;
        }
    }
}
