using System;
using UnityEngine;

public class BattleSimulator : MonoBehaviour
{
    UIWordSelector userInput;

    private Enemy enemy;
    private Player player;

    private float enemyAttackCooldown;
    [SerializeField]
    private float enemyAttackModifier = 0.9f;
    [SerializeField]
    private float enemyAttackCooldownMinumum = 0.1f;

    private float timeElapsed = 0f; // To track time

    WordManager wordManager;

    private string chosenWord;
    void Start()
    {
        wordManager = FindObjectOfType<WordManager>();

        userInput = FindObjectOfType<UIWordSelector>();
        userInput.OnMove += MoveByPlayer;

        enemy = FindObjectOfType<Enemy>();
        player = FindObjectOfType<Player>();

        enemyAttackCooldown = enemy.GetBaseAttackCooldown();
        chosenWord = enemy.PickWord();
    }
    // Update is called once per frame
    void Update()
    {

        timeElapsed += Time.deltaTime;
        if (timeElapsed >= enemyAttackCooldown)
        {
            MoveByEnemy(); // Call your function
            timeElapsed = 0f;
        }
        //StatucEffectTick();
    }

    private void MoveByPlayer(SO_Word wordData)
    {
        enemy.ChangeHealth((sbyte)(Mathf.Min(0, -wordData.attackModifier - enemy.GetBaseAttackCooldown())));
        enemy.AfflictStatusEffect(wordData.StatusEffect);

        player.ChangeHealth(wordData.HealthModifier);
        player.BoostDefence((byte)wordData.DefenceModifier);

        enemyAttackCooldown = Math.Max(enemyAttackCooldownMinumum, enemyAttackCooldown * enemyAttackModifier);

    }

    private void MoveByEnemy()
    {
        //var wordData = wordManager.GetWordDataFromWord(chosenWord);

        //player.ChangeHealth((sbyte)(-wordData.attackModifier - enemy.GetDefence()));
        //player.AfflictStatusEffect(wordData.StatusEffect);

        //enemy.ChangeHealth(wordData.HealthModifier);
        //enemy.BoostDefence((byte)wordData.DefenceModifier);
    }
}
