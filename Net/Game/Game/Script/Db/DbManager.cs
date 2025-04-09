using MySql.Data.MySqlClient;
using Org.BouncyCastle.Utilities.IO;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
public class DbManager
{
    public static MySqlConnection mySql;
    
    public static bool Connect(string db,string ip,int port,string user,string pw)
    {
        mySql = new MySqlConnection();
        
        string s = string.Format
            ("Database={0};Data Source={1};port={2};User Id={3};Password={4}", db, ip, port, user, pw);
        mySql.ConnectionString = s;
        try
        {
            mySql.Open();
            Console.WriteLine("{数据库}connect succ");
            return true;
        }
        catch(Exception e)
        {
            Console.WriteLine("{数据库}connect fail,"+e.Message);
            return false;
        }

    }
    private static bool IsSafeString(string str)
    {
        return !Regex.IsMatch(str, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']");
    }
    public static bool IsAccountExist(string id)
    {
        if(!DbManager.IsSafeString(id))
        {
            return false;
        }
        string s = string.Format("select * from account where id='{0}';", id);
        try
        {
            MySqlCommand cmd = new MySqlCommand(s, mySql);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            bool hasRows = dataReader.HasRows;
            dataReader.Close();
            return !hasRows;
        }
        catch(Exception e)
        {
            Console.WriteLine("{数据库}IsSafeString err," + e.Message);
            return false;
        }
    }
    public static bool Register(string id,string pw)
    {
        if(!DbManager.IsSafeString(id))
        {
            Console.WriteLine("{数据库}Register Fail,id not safe,");
            return false;
        }
        if (!DbManager.IsSafeString(pw))
        {
            Console.WriteLine("{数据库}Register Fail,pw not safe,");
            return false;
        }
        if (!DbManager.IsAccountExist(id))
        {
            Console.WriteLine("{数据库}Register Fail,id exist,");
            return false;
        }
        string sql = string.Format("insert into account set id = '{0}',pw = '{1}';", id, pw);
        try
        {
            MySqlCommand cmd = new MySqlCommand(sql, mySql);
            cmd.ExecuteNonQuery();
            return true;
        }
        catch(Exception e)
        {
            Console.WriteLine("{数据库}Register Fail," + e.Message);
            return false;
        }
    }
    public static bool CreatePlayer(string id)
    {
        if (!DbManager.IsSafeString(id))
        {
            Console.WriteLine("{数据库}CreatePlayer Fail,id not safe,");
            return false;
        }
        PlayerData playerData = new PlayerData();
        string data = JsonConvert.SerializeObject(playerData);
        string sql = string.Format("insert into player set id = '{0}',data = '{1}';", id, data);
        try
        {
            MySqlCommand cmd = new MySqlCommand(sql, mySql);
            cmd.ExecuteNonQuery();
            return true;
        }
        catch(Exception e)
        {
            Console.WriteLine("{数据库}Create Player fail," + e.Message);
            return false;
        }
    }
    public static bool CheckPassword(string id,string pw)
    {
        if (!DbManager.IsSafeString(id))
        {
            Console.WriteLine("{数据库}CheckPassWord Fail,id not safe,");
            return false;
        }
        if (!DbManager.IsSafeString(pw))
        {
            Console.WriteLine("{数据库}CheckPassWord Fail,pw not safe,");
            return false;
        }
        string sql = string.Format("select * from account where id ='{0}'and pw ='{1}';", id, pw);
        try
        {
            MySqlCommand cmd = new MySqlCommand(sql, mySql);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            bool hasRows = dataReader.HasRows;
            dataReader.Close();
            return hasRows;
        }
        catch(Exception e)
        {
            Console.WriteLine("{数据库}Check PassWord err," + e.Message);
            return false;
        }
    }
    public static PlayerData GetPlayerData(string id)
    {
        if (!DbManager.IsSafeString(id))
        {
            Console.WriteLine("{数据库}Register Fail,id not safe,");
            return null;
        }
        string sql = string.Format("select * from player where id = '{0}';", id);
        try
        {
            MySqlCommand cmd = new MySqlCommand(sql, mySql);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            if(!dataReader.HasRows)
            {
                
                dataReader.Close();
                return null;
            }
            dataReader.Read();
            string data = dataReader.GetString("data");
            PlayerData playerData = JsonConvert.DeserializeObject<PlayerData>(data);
            dataReader.Close();
            return playerData;
        }
        catch(Exception e)
        {
            Console.WriteLine("{数据库}GetPlayerData err," + e.Message);
            return null;
        }
    }
    public static bool UpdataPlayerData(string id,PlayerData playerData)
    {
        string data = JsonConvert.SerializeObject(playerData);
        string sql = string.Format("update player set data = '{0}' where id = '{1}'", data, id);
        try
        {
            MySqlCommand cmd = new MySqlCommand(sql, mySql);
            cmd.ExecuteNonQuery();
            return true;
        }
        catch(Exception e)
        {
            Console.WriteLine("{数据库}UpdataPlayerData err," + e.Message);
            return false;
        }
    }
}

