using System.Collections;
using System.Collections.Generic;

public class MsgMove : MsgBase
{
    public MsgMove()
    {
        protoName = "MsgMove";
    }
    public int x = 0;
    public int y = 0;
    public int z = 0;
}
public class MsgAttack:MsgBase
{
    public MsgAttack()
    {
        protoName = "MsgAttack";
    }
    public string desc = "172.28.17.121:8888 ";
}
