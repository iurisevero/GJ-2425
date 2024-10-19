using UnityEngine;
using System;
using System.Collections;
using Handler = System.Action<System.Object, System.Object>;

public static class EventSystemExtensions
{
    public static void PostNotification(this object obj, string notificationName)
    {
        if(EventSystemCenter.instance != null)
            EventSystemCenter.instance.PostNotification(notificationName, obj);
    }

    public static void PostNotification(this object obj, string notificationName, object e)
    {
        if(EventSystemCenter.instance != null)
            EventSystemCenter.instance.PostNotification(notificationName, obj, e);
    }

    public static void AddObserver(this object obj, Handler handler, string notificationName)
    {
        if(EventSystemCenter.instance != null)
            EventSystemCenter.instance.AddObserver(handler, notificationName);
    }

    public static void AddObserver(this object obj, Handler handler, string notificationName, object sender)
    {
        if(EventSystemCenter.instance != null)
            EventSystemCenter.instance.AddObserver(handler, notificationName, sender);
    }

    public static void RemoveObserver(this object obj, Handler handler, string notificationName)
    {
        if(EventSystemCenter.instance != null)
            EventSystemCenter.instance.RemoveObserver(handler, notificationName);
    }

    public static void RemoveObserver(this object obj, Handler handler, string notificationName, System.Object sender)
    {
        if(EventSystemCenter.instance != null)
            EventSystemCenter.instance.RemoveObserver(handler, notificationName, sender);
    }
}
