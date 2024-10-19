using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Unity.VisualScripting;

public class CheckOnEnemyEnterForParent : MonoBehaviour
{
    public SpecialHomingBullet specialHomingBullet;

    void OnTriggerEnter(Collider other)
    {
        specialHomingBullet.CustomOnTriggerEnter(other);
    }
}
