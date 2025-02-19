using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LoadInOwnedLetters : MonoBehaviour
{
    [SerializeField]
    private string AvailableLetters;

    private LettersOwned letters;

    public LettersOwned Letters => letters;
    // Start is called before the first frame update

    BattleSimulator battleSim;
    void Awake()
    {
        LoadInKnownLetters();

        battleSim = FindObjectOfType<BattleSimulator>();
        battleSim.OnPrizeLetterObtained += AddLetterToOwnedLetters;
    }

    private void LoadInKnownLetters()
    {
        if (letters.letters != null)
            AvailableLetters = new string(letters.letters.ToArray());
        else
            letters.letters = AvailableLetters.ToCharArray().ToList();
    }


    private void AddLetterToOwnedLetters(char prizeLetter)
    {
        AvailableLetters += prizeLetter.ToString().ToUpper();
        SaveKnownLetters(AvailableLetters.ToCharArray().ToList());
    }

    private void SaveKnownLetters(List<char> saveData)
    {
        letters.letters = saveData;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
