
using Protocol;
using System.Collections.Generic;
using UnityEngine;
using MsgType = Protocol.MsgType;

public delegate void Notify(NetMsg s);


public static class NotifyManager
{
    private static Queue<NetMsg> netMsgs = new Queue<NetMsg>();

    private static Dictionary<MsgType, Notify> notifyDic = new Dictionary<MsgType, Notify>();

    public static void OnReciveMsg(NetMsg netMsg)
    {
        netMsgs.Enqueue(netMsg);
    }

    public static void Update()
    {
        if (netMsgs.Count != 0)
        {
            var data = netMsgs.Dequeue();

            //MsgCpu.Single.OnReciveMsg(data);
            if (notifyDic.ContainsKey(data.msgType))
                notifyDic[data.msgType](data);

        }
    }

    public static void AddNotify(MsgType type, Notify action)
    {
        if (notifyDic.ContainsKey(type))
        {
            notifyDic[type] += action;
        }
        else
        {
            notifyDic[type] = new Notify(action);
        }
    }

    public static void RemoveNotify(MsgType type, Notify action)
    {
        if (notifyDic.ContainsKey(type))
        {
            notifyDic[type] -= action;
        }
    }

    public static Notify GetNotify(MsgType type)
    {
        if (notifyDic.ContainsKey(type))
        {
            return notifyDic[type];
        }
        else
        {
            notifyDic[type] = new Notify((s)=> { });
            return notifyDic[type];
        }
    }

}
