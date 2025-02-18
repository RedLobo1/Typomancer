using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIWordSelector : MonoBehaviour
{
    [SerializeField] private TMP_Text[] letterSlots; // letter1, letter2, letter3, letter4;
    [SerializeField] private GameObject _pointer;

    public LoadInOwnedLetters loadInOwnedLetters;

    private LettersOwned letters;

    private float tolerance = 0.01f;

    void Start()
    {
        loadInOwnedLetters = FindObjectOfType<LoadInOwnedLetters>();
        letters = loadInOwnedLetters.Letters;

        LoadInStartSequence();
    }

    private void LoadInStartSequence()
    {
        UpdateAllLetters();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            UpdateCurrentSelectedLetter();
            CheckForCorrectWord();
        }

    }

    private void CheckForCorrectWord()
    {
        var word = GetSpelledWord();
        Debug.Log(word);
    }

    private string GetSpelledWord()
    {
        List<char> slots = new();
        foreach (var letter in letterSlots)
        {
            if (CheckIfVisible(letter))
            {
                slots.Add(letter.text.ToCharArray()[0]);
            }
        }
        return new string(slots.ToArray());
    }

    private void UpdateCurrentSelectedLetter()
    {
        var ptr_pos = _pointer.transform.position;
        foreach (var letter in letterSlots)
        {
            if (Vector3.Distance(ptr_pos, letter.transform.position) < tolerance)
            {
                letter.text = GetRandomLetter(letter.text);
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
    }

    private string GetRandomLetter(string letter)
    {
        string chosenLetter = "";

        do
        {
            chosenLetter = letters.letters[Random.Range(0, letters.letters.Count)].ToString();
        }
        while (letter == chosenLetter);
        return chosenLetter;
    }

    private bool CheckIfVisible(TMP_Text letter)
    {
        return letter.transform.parent.gameObject.activeInHierarchy;
    }
}
