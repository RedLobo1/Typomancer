using System;
using System.Collections.Generic;

[Serializable]
public class StringByteDictionary
{
    public List<StringBytePair> entries = new List<StringBytePair>();

    public Dictionary<string, float> ToDictionary()
    {
        Dictionary<string, float> dictionary = new Dictionary<string, float>();
        foreach (var pair in entries)
        {
            dictionary[pair.word] = pair.odds;
        }
        return dictionary;
    }
}