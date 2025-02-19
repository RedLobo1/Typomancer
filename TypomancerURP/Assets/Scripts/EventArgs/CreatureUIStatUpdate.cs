using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CreatureUIStatUpdate : EventArgs
{
    public short Health { get; }
    public short MaxHealth { get; }

    public CreatureUIStatUpdate(short maxHealth, short health)
    {
        this.MaxHealth = maxHealth;
        this.Health = health;
    }
}
