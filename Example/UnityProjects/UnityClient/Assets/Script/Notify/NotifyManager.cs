
using System.Collections.Generic;
using UnityEngine;
using MsgType = Protocol.MsgType;

public delegate void Notify(string s);


class NotifyManager : SingleBase<NotifyManager>
{

    private Dictionary<MsgType, Notify> notifyDic = new Dictionary<MsgType, Notify>();

    public void AddNotify(MsgType type, Notify action)
    {
        if (notifyDic.ContainsKey(type))
        {
            notifyDic[type] += action;
        }
        else
        {
            notifyDic[type] = new Notify(action);
        }
        Debug.Log("注册成功" + type.ToString());
    }

    public void RemoveNotify(MsgType type, Notify action)
    {
        if (notifyDic.ContainsKey(type))
        {
            notifyDic[type] -= action;
            Debug.Log("注销成功" + type.ToString());
        }
    }

    public Notify GetNotify(MsgType type)
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
