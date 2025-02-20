using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private bool FindAllObjectsOnReload = false;
    [SerializeField] private bool QuitPressingStartSelect = true;

    //lists of objects
    private BattleSimulator battleSim;
    private WordManager wordManager;
    private UIWordSelector userInput;
    private SelectionVisual selectables;


    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        FindAllObjectsOfType();
        PlayAmbiance();
        if (wordManager != null)
            wordManager.OnWordchecked += ActivateTabSound;

        if (userInput != null)
            userInput.OnPauseStateUpdate += PlayLexiconOpenClose;
        if (selectables != null)
            selectables.OnChangedSelection += PlayUIMovingSound;
    }

    private void ActivateTabSound(Color color, bool arg2) ///////
    {
        if (arg2)
            AudioManager.Instance.Play("Shuffle");
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        FindAllObjectsOfType();
    }

    private void Update()
    {

        if (QuitPressingStartSelect)
        {
            //if (Input.GetButton("JoystickButton6") && Input.GetButton("JoystickButton7"))
            //    Application.Quit();
        }
    }
    private void FindAllObjectsOfType()
    {
        //character = GameObject.FindObjectOfType<CharacterController>();
        selectables = FindObjectOfType<SelectionVisual>();
        battleSim = FindObjectOfType<BattleSimulator>();
        userInput = FindObjectOfType<UIWordSelector>();
        wordManager = FindObjectOfType<WordManager>();

        if (battleSim != null)
        {
            battleSim.OnHealthChanged += HealSound; //general health update for SE or stat boost Animation
                                                    //game end events
            battleSim.OnEnemyBeaten += LogicWhenEnemyDefeated;
            battleSim.OnGameOver += LogicWhenPlayerDefeated;
            //battleSim.OnPrizeLetterObtained += OnPrizeLetterObtained;
            battleSim.OnDefenceChanged += DefenceSound;
            battleSim.OnStatusEffectAfflicted += StatusEffectSound;
        }
        if (wordManager != null)
        {
            wordManager.OnWordchecked += WordFormedSE;
        }
    }

    private void StatusEffectSound(Creature creature, EStatusEffect effect)
    {
        switch (effect)
        {
            case EStatusEffect.Blind: AudioManager.Instance.Play("Slow"); break; //pic
            case EStatusEffect.Sick: AudioManager.Instance.Play("Sick"); break; //ill
            case EStatusEffect.Stun: AudioManager.Instance.Play("Stun"); break; //eel
        }
    }

    private void PlayUIMovingSound()
    {
        AudioManager.Instance.Play("Click");
    }

    private void PlayLexiconOpenClose(bool obj)
    {
        AudioManager.Instance.Play("Paper");
    }

    private void OnPrizeLetterObtained(char obj)
    {
        throw new NotImplementedException();
    }

    private void LogicWhenPlayerDefeated()
    {
        AudioManager.Instance.Play("Debuff");
    }

    private void LogicWhenEnemyDefeated()
    {
        AudioManager.Instance.Play("Attack");
    }

    private void PlayOnNextLevel()
    {
        AudioManager.Instance.Play("NextLevel");
    }
    private void PlayAmbiance()
    {
        AudioManager.Instance.Play("Ambiance");
        AudioManager.Instance.Play("Music");
    }
    private void WordFormedSE(Color color, bool IsCorrect)
    {
        if (IsCorrect)
            AudioManager.Instance.Play("Spell");
        else
            AudioManager.Instance.Play("Wave");

    }
    private void HealSound(Creature creature, sbyte healthChanged)
    {
        if (healthChanged < 0)
            AudioManager.Instance.Play("Attack");
        else if (healthChanged > 0)
            AudioManager.Instance.Play("Heal");

    }
    private void DefenceSound(Creature creature, byte defenceChanged)
    {
        if (defenceChanged > 0)
        {
            AudioManager.Instance.Play("Defence");
        }
    }
}
