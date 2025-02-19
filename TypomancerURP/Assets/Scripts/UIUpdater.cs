using System;
using TMPro;
using UnityEngine;

public class UIUpdater : MonoBehaviour
{
    [SerializeField]
    private GameObject PlayerName;
    // Start is called before the first frame update

    private BattleSimulator battleSim;
    void Start()
    {
        battleSim = FindObjectOfType<BattleSimulator>();
        battleSim.OnPrizeLetterObtained += UpdatePlayerName;
    }

    private void UpdatePlayerName(char prizeLetter)
    {
        foreach (var letterbox in PlayerName.GetComponentsInChildren<Transform>())
        {
            foreach(TMP_Text letter in letterbox)
            {
                if (letter.text == prizeLetter.ToString())
                {
                    letter.enabled = true;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
