using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class LoadInOwnedLetters : MonoBehaviour
{
    [SerializeField]
    private TMP_Text m_Text;

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
            m_Text.text = new string(letters.letters.ToArray());
        else
            letters.letters = m_Text.text.ToCharArray().ToList();
    }


    private void AddLetterToOwnedLetters(char prizeLetter)
    {
        m_Text.text += prizeLetter.ToString().ToUpper();
        SaveKnownLetters(m_Text.text.ToCharArray().ToList());
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
