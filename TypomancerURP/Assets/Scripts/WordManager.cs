using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordManager : MonoBehaviour
{
    // Start is called before the first frame update
    private List<List<SO_Word>> _wordList = new();
    private List<SO_Word> _AttackList = new List<SO_Word>();
    private List<SO_Word> _DefenceList = new List<SO_Word>();
    private List<SO_Word> _HealList = new List<SO_Word>();
    private List<SO_Word> _StatusList = new List<SO_Word>();
    UIWordSelector UI;

    private string[] types = { "Attacks", "Defence", "Health", "Status" };
    private string[] wordGroups = { "3-LetterWords"/*, "4-LetterWords"*/};

    private string[] hexColors = { "#E31D2B", "#00A0FF", "#1DE276", "#E8DE1C", "#FFFFFF" };
    private List<Color> colors = new List<Color>();

    public event Action<bool> onWordchecked;

    void Awake()
    {
        ConvertHexToColors();
        LoadDictionary();
        //Debug.Log(_wordList.Count);

        UI = FindObjectOfType<UIWordSelector>();
        UI.OnValidateWord += ValidateWord;
    }

    private void ConvertHexToColors()
    {
        foreach (var hex in hexColors)
        {
            ColorUtility.TryParseHtmlString(hex, out Color color);
            colors.Add(color);
        }
    }

    private void ValidateWord(object sender, WordEventArgs e)
    {
        var color = returnColorForSet(GetWordFromEventArgs(e));
        foreach (var letter in e.Word)
        {
            letter.transform.parent.GetComponent<Image>().color = color;
        }
        onWordchecked?.Invoke(IsWordInDictionary(color));

    }

    private bool IsWordInDictionary(Color color)
    {
        if (color != colors[colors.Count - 1]) return true;
        else return false;
    }

    public SO_Word GetWordDataFromWord(string spelledWord)
    {
        for (int i = 0; i < _wordList.Count; i++)
        {
            foreach (var word in _wordList[i])
            {
                if (word.Word.ToLower() == spelledWord.ToLower())
                {
                    return word;
                }
            }
        }
        return null;
    }

    private Color returnColorForSet(string spelledWord)
    {
        for (int i = 0; i < _wordList.Count; i++)
        {
            foreach (var word in _wordList[i])
            {
                if (word.Word.ToLower() == spelledWord.ToLower())
                {
                    return colors[i];
                }
            }

        }
        return colors[colors.Count - 1];

    }

    public string GetWordFromEventArgs(WordEventArgs e)
    {
        List<char> Letters = new();
        
        foreach (var letter in e.Word)
        {
            if(letter.text != "")
                Letters.Add(letter.text.ToCharArray()[0]);
        }
        return new string(Letters.ToArray());
    }

    private void LoadDictionary()
    {
        foreach (string wordGroup in wordGroups)
            foreach (string type in types)
                switch (type)
                {
                    case "Attacks": _AttackList.AddRange(Resources.LoadAll<SO_Word>($"Dictionary/{wordGroup}/{type}")); break;
                    case "Defence": _DefenceList.AddRange(Resources.LoadAll<SO_Word>($"Dictionary/{wordGroup}/{type}")); break;
                    case "Health": _HealList.AddRange(Resources.LoadAll<SO_Word>($"Dictionary/{wordGroup}/{type}")); break;
                    case "Status": _StatusList.AddRange(Resources.LoadAll<SO_Word>($"Dictionary/{wordGroup}/{type}")); break;
                }

        _wordList.Add(_AttackList);
        _wordList.Add(_DefenceList);
        _wordList.Add(_HealList);
        _wordList.Add(_StatusList);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
