using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.SceneManagement;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private bool FindAllObjectsOnReload = false;
    [SerializeField] private bool QuitPressingStartSelect = true;

    //list of objects
    private CharacterController character;
    [SerializeField] private GameObject PlayerName;
    [SerializeField] private Slider EnemyAttackSlider;
    [SerializeField] private Animator CameraAnimator;
    [SerializeField] private Slider EnemyHealthSlider;
    [SerializeField] private Slider PlayerHealthSlider;
    [SerializeField] private Image Tab;
    [SerializeField] private Transform Word;

    private BattleSimulator battleSim;
    private WordManager wordManager;

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        FindAllObjectsOfType();
        PlayAmbiance();
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

        battleSim = FindObjectOfType<BattleSimulator>();
        wordManager = FindObjectOfType<WordManager>();
        battleSim.OnHealthChanged += HealSound; //general health update for SE or stat boost Animation
        wordManager.OnWordchecked += WordFormedSE;

        //game end events
        battleSim.OnEnemyBeaten += LogicWhenEnemyDefeated;
        battleSim.OnGameOver += LogicWhenPlayerDefeated;
        //battleSim.OnPrizeLetterObtained += OnPrizeLetterObtained;
        battleSim.OnDefenceChanged += DefenceSound;
        battleSim.OnStatusEffectAfflicted += StatusEffectSound;
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
        if(IsCorrect)
            AudioManager.Instance.Play("Spell");
        else
            AudioManager.Instance.Play("Click");

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
