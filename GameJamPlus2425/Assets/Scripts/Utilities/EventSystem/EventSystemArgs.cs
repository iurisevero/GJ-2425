using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

// var args = new Info (1,3.14,'a');
public class Info
{
    public object[] args;

    public Info(params object[] args)
    {
        this.args = args;
    }
}
