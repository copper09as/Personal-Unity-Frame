﻿public class PlayerManager
{
    static Dictionary<string, Player> players = new Dictionary<string, Player>();

    public static bool IsOnline(string id)
    {
        return players.ContainsKey(id);
    }
    public static Player GetPlayer(string id)
    {
        if(players.ContainsKey(id))
        {
            return players[id];
        }
        return null;
    }
    public static void AddPlayer(string id,Player player)
    {
        players.Add(id, player);
    }
    public static void RemovePlayer(string id)
    {
        players.Remove(id);
    }
}

