using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StringByteDictionary
{
    public List<StringBytePair> entries = new List<StringBytePair>();

    public Dictionary<string, byte> ToDictionary()
    {
        Dictionary<string, byte> dictionary = new Dictionary<string, byte>();
        foreach (var pair in entries)
        {
            dictionary[pair.key] = pair.value;
        }
        return dictionary;
    }
}