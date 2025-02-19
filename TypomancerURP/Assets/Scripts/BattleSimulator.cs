using System;
using UnityEngine;

public class BattleSimulator : MonoBehaviour
{
    UIWordSelector userInput;

    private Enemy enemy;
    private Player player;

    public float enemyAttackCooldown;
    [SerializeField]
    private float enemyAttackModifier = 0.9f;
    [SerializeField]
    private float enemyAttackCooldownMinumum = 0.1f;

    private float EnemyCooldownTimePassed = 0f; // To track time

    WordManager wordManager;

    public event EventHandler<CreatureUIStatUpdate> OnPlayerStatUpdate;
    public event Action<CreatureUIStatUpdate, float> OnEnemyStatUpdate;
    public event Action OnGameOver;
    public event Action OnEnemyBeaten;

    public event Action<Creature, sbyte> OnHealthChanged;
    public event Action<Creature, byte> OnDefenceChanged;
    public event Action<Creature, EStatusEffect> OnStatusEffectAfflicted;
    public event Action<char> OnPrizeLetterObtained;
    public event Action<string> OnEnemyWordPicked;
    //public event Action<char> OnPrizeLetterObtained;

    private string chosenWord;
    private bool battlePaused = false;

    void Start()
    {
        wordManager = FindObjectOfType<WordManager>();

        userInput = FindObjectOfType<UIWordSelector>();
        userInput.OnMove += MoveByPlayer;

        enemy = FindObjectOfType<Enemy>();
        player = FindObjectOfType<Player>();

        enemyAttackCooldown = enemy.GetBaseAttackCooldown();
        chosenWord = enemy.PickWord();
        OnEnemyWordPicked?.Invoke(chosenWord);
    }
    // Update is called once per frame
    void Update()
    {
        if (!battlePaused)
        {
            BattleFinishCheck();

            EnemyCooldownTick();
            StatusTick(player);
            StatusTick(enemy); ;
        }
    }

    private void BattleFinishCheck()
    {
        if (player.GetHealth() <= 0)
        {
            OnGameOver?.Invoke();
            battlePaused = true;
        }
        if (enemy.GetHealth() <= 0)
        {
            OnPrizeLetterObtained?.Invoke(enemy.GetPrizeLetter());
            OnEnemyBeaten?.Invoke();
            battlePaused = true;
        }
    }

    private void EnemyCooldownTick()
    {
        EnemyCooldownTimePassed += Time.deltaTime;
        if (EnemyCooldownTimePassed >= enemyAttackCooldown)
        {
            MoveByEnemy(); // Call your function
            EnemyCooldownTimePassed = 0f;
        }
    }

    private void StatusTick(Creature player)
    {
        //switch(player.GetStatusEffect)
        //player.statusTimer
    }

    private void MoveByPlayer(SO_Word moveData)
    {
        ExecuteStatChanges(player, enemy, moveData);
        enemyAttackCooldown = Math.Max(enemyAttackCooldownMinumum, enemyAttackCooldown * enemyAttackModifier);
    }

    private void MoveByEnemy()
    {
        var moveData = wordManager.GetWordDataFromWord(chosenWord);

        ExecuteStatChanges(player, enemy, moveData);

        chosenWord = enemy.PickWord(); //enemy picks new word after attacking
        OnEnemyWordPicked?.Invoke(chosenWord);
    }

    private void ExecuteStatChanges(Creature attacker, Creature defender, SO_Word MoveData)
    {
        sbyte healthModifier = (sbyte)(Mathf.Min(0, -MoveData.attackModifier - enemy.GetDefence()));
        UpdateHealth(defender, healthModifier);
        if (MoveData.attackModifier != 0)
            UpdateDefence(defender, 0); //if the attack does damage, remove the defence

        UpdateHealth(attacker, MoveData.HealthModifier);
        UpdateDefence(attacker, (byte)MoveData.DefenceModifier);


        if (MoveData.StatusEffect != EStatusEffect.None)
        {
            OnStatusEffectAfflicted?.Invoke(defender, MoveData.StatusEffect);
            defender.AfflictStatusEffect(MoveData.StatusEffect);
        }



        OnPlayerStatUpdate?.Invoke(this, new CreatureUIStatUpdate(player.GetHealth()));
        OnEnemyStatUpdate?.Invoke(new CreatureUIStatUpdate(enemy.GetHealth()), (EnemyCooldownTimePassed / enemyAttackCooldown));
    }
    private void UpdateHealth(Creature creature, sbyte healthModifier)
    {
        if (healthModifier != 0)
        {
            OnHealthChanged?.Invoke(creature, healthModifier);
            creature.ChangeHealth(healthModifier);
        }
    }
    private void UpdateDefence(Creature creature, byte DefenceModifier)
    {
        if (DefenceModifier != 0)
        {
            OnDefenceChanged?.Invoke(creature, DefenceModifier);
            creature.ChangeDefence(DefenceModifier);
        }
    }
}
