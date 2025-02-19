using System;

[Serializable]
public class StringBytePair
{
    public string word;
    public byte odds;

    public StringBytePair(string key, byte value)
    {
        this.word = key;
        this.odds = value;
    }
}