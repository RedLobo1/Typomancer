using System;
using UnityEngine;

[Serializable]
public class StringBytePair
{
    public string key;
    public byte value;

    public StringBytePair(string key, byte value)
    {
        this.key = key;
        this.value = value;
    }
}