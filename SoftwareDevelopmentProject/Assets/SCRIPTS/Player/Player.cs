using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : SingletonMonobehaviour<Player>
{
    //Private

    //Protected
    protected override void Awake()
    {
        Singleton();
    }

    //Public
}
