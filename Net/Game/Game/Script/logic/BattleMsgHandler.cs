﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Script.Net;

public partial class MsgHandler()
{
    public static void MsgMove(ClientState c,MsgBase msgBase)
    {
        MsgMove msgMove = (MsgMove)msgBase;
        Console.WriteLine(msgMove.x);
        msgMove.x++;
        NetManager.Send(c, msgMove);
    }


}
