using Game.Script.Net;

public class Player
{
    public string id = "";
    public ClientState state;

    public int x;
    public int y;
    public int z;

    public PlayerData data;

    public Player(ClientState clientState)
    {
        this.state = clientState;
    }
    public void Send(MsgBase msgBase)
    {
        NetManager.Send(state, msgBase);
    }
}

