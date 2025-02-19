using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CreatureUIStatUpdate : EventArgs
{
    public short Health { get; }

    public CreatureUIStatUpdate(short health)
    {
        this.Health = health;
    }
}
