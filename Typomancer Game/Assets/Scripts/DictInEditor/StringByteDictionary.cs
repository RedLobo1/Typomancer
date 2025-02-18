using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StringByteDictionary
{
    public List<StringBytePair> entries = new List<StringBytePair>();

    public Dictionary<string, float> ToDictionary()
    {
        Dictionary<string, float> dictionary = new Dictionary<string, float>();
        foreach (var pair in entries)
        {
            dictionary[pair.key] = pair.value;
        }
        return dictionary;
    }
}