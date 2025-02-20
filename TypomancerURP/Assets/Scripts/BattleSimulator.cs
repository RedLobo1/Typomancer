using System;
using UnityEngine;

public class BattleSimulator : MonoBehaviour
{
    UIWordSelector userInput;

    private Enemy enemy;
    private Player player;

    private float enemyAttackCooldownCurrent;
    private float enemyAttackCooldownStartTurn;
    [SerializeField]
    private float enemyAttackModifier = 0.9f;
    [SerializeField]
    private float enemyAttackCooldownMinumum = 0.1f;

    private float EnemyCooldownTimePassed = 0f; // To track time

    WordManager wordManager;
    UIWordSelector UIWordSelector;

    public event EventHandler<CreatureUIStatUpdate> OnPlayerStatUpdate;
    public event EventHandler<CreatureUIStatUpdate> OnEnemyStatUpdate;
    public event Action<float> OnEnemyCooldownTimePassed;
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

    [SerializeField]
    private float stunTime = 5f;
    [SerializeField]
    private float sickTime = 5f;
    [SerializeField]
    private float sickDamageInterval = 1f;
    [SerializeField]
    private sbyte sickDamageAmount = 1;
    [SerializeField]
    private float blindSetBackamount = 3f;

    void Start()
    {
        wordManager = FindObjectOfType<WordManager>();
        UIWordSelector = FindObjectOfType<UIWordSelector>();
        UIWordSelector.OnPauseStateUpdate += PauseToggle;

        userInput = FindObjectOfType<UIWordSelector>();
        userInput.OnMove += MoveByPlayer;

        enemy = FindObjectOfType<Enemy>();
        player = FindObjectOfType<Player>();

        enemyAttackCooldownCurrent = enemy.GetBaseAttackCooldown();
        enemyAttackCooldownStartTurn = enemyAttackCooldownCurrent;
        RerollEnemyWord();
    }

    private void PauseToggle(bool state)
    {
        battlePaused = state;
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
        if (enemy.GetStatusEffect() != EStatusEffect.Stun)
            EnemyCooldownTimePassed += Time.deltaTime;
        else
            EnemyCooldownTimePassed += Time.deltaTime / 2;

        OnEnemyCooldownTimePassed?.Invoke(EnemyCooldownTimePassed / enemyAttackCooldownStartTurn);

        if (EnemyCooldownTimePassed >= enemyAttackCooldownCurrent)
        {

            MoveByEnemy(); // Call your function
            EnemyCooldownTimePassed = 0f;
            enemyAttackCooldownStartTurn = enemyAttackCooldownCurrent;
        }
    }

    private void StatusTick(Creature creature)
    {
        switch (player.GetStatusEffect())
        {
            case EStatusEffect.Blind: OnBlind(creature); break;
            case EStatusEffect.Sick: OnSick(creature); break;
            case EStatusEffect.Stun: OnStun(creature); break;
        }
        //player.statusTimer
    }
    private void OnBlind(Creature creature)
    {
        if (creature is Player)
        {
            userInput.ResetLetters(3f); //rest letters, only restore after 3s
        }
        if (creature is Enemy)
        {
            enemyAttackCooldownCurrent = MathF.Max(enemyAttackCooldownCurrent - blindSetBackamount, 0);//roll back timer by 3s or until 0
            RerollEnemyWord();//reroll chosen word
            
        }
        RemoveStatusEffectFromCreature(creature);
    }

    private void RemoveStatusEffectFromCreature(Creature creature)
    {
        creature.SetStatusEffect(EStatusEffect.None);//heal stun
        OnStatusEffectAfflicted?.Invoke(creature, EStatusEffect.None);
    }

    private void OnSick(Creature creature)
    {

        //1 damage every second, heal after 5s
        creature.statusTimer += Time.deltaTime;
        if (creature.statusTimer >= sickDamageInterval)
        {
            creature.damagecounter++;
            UpdateHealth(creature, (sbyte)-sickDamageAmount); //every 1 second afflict damage
            creature.statusTimer = 0;
        }
        if (creature.damagecounter >= sickTime) //after 5s remove stun
        {
            creature.damagecounter = 0;
            creature.statusTimer = 0; //reset status timer
            RemoveStatusEffectFromCreature(creature);
        }
    }
    private void OnStun(Creature creature)
    {
        if (creature is Player)
        {
            userInput.IsMovementBlocked = true; //block player pointer until unstun
        }

        creature.statusTimer += Time.deltaTime;
        if (creature.statusTimer >= stunTime) //after 5s remove stun
        {
            creature.statusTimer = 0; //reset status timer
            if (creature is Player) userInput.IsMovementBlocked = false;
            RemoveStatusEffectFromCreature(creature);
        }
    }
    private void RerollEnemyWord()
    {
        chosenWord = enemy.PickWord();
        OnEnemyWordPicked?.Invoke(chosenWord);
    }


    private void MoveByPlayer(SO_Word moveData)
    {
        ExecuteStatChanges(player, enemy, moveData);
        enemyAttackCooldownCurrent = Math.Max(enemyAttackCooldownMinumum, enemyAttackCooldownCurrent * enemyAttackModifier);
    }

    private void MoveByEnemy()
    {
        var moveData = wordManager.GetWordDataFromWord(chosenWord);

        ExecuteStatChanges(enemy, player, moveData);

        RerollEnemyWord();
    }

    private void ExecuteStatChanges(Creature attacker, Creature defender, SO_Word MoveData)
    {
        sbyte healthModifier = (sbyte)(Mathf.Min(0, -MoveData.attackModifier + enemy.GetDefence()));
        UpdateHealth(defender, healthModifier);
        if (MoveData.attackModifier != 0)
            LiftDefence(defender); //if the attack does damage, remove the defence

        UpdateHealth(attacker, MoveData.HealthModifier);
        UpdateDefence(attacker, (byte)MoveData.DefenceModifier);


        if (MoveData.StatusEffect != defender.GetStatusEffect())
        {
            OnStatusEffectAfflicted?.Invoke(defender, MoveData.StatusEffect);
            defender.SetStatusEffect(MoveData.StatusEffect);
        }



        OnPlayerStatUpdate?.Invoke(this, new CreatureUIStatUpdate(player.GetMaxHealth(), player.GetHealth()));
        OnEnemyStatUpdate?.Invoke(this, new CreatureUIStatUpdate(enemy.GetMaxHealth(), enemy.GetHealth()));
    }
    private void UpdateHealth(Creature creature, sbyte healthModifier)
    {
        if (healthModifier != 0)
        {
            creature.ChangeHealth(healthModifier);
            OnHealthChanged?.Invoke(creature, healthModifier);
            if (creature is Player player) OnPlayerStatUpdate?.Invoke(this, new CreatureUIStatUpdate(player.GetMaxHealth(), player.GetHealth()));
            if (creature is Player enemy) OnEnemyStatUpdate?.Invoke(this, new CreatureUIStatUpdate(enemy.GetMaxHealth(), enemy.GetHealth()));
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

    private void LiftDefence(Creature creature)
    {
        OnDefenceChanged?.Invoke(creature, 0);
        creature.ChangeDefence(0);
    }
    private bool GetPauseState()
    {
        return battlePaused;
    }
}
