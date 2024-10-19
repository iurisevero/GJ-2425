using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// To test this example, just add it to any object in scene

public class EventSystemExample : MonoBehaviour
{
    public const string CustomEventName = "EventSystemExample.CustomEventName";
    public const string CustomEventName2 = "EventSystemExample.CustomEventName2";
    public string _name = "EventSystemExample";

    void Start()
    {
        StartCoroutine(ExampleCoroutine());
    }

    void OnEnable()
    {
        this.AddObserver(CustomFunction1, CustomEventName);
        this.AddObserver(CustomFunction2, CustomEventName2);
    }

    void OnDisable()
    {
        this.RemoveObserver(CustomFunction1, CustomEventName);
        this.RemoveObserver(CustomFunction2, CustomEventName2);
    }

    IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(2f);
        this.PostNotification(CustomEventName);
        yield return new WaitForSeconds(2f);
        this.PostNotification(CustomEventName2, new Info(0, true));
    }

    void CustomFunction1(object sender, object args)
    {
        Debug.Log("CustomFunction1 Sender: " + sender);
        Debug.Log("CustomFunction1 Args: " + args);
    }

    void CustomFunction2(object sender, object args)
    {
        EventSystemExample senderObj = sender as EventSystemExample;
        Debug.Log("CustomFunction1 Sender: " + senderObj.name);
        var info = args as Info;
        int arg0 = 0;
        bool arg1 = false;
        if (info.args[0] is int)
            arg0 = (int)info.args[0];
        else
            Debug.LogWarning("Argumento 1 do tipo inválido");

        if (info.args[1] is bool)
            arg1 = (bool)info.args[1];
        else
            Debug.LogWarning("Argumento 1 do tipo inválido");

        Debug.Log("CustomFunction1 Args: [" + info.args[0] + ", " + info.args[1] + "]");
    }
}
