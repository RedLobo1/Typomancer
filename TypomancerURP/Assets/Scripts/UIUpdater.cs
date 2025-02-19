using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIUpdater : MonoBehaviour
{
    [SerializeField]
    private GameObject PlayerName;

    [SerializeField] private Slider EnemyAttackSlider;

    [SerializeField] private Slider EnemyHealthSlider;
    // Start is called before the first frame update

    private BattleSimulator battleSim;
    void Start()
    {
        battleSim = FindObjectOfType<BattleSimulator>();
        battleSim.OnPrizeLetterObtained += UpdatePlayerName;

        battleSim.OnEnemyWordPicked += UpdateEnemyAttack;
        battleSim.OnEnemyStatUpdate += UpdateEnemyAttackTimer;
    }

    private void UpdateEnemyAttackTimer(CreatureUIStatUpdate stats, float percentage)
    {
        EnemyAttackSlider.value = percentage;
        EnemyHealthSlider.value = stats.Health;
    }

    private void UpdateEnemyAttack(string attack)
    {
        attack.ToCharArray();

        
    }

    private void UpdatePlayerName(char prizeLetter)
    {
        //foreach (var letterbox in PlayerName.GetComponentsInChildren<Transform>())
        //{
        //    foreach(TMP_Text letter in letterbox)
        //    {
        //        if (letter.text == prizeLetter.ToString())
        //        {
        //            letter.enabled = true;
        //        }
        //    }
        //}
    }

    // Update is called once per frame
    void Update()
    {

    }
}
