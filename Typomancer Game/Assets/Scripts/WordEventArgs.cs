using System;
using System.Collections.Generic;
using TMPro;

public class WordEventArgs : EventArgs
{
    public List<TMP_Text> Word { get; }

    public WordEventArgs(List<TMP_Text> word)
    {
        Word = word;
    }
}
