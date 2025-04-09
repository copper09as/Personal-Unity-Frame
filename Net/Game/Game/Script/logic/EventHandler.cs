using Game.Script.Net;


public class EventHandler
{
    public static void OnDisconnect(ClientState c)
    {
        Console.WriteLine("Close");
        if(c.player != null)
        {
            DbManager.UpdataPlayerData(c.player.id, c.player.data);
            PlayerManager.RemovePlayer(c.player.id);
        }
    }
    public static void OnTimer()
    {
        CheckPing();
    }
    public static void CheckPing()
    {
        long timeNow = NetManager.GetTimeStamp();
        foreach(ClientState s in NetManager.clients.Values)
        {
            if(timeNow - s.lastPingTime>NetManager.pingInterval*4)
            {
                Console.WriteLine("Ping Close"+s.socket.RemoteEndPoint.ToString());
                NetManager.Close(s);
                continue;
            }
            
        }

    }
}

