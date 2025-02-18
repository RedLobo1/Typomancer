using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WordManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<SO_Word> _wordList = new List<SO_Word>();
    UIWordSelector UI;
    void Start()
    {
        LoadDictionary();
        Debug.Log(_wordList.Count);

        UI = FindObjectOfType<UIWordSelector>();
        UI.OnValidateWord += ValidateWord;
    }

    private void ValidateWord(object sender, WordEventArgs e)
    {
        if (IsWordInDictionary(GetWordFromEvent(e)))
        {
            foreach (var letter in e.Word)
            {
                letter.color = Color.green;
            }

        }
        else
        {
            foreach (var letter in e.Word)
            {
                letter.color = Color.black;
            }
        }
    }

    private bool IsWordInDictionary(string spelledWord)
    {
        foreach (var word in _wordList)
        {
            if (word.Word.ToLower() == spelledWord.ToLower())
            {

                return true;
            }
        }
        return false;
    }

    private string GetWordFromEvent(WordEventArgs e)
    {
        List<char> Letters = new();
        foreach (var letter in e.Word)
        {
            Letters.Add(letter.text.ToCharArray()[0]);
        }
        return new string(Letters.ToArray());
    }

    private void LoadDictionary()
    {
        string[] types = { "Attacks", "Defence", "Health", "Status" };
        string[] wordGroups = { "3-LetterWords"/*, "4-LetterWords"*/};
        foreach (string wordGroup in wordGroups)
            foreach (string type in types)
                _wordList.AddRange(Resources.LoadAll<SO_Word>($"Dictionary/{wordGroup}/{type}").ToList());
    }

    // Update is called once per frame
    void Update()
    {

    }
}
