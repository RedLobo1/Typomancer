using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopulateLexicon : MonoBehaviour
{
    [SerializeField]
    private TMP_Text TotalWordCount;
    [SerializeField]
    private TMP_Text KnownWordCount;

    [SerializeField]
    private List<Transform> WordPanels;

    private List<string> KnownWords = new();

    private WordManager wordManager;
    private BattleSimulator battleSim;
    private UIWordSelector userInput;


    private List<List<SO_Word>> _wordList;
    private int _totalWords;
    // Start is called before the first frame update
    void Start()
    {
        wordManager = FindObjectOfType<WordManager>();
        wordManager.OnFullWordListLoaded += SaveWordList;
        wordManager.OnFullWordListLoaded += CountTotalWords;

        battleSim = FindObjectOfType<BattleSimulator>();
        battleSim.OnEnemyWordPicked += AddWordToLexicon;

        userInput = FindObjectOfType<UIWordSelector>();
        userInput.OnMove += AddWordToLexicon;
    }

    private void AddWordToLexicon(SO_Word word)
    {
        if (!KnownWords.Contains(word.name.ToUpper()))
        {
            KnownWords.Add(word.name.ToUpper());
            KnownWordCount.text = KnownWords.Count.ToString();
            PopulatePanel(word);
        }
    }

    private void AddWordToLexicon(string word)
    {
        if (!KnownWords.Contains(word.ToUpper()))
        {
            KnownWords.Add(word.ToUpper());
            KnownWordCount.text = KnownWords.Count.ToString();
            PopulatePanel(wordManager.GetWordDataFromWord(word));
        }
    }

    private void PopulatePanel(SO_Word word)
    {
        if (WordPanels != null)
        {
            for (int i = 0; i < _wordList.Count; i++)
            {
                if (_wordList[i].Contains(word))
                {
                    var childGameObject = WordPanels[i].GetChild(0).gameObject;
                    GameObject newText = Instantiate(childGameObject, WordPanels[i]);
                    newText.GetComponent<TMP_Text>().text = word.name.ToUpper();
                    newText.SetActive(true);
                }
            }
        }
    }

    private void CountTotalWords(List<List<SO_Word>> wordlist)
    {
        foreach (List<SO_Word> list in wordlist)
            foreach (SO_Word word in list)
                _totalWords++;
        TotalWordCount.text = _totalWords.ToString(); ;
    }

    private void SaveWordList(List<List<SO_Word>> wordlist)
    {
        _wordList = wordlist;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
