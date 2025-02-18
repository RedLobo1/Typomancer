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
    void Awake()
    {
        LoadInKnownLetters();
    }

    private void LoadInKnownLetters()
    {
        if (letters.letters != null)
            m_Text.text = new string(letters.letters.ToArray());
        else
            letters.letters = m_Text.text.ToCharArray().ToList();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
