using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIWordSelector : MonoBehaviour
{
    [SerializeField] private TMP_Text[] letterSlots; // letter1, letter2, letter3, letter4;
    [SerializeField] private GameObject Lexicon;
    [SerializeField] private GameObject _pointer;

    public LoadInOwnedLetters loadInOwnedLetters;

    private LettersOwned letters;

    private float tolerance = 0.01f;

    public event EventHandler<WordEventArgs> OnValidateWord;
    public event Action<bool> OnPauseStateUpdate;

    private WordManager wordManager;

    private bool canSubmit = false;


    public Action<SO_Word> OnMove;

    public bool IsMovementBlocked = false;

    void Start()
    {
        loadInOwnedLetters = FindObjectOfType<LoadInOwnedLetters>();
        wordManager = FindObjectOfType<WordManager>();

        letters = loadInOwnedLetters.Letters;

        LoadInStartSequence();
        OnValidateWord?.Invoke(this, GetWordEventArgs());

        wordManager.OnWordchecked += TogglCanSubmit;


    }


    private void TogglCanSubmit(Color color, bool isWordCorrect)
    {
        canSubmit = isWordCorrect;
    }

    private void LoadInStartSequence()
    {
        UpdateAllLetters();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space) && !IsMovementBlocked)
        {
            UpdateCurrentSelectedLetter();

        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Lexicon.SetActive(!Lexicon.activeInHierarchy); //toggle visibility
            OnPauseStateUpdate?.Invoke(Lexicon.activeInHierarchy); //Toggle pause
        }

        if (canSubmit)
            if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
            {
                SubmitWord();
            }

    }

    private void SubmitWord()
    {
        canSubmit = false;
        OnMove?.Invoke(wordManager.GetWordDataFromWord(GetWord()));
        ResetLetters();

    }

    public void ResetLetters(float time = 1f)
    {
        ClearLetters();
        Invoke("UpdateAllLetters", time);
    }

    private void ClearLetters()
    {
        foreach (var letter in letterSlots)
        {
            letter.text = "";
            letter.transform.parent.GetComponent<Image>().color = Color.white;
        }
        wordManager.ResetTab();
    }

    private void CheckForCorrectWord()
    {
        //var word = GetSpelledWord();
        OnValidateWord?.Invoke(this, GetWordEventArgs());
    }
    private string GetWord()
    {
        //List<char> slots = new();
        List<char> slots = new();
        foreach (var letter in letterSlots)
        {
            if (CheckIfVisible(letter))
            {
                slots.Add(letter.text[0]);
            }
        }
        //return new WordEventArgs(slots);
        return new string(slots.ToArray());
    }

    private WordEventArgs GetWordEventArgs()
    {
        //List<char> slots = new();
        List<TMP_Text> slots = new();
        foreach (var letter in letterSlots)
        {
            if (CheckIfVisible(letter))
            {
                slots.Add(letter);
            }
        }
        return new WordEventArgs(slots);
        //return new string(slots.ToArray());
    }

    private void UpdateCurrentSelectedLetter()
    {
        var ptr_pos = _pointer.transform.position;
        foreach (var letter in letterSlots)
        {
            if (Vector3.Distance(ptr_pos, letter.transform.position) < tolerance)
            {
                if (letter.text != "" && letter.text != null)
                {
                    letter.text = GetRandomLetter(letter.text);
                    CheckForCorrectWord();
                }
            }
        }
    }
    private void UpdateAllLetters()
    {
        var ptr_pos = _pointer.transform.position;
        foreach (var letter in letterSlots)
        {
            if (CheckIfVisible(letter))
            {
                letter.text = GetRandomLetter(letter.text);
            }
        }
        CheckForCorrectWord();
    }

    private string GetRandomLetter(string letter)
    {
        string chosenLetter = "";

        do
        {
            chosenLetter = letters.letters[UnityEngine.Random.Range(0, letters.letters.Count)].ToString();
        }
        while (letter == chosenLetter);
        return chosenLetter;
    }

    private bool CheckIfVisible(TMP_Text letter)
    {
        return letter.transform.parent.gameObject.activeInHierarchy;
    }
}
