using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WordManager : MonoBehaviour
{
    // Start is called before the first frame update
    List<SO_Word> _wordList = new List<SO_Word>();
    void Start()
    {
        LoadDictionary();
        Debug.Log(_wordList.Count);
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
